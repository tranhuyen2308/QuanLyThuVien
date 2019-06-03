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
    public partial class ucUser : UserControl
    {
        
        public UserManager manager = new UserManager();
        int? TotalPage = 0;
        int? PageIndex = 1;
        int? PageSize = 20;
        string Searching = "";
        int? PositionIdSearch = null;
        int? UnitIdSearch = null;
        int? RoleIdSearch = null;
        string GenderSearch = "";
        string PhoneNumberSearch = "";

        PositionManager positionManager = new PositionManager();
        UnitManager unitManager = new UnitManager();
        RoleManager roleManager = new RoleManager();

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

        public ucUser()
        {
            InitializeComponent();

            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.AllowUserToAddRows = false;
            dgvData.ReadOnly = true;
            dgvData.MultiSelect = false;

            cmbPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;

            txtId.Enabled = false;

            txtPhoneNumber.MaxLength = 10;

            // Load Combobox Gender
            cmbGender.DataSource = listGender;
            cmbGender.DisplayMember = "Name";
            cmbGender.ValueMember = "Id";

            // Load combobox for position and unit
            cmbPosition.DataSource = LoadPosition();
            cmbPosition.DisplayMember = "Name";
            cmbPosition.ValueMember = "Id";

            PositionIdSearch = int.Parse(cmbPosition.SelectedValue.ToString());

            // Load combobox for position and unit
            cmbUnit.DataSource = LoadUnit();
            cmbUnit.DisplayMember = "Name";
            cmbUnit.ValueMember = "Id";

            UnitIdSearch = int.Parse(cmbUnit.SelectedValue.ToString());

            // Load combobox for role
            cmbRole.DataSource = LoadRole();
            cmbRole.DisplayMember = "Name";
            cmbRole.ValueMember = "Id";

            RoleIdSearch = int.Parse(cmbRole.SelectedValue.ToString());

            btnDelete.Enabled = txtId.Text == "" ? false : true;
            btnSave.Enabled = txtName.Text == "" ? false : true;
        }

        public List<PositionBindingModel> LoadPosition()
        {
            var listRs = positionManager.GetList();
            listRs.ListOfObjects.Insert(0, new PositionBindingModel()
            {
                Id = -1,
                Name = "Chọn tất cả"
            });
            return listRs.ListOfObjects;
        }

        public List<UnitBindingModel> LoadUnit()
        {
            var listRs = unitManager.GetList();
            listRs.ListOfObjects.Insert(0, new UnitBindingModel()
            {
                Id = -1,
                Name = "Chọn tất cả"
            });
            return listRs.ListOfObjects;
        }

        public List<RoleBindingModel> LoadRole()
        {
            var listRs = roleManager.GetList();
            listRs.ListOfObjects.Insert(0, new RoleBindingModel()
            {
                Id = -1,
                Name = "Chọn tất cả"
            });
            return listRs.ListOfObjects;
        }

        private void LoadData(string name,  int? positionIdSearch, int? unitIdSearch, int? roleIdSearch, int? pageIndex, int? pageSize)
        {

            dgvData.DataSource = GetList(name, positionIdSearch, unitIdSearch, roleIdSearch, pageIndex, pageSize);

            BindingData();

            lblNVGTotalPage.Text = TotalPage.ToString();
            txtNVGCurrentPage.Text = pageIndex.ToString();

            if (pageIndex <= 1)
            {
                btnNVGFirst.Enabled = false;
                btnNGVPrevious.Enabled = false;
                btnNVGNext.Enabled = true;
                btnNVGLast.Enabled = true;
            }
            else if (pageIndex > 1 && pageIndex < TotalPage)
            {
                btnNVGFirst.Enabled = true;
                btnNGVPrevious.Enabled = true;
                btnNVGNext.Enabled = true;
                btnNVGLast.Enabled = true;
            }
            else
            {
                btnNVGFirst.Enabled = true;
                btnNGVPrevious.Enabled = true;
                btnNVGNext.Enabled = false;
                btnNVGLast.Enabled = false;
            }

        }

        public List<UserBindingViewModel> GetList(string name, int? positionIdSearch, int? unitIdSearch, int? roleIdSearch, int? pageIndex, int? pageSize)
        {
            var listRs = manager.GetList(name, positionIdSearch, unitIdSearch, roleIdSearch, pageIndex, pageSize);

            TotalPage = (listRs.TotalRecord / pageSize) + (listRs.TotalRecord % pageSize == 0 ? 0 : 1);
            return listRs.ListOfObjects;
        }

        public void BindingData()
        {
            txtId.Text = txtName.Text = txtAddress.Text = txtEmail.Text = txtAccount.Text = txtPassword.Text = txtPhoneNumber.Text = "";

            dtiDayOfBirth.Value = DateTime.Now;

            Binding bdId = new Binding("Text", dgvData.DataSource, "Id", true, DataSourceUpdateMode.Never);
            txtId.DataBindings.Clear();
            txtId.DataBindings.Add(bdId);

            Binding bdName = new Binding("Text", dgvData.DataSource, "Name", true, DataSourceUpdateMode.Never);
            txtName.DataBindings.Clear();
            txtName.DataBindings.Add(bdName);

            Binding bdAddress = new Binding("Text", dgvData.DataSource, "Address", true, DataSourceUpdateMode.Never);
            txtAddress.DataBindings.Clear();
            txtAddress.DataBindings.Add(bdAddress);

            Binding bdDayOfBirth = new Binding("Value", dgvData.DataSource, "DayOfBirth", true, DataSourceUpdateMode.Never);
            dtiDayOfBirth.DataBindings.Clear();
            dtiDayOfBirth.DataBindings.Add(bdDayOfBirth);

            Binding bdEmail = new Binding("Text", dgvData.DataSource, "Email", true, DataSourceUpdateMode.Never);
            txtEmail.DataBindings.Clear();
            txtEmail.DataBindings.Add(bdEmail);

            Binding bdGender = new Binding("Text", dgvData.DataSource, "Gender", true, DataSourceUpdateMode.Never);
            cmbGender.DataBindings.Clear();
            cmbGender.DataBindings.Add(bdGender);

            Binding bdAccount = new Binding("Text", dgvData.DataSource, "Account", true, DataSourceUpdateMode.Never);
            txtAccount.DataBindings.Clear();
            txtAccount.DataBindings.Add(bdAccount);

            Binding bdPassword = new Binding("Text", dgvData.DataSource, "Password", true, DataSourceUpdateMode.Never);
            txtPassword.DataBindings.Clear();
            txtPassword.DataBindings.Add(bdPassword);

            Binding bdPhoneNumber = new Binding("Text", dgvData.DataSource, "PhoneNumber", true, DataSourceUpdateMode.Never);
            txtPhoneNumber.DataBindings.Clear();
            txtPhoneNumber.DataBindings.Add(bdPhoneNumber);

        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPosition.SelectedValue.ToString()))
            {
                try
                {
                    PositionIdSearch = int.Parse(cmbPosition.SelectedValue.ToString());

                    if (PositionIdSearch != -1 && UnitIdSearch != -1 && RoleIdSearch != -1)
                        btnAddNew.Enabled = true;
                    else
                        btnAddNew.Enabled = false;

                    LoadData(Searching, PositionIdSearch, UnitIdSearch, RoleIdSearch, PageIndex, PageSize);
                }
                catch (Exception) // for first seasion
                {
                    btnAddNew.Enabled = false;
                    PositionIdSearch = -1;
                    LoadData(Searching, PositionIdSearch, UnitIdSearch,RoleIdSearch, PageIndex, PageSize);
                }

            }
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbUnit.SelectedValue.ToString()))
            {
                try
                {
                    UnitIdSearch = int.Parse(cmbUnit.SelectedValue.ToString());
                    if (PositionIdSearch != -1 && UnitIdSearch != -1 && RoleIdSearch != -1)
                        btnAddNew.Enabled = true;
                    else
                        btnAddNew.Enabled = false;

                    LoadData(Searching, PositionIdSearch, UnitIdSearch, RoleIdSearch, PageIndex, PageSize);
                }
                catch (Exception) // for first seasion
                {
                    btnAddNew.Enabled = false;
                    UnitIdSearch = -1;
                    LoadData(Searching, PositionIdSearch, UnitIdSearch, RoleIdSearch, PageIndex, PageSize);
                }

            }
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbRole.SelectedValue.ToString()))
            {
                try
                {
                    RoleIdSearch = int.Parse(cmbRole.SelectedValue.ToString());
                    if (PositionIdSearch != -1 && UnitIdSearch != -1 && RoleIdSearch != -1)
                        btnAddNew.Enabled = true;
                    else
                        btnAddNew.Enabled = false;

                    LoadData(Searching, PositionIdSearch, UnitIdSearch, RoleIdSearch, PageIndex, PageSize);
                }
                catch (Exception) // for first seasion
                {
                    btnAddNew.Enabled = false;
                    RoleIdSearch = -1;
                    LoadData(Searching, PositionIdSearch, UnitIdSearch, RoleIdSearch, PageIndex, PageSize);
                }

            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = txtId.Text == "" ? false : true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = txtName.Text == "" ? false : true;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Searching = txtSearch.Text;
            PageIndex = 1;
            LoadData(Searching, PositionIdSearch, UnitIdSearch, RoleIdSearch, PageIndex, PageSize);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            txtId.Text = txtName.Text = txtAddress.Text = txtEmail.Text = txtAccount.Text = txtPassword.Text = txtPhoneNumber.Text = "";
            dtiDayOfBirth.Value = DateTime.Now;
            txtName.Focus();

            txtId.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool add = txtId.Text == "" ? false : true;

            if (!add)
            {
                UserBindingModel model = new UserBindingModel()
                {
                    Name = txtName.Text,
                    Password = txtPassword.Text,
                    Account = txtAccount.Text,
                    Address = txtAddress.Text,
                    DayOfBirth = dtiDayOfBirth.Value,
                    Email = txtEmail.Text,
                    Gender = cmbGender.Text,
                    PhoneNumber = txtPhoneNumber.Text,

                    PositionId = int.Parse(cmbPosition.SelectedValue.ToString()),
                    UnitId = int.Parse(cmbUnit.SelectedValue.ToString()),
                    RoleId = int.Parse(cmbRole.SelectedValue.ToString())
                };

                if (model.PositionId == -1 || model.UnitId == -1 || model.RoleId == -1)
                {
                    MessageBox.Show("Chưa chọn chức vụ hoặc đơn vị hoặc quyền.", "Thông báo");
                    return;
                }

                if (manager.Create(model))
                {
                    add = false;
                    MessageBox.Show("Thêm mới thành công !", "Thông báo");
                }
                else
                    MessageBox.Show("Thêm mới không thành công!. Liên hệ với quản trị viên.", "Thông báo");
            }
            else
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

                    PositionId = int.Parse(cmbPosition.SelectedValue.ToString()),
                    UnitId = int.Parse(cmbUnit.SelectedValue.ToString()),
                    RoleId = int.Parse(cmbRole.SelectedValue.ToString())
                };

                
                if (manager.Update(model))
                    MessageBox.Show("Sửa thành công !", "Thông báo");
                else
                    MessageBox.Show("Sửa không thành công!. Liên hệ với quản trị viên.", "Thông báo");
            }

            LoadData(Searching, PositionIdSearch, UnitIdSearch, RoleIdSearch, PageIndex, PageSize);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa !", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int id = int.Parse(txtId.Text);
                if (manager.Delete(id))
                    MessageBox.Show("Xóa thành công !", "Thông báo");
                else
                    MessageBox.Show("Xóa không thành công. Có danh mục đang sử dụng nên không xóa được", "Thông báo");

                LoadData(Searching, PositionIdSearch, UnitIdSearch, RoleIdSearch, PageIndex, PageSize);
            }
        }

        private void btnNVGFirst_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            LoadData(Searching, PositionIdSearch, UnitIdSearch, RoleIdSearch, PageIndex, PageSize);
        }

        private void btnNGVPrevious_Click(object sender, EventArgs e)
        {
            PageIndex -= 1;
            LoadData(Searching, PositionIdSearch, UnitIdSearch, RoleIdSearch, PageIndex, PageSize);
        }
        private void btnNVGNext_Click(object sender, EventArgs e)
        {
            PageIndex += 1;
            LoadData(Searching, PositionIdSearch, UnitIdSearch, RoleIdSearch, PageIndex, PageSize);
        }


        private void btnNVGLast_Click(object sender, EventArgs e)
        {
            PageIndex = TotalPage;
            LoadData(Searching, PositionIdSearch, UnitIdSearch, RoleIdSearch, PageIndex, PageSize);
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (txtPhoneNumber.Text.Length <= 10))
            {
                e.Handled = true;
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            // Load combobox for position and unit
            cmbPosition.DataSource = LoadPosition();
            cmbPosition.DisplayMember = "Name";
            cmbPosition.ValueMember = "Id";

            PositionIdSearch = int.Parse(cmbPosition.SelectedValue.ToString());

            // Load combobox for position and unit
            cmbUnit.DataSource = LoadUnit();
            cmbUnit.DisplayMember = "Name";
            cmbUnit.ValueMember = "Id";

            UnitIdSearch = int.Parse(cmbUnit.SelectedValue.ToString());

            // Load combobox for role
            cmbRole.DataSource = LoadRole();
            cmbRole.DisplayMember = "Name";
            cmbRole.ValueMember = "Id";

            RoleIdSearch = int.Parse(cmbRole.SelectedValue.ToString());
        }
        
    }

    
}
