using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z119.Business.Manager;
using Z119.Common;
using Z119.Model.Bindingmodel;

namespace Z119.Shell
{
    public partial class frmLogin : Form
    {
        public event EventLoginSuccess eventLogin = null;

        LoginManager manager = new LoginManager();
        UserBindingViewModel model = new UserBindingViewModel();



        string account = "";
        string password = "";

        public frmLogin()
        {
            InitializeComponent();

            lblStatus.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            account = txtAccount.Text;
            password = txtPassword.Text;

            model = manager.Login(account, password);
            if (model != null)
            {
                CurrentPersion user = new CurrentPersion();

                user.Id = model.Id;
                user.Account = model.Account;
                user.Name = model.Name;
                user.Password = model.Password;
                user.Gender = model.Gender;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                user.Position = model.Position;
                user.Unit = model.Unit;
                user.Email = model.Email;
                user.DayOfBirth = model.DayOfBirth;
                user.Role = model.Role;

                if (eventLogin != null)
                    eventLogin(this, new LoginSuccessEventArgs() { user = user });

                this.Close();
            }

            else
                lblStatus.Text = "Tên đăng nhập hoặc mật khẩu không đúng";
        }
    }
}
