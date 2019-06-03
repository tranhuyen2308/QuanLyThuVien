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
    public partial class ucReader : UserControl
    {
        public ReaderManager manager = new ReaderManager();
        int? TotalPage = 0;
        int? PageIndex = 1;
        int? PageSize = 20;
        string Searching = "";
        int? PositionIdSearch = null;
        int? UnitIdSearch = null;
        string GenderSearch = "";
        string PhoneNumberSearch = "";

        PositionManager positionManager = new PositionManager();
        UnitManager unitManager = new UnitManager();

        List<Gender> listGender = new List<UcControl.Gender>() {
            new UcControl.Gender(){
                Id = true,
                Name = "Nam"
            },
            new UcControl.Gender(){
                Id = false,
                Name = "Nữ"
            }
        };

        public ucReader()
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
        private void LoadData(string name, string gender, string phoneNumber, int? positionIdSearch, int? unitIdSearch, int? pageIndex, int? pageSize)
        {

            dgvData.DataSource = GetList(name, gender, phoneNumber, positionIdSearch, unitIdSearch, pageIndex, pageSize);

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

        public List<ReaderBindingViewModel> GetList(string name, string gender, string phoneNumber, int? positionIdSearch, int? unitIdSearch, int? pageIndex, int? pageSize)
        {
            var listRs = manager.GetList(name, gender, phoneNumber, positionIdSearch, unitIdSearch, pageIndex, pageSize);

            TotalPage = (listRs.TotalRecord / pageSize) + (listRs.TotalRecord % pageSize == 0 ? 0 : 1);
            return listRs.ListOfObjects;
        }

        public void BindingData()
        {
            txtId.Text = txtName.Text = txtAddress.Text = txtEmail.Text = txtNote.Text = txtPhoneNumber.Text = "";

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

            Binding bdNote = new Binding("Text", dgvData.DataSource, "Note", true, DataSourceUpdateMode.Never);
            txtNote.DataBindings.Clear();
            txtNote.DataBindings.Add(bdNote);

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

                    if (PositionIdSearch != -1 && UnitIdSearch != -1)
                        btnAddNew.Enabled = true;
                    else
                        btnAddNew.Enabled = false;

                    LoadData(Searching, GenderSearch, PhoneNumberSearch, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
                }
                catch (Exception) // for first seasion
                {
                    btnAddNew.Enabled = false;
                    PositionIdSearch = -1;
                    LoadData(Searching, GenderSearch, PhoneNumberSearch, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
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

                    if (PositionIdSearch != -1 && UnitIdSearch != -1)
                        btnAddNew.Enabled = true;
                    else
                        btnAddNew.Enabled = false;

                    LoadData(Searching, GenderSearch, PhoneNumberSearch, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
                }
                catch (Exception) // for first seasion
                {
                    btnAddNew.Enabled = false;
                    UnitIdSearch = -1;
                    LoadData(Searching, GenderSearch, PhoneNumberSearch, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
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
            LoadData(Searching, GenderSearch, PhoneNumberSearch, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            txtId.Text = txtName.Text = txtAddress.Text = txtEmail.Text = txtNote.Text = txtPhoneNumber.Text = "";
            dtiDayOfBirth.Value = DateTime.Now;
            txtName.Focus();

            txtId.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool add = txtId.Text == "" ? false : true;

            if (!add)
            {
                ReaderBindingModel model = new ReaderBindingModel()
                {
                    Name = txtName.Text,
                    Address = txtAddress.Text,
                    DayOfBirth = dtiDayOfBirth.Value,
                    Email = txtEmail.Text,
                    Gender = cmbGender.Text,
                    Note = txtNote.Text,
                    PhoneNumber = txtPhoneNumber.Text,

                    PositionId = int.Parse(cmbPosition.SelectedValue.ToString()),
                    UnitId = int.Parse(cmbUnit.SelectedValue.ToString())
                };

                if (model.PositionId == -1 || model.UnitId == -1)
                {
                    MessageBox.Show("Chưa chọn chức vụ hoặc đơn vị.", "Thông báo");
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
                ReaderBindingModel model = new ReaderBindingModel()
                {
                    Id = int.Parse(txtId.Text),
                    Name = txtName.Text,
                    Address = txtAddress.Text,
                    DayOfBirth = dtiDayOfBirth.Value,
                    Email = txtEmail.Text,
                    Gender = cmbGender.Text,
                    Note = txtNote.Text,
                    PhoneNumber = txtPhoneNumber.Text,

                    PositionId = int.Parse(cmbPosition.SelectedValue.ToString()),
                    UnitId = int.Parse(cmbUnit.SelectedValue.ToString())
                };


                if (manager.Update(model))
                    MessageBox.Show("Sửa thành công !", "Thông báo");
                else
                    MessageBox.Show("Sửa không thành công!. Liên hệ với quản trị viên.", "Thông báo");
            }

            LoadData(Searching, GenderSearch, PhoneNumberSearch, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
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

                LoadData(Searching, GenderSearch, PhoneNumberSearch, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
            }
        }

        private void btnNVGFirst_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            LoadData(Searching, GenderSearch, PhoneNumberSearch, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
        }

        private void btnNGVPrevious_Click(object sender, EventArgs e)
        {
            PageIndex -= 1;
            LoadData(Searching, GenderSearch, PhoneNumberSearch, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
        }
        private void btnNVGNext_Click(object sender, EventArgs e)
        {
            PageIndex += 1;
            LoadData(Searching, GenderSearch, PhoneNumberSearch, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
        }


        private void btnNVGLast_Click(object sender, EventArgs e)
        {
            PageIndex = TotalPage;
            LoadData(Searching, GenderSearch, PhoneNumberSearch, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (txtPhoneNumber.Text.Length <= 10))
            {
                e.Handled = true;
            }

            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            //{
            //    e.Handled = true;
            //}

            // only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
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
        }
        
    }

    public class Gender
    {
        public bool Id { get; set; }
        public string Name { get; set; }
    }
}
