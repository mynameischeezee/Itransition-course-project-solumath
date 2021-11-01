using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Itransition_course_project.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Itransition_course_project.Models
{
    public class RateUserPost : BasicModel
    {
        [ForeignKey(nameof(PostId))]
        public Post Post {get; set;}
        public int? PostId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        [Range(1,5, ErrorMessage = "Rate should be between 1 and 5.")]
        public int Rate { get; set; }
    }
}