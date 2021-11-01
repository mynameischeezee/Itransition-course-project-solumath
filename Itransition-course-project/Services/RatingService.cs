using System;
using System.Linq;
using Itransition_course_project.Data;
using Itransition_course_project.Services.Abstract;

namespace Itransition_course_project.Services
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;
        public RatingService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CheckIfRateExists(string userId, int postId)
        {
            return _context.RateUserPosts.Any(x => x.UserId == userId && x.PostId == postId);
        }
        public string GetAverageRating(int postId)
        {
            var rates = _context.RateUserPosts.ToList().FindAll(x => x.PostId == postId);
            if (rates.Count() == 0)
            {
                return "0";
            }
            return (rates.Sum(x => Convert.ToInt32(x.Rate)) / rates.Count()).ToString();
        }
    }
}