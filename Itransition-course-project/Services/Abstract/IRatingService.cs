namespace Itransition_course_project.Services.Abstract
{
    public interface IRatingService
    {
        bool CheckIfRateExists(string userId, int postId);
        string GetAverageRating(int postId);
    }
}