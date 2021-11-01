using System.Linq;
using Itransition_course_project.Data;
using Itransition_course_project.Services.Abstract;

namespace Itransition_course_project.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly ApplicationDbContext _context;
        public AnswerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CheckIfAnswerExists(string userId, int postId)
        {
            return _context.Answers.Any(x => x.UserId == userId && x.PostId == postId);
        }
    }
}