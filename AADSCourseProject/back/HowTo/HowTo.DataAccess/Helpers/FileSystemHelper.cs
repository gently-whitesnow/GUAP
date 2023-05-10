using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.Entities.Options;
using Microsoft.Extensions.Options;

namespace HowTo.DataAccess.Helpers;

public class FileSystemHelper
{
    private readonly FileSystemOptions _fileSystemOptions;

    public FileSystemHelper(IOptions<FileSystemOptions> fileSystemOptions)
    {
        _fileSystemOptions = fileSystemOptions.Value;
    }

    public Task<OperationResult> SaveCourseFilesAsync(int courseId, MultipartFormDataContent files)
    {
        return SaveFilesAsync(courseId.ToString(), files);
    }

    public Task<OperationResult> SaveArticleFilesAsync(int courseId, int articleId, MultipartFormDataContent files)
    {
        return SaveFilesAsync($"{courseId}/{articleId}", files);
    }

    public Task<OperationResult<MultipartFormDataContent>> GetCourseFilesAsync(int courseId)
    {
        return GetFilesAsync(courseId.ToString());
    }

    public Task<OperationResult<MultipartFormDataContent>> GetArticleFilesAsync(int courseId, int articleId)
    {
        return GetFilesAsync($"{courseId}/{articleId}");
    }
    
    public Task<OperationResult> DeleteCourseFilesAsync(int courseId)
    {
        return DeleteFilesAsync(courseId.ToString());
    }

    public Task<OperationResult> DeleteArticleFilesAsync(int courseId, int articleId)
    {
        return DeleteFilesAsync($"{courseId}/{articleId}");
    }

    private async Task<OperationResult> SaveFilesAsync(string path, MultipartFormDataContent files)
    {
        try
        {
            foreach (var content in files)
            {
                var fileName = content.Headers.ContentDisposition.FileName;
                var filePath = Path.Combine(_fileSystemOptions.RootPath, path, fileName);
                new FileInfo(filePath).Directory?.Create();
                
                await using var stream = await content.ReadAsStreamAsync();
                var fileStream = File.Create(filePath);
                await stream.CopyToAsync(fileStream);
                fileStream.Close();
            }

            return OperationResult.Ok;
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }

    private async Task<OperationResult<MultipartFormDataContent>> GetFilesAsync(string path)
    {
        try
        {
            return new(await Task.Run(() =>
            {
                var files = new MultipartFormDataContent();
                var directory = new DirectoryInfo($"{_fileSystemOptions.RootPath}/{path}");
                foreach (var file in directory.GetFiles())
                {
                    var fileStream = File.OpenRead(file.FullName);
                    var streamContent = new StreamContent(fileStream);
                    files.Add(streamContent, file.Name, file.Name);
                }

                return files;
            }));
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
    
    private async Task<OperationResult> DeleteFilesAsync(string path)
    {
        try
        {
            return new(await Task.Run(() =>
            {
                var directory = new DirectoryInfo($"{_fileSystemOptions.RootPath}/{path}");
                foreach (var file in directory.GetFiles())
                {
                    File.Delete(file.FullName);
                }

                return ActionStatus.Ok;
            }));
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}