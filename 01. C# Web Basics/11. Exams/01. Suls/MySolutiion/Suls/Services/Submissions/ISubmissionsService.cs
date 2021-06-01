using Suls.ViewModels.Submissions;

namespace Suls.Services.Submissions
{
    public interface ISubmissionsService
    {
        public void Create(SubmissionsCreateInputModel model, string userId);

        void Delete(string id);
    }
}
