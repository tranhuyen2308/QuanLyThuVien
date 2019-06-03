using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z119.Model.Bindingmodel;

namespace Z119.Business.Manager
{
    public class LoginManager
    {
        UserManager manager = new UserManager();

        public UserBindingViewModel Login(string account, string password)
        {
            return manager.getUser(account, password);
        }
    }
}
