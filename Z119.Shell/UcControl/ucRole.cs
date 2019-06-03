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
    public partial class ucRole : UserControl
    {
        public RoleManager manager = new RoleManager();
        int? TotalPage = 0;
        int? PageIndex = 1;
        int? PageSize = 20;
        string Searching = "";

        public ucRole()
        {
            InitializeComponent();

            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.AllowUserToAddRows = false;
            dgvData.ReadOnly = true;
            dgvData.MultiSelect = false;

            txtId.Enabled = false;

            LoadData(Searching, PageIndex, PageSize);

            btnDelete.Enabled = txtId.Text == "" ? false : true;
            btnSave.Enabled = txtName.Text == "" ? false : true;
        }

        private void LoadData(string name, int? pageIndex, int? pageSize)
        {
            dgvData.DataSource = GetList(name, pageIndex, pageSize);
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

            if (TotalPage == 1)
            {
                btnNVGFirst.Enabled = false;
                btnNGVPrevious.Enabled = false;
                btnNVGNext.Enabled = false;
                btnNVGLast.Enabled = false;
            }

        }


        public List<RoleBindingModel> GetList(string name, int? pageIndex, int? pageSize)
        {
            var listRs = manager.GetList(name, pageIndex, pageSize);

            TotalPage = (listRs.TotalRecord / pageSize) + (listRs.TotalRecord % pageSize == 0 ? 0 : 1);
            //totalPage = listRs.TotalRecord;
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
                RoleBindingModel model = new RoleBindingModel()
                {
                    Name = txtName.Text
                };

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
                RoleBindingModel model = new RoleBindingModel()
                {
                    Id = int.Parse(txtId.Text),
                    Name = txtName.Text
                };

                if (manager.Update(model))
                    MessageBox.Show("Sửa thành công !", "Thông báo");
                else
                    MessageBox.Show("Sửa không thành công!. Liên hệ với quản trị viên.", "Thông báo");
            }

            LoadData(Searching, PageIndex, PageSize);
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

                LoadData(Searching, PageIndex, PageSize);
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
            LoadData(Searching, PageIndex, PageSize);
        }

        private void btnNGVPrevious_Click(object sender, EventArgs e)
        {
            PageIndex -= 1;
            LoadData(Searching, PageIndex, PageSize);
        }
        private void btnNVGNext_Click(object sender, EventArgs e)
        {
            PageIndex += 1;
            LoadData(Searching, PageIndex, PageSize);
        }


        private void btnNVGLast_Click(object sender, EventArgs e)
        {
            PageIndex = TotalPage;
            LoadData(Searching, PageIndex, PageSize);
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Searching = txtSearch.Text;
            PageIndex = 1;
            LoadData(Searching, PageIndex, PageSize);
        }
        
    }
}
