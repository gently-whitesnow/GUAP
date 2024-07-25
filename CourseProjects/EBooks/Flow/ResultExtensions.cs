using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flow;

public static class ResultExtensions
{
    public static async Task<IResult> AsResultAsync(
        this Task<Result> operationTask,
        int successStatusCode = 200)
    {
        var operation = await operationTask;

        return operation.AsResult(successStatusCode);
    }
    
    public static async Task<IResult> AsResultAsync<TValue>(
        this Task<Result<TValue>> operationTask,
        int successStatusCode = 200)
    {
        var operation = await operationTask;

        return operation.AsResult(successStatusCode);
    }

    public static async Task<IResult> AsResultAsync<TValue, TView>(
        this Task<Result<TValue>> operationTask,
        Func<TValue, TView> toView,
        int successStatusCode = 200)
    {
        var operation = await operationTask;

        return operation.AsResult(toView, successStatusCode);
    }
    
    public static IResult AsResult(
        this Result operation,
        int successStatusCode = 200)
    {
        if (operation.IsSuccess)
            return Results.StatusCode(successStatusCode);

        return Results.Json(operation.Error, statusCode: operation.Error!.ExtractStatusCode());
    }

    public static IResult AsResult<TValue>(
        this Result<TValue> operation,
        int successStatusCode = 200)
    {
        if (operation.IsSuccess)
            return Results.Json(operation.Value, statusCode: successStatusCode);

        return Results.Json(operation.Error, statusCode: operation.Error!.ExtractStatusCode());
    }

    public static IResult AsResult<TValue, TView>(
        this Result<TValue> operation,
        Func<TValue, TView> toView,
        int successStatusCode = 200)
    {
        if (operation.HasError)
            return Results.Json(operation.Error, statusCode: operation.Error!.ExtractStatusCode());

        if (operation.Value == null)
            return Results.Json(ConvertNullToContract(typeof(TValue)), statusCode: successStatusCode);

        return Results.Json(toView(operation.Value), statusCode: successStatusCode);
    }
    
    public static async Task<IActionResult> AsActionResultAsync(
        this Task<Result> operationTask,
        int successStatusCode = 200)
    {
        var operation = await operationTask;

        return operation.AsActionResult(successStatusCode);
    }

    public static async Task<IActionResult> AsActionResultAsync<TValue>(
        this Task<Result<TValue>> operationTask,
        int successStatusCode = 200)
    {
        var operation = await operationTask;

        return operation.AsActionResult(successStatusCode);
    }

    public static async Task<IActionResult> AsActionResultAsync<TValue, TView>(
        this Task<Result<TValue>> operationTask,
        Func<TValue, TView> toView,
        int successStatusCode = 200)
    {
        var operation = await operationTask;

        return operation.AsActionResult(toView, successStatusCode);
    }
    
    public static IActionResult AsActionResult(
        this Result operation,
        int successStatusCode = 200)
    {
        if (operation.IsSuccess)
            return new StatusCodeResult(successStatusCode);

        return new JsonResult(operation.Error)
        {
            StatusCode = operation.Error!.ExtractStatusCode()
        };
    }

    public static IActionResult AsActionResult<TValue>(
        this Result<TValue> operation,
        int successStatusCode = 200)
    {
        if (operation.IsSuccess)
            return new JsonResult(operation.Value)
            {
                StatusCode = successStatusCode
            };

        return new JsonResult(operation.Error)
        {
            StatusCode = operation.Error!.ExtractStatusCode()
        };
    }

    public static IActionResult AsActionResult<TValue, TView>(
        this Result<TValue> operation,
        Func<TValue, TView> toView,
        int successStatusCode = 200)
    {
        if (operation.HasError)
            return new JsonResult(operation.Error)
            {
                StatusCode = operation.Error!.ExtractStatusCode()
            };

        if (operation.Value == null)
            return new JsonResult(ConvertNullToContract(typeof(TValue)))
            {
                StatusCode = successStatusCode
            };

        return new JsonResult(toView(operation.Value))
        {
            StatusCode = successStatusCode
        };
    }

    private static readonly object EmptyObject = new { };

    private static object ConvertNullToContract(Type type)
    {
        var typeIsArray = typeof(ICollection).IsAssignableFrom(type);
        var typeIsDictionary = typeof(IDictionary).IsAssignableFrom(type);
        if (typeIsArray && !typeIsDictionary)
            return Array.Empty<object>();

        return EmptyObject;
    }
}