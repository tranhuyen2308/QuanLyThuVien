using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z119.Common;

namespace Z119.Shell
{
    public delegate void EventLoginSuccess(object sender, LoginSuccessEventArgs e);
    public class LoginSuccessEventArgs : EventArgs
    {
        public CurrentPersion user { get; set; }
    }
}
