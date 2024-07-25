using System.Collections.ObjectModel;
using EBooks.Core.Entities.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace EBooks.DA;

public class FileSystemHelper
{
    private readonly FileSystemOptions _fileSystemOptions;
    private const int _imageMaxSize = 864;
    private static readonly ReadOnlyCollection<byte[]> EmptyCollection = new List<byte[]>().AsReadOnly();

    public FileSystemHelper(IOptions<FileSystemOptions> fileSystemOptions)
    {
        _fileSystemOptions = fileSystemOptions.Value;
    }

    public Task SaveBookFilesAsync(uint bookId, IFormFile files)
    {
        return SaveImageAsync(bookId.ToString(), files);
    }
    

    public Task<IReadOnlyCollection<byte[]>> GetBookFilesAsync(uint bookId)
    {
        return GetFilesAsync(bookId.ToString());
    }

    public Task DeleteBookDirectoryAsync(uint bookId)
    {
        return DeleteDirectoryAsync(bookId.ToString());
    }
    
    private async Task<IReadOnlyCollection<byte[]>> GetFilesAsync(string path)
    {
        
            var files = new List<byte[]>();
            var directory = new DirectoryInfo($"{_fileSystemOptions.RootPath}/{path}");
            if (!directory.Exists)
                return EmptyCollection;

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

            return files;
    }


    private Task DeleteDirectoryAsync(string path)
    {
            return Task.Run(() =>
            {
                var directory = new DirectoryInfo($"{_fileSystemOptions.RootPath}/{path}");
                if (!directory.Exists)
                    return;

                directory.Delete(true);
            });
    }

    private async Task SaveImageAsync(string path, IFormFile file)
    {
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(_fileSystemOptions.RootPath, path, fileName);
            new FileInfo(filePath).Directory?.Create();
            await using var stream = new FileStream(filePath, FileMode.Create);
            using var image = await Image.LoadAsync(file.OpenReadStream());

            // if (image.Height > image.Width)
            //     image.Mutate(c => c.Resize
            //         (_imageMaxSize, image.Height * _imageMaxSize / image.Width));
            // else if (image.Height < image.Width)
            //     image.Mutate(c => c.Resize
            //         (image.Width * _imageMaxSize / image.Height, _imageMaxSize));

            await image.SaveAsync(stream, image.Metadata.DecodedImageFormat);
    }
}