using System.ComponentModel.DataAnnotations.Schema;

namespace HowTo.Entities.UserInfo;

[ComplexType]
public class ApprovedViewArticleEntity
{
    public ApprovedViewArticleEntity(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}