using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Z119.Business.Manager;
using Z119.Model.Bindingmodel;
using Z119.Common;

namespace Z119.Shell.UcControl
{
    public partial class ucBook : UserControl
    {
        public bool Add = false;

        public BookManager manager = new BookManager();
        int? TotalPage = 0;
        int? PageIndex = 1;
        int? PageSize = 20;
        string Searching = "";
        int? CategoryIdSearch = null;
        int? SpecialCategoryIdSearch = null;
        int? BookShelfIdSearch = null;
        int? PublisherIdSearch = null;
        int? AuthorIdSearch = null;

        List<ReaderBindingforBorrower> lstReader = new List<ReaderBindingforBorrower>();

        CategoryManager categoryManager = new CategoryManager();
        SpecialCategoryManager specialCategoryManager = new SpecialCategoryManager();
        BookShelfManager bookShelfManager = new BookShelfManager();
        PublisherManager publisherManager = new PublisherManager();
        AuthorManager authorManager = new AuthorManager();

        ReaderManager readerManager = new ReaderManager();
        UnitManager unitManager = new UnitManager();

        public ucBook()
        {
            InitializeComponent();

            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvData.AllowUserToAddRows = false;
            dgvData.ReadOnly = true;
            dgvData.MultiSelect = false;

            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSpecialCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBookShelf.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPublisher.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAuthor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReaderForBorrow.DropDownStyle = ComboBoxStyle.DropDownList;


            txtId.Enabled = false;
            txtStatus.Enabled = false;
            txtReleaseYear.MaxLength = 4;
            txtPageCount.MaxLength = 4;

            btnBorrowBook.Enabled = false;

            // Load combobox category
            cmbCategory.DataSource = LoadCategory();
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "Id";

            CategoryIdSearch = int.Parse(cmbCategory.SelectedValue.ToString());

            // Load combobox special category
            cmbSpecialCategory.DataSource = LoadSpecialCategory();
            cmbSpecialCategory.DisplayMember = "Name";
            cmbSpecialCategory.ValueMember = "Id";

            SpecialCategoryIdSearch = int.Parse(cmbSpecialCategory.SelectedValue.ToString());

            // Load combobox bookshelf
            cmbBookShelf.DataSource = LoadBookShelf();
            cmbBookShelf.DisplayMember = "Name";
            cmbBookShelf.ValueMember = "Id";

            BookShelfIdSearch = int.Parse(cmbBookShelf.SelectedValue.ToString());

            // Load combobox publisher
            cmbPublisher.DataSource = LoadPublisher();
            cmbPublisher.DisplayMember = "Name";
            cmbPublisher.ValueMember = "Id";

            PublisherIdSearch = int.Parse(cmbPublisher.SelectedValue.ToString());

            // Load combobox author
            cmbAuthor.DataSource = LoadAuthor();
            cmbAuthor.DisplayMember = "Name";
            cmbAuthor.ValueMember = "Id";

            AuthorIdSearch = int.Parse(cmbAuthor.SelectedValue.ToString());

            

            // Load Unit manager// Load combobox for position and unit
            cmbUnit.DataSource = LoadUnit();
            cmbUnit.DisplayMember = "Name";
            cmbUnit.ValueMember = "Id";

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

        public List<CategoryViewModel> LoadCategory()
        {
            var listRs = categoryManager.GetList();
            listRs.ListOfObjects.Insert(0, new CategoryViewModel()
            {
                Id = -1,
                Name = "Chọn tất cả"
            });
            return listRs.ListOfObjects;
        }

        public List<SpecialCategoryBindingModel> LoadSpecialCategory()
        {
            var listRs = specialCategoryManager.GetList();
            listRs.ListOfObjects.Insert(0, new SpecialCategoryBindingModel()
            {
                Id = -1,
                Name = "Chọn tất cả"
            });
            return listRs.ListOfObjects;
        }

        public List<BookShelfBindingModel> LoadBookShelf()
        {
            var listRs = bookShelfManager.GetList();
            listRs.ListOfObjects.Insert(0, new BookShelfBindingModel()
            {
                Id = -1,
                Name = "Chọn tất cả"
            });
            return listRs.ListOfObjects;
        }

        public List<PublisherBindingModel> LoadPublisher()
        {
            var listRs = publisherManager.GetList();
            listRs.ListOfObjects.Insert(0, new PublisherBindingModel()
            {
                Id = -1,
                Name = "Chọn tất cả"
            });
            return listRs.ListOfObjects;
        }

        public List<AuthorBindingModel> LoadAuthor()
        {
            var listRs = authorManager.GetList();
            listRs.ListOfObjects.Insert(0, new AuthorBindingModel()
            {
                Id = -1,
                Name = "Chọn tất cả"
            });
            return listRs.ListOfObjects;
        }



        private void LoadData(string searching, int? categoryIdSearch, int? specialCategoryIdSearch, int? bookShelfIdSearch, int? publisherIdSearch, int? authorIdSearch, int? pageIndex, int? pageSize)
        {

            dgvData.DataSource = GetList(searching, categoryIdSearch, specialCategoryIdSearch, bookShelfIdSearch, publisherIdSearch, authorIdSearch, pageIndex, pageSize);

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

        public List<BookBindingViewModel> GetList(string name, int? categoryIdSearch, int? specialCategoryIdSearch, int? bookShelfIdSearch, int? publisherIdSearch, int? authorIdSearch, int? pageIndex, int? pageSize)
        {
            var listRs = manager.GetList(name, categoryIdSearch, specialCategoryIdSearch, bookShelfIdSearch, publisherIdSearch, authorIdSearch, pageIndex, pageSize);

            TotalPage = (listRs.TotalRecord / pageSize) + (listRs.TotalRecord % pageSize == 0 ? 0 : 1);
            return listRs.ListOfObjects;
        }

        public void BindingData()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtLanguage.Text = "";
            txtNote.Text = "";
            txtPageCount.Text = "";
            txtPrice.Text = "";
            txtReleaseYear.Text = "";
            txtStatus.Text = "";


            Binding bdId = new Binding("Text", dgvData.DataSource, "Id", true, DataSourceUpdateMode.Never);
            txtId.DataBindings.Clear();
            txtId.DataBindings.Add(bdId);

            Binding bdName = new Binding("Text", dgvData.DataSource, "Name", true, DataSourceUpdateMode.Never);
            txtName.DataBindings.Clear();
            txtName.DataBindings.Add(bdName);

            Binding bdLanguage = new Binding("Text", dgvData.DataSource, "Language", true, DataSourceUpdateMode.Never);
            txtLanguage.DataBindings.Clear();
            txtLanguage.DataBindings.Add(bdLanguage);

            Binding bdNote = new Binding("Text", dgvData.DataSource, "Note", true, DataSourceUpdateMode.Never);
            txtNote.DataBindings.Clear();
            txtNote.DataBindings.Add(bdNote);

            Binding bdPageCount = new Binding("Text", dgvData.DataSource, "PageCount", true, DataSourceUpdateMode.Never);
            txtPageCount.DataBindings.Clear();
            txtPageCount.DataBindings.Add(bdPageCount);

            Binding bdPrice = new Binding("Text", dgvData.DataSource, "Price", true, DataSourceUpdateMode.Never);
            txtPrice.DataBindings.Clear();
            txtPrice.DataBindings.Add(bdPrice);

            Binding bdReleaseYear = new Binding("Text", dgvData.DataSource, "ReleaseYear", true, DataSourceUpdateMode.Never);
            txtReleaseYear.DataBindings.Clear();
            txtReleaseYear.DataBindings.Add(bdReleaseYear);

            Binding bdStatus = new Binding("Text", dgvData.DataSource, "Status", true, DataSourceUpdateMode.Never);
            txtStatus.DataBindings.Clear();
            txtStatus.DataBindings.Add(bdStatus);

        }

        private void cmbAuthor_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(cmbAuthor.SelectedValue.ToString()))
            {
                try
                {
                    AuthorIdSearch = int.Parse(cmbAuthor.SelectedValue.ToString());

                    if (AuthorIdSearch != -1 && PublisherIdSearch != -1 && BookShelfIdSearch != -1 &&
                        CategoryIdSearch != -1 && SpecialCategoryIdSearch != -1)
                        btnAddNew.Enabled = true;
                    else
                        btnAddNew.Enabled = false;

                    LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
                }
                catch (Exception) // for first seasion
                {
                    btnAddNew.Enabled = false;
                    AuthorIdSearch = -1;
                    LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
                }

            }
        }

        private void cmbPublisher_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(cmbPublisher.SelectedValue.ToString()))
            {
                try
                {
                    PublisherIdSearch = int.Parse(cmbPublisher.SelectedValue.ToString());

                    if (AuthorIdSearch != -1 && PublisherIdSearch != -1 && BookShelfIdSearch != -1 &&
                        CategoryIdSearch != -1 && SpecialCategoryIdSearch != -1)
                        btnAddNew.Enabled = true;
                    else
                        btnAddNew.Enabled = false;

                    LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
                }
                catch (Exception) // for first seasion
                {
                    btnAddNew.Enabled = false;
                    PublisherIdSearch = -1;
                    LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
                }

            }
        }

        private void cmbBookShelf_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(cmbBookShelf.SelectedValue.ToString()))
            {
                try
                {
                    BookShelfIdSearch = int.Parse(cmbBookShelf.SelectedValue.ToString());

                    if (AuthorIdSearch != -1 && PublisherIdSearch != -1 && BookShelfIdSearch != -1 &&
                        CategoryIdSearch != -1 && SpecialCategoryIdSearch != -1)
                        btnAddNew.Enabled = true;
                    else
                        btnAddNew.Enabled = false;

                    LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
                }
                catch (Exception) // for first seasion
                {
                    btnAddNew.Enabled = false;
                    BookShelfIdSearch = -1;
                    LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
                }

            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(cmbCategory.SelectedValue.ToString()))
            {
                try
                {
                    CategoryIdSearch = int.Parse(cmbCategory.SelectedValue.ToString());

                    if (AuthorIdSearch != -1 && PublisherIdSearch != -1 && BookShelfIdSearch != -1 &&
                        CategoryIdSearch != -1 && SpecialCategoryIdSearch != -1)
                        btnAddNew.Enabled = true;
                    else
                        btnAddNew.Enabled = false;

                    LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
                }
                catch (Exception) // for first seasion
                {
                    btnAddNew.Enabled = false;
                    CategoryIdSearch = -1;
                    LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
                }

            }
        }

        private void cmbSpecialCategory_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(cmbSpecialCategory.SelectedValue.ToString()))
            {
                try
                {
                    SpecialCategoryIdSearch = int.Parse(cmbSpecialCategory.SelectedValue.ToString());

                    if (AuthorIdSearch != -1 && PublisherIdSearch != -1 && BookShelfIdSearch != -1 &&
                        CategoryIdSearch != -1 && SpecialCategoryIdSearch != -1)
                        btnAddNew.Enabled = true;
                    else
                        btnAddNew.Enabled = false;

                    LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
                }
                catch (Exception) // for first seasion
                {
                    btnAddNew.Enabled = false;
                    SpecialCategoryIdSearch = -1;
                    LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
                }

            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = txtId.Text.Trim() == "" ? false : true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = (txtName.Text == "" ? false : true && txtId.Text == "" ? false : true);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Searching = txtSearch.Text;
            PageIndex = 1;
            LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtId.Text = "";
            txtName.Text = "";
            txtLanguage.Text = "";
            txtNote.Text = "";
            txtPageCount.Text = "";
            txtPrice.Text = "";
            txtReleaseYear.Text = "";
            txtStatus.Text = SystemConstants.NotBorrow;

            txtId.Focus();
            Add = true;
            txtId.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int pageCount = 0;
            if (!string.IsNullOrWhiteSpace(txtPageCount.Text))
            {
                pageCount = int.Parse(txtPageCount.Text.Trim());
            }

            if (Add)
            {
                BookBindingModel model = new BookBindingModel()
                {
                    Id = txtId.Text.Trim(),
                    Name = txtName.Text.Trim(),
                    Language = txtLanguage.Text.Trim(),

                    ReleaseYear = txtReleaseYear.Text.Trim(),
                    Note = txtNote.Text.Trim(),
                    
                    PageCount = pageCount,
                    Price = txtPageCount.Text.Trim(),
                    Status = SystemConstants.NotBorrow,

                    AuthorId = int.Parse(cmbAuthor.SelectedValue.ToString()),
                    CategoryId = int.Parse(cmbCategory.SelectedValue.ToString()),
                    SpecialCategoryId = int.Parse(cmbSpecialCategory.SelectedValue.ToString()),
                    PublisherId = int.Parse(cmbPublisher.SelectedValue.ToString()),
                    BookShelfId = int.Parse(cmbBookShelf.SelectedValue.ToString())


                };

                if (model.AuthorId == -1 || model.CategoryId == -1 || model.SpecialCategoryId == -1 || model.PublisherId == -1 || model.BookShelfId == -1)
                {
                    MessageBox.Show("Chưa chọn tác giả, hoặc danh mục, hoặc chủng loại ngành, hoặc giá sách hoặc nhà xuất bản.", "Thông báo");
                    return;
                }

                if (manager.Create(model))
                {
                    txtId.Enabled = false;
                    Add = false;
                    MessageBox.Show("Thêm mới thành công !", "Thông báo");
                }
                else
                    MessageBox.Show("Thêm mới không thành công!. Liên hệ với quản trị viên.", "Thông báo");
            }
            else
            {
                BookBindingModel model = new BookBindingModel()
                {
                    Id = txtId.Text.Trim(),
                    Name = txtName.Text.Trim(),
                    Language = txtLanguage.Text.Trim(),

                    ReleaseYear = txtReleaseYear.Text.Trim(),
                    Note = txtNote.Text.Trim(),
                    PageCount = pageCount,
                    Price = txtPageCount.Text.Trim()

                    //AuthorId = int.Parse(cmbAuthor.SelectedValue.ToString()),
                    //CategoryId = int.Parse(cmbCategory.SelectedValue.ToString()),
                    //SpecialCategoryId = int.Parse(cmbSpecialCategory.SelectedValue.ToString()),
                    //PublisherId = int.Parse(cmbPublisher.SelectedValue.ToString()),
                    //BookShelfId = int.Parse(cmbBookShelf.SelectedValue.ToString())
                };


                if (manager.Update(model))
                    MessageBox.Show("Sửa thành công !", "Thông báo");
                else
                    MessageBox.Show("Sửa không thành công!. Liên hệ với quản trị viên.", "Thông báo");
            }

            LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa !", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string id = txtId.Text.Trim();
                if (manager.Delete(id))
                    MessageBox.Show("Xóa thành công !", "Thông báo");
                else
                    MessageBox.Show("Xóa không thành công. Có danh mục đang sử dụng nên không xóa được", "Thông báo");

                LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
            }
        }

        private void btnNVGFirst_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);

        }

        private void btnNGVPrevious_Click(object sender, EventArgs e)
        {
            PageIndex -= 1;
            LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
        }
        private void btnNVGNext_Click(object sender, EventArgs e)
        {
            PageIndex += 1;
            LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
        }


        private void btnNVGLast_Click(object sender, EventArgs e)
        {
            PageIndex = TotalPage;
            LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
        }

        private void txtReleaseYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPageCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        

        BorrowerManager borrowerManager = new BorrowerManager();

        private void btnBorrowBook_Click(object sender, EventArgs e)
        {
            if (txtStatus.Text.Equals(SystemConstants.Borrowed))
            {
                MessageBox.Show("Sách đã được mượn rồi", "Thông báo");
                return;
            }
            if (MessageBox.Show("Bạn có muốn cho mượn không?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                BorrowerBindingModel model = new BorrowerBindingModel()
                {
                    BookId = txtId.Text,
                    BorrowDate = DateTime.Now,
                    CreatedBy = Main.currentUser.Name,
                    ReaderId = int.Parse(cmbReaderForBorrow.SelectedValue.ToString())
                };

                if (borrowerManager.Create(model)) // Tạo dữ liệu trong borrower
                {
                    // Cập nhật trạng thái sách đã mượn và đưa vào danh sách mượn
                    manager.UpdateStatusForBorrow(txtId.Text);

                    MessageBox.Show("Cho mượn thành công", "Thông báo");
                   
                    LoadData(Searching, CategoryIdSearch, SpecialCategoryIdSearch, BookShelfIdSearch, PublisherIdSearch, AuthorIdSearch, PageIndex, PageSize);
                }
                else
                {
                    MessageBox.Show("Mượn không thành công xin vui lòng kiểm tra lại mã người mượn", "Thông báo");
                }

            }


        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            // Load combobox category
            cmbCategory.DataSource = LoadCategory();
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "Id";

            CategoryIdSearch = int.Parse(cmbCategory.SelectedValue.ToString());

            // Load combobox special category
            cmbSpecialCategory.DataSource = LoadSpecialCategory();
            cmbSpecialCategory.DisplayMember = "Name";
            cmbSpecialCategory.ValueMember = "Id";

            SpecialCategoryIdSearch = int.Parse(cmbSpecialCategory.SelectedValue.ToString());

            // Load combobox bookshelf
            cmbBookShelf.DataSource = LoadBookShelf();
            cmbBookShelf.DisplayMember = "Name";
            cmbBookShelf.ValueMember = "Id";

            BookShelfIdSearch = int.Parse(cmbBookShelf.SelectedValue.ToString());

            // Load combobox publisher
            cmbPublisher.DataSource = LoadPublisher();
            cmbPublisher.DisplayMember = "Name";
            cmbPublisher.ValueMember = "Id";

            PublisherIdSearch = int.Parse(cmbPublisher.SelectedValue.ToString());

            // Load combobox author
            cmbAuthor.DataSource = LoadAuthor();
            cmbAuthor.DisplayMember = "Name";
            cmbAuthor.ValueMember = "Id";

            AuthorIdSearch = int.Parse(cmbAuthor.SelectedValue.ToString());

            // Load Unit manager// Load combobox for position and unit
            cmbUnit.DataSource = LoadUnit();
            cmbUnit.DisplayMember = "Name";
            cmbUnit.ValueMember = "Id";

        }

        private void cmbBorrower_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbAuthor.SelectedValue.ToString()))
            {
                try
                {
                    var readerId = int.Parse(cmbReaderForBorrow.SelectedValue.ToString());

                    if (readerId != -1 && !string.IsNullOrWhiteSpace(txtId.Text))
                        btnBorrowBook.Enabled = true;
                    else
                        btnBorrowBook.Enabled = false;

                }
                catch (Exception) // for first seasion
                {
                    btnBorrowBook.Enabled = false;
                }

            }
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbUnit.SelectedValue.ToString()))
            {
                try
                {
                    var unitIdSearch = int.Parse(cmbUnit.SelectedValue.ToString());

                    LoadDataReaderForComboboxByUnit(unitIdSearch);
                }
                catch (Exception) // for first seasion
                {
                    var unitIdSearch = -1;
                    LoadDataReaderForComboboxByUnit(unitIdSearch);
                }

            }
        }

        public void LoadDataReaderForComboboxByUnit(int unitId)
        {
            // Load reader for borrower
            lstReader = readerManager.GetListReaderForBorrowerByUnit(unitId);
            lstReader.Insert(0, new ReaderBindingforBorrower()
            {
                Id = -1,
                Name = "Chọn đọc giả"
            });

            cmbReaderForBorrow.DataSource = lstReader;
            cmbReaderForBorrow.DisplayMember = "Name";
            cmbReaderForBorrow.ValueMember = "Id";
            cmbReaderForBorrow.DropDownStyle = ComboBoxStyle.DropDownList;
        }


    }
}
