namespace Git.ViewModels.Repositories
{
    public class RepositoryViewModel
    {
        public string Id { get; set; }

        public string RepoName { get; set; }

        public string OwnerName { get; set; }

        public string CreatedOn { get; set; }

        public int Commits { get; set; }
    }
}
