namespace HowTo.Entities.Article;

public class CreateArticleRequest
{
    public string Title { get; set; }
    public string Path { get; set; }
    public MultipartFormDataContent Content { get; set; }
}