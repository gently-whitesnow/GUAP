using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HowTo.Entities.UserInfo;

/// <summary>
/// Информация, которую нельзя получить из авторизационной модели
/// </summary>
public class UserInfoDto
{
    [Required]
    public Guid Id { get; set; }
    public int? LastReadCourseId { get; set; }
    public List<ApprovedViewArticleEntity> ApprovedViewArticleIds { get; set; }
}