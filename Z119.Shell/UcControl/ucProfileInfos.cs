using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z119.Business.Manager;
using Z119.Model.Bindingmodel;

namespace Z119.Shell.UcControl
{
    public partial class ucProfileInfos : UserControl
    {
        public UserManager manager = new UserManager();

        List<Gender> listGender = new List<Gender>() {
            new Gender(){
                Id = true,
                Name = "Nam"
            },
            new Gender(){
                Id = false,
                Name = "Nữ"
            }
        };

        public ucProfileInfos()
        {
            InitializeComponent();

            txtId.Enabled = false;
            txtPosition.Enabled = false;
            txtUnit.Enabled = false;
            txtRole.Enabled = false;

            // Load Combobox Gender
            cmbGender.DataSource = listGender;
            cmbGender.DisplayMember = "Name";
            cmbGender.ValueMember = "Id";

            txtId.Text = Main.currentUser.Id.ToString();
            txtName.Text =  Main.currentUser.Name;
            try
            {
                dtiDayOfBirth.Value = DateTime.Parse(Main.currentUser.DayOfBirth.ToString());
            }
            catch (Exception)
            {
                dtiDayOfBirth.Value = DateTime.Now;
            }
            
            cmbGender.Text = Main.currentUser.Gender;
            txtEmail.Text = Main.currentUser.Email;
            txtAddress.Text = Main.currentUser.Address;
            txtPhoneNumber.Text = Main.currentUser.PhoneNumber;

            txtAccount.Text = Main.currentUser.Account;
            txtPassword.Text = Main.currentUser.Password;
            txtPosition.Text = Main.currentUser.Position;
            txtUnit.Text = Main.currentUser.Unit;
            txtRole.Text = Main.currentUser.Role;

            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UserBindingModel model = new UserBindingModel()
            {
                Id = int.Parse(txtId.Text),
                Name = txtName.Text,
                Password = txtPassword.Text,
                Account = txtAccount.Text,
                Address = txtAddress.Text,
                DayOfBirth = dtiDayOfBirth.Value,
                Email = txtEmail.Text,
                Gender = cmbGender.Text,
                PhoneNumber = txtPhoneNumber.Text,

            };


            if (manager.Update(model))
            {
                MessageBox.Show("Sửa thành công !", "Thông báo");
                
                Main.currentUser.Name = txtName.Text;
                Main.currentUser.DayOfBirth = dtiDayOfBirth.Value;
                Main.currentUser.Gender = cmbGender.Text;
                Main.currentUser.Email = txtEmail.Text; 
                Main.currentUser.Address = txtAddress.Text;
                Main.currentUser.PhoneNumber = txtPhoneNumber.Text;

                Main.currentUser.Account = txtAccount.Text;
                Main.currentUser.Password = txtPassword.Text;
            }
                
            else
                MessageBox.Show("Sửa không thành công!. Liên hệ với quản trị viên.", "Thông báo");
        }
    }
}
