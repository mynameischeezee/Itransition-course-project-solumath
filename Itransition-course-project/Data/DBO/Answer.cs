using System;
using System.ComponentModel.DataAnnotations.Schema;
using Itransition_course_project.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Itransition_course_project.Models
{
    public class Answer : BasicModel
    {
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
        public int? PostId { get; set; }
        public string UserAnswer { get; set; }
    }
}