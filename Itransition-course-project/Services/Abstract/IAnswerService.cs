namespace Itransition_course_project.Services.Abstract
{
    public interface IAnswerService
    {
        bool CheckIfAnswerExists(string userId, int postId);
    }
}