using ATI.Services.Common.Behaviors;
using ATI.Services.Common.Extensions.OperationResult;
using Xunit.Abstractions;

namespace HowTo.Tests;

public class OperationResultExtensionTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private static readonly OperationResult<int> IntSuccessOp = new(0);
    private static readonly OperationResult<int> Fail = new(ActionStatus.InternalServerError, "Err");

    public OperationResultExtensionTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void TestOperationChain()
    {
        var result = IntSuccessOp
            .Next(i => new OperationResult<int>(++i))
            .NextBi(IntSuccessOp, (f, s) => f + s)
            .Fallback(failed => new OperationResult<int>(100));
        
        Assert.True(result.IsSuccessWith(i => i == 1, out var _));
    }

    [Fact]
    public void TestMapSuccess()
    {
        var result = IntSuccessOp
            .Next(i => ++i)
            .Next(i => ++i);
        
        Assert.True(result.Success);
        Assert.Equal(2, result.Value);
    }
    
    [Fact]
    public void TestMapFail()
    {
        var result = IntSuccessOp
            .Next(i => ++i)
            .Next(_ => Fail)
            .Next(i => ++i);
        
        Assert.False(result.Success);
        Assert.Equal("Err", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task AsyncOperationFuncTests()
    {
        var operationResult = new OperationResult<string>("test");
        var chain = operationResult
            .NextAsync(AppendAsync)
            .NextAsync(AppendAsync);
        
        var result = await chain;
        Assert.Equal("testappended textappended text", result.Value);
    }
    
    [Fact]
    public async Task AsyncOperationFuncFailTests()
    {
        var operationResult = new OperationResult<string>("test");
        var chain = operationResult
            .NextAsync(AppendAsync)
            .NextAsync(i => new OperationResult<string>(ActionStatus.InternalServerError, "Err"))
            .NextAsync(AppendAsync);
        
        var result = await chain;
        
        Assert.False(result.Success);
        Assert.Equal("Err", result.Errors.First().ErrorMessage);
    }
    
    [Fact]
    public async Task AsyncFuncTests()
    {
        var chain = IntSuccessOp
            .NextAsync(AppendValueAsync)
            .NextAsync(AppendValueAsync);
        
        var result = await chain;
        Assert.True(result.IsSuccessWith(v => v == 200, out var value));
        Assert.Equal(200, value);
    }
    
    [Fact]
    public async Task AsyncFuncFailTests()
    {
        var chain = IntSuccessOp
            .NextAsync(AppendValueAsync)
            .NextAsync(i => Fail)
            .NextAsync(AppendValueAsync);
        
        var result = await chain;
        Assert.False(result.Success);
        Assert.False(result.IsSuccessWith(f => f > 0, out var value));
        Assert.Equal("Err", result.Errors.First().ErrorMessage);
    }
    
    [Fact]
    public async Task AsyncFuncSuccessFallbackTests()
    {
        var chain = IntSuccessOp
            .NextAsync(AppendValueOperationAsync)
            .FallbackAsync(failed => Task.FromResult(new OperationResult<int>(1000))) // does nothing if previous success
            .NextAsync(AppendValueOperationAsync);
        
        var result = await chain;
        Assert.True(result.IsSuccessWith(v => v == 200, out var value));
        Assert.Equal(200, value);
    }
    
    [Fact]
    public async Task AsyncFuncFailFallbackTests()
    {
        var chain = IntSuccessOp
            .NextAsync(AppendValueOperationAsync)
            .NextAsync(_ => Fail)
            //.InvokeOnErrorAsync(failed => ) call something on fail or next
            .FallbackAsync(failed => // replace previous failed with fallback value
            {
                // log failed
                _testOutputHelper.WriteLine(failed.Errors.First().ErrorMessage);
                // then return fallback value
                return new OperationResult<int>(100);
            })
            .NextAsync(AppendValueOperationAsync);
        
        var result = await chain;
        Assert.True(result.IsSuccessWith(v => v == 200, out var value));
        Assert.Equal(200, value);
    }
    
    [Fact]
    public void InvokeOnSuccessTests()
    {
        var testValue = 0;
        var result = IntSuccessOp
            .Next(i => new OperationResult<int>(i + 100))
            .InvokeOnSuccess(_ => ++testValue)
            .Next(i => i + 100);
        
        Assert.True(result.IsSuccessWith(v => v == 200, out var value));
        Assert.Equal(1, testValue);
        Assert.Equal(200, value);
    }
    
    [Fact]
    public void InvokeOnSuccessWhenFailTests()
    {
        var testValue = 0;
        var result = IntSuccessOp
            .Next(_ => Fail)
            .InvokeOnSuccess(_ => ++testValue)
            .Next(i => i + 100);
        
        Assert.False(result.Success);
        Assert.Equal(0, testValue);
    }

    [Fact]
    public void IsSuccessWithTest()
    {
        var referenceType = new OperationResult<string>(ActionStatus.InternalServerError, "Err");
        
        Assert.False(referenceType.IsSuccessWith(string.IsNullOrEmpty, out var v));
        Assert.Null(v);

        var refTypeSucc = new OperationResult<string>("");
        Assert.True(refTypeSucc.IsSuccessWith(string.IsNullOrEmpty, out var v2));
        Assert.NotNull(v2);
    }

    [Fact]
    public async Task MapBiAsyncTest()
    {
        var task = IntSuccessOp
            .NextAsync(AppendValueAsync)
            .NextBiAsync(AppendValueOperationAsync(0), (f, s) => f + s)
            .NextBiAsync(new OperationResult<int>(20), (f, s) => AppendValueOperationAsync(f + s));

        var result = await task;
        Assert.True(result.IsSuccessWith(i => i == 320, out var _));
    }
    
    [Fact]
    public async Task MapBiFailAsyncTest()
    {
        var task = IntSuccessOp
            .NextAsync(AppendValueAsync)
            .NextBiAsync(AppendValueOperationAsync(0), (f, s) => f + s)
            .NextBiAsync(new OperationResult<int>(ActionStatus.InternalServerError, "Err"), (f, s) => AppendValueOperationAsync(f + s));

        var result = await task;
        Assert.False(result.IsSuccessWith(i => i == 320, out var v));
        Assert.Equal(0, v);
        Assert.Equal("Err", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task MapBiAsyncFromSyncContextTest()
    {
        var task = IntSuccessOp
            .NextBiAsync(IntSuccessOp, (i, i1) => AppendValueOperationAsync(i + i1))
            .NextBiAsync(IntSuccessOp, (i, i1) => AppendValueOperationAsync(i + i1));

        var result = await task;
        Assert.True(result.IsSuccessWith(i => i == 200, out var _));
    }
    
    [Fact]
    public async Task MapBiAsyncFailedFromSyncContextTest()
    {
        var task = IntSuccessOp
            .NextBiAsync(IntSuccessOp, (i, i1) => AppendValueOperationAsync(i + i1))
            .NextBiAsync(Fail, (i, i1) => AppendValueOperationAsync(i + i1))
            .NextBiAsync(IntSuccessOp, (i, i1) => AppendValueOperationAsync(i + i1));

        var result = await task;
        Assert.False(result.IsSuccessWith(i => i == 200, out var value));
        Assert.Equal(0, value);
        Assert.Equal("Err", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public void InvokeOnErrorTest()
    {
        var marker = 0;

        var result = IntSuccessOp
            .Next(_ => Fail)
            .InvokeOnError(failed =>
            {
                if (failed.Errors.First().ErrorMessage == "Err")
                    marker += 1;
            });
        
        Assert.False(result.Success);
        Assert.Equal(1, marker);
        Assert.Equal("Err", result.Errors.First().ErrorMessage);

        var succMarker = 0;
        var succResult = IntSuccessOp
            .Next(i => ++i)
            .InvokeOnError(failed =>
            {
                if (failed.Errors.First().ErrorMessage == "Err")
                    succMarker += 1;
            });
        
        Assert.True(succResult.Success);
        Assert.Equal(0, succMarker);
        Assert.True(succResult.IsSuccessWith(i => i == 1, out var _));
    }
    
    
    [Fact]
    public async Task InvokeOnErrorAsyncTest()
    {
        var marker = 0;

        var task = IntSuccessOp
            .NextAsync(_ => Task.FromResult(Fail))
            .InvokeOnErrorAsync(failed =>
            {
                if (failed.Errors.First().ErrorMessage == "Err")
                    marker += 1;
            });

        var result = await task;
        
        Assert.False(result.Success);
        Assert.Equal(1, marker);
        Assert.Equal("Err", result.Errors.First().ErrorMessage);

        var succMarker = 0;
        var succTask = IntSuccessOp
            .NextAsync(AppendValueAsync)
            .InvokeOnErrorAsync(failed =>
            {
                if (failed.Errors.First().ErrorMessage == "Err")
                    succMarker += 1;
            });

        var succResult = await succTask;
        Assert.Equal(0, succMarker);
        Assert.True(succResult.IsSuccessWith(i => i == 100, out var _));
    }

    private static async Task<OperationResult<string>> AppendAsync(string v)
    {
        await Task.Delay(1);
        return new(v + "appended text");
    }
    
    private static async Task<int> AppendValueAsync(int v)
    {
        await Task.Delay(1);
        return v + 100;
    }
    
    private static async Task<OperationResult<int>> AppendValueOperationAsync(int v)
    {
        await Task.Delay(1);
        return new(v + 100);
    }
}