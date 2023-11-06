namespace ATI.Services.Common.Extensions.OperationResult

open System
open System.Diagnostics.CodeAnalysis
open System.Runtime.InteropServices
open System.Threading.Tasks
open ATI.Services.Common.Behaviors
open System.Runtime.CompilerServices
open FSharpPlus

[<Extension>]
type OperationResultExtensions =

    [<Extension>]
    static member Next (x: OperationResult<_>, f: Func<_, 'R>) =
        x |> _OperationResult.map f.Invoke

    [<Extension>]
    static member Next (x: OperationResult<_>, f: OperationFunc<_, 'R>) =
        x |> (_OperationResult.bind f.Invoke)

    [<Extension>]
    static member Fallback (x: OperationResult<_>, f: Func<_, 'R>) =
        x |> _OperationResult.fallMap f.Invoke

    [<Extension>]
    static member Fallback (x: OperationResult<_>, f: OperationFunc<_, 'R>) =
        x |> (_OperationResult.fallBind f.Invoke)

    [<Extension>]
    static member NextAsync (x: OperationResult<_>, f: AsyncOperationFunc<_, 'R>) =
        x |> (Task.FromResult >> _AsyncOperationResult.bind f.Invoke)

    [<Extension>]
    static member NextAsync (x: OperationResult<_>, f: AsyncFunc<_, 'R>) =
        x |> (Task.FromResult >> _AsyncOperationResult.map f.Invoke)

    [<Extension>]
    static member NextBiAsync (x: OperationResult<'a>, s: AsyncResult<'b>, f: AsyncBiOperationFunc<'a, 'b, 'R>) =
        ((x |> Task.FromResult) , s) ||> _AsyncOperationResult.bind2 f.Invoke

    [<Extension>]
    static member NextBiAsync (x: OperationResult<'a>, s: OperationResult<'b>, f: AsyncBiOperationFunc<'a, 'b, 'R>) =
        s |> (Task.FromResult >> _AsyncOperationResult.bind2 f.Invoke (x |> Task.FromResult))

    [<Extension>]
    static member NextBi (x: OperationResult<'a>, s: OperationResult<'b>, f: Func<'a, 'b, 'R>) =
        (x, s) ||> _OperationResult.map2 f.Invoke

    [<Extension>]
    static member NextBi (x: OperationResult<'a>, s: OperationResult<'b>, f: BiOperationFunc<'a, 'b, 'R>) =
        (x, s) ||> _OperationResult.bind2 f.Invoke

    [<Extension>]
    static member NextAsync (x: AsyncResult<_>, f: AsyncOperationFunc<_, 'R>) =
        x |> _AsyncOperationResult.bind f.Invoke

    [<Extension>]
    static member FallbackAsync (x: AsyncResult<_>, f: AsyncOperationFunc<OperationResult, _>) =
        x |> _AsyncOperationResult.fallBind f.Invoke

    [<Extension>]
    static member NextAsync (x: AsyncResult<'a>, f: AsyncFunc<'a, 'R>) =
        x |> _AsyncOperationResult.map f.Invoke

    [<Extension>]
    static member FallbackAsync (x: AsyncResult<'a>, f: AsyncFunc<OperationResult, 'a>) =
        x |> _AsyncOperationResult.fallMap f.Invoke

    [<Extension>]
    static member NextAsync (x: AsyncResult<'a>, f: Func<'a, 'R>) =
        x |> _AsyncOperationResult.map (f.Invoke >> Task.FromResult)

    [<Extension>]
    static member FallbackAsync (x: AsyncResult<'a>, f: Func<OperationResult, 'a>) =
        x |> _AsyncOperationResult.fallMap (f.Invoke >> Task.FromResult)

    [<Extension>]
    static member NextAsync (x: AsyncResult<'a>, f: OperationFunc<'a, 'R>) =
        x |> _AsyncOperationResult.bind (f.Invoke >> Task.FromResult)

    [<Extension>]
    static member FallbackAsync (x: AsyncResult<'a>, f: Func<OperationResult, OperationResult<'a>>) =
        x |> _AsyncOperationResult.fallBind (f.Invoke >> Task.FromResult)

    [<Extension>]
    static member NextBiAsync (x: AsyncResult<'a>, s: AsyncResult<'b>, f: AsyncBiFunc<'a, 'b, 'R>) =
        (x, s) ||> _AsyncOperationResult.map2 f.Invoke

    [<Extension>]
    static member NextBiAsync (x: AsyncResult<'a>, s: OperationResult<'b>, f: AsyncBiFunc<'a, 'b, 'R>) =
        s |> (Task.FromResult >> _AsyncOperationResult.map2 f.Invoke x)

    [<Extension>]
    static member NextBiAsync (x: AsyncResult<'a>, s: AsyncResult<'b>, f: AsyncBiOperationFunc<'a, 'b, 'R>) =
        (x, s) ||> _AsyncOperationResult.bind2 f.Invoke

    [<Extension>]
    static member NextBiAsync (x: AsyncResult<'a>, s: OperationResult<'b>, f: AsyncBiOperationFunc<'a, 'b, 'R>) =
        s |> (Task.FromResult >> _AsyncOperationResult.bind2 f.Invoke x)

    [<Extension>]
    static member NextBiAsync (x: AsyncResult<'a>, s: AsyncResult<'b>, f: Func<'a, 'b, 'R>) =
        (x, s) ||> _AsyncOperationResult.map2 (f.Invoke >> Task.FromResult)

    [<Extension>]
    static member NextBiAsync (x: AsyncResult<'a>, s: OperationResult<'b>, f: Func<'a, 'b, 'R>) =
        s |> (Task.FromResult >> _AsyncOperationResult.map2 (f.Invoke >> Task.FromResult) x)

    [<Extension>]
    static member InvokeOnSuccess (x: OperationResult<_>, f: Action<_>) =
        x |> _OperationResult.iter f.Invoke

    [<Extension>]
    static member InvokeOnSuccess (x: OperationResult<_>, f: Action) =
        x |> _OperationResult.iterUnit f.Invoke

    [<Extension>]
    static member InvokeOnSuccessAsync (x: AsyncResult<'a>, f: Action<_>) =
        x |> _AsyncOperationResult.iter f.Invoke

    [<Extension>]
    static member InvokeOnSuccessAsync (x: AsyncResult<'a>, f: Action) =
        x |> _AsyncOperationResult.iterUnit f.Invoke

    [<Extension>]
    static member InvokeOnError (x: OperationResult<_>, f: Action<_>) =
        x |> _OperationResult.iterWhenFail f.Invoke

    [<Extension>]
    static member InvokeOnError (x: OperationResult<_>, f: Action) =
        x |> _OperationResult.iterWhenFailUnit f.Invoke

    [<Extension>]
    static member InvokeOnErrorAsync (x: AsyncResult<'a>, f: Action<_>) =
        x |> _AsyncOperationResult.iterWhenFail f.Invoke

    [<Extension>]
    static member InvokeOnErrorAsync (x: AsyncResult<_>, f: Action) =
        x |> _AsyncOperationResult.iterWhenFailUnit f.Invoke

    [<Extension>]
    static member IsSuccessWith (x: OperationResult<_>, f: Func<_, bool>, [<Out>] [<MaybeNullWhen(false)>] value: 'a byref) =
        value <- x.Value
        x |> (_OperationResult.map f.Invoke >> _OperationResult.get)

    [<Extension>]
    static member IsSuccessWith (x: OperationResult<_>, f: Func<_, bool>) =
        x |> (_OperationResult.map f.Invoke >> _OperationResult.get)

    [<Extension>]
    static member IsNotSuccess (x: OperationResult<_>, [<Out>] [<MaybeNullWhen(true)>] value: 'a byref) =
        value <- x.Value
        (not x.Success)