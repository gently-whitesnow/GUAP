namespace ATI.Services.Common.Extensions.OperationResult

open Microsoft.FSharp.Core
open ATI.Services.Common.Behaviors
open System
open System.Threading.Tasks
open FSharpPlus

[<AutoOpen>]
module _Common =
    type OperationFunc<'a, 'R> = Func<'a, OperationResult<'R>>
    type BiOperationFunc<'a, 'b, 'R> = Func<'a, 'b, OperationResult<'R>>
    type AsyncResult<'R> = Task<OperationResult<'R>>
    type AsyncFunc<'a, 'R> = Func<'a, Task<'R>>
    type AsyncBiFunc<'a, 'b, 'R> = Func<'a, 'b, Task<'R>>
    type AsyncOperationFunc<'a, 'R> = Func<'a, Task<OperationResult<'R>>>
    type AsyncBiOperationFunc<'a, 'b, 'R> = Func<'a, 'b, Task<OperationResult<'R>>>
    type NestedOperationResult<'R> = OperationResult<OperationResult<'R>>

    let (|Success|Fail|) (operationResult: OperationResult<_>) =
        match operationResult.Success with
        | true -> Success operationResult.Value
        | false -> Fail(operationResult |> OperationResult)

module _OperationResult =
    let inline zip x y =
        match x, y with
        | Success v, Success v2 -> OperationResult<'a * 'b>((v, v2))
        | Fail failed, _ -> failed |> OperationResult<'a * 'b>
        | _, Fail failed -> failed |> OperationResult<'a * 'b>

    let inline bind f x =
        match x with
        | Success v -> f v
        | Fail failed -> failed |> OperationResult<'R>

    let inline fallBind f x =
        match x with
        | Fail v -> f v
        | Success _ -> x

    let inline bind2 f x y = (zip x y) |> (bind f)

    let inline map (f: _ -> 'R) a =
        a |> bind (f >> OperationResult<'R>)

    let inline fallMap (f: _ -> 'R) a =
        a |> fallBind (f >> OperationResult<'R>)

    let inline map2 (f: _ -> 'R) a b =
        (a, b) ||> bind2 (f >> OperationResult<'R>)

    let inline flatten a = a |> bind id

    let inline iter (f: 'a -> unit) x =
        x |> map f |> ignore
        x

    let inline iterUnit f x =
        match x with
        | Fail _ -> x
        | Success _ ->
            f()
            x
    
    let inline iterWhenFail (f: _ -> unit) x =
        match x with
        | Fail failed ->
            failed |> f
            x
        | Success _ -> x

    let inline iterWhenFailUnit f x =
        match x with
        | Fail _ ->
            f()
            x
        | Success _ -> x

    let get (x: OperationResult<'R>) = x.Value

module _AsyncOperationResult =
    let inline bind f x =
        task {
            let! op = x

            return!
                match op with
                | Success v -> f v
                | Fail v -> v |> (OperationResult<'R> >> Task.FromResult)
        }

    let inline fallBind f x : AsyncResult<'R> =
        task {
            let! op = x

            return!
                match op with
                | Fail v -> f v
                | Success _ -> op |> Task.FromResult
        }

    let inline bind2 (f: 'a * 'b -> AsyncResult<'R>) x y =
        task {
            let! first = x
            let! second = y

            return!
                second
                |> (_OperationResult.zip first
                    >> Task.FromResult
                    >> bind f)
        }

    let inline map (f: _ -> Task<'R>) a =
        a |> bind (f >> Task.map OperationResult<'R>)

    let inline fallMap (f: _ -> Task<'R>) a =
        a |> fallBind (f >> Task.map OperationResult<'R>)

    let inline map2 (f: _ -> Task<'R>) a b =
        (a, b) ||> bind2 (f >> Task.map OperationResult<'R>)

    let inline iter (f: 'a -> unit) x =
        x |> map (f >> Task.FromResult) |> ignore
        x

    let inline iterUnit f x =
        task {
            let! op = x
            return! match op with
                    | Fail _ -> x
                    | Success _ ->
                        f()
                        x
        }

    let inline iterWhenFail f x =
        task {
            let! op = x
            return! match op with
                    | Fail failed ->
                       failed |> f
                       x
                    | Success _ -> x
        }

    let inline iterWhenFailUnit f x =
        task {
            let! op = x
            return! match op with
                    | Fail _ ->
                       f()
                       x
                    | Success _ -> x
        }
