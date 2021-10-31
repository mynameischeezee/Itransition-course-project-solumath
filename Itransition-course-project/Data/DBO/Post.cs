using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Itransition_course_project.Data;
using Itransition_course_project.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
namespace Itransition_course_project.Models
{
    public class Post : BasicModel
    {
        public string Name { get; set; }
        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }
        public int? TopicId { get; set; }
        [ForeignKey(nameof(CreatedByUserId))]
        public ApplicationUser CreatedByUser { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime DateCreated { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile1 { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageUrl1 { get; set; }
        public string ImageStorageName1 { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile2 { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageUrl2 { get; set; }
        public string ImageStorageName2 { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile3 { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageUrl3 { get; set; }
        public string ImageStorageName3 { get; set; }
        public string PostText { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        
        
    }
}