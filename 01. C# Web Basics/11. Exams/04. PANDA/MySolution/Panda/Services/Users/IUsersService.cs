using Panda.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Services.Users
{
    public interface IUsersService
    {
        void Create(RegisterInputModel register);

        bool IsUsernameAvailable(RegisterInputModel register);

        bool IsEmailAvailable(RegisterInputModel registerl);

        string GetUserId(LoginInputModel login);

        string GetUsername(string id);

        ICollection<string> GetAllUsernames();
    }

}
