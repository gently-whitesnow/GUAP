using ATI.Services.Common.Behaviors;

namespace HowTo.Entities.Extensions;

// TODO think about it
// public static class OperationResultExtension
// {
//     public static OperationResult<T> NextIfOk<T>(this OperationResult<T> result, Oper)
//     {
//         if (!result.Success)
//             return result;
//         return new(result);
//         
//     }
// }
//
// public class SuccessOperationContainer<TResult>
// {
//     public OperationResult<TResult> Operation { get; set; }
//     public SuccessOperationContainer(params OperationResult[] operationResults)
//     {
//         foreach (var operation in operationResults)
//         {
//             if (!operation.Success)
//             {
//                 Operation = new (operation);
//                 return;
//             }
//         }
//
//         OperationResult = new(operationResults[^1]);
//     }
// }