using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.Entities.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace HowTo.DataAccess.Helpers;

public class FileSystemHelper
{
    private readonly FileSystemOptions _fileSystemOptions;

    public FileSystemHelper(IOptions<FileSystemOptions> fileSystemOptions)
    {
        _fileSystemOptions = fileSystemOptions.Value;
    }

    public Task<OperationResult> SaveCourseFilesAsync(int courseId, IFormFile files)
    {
        return SaveFileAsync(courseId.ToString(), files);
    }

    public Task<OperationResult> SaveArticleFilesAsync(int courseId, int articleId, IFormFile file)
    {
        return SaveFileAsync($"{courseId}/{articleId}", file);
    }

    public Task<OperationResult<List<byte[]>>> GetCourseFilesAsync(int courseId)
    {
        return GetFilesAsync(courseId.ToString());
    }

    public Task<OperationResult<List<byte[]>>> GetArticleFilesAsync(int courseId, int articleId)
    {
        return GetFilesAsync($"{courseId}/{articleId}");
    }
    
    public Task<OperationResult> DeleteCourseDirectoryAsync(int courseId)
    {
        return DeleteDirectoryAsync(courseId.ToString());
    }

    public Task<OperationResult> DeleteArticleDirectoryAsync(int courseId, int articleId)
    {
        return DeleteDirectoryAsync($"{courseId}/{articleId}");
    }

    private async Task<OperationResult> SaveFileAsync(string path, IFormFile file)
    {
        try
        {
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(_fileSystemOptions.RootPath, path, fileName);
            new FileInfo(filePath).Directory?.Create();
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return OperationResult.Ok;
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }

    private async Task<OperationResult<List<byte[]>>> GetFilesAsync(string path)
    {
        try
        {
            var files = new List<byte[]>();
            var directory = new DirectoryInfo($"{_fileSystemOptions.RootPath}/{path}");
            if (!directory.Exists)
                return new();
            
            foreach (var file in directory.GetFiles())
            {
                byte[] fileBytes;
                await using (var stream = new FileStream(file.FullName, FileMode.Open))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        fileBytes = memoryStream.ToArray();
                    }
                }
                files.Add(fileBytes);
            }

            return new (files);
        }
        catch (Exception ex)
        {
            return new (ex);
        }
    }

    
    private async Task<OperationResult> DeleteDirectoryAsync(string path)
    {
        try
        {
            return new(await Task.Run(() =>
            {
                var directory = new DirectoryInfo($"{_fileSystemOptions.RootPath}/{path}");
                if (!directory.Exists)
                    return ActionStatus.Ok;
                
                directory.Delete(true);

                return ActionStatus.Ok;
            }));
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}