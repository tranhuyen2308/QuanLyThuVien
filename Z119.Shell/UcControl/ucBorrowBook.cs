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
    public partial class ucBorrowBook : UserControl
    {

        public BorrowerManager manager = new BorrowerManager();
        int? TotalPage = 0;
        int? PageIndex = 1;
        int? PageSize = 20;
        string Searching = ""; // ReaderId, ReaderName, BookId, BookName, Note
        int? PositionIdSearch = null;
        int? UnitIdSearch = null;

        PositionManager positionManager = new PositionManager();
        UnitManager unitManager = new UnitManager();

        
        public ucBorrowBook()
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

            txtReaderId.Enabled = false;
            txtReaderName.Enabled = false;
            txtBookId.Enabled = false;
            txtBookName.Enabled = false;

            btnPayBook.Enabled = false;

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

        private void LoadData(string searching, int? positionIdSearch, int? unitIdSearch, int? pageIndex, int? pageSize)
        {

            dgvData.DataSource = GetList(searching, positionIdSearch, unitIdSearch, pageIndex, pageSize);

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

        public List<BorrowerBindingViewModel> GetList(string searching, int? positionIdSearch, int? unitIdSearch, int? pageIndex, int? pageSize)
        {
            var listRs = manager.GetList(searching, positionIdSearch, unitIdSearch, pageIndex, pageSize);

            TotalPage = (listRs.TotalRecord / pageSize) + (listRs.TotalRecord % pageSize == 0 ? 0 : 1);
            return listRs.ListOfObjects;
        }

        public void BindingData()
        {

            txtBookId.Text = "";
            txtBookName.Text = "";
            txtReaderId.Text = "";
            txtReaderName.Text = "";

            Binding bdReaderId = new Binding("Text", dgvData.DataSource, "ReaderId", true, DataSourceUpdateMode.Never);
            txtReaderId.DataBindings.Clear();
            txtReaderId.DataBindings.Add(bdReaderId);

            Binding bdReaderName = new Binding("Text", dgvData.DataSource, "ReaderName", true, DataSourceUpdateMode.Never);
            txtReaderName.DataBindings.Clear();
            txtReaderName.DataBindings.Add(bdReaderName);

            Binding bdBookId = new Binding("Text", dgvData.DataSource, "BookId", true, DataSourceUpdateMode.Never);
            txtBookId.DataBindings.Clear();
            txtBookId.DataBindings.Add(bdBookId);

            Binding bdBookName = new Binding("Text", dgvData.DataSource, "BookName", true, DataSourceUpdateMode.Never);
            txtBookName.DataBindings.Clear();
            txtBookName.DataBindings.Add(bdBookName);

        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPosition.SelectedValue.ToString()))
            {
                try
                {
                    PositionIdSearch = int.Parse(cmbPosition.SelectedValue.ToString());
                    LoadData(Searching, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
                }
                catch (Exception) // for first seasion
                {
                    PositionIdSearch = -1;
                    LoadData(Searching, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
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
                    LoadData(Searching, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
                }
                catch (Exception) // for first seasion
                {
                    UnitIdSearch = -1;
                    LoadData(Searching, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
                }

            }
        }

        private void txtReaderId_TextChanged(object sender, EventArgs e)
        {
            btnPayBook.Enabled = txtReaderId.Text == "" ? false : true;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Searching = txtSearch.Text;
            PageIndex = 1;
            LoadData(Searching, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
        }

        private void btnNVGFirst_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            LoadData(Searching, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
        }

        private void btnNGVPrevious_Click(object sender, EventArgs e)
        {
            PageIndex -= 1;
            LoadData(Searching, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
        }
        private void btnNVGNext_Click(object sender, EventArgs e)
        {
            PageIndex += 1;
            LoadData(Searching, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
        }


        private void btnNVGLast_Click(object sender, EventArgs e)
        {
            PageIndex = TotalPage;
            LoadData(Searching, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
        }

        private void btnPayBook_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn trả sách không?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int readerId = int.Parse(txtReaderId.Text);
                string bookId = txtBookId.Text;
                if (manager.PaymentBook(bookId))
                {
                    MessageBox.Show("Trả sách thành công.", "Thông báo");
                    LoadData(Searching, PositionIdSearch, UnitIdSearch, PageIndex, PageSize);
                }
                else
                {
                    MessageBox.Show("Trả sách không thành công. Liên hệ với quản trị viên.", "Thông báo");
                }
            }
            
        }

        private void txtBookName_TextChanged(object sender, EventArgs e)
        {
            
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
}
