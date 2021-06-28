using System;
using System.Collections.Generic;

namespace CarShop.ViewModels.Issues
{
    public class AllIssuesViewModel
    {
        public string Model { get; set; }

        public string CarId { get; set; }

        public string UserRole { get; set; }

        public ICollection<IssueViewModel> Issues { get; set; }
    }
}
