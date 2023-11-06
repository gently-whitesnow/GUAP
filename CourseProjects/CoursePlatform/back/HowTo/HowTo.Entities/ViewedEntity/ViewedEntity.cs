using System.ComponentModel.DataAnnotations;

namespace HowTo.Entities.ViewedEntity;

public class ViewedEntity
{
    public ViewedEntity(int courseId, int articleId)
    {
        CourseId = courseId;
        ArticleId = articleId;
    }
    
    [Key]
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int ArticleId { get; set; }
}