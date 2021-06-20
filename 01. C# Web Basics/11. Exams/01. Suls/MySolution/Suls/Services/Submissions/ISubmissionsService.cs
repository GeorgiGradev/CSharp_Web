using Suls.ViewModels.Submissions;

namespace Suls.Services.Submissions
{
    public interface ISubmissionsService
    {
        public void CreateSubmission(string problemId, string userId, SubmissionInputModel model);

        void DeleteSubmission(string submissionId);
    }
}
