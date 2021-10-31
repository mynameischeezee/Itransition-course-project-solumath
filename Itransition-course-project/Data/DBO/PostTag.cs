using System.ComponentModel.DataAnnotations.Schema;

namespace Itransition_course_project.Models
{
    public class PostTag : BasicModel
    {
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
        public int? PostId { get; set; }
        [ForeignKey(nameof(TagId))]
        public Tag Tag { get; set; }
        public int? TagId { get; set; }
    }
}