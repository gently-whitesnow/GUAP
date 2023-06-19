using System.Text;
using HowTo.DataAccess.Helpers;
using HowTo.DataAccess.Managers;
using HowTo.DataAccess.Repositories;
using HowTo.Entities;
using HowTo.Entities.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace HowTo.Tests;

public abstract class BaseTests : IDisposable
{
    protected readonly ArticleManager _articleManager;
    protected readonly CourseManager _courseManager;
    protected readonly ViewManager _viewManager;
    protected readonly UserInfoManager _userInfoManager;
    protected readonly ApplicationContext _dbContext;
    protected readonly FileSystemHelper _fileSystemHelper;
    protected readonly SummaryManager _summaryManager;
    protected readonly InteractiveManager _interactiveManager;

    protected const string _firstFormFileContent = "#first content form file";
    protected const string _secondFormFileContent = "#second content form file";

    private readonly string _rootPath;

    protected BaseTests(string rootPath)
    {
        _rootPath = rootPath;
        _dbContext = new ApplicationContext(Options.Create(new DbSettings
        {
            ConnectionString = "Filename=:memory:"
        }));

        _dbContext.Database.OpenConnection();
        _dbContext.Database.EnsureCreated();

        _fileSystemHelper = new FileSystemHelper(Options.Create(new FileSystemOptions
        {
            RootPath = _rootPath
        }));

        var viewRepository = new ViewRepository(_dbContext);
        _viewManager = new ViewManager(viewRepository);

        var userInfoRepository = new UserInfoRepository(_dbContext);
        _userInfoManager = new UserInfoManager(userInfoRepository);

        var articleRepository = new ArticleRepository(_dbContext);
        _articleManager = new ArticleManager(articleRepository, _fileSystemHelper, _viewManager, _userInfoManager);

        var courseRepository = new CourseRepository(_dbContext);
        _courseManager = new CourseManager(courseRepository, _fileSystemHelper, _userInfoManager);

        _summaryManager = new SummaryManager(_userInfoManager, _courseManager, _fileSystemHelper);

        var interactiveRepository = new InteractiveRepository(_dbContext);
        _interactiveManager = new InteractiveManager(interactiveRepository, articleRepository);
    }

    public IFormFile GetFormFile(string content)
    {
        var memoryStream = new MemoryStream();

        var contentBytes = Encoding.UTF8.GetBytes(content);
        memoryStream.Write(contentBytes, 0, contentBytes.Length);

        memoryStream.Position = 0;
        return new FormFile(
            baseStream: memoryStream,
            baseStreamOffset: 0,
            length: memoryStream.Length,
            name: "formFile",
            fileName: "example.md");
    }
    
    public IFormFile GetFormImage()
    {
        int width = 200;
        int height = 200;
        
        using var image = new Image<Rgba32>(width, height);
        image.Mutate(ctx =>
        {
            ctx.Resize(new Size(Random.Shared.Next(250),Random.Shared.Next(250)));
        });

        var memoryStream = new MemoryStream();
        
        image.Save(memoryStream, new PngEncoder());
        memoryStream.Position = 0;
        return new FormFile(
            baseStream: memoryStream,
            baseStreamOffset: 0,
            length: memoryStream.Length,
            name: "formFile",
            fileName: "example.png");
    }

    public void Dispose()
    {
        var directory = new DirectoryInfo(_rootPath);

        if (directory.Exists)
        {
            foreach (var file in directory.GetFiles())
            {
                file.Delete();
            }

            foreach (var dir in directory.GetDirectories())
            {
                dir.Delete(true);
            }

            directory.Delete();
        }
        
        _dbContext.Database.CloseConnection();
        _dbContext.Dispose();
    }
    
    
    protected bool CompareByteArrayAndFormFile(byte[] byteArray, IFormFile formFile)
    {
        using var memoryStream = new MemoryStream();
        formFile.CopyTo(memoryStream);
        byte[] fileBytes = memoryStream.ToArray();
        
        if (byteArray.Length == fileBytes.Length)
        {
            for (int i = 0; i < byteArray.Length; i++)
            {
                if (byteArray[i] != fileBytes[i])
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}