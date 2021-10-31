using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Itransition_course_project.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Itransition_course_project.Models
{
    public class RateUserPost : BasicModel
    {
        public Post Post {get; set;}
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public int Rate { get; set; }
    }
}