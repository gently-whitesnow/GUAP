using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HowTo.Entities.UserInfo;

/// <summary>
/// Информация, которую нельзя получить из авторизационной модели
/// </summary>
public class UserUniqueInfoDto
{
    [Required]
    // UserId
    public Guid Id { get; set; }

    public int? LastReadCourseId { get; set; }
    public virtual List<ViewedEntity.ViewedEntity> ApprovedViewArticleIds { get; set; }
}