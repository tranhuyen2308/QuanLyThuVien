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
    public partial class ucCategory : UserControl
    {
        public CategoryManager manager = new CategoryManager();
        int? TotalPage = 0;
        int? PageIndex = 1;
        int? PageSize = 20;
        string Searching = "";
        int? SpecializedIdSearch = null;

        SpecializedManager specializedManager = new SpecializedManager();

        
        public ucCategory()
        {
            InitializeComponent();

            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.AllowUserToAddRows = false;
            dgvData.ReadOnly = true;
            dgvData.MultiSelect = false;

            cmbSpecialized.DropDownStyle = ComboBoxStyle.DropDownList;

            txtId.Enabled = false;

            // Load combobox
            cmbSpecialized.DataSource = LoadNganh();
            cmbSpecialized.DisplayMember = "Name";
            cmbSpecialized.ValueMember = "Id";

            SpecializedIdSearch = int.Parse(cmbSpecialized.SelectedValue.ToString());

            btnDelete.Enabled = txtId.Text == "" ? false : true;
            btnSave.Enabled = txtName.Text == "" ? false : true;

        }

        // Load vao combobox roi load datagridview theo combobox
        private void LoadData(int? specializedIdSearch, string name, int? pageIndex, int? pageSize)
        {

            dgvData.DataSource = GetList(specializedIdSearch, name, pageIndex, pageSize);

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

        public List<SpecializedBindingModel> LoadNganh()
        {
            var listRs = specializedManager.GetList();
            listRs.ListOfObjects.Insert(0, new SpecializedBindingModel() {
                Id = -1,
                Name = "Chọn tất cả"
            });
            return listRs.ListOfObjects;
        }

        public List<CategoryViewModel> GetList(int? specializedIdSearch, string name, int? pageIndex, int? pageSize)
        {
            var listRs = manager.GetList(specializedIdSearch, name, pageIndex, pageSize);

            TotalPage = (listRs.TotalRecord / pageSize) + (listRs.TotalRecord % pageSize == 0 ? 0 : 1);
            return listRs.ListOfObjects;
        }

        public void BindingData()
        {
            txtId.Text = txtName.Text = "";

            Binding bdId = new Binding("Text", dgvData.DataSource, "Id", true, DataSourceUpdateMode.Never);
            txtId.DataBindings.Clear();
            txtId.DataBindings.Add(bdId);

            Binding bdName = new Binding("Text", dgvData.DataSource, "Name", true, DataSourceUpdateMode.Never);
            txtName.DataBindings.Clear();
            txtName.DataBindings.Add(bdName);

            //Binding bdSpecializedName = new Binding("SelectedValue", dgvData.DataSource, "SpecializedId", true, DataSourceUpdateMode.Never);
            //cmbSpecialized.DataBindings.Clear();
            //cmbSpecialized.DataBindings.Add(bdSpecializedName);

            

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtName.Focus();

            txtId.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool add = txtId.Text == "" ? false : true;

            if (!add)
            {
                CategoryBindingModel model = new CategoryBindingModel()
                {
                    Name = txtName.Text,
                    SpecializedId = int.Parse(cmbSpecialized.SelectedValue.ToString())
                };

                if (model.SpecializedId == -1)
                {
                    MessageBox.Show("Chưa chọn ngành.", "Thông báo");
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
                CategoryBindingModel model = new CategoryBindingModel()
                {
                    Id = int.Parse(txtId.Text),
                    Name = txtName.Text
                };

                if (manager.Update(model))
                    MessageBox.Show("Sửa thành công !", "Thông báo");
                else
                    MessageBox.Show("Sửa không thành công!. Liên hệ với quản trị viên.", "Thông báo");
            }

            LoadData(SpecializedIdSearch, Searching, PageIndex, PageSize);
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

                LoadData(SpecializedIdSearch, Searching, PageIndex, PageSize);
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

        private void btnNVGFirst_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            LoadData(SpecializedIdSearch, Searching, PageIndex, PageSize);
        }

        private void btnNGVPrevious_Click(object sender, EventArgs e)
        {
            PageIndex -= 1;
            LoadData(SpecializedIdSearch, Searching, PageIndex, PageSize);
        }
        private void btnNVGNext_Click(object sender, EventArgs e)
        {
            PageIndex += 1;
            LoadData(SpecializedIdSearch, Searching, PageIndex, PageSize);
        }


        private void btnNVGLast_Click(object sender, EventArgs e)
        {
            PageIndex = TotalPage;
            LoadData(SpecializedIdSearch, Searching, PageIndex, PageSize);
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Searching = txtSearch.Text;
            PageIndex = 1;
            LoadData(SpecializedIdSearch, Searching, PageIndex, PageSize);
        }

        private void cmbSpecialized_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbSpecialized.SelectedValue.ToString()))
            {
                try
                {
                    SpecializedIdSearch = int.Parse(cmbSpecialized.SelectedValue.ToString());

                    if (SpecializedIdSearch != -1)
                        btnAddNew.Enabled = true;
                    else
                        btnAddNew.Enabled = false;

                    LoadData(SpecializedIdSearch, Searching, PageIndex, PageSize);
                }
                catch (Exception) // for first seasion
                {
                    btnAddNew.Enabled = false;
                    SpecializedIdSearch = -1;
                    LoadData(SpecializedIdSearch, Searching, PageIndex, PageSize);
                }

            }
            
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            // Load combobox
            cmbSpecialized.DataSource = LoadNganh();
            cmbSpecialized.DisplayMember = "Name";
            cmbSpecialized.ValueMember = "Id";

            SpecializedIdSearch = int.Parse(cmbSpecialized.SelectedValue.ToString());
        }
        
    }
}
