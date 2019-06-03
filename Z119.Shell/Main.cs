using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevComponents.DotNetBar;
using Z119.Shell.UcControl;
using Z119.Common;

namespace Z119.Shell
{

    

    public partial class Main : Office2007RibbonForm
	{

        public static CurrentPersion currentUser = new CurrentPersion();

        public Main()
		{
			InitializeComponent();

            rbbLibrary.Enabled = false;
            rbbBorrowPay.Enabled = false;
            rbbLibrary.Enabled = false;
            rbbpersonel.Enabled = false;
            rbbReport.Enabled = false;
            btnLogout.Enabled = false;
            btnProfileInfor.Enabled = false;

        }
        


        #region TAB


        // ---------------------- AddnewTab ----------------------
        /// <summary>
        /// add tab
        /// </summary>
        /// <param name="tabName"></param>
        /// <param name="ucContent"></param>
        private void AddNewTab(string tabName, UserControl ucContent)
		{
			try
			{
				// Kiểm tra xem tab đã có hay chưa
				foreach (TabItem tab in this.tabContent.Tabs)
				{
					if (tab.Text == tabName)
					{
						this.tabContent.SelectedTab = tab;
						return;
					}
				}

				// Nếu chưa có thì ...

				TabControlPanel newTabPanel = new TabControlPanel();
				TabItem newTabPage = new TabItem(this.components);

				newTabPanel.Dock = System.Windows.Forms.DockStyle.Fill;
				newTabPanel.Location = new System.Drawing.Point(0, 26);
				newTabPanel.Name = "tabControlPanel1";
				newTabPanel.Padding = new System.Windows.Forms.Padding(1);
				newTabPanel.Size = new System.Drawing.Size(993, 404);
				newTabPanel.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
				newTabPanel.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
				newTabPanel.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
				newTabPanel.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
				newTabPanel.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
				| DevComponents.DotNetBar.eBorderSide.Bottom)));
				newTabPanel.Style.GradientAngle = 90;
				newTabPanel.TabIndex = 1;

				// Gắn tabitem cho panel
				newTabPanel.TabItem = newTabPage;


				/*
				 * New tabPage
				 */
				Random ran = new Random();
				newTabPage.Name = tabName + ran.Next(100000) + ran.Next(1248);
				newTabPage.AttachedControl = newTabPanel;
				newTabPage.Text = tabName;

				// Control
				ucContent.Dock = DockStyle.Fill;
				newTabPanel.Controls.Add(ucContent);

                newTabPage.Click += NewTabPage_Click;

				/*
				 * Add tabPage to TabControl
				 */
				tabContent.Controls.Add(newTabPanel);
				tabContent.Tabs.Add(newTabPage);
				tabContent.SelectedTab = newTabPage;
			}
			catch { };
      }

        // Reload Page
        private void NewTabPage_Click(object sender, EventArgs e)
        {
            //MessageBox.Show((sender as TabItem).Text);
        }



        #region Xóa tab
        private void tabContent_TabItemClose(object sender, TabStripActionEventArgs e)
        {
            this.tabContent.Tabs.Remove(this.tabContent.SelectedTab);
            tabContent.Refresh();
        }

        // Close
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabContent.Tabs.Count == 0)
                return;
            this.tabContent.Tabs.Remove(this.tabContent.SelectedTab);

            tabContent.Refresh();
        }
        // Close All
        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Nếu không có tab nào
            int nTab = tabContent.Tabs.Count;
            if (nTab == 0)
                return;
            // Xóa ngược về
            for (int i = nTab - 1; i >= 0; i--)
            {
                tabContent.Tabs.RemoveAt(i);
            }
            tabContent.Refresh();
        }
        // Close All But This
        private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Nếu không có tab nào
            int nTab = tabContent.Tabs.Count;
            if (nTab == 0)
                return;
            // kiểm tra tab được chọn và  xóa các tab còn lại
            for (int i = nTab - 1; i >= 0; i--)
            {
                if (tabContent.Tabs[i] != tabContent.SelectedTab)
                {
                    tabContent.Tabs.RemoveAt(i);
                }
            }
            tabContent.Refresh();
        }

        #endregion

        #endregion Eng TAB


        private void btniCategory_Click(object sender, EventArgs e)
        {
            ucCategory ucItem = new ucCategory();
            AddNewTab("Danh Mục", ucItem);
        }

        private void btniSpecialized_Click(object sender, EventArgs e)
        {
            usSpecialized ucItem = new usSpecialized();
            AddNewTab("Ngành", ucItem);
        }

        private void btniSpecialCategory_Click(object sender, EventArgs e)
        {
            ucSpecialCategory ucItem = new ucSpecialCategory();
            AddNewTab("Chủng Loại Danh Mục", ucItem);
        }

        private void btniBook_Click(object sender, EventArgs e)
        {
            ucBook ucItem = new ucBook();
            AddNewTab("Sách", ucItem);
        }

        private void btniUser_Click(object sender, EventArgs e)
        {
            ucUser ucItem = new ucUser();
            AddNewTab("Nhân Viên", ucItem);
        }

        private void btniReader_Click(object sender, EventArgs e)
        {
            ucReader ucItem = new ucReader();
            AddNewTab("Đọc Giả", ucItem);
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            ucBorrowBook ucItem = new ucBorrowBook();
            AddNewTab("Mượn Sách", ucItem);
        }

        private void btniPay_Click(object sender, EventArgs e)
        {
            
        }

        private void btnReportAll_Click(object sender, EventArgs e)
        {
            ucReportAll ucItem = new ucReportAll();
            AddNewTab("Báo Cáo Tổng Hợp", ucItem);
        }


        private void btnBookShelf_Click(object sender, EventArgs e)
        {
            ucBookShelf ucItem = new ucBookShelf();
            AddNewTab("Kệ Sách", ucItem);
        }

        private void btnAuthor_Click(object sender, EventArgs e)
        {
            ucAuthor ucItem = new ucAuthor();
            AddNewTab("Tác Giả", ucItem);
        }

        private void btnPublisher_Click(object sender, EventArgs e)
        {
            ucPublisher ucItem = new ucPublisher();
            AddNewTab("Nhà Xuất Bản", ucItem);
        }

        private void btnUnit_Click(object sender, EventArgs e)
        {
            ucUnit ucItem = new ucUnit();
            AddNewTab("Đơn Vị", ucItem);
        }

        private void btnPosition_Click(object sender, EventArgs e)
        {
            ucPosition ucItem = new ucPosition();
            AddNewTab("Chức Vụ", ucItem);
        }

        private void btnRole_Click(object sender, EventArgs e)
        {
            ucRole ucItem = new ucRole();
            AddNewTab("Quyền nhân viên", ucItem);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.eventLogin += Login_eventLogin;
            login.ShowDialog();
        }

        private void Login_eventLogin(object sender, LoginSuccessEventArgs e)
        {
            currentUser = e.user;

            rbbLibrary.Enabled = true;
            rbbBorrowPay.Enabled = true;
            rbbLibrary.Enabled = true;
            rbbpersonel.Enabled = true;
            rbbReport.Enabled = true;
            btnLogout.Enabled = true;
            btnProfileInfor.Enabled = true;

            btnLogin.Enabled = false;

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            closeAllToolStripMenuItem_Click(null, null);

            rbbLibrary.Enabled = false;
            rbbBorrowPay.Enabled = false;
            rbbLibrary.Enabled = false;
            rbbpersonel.Enabled = false;
            rbbReport.Enabled = false;
            btnLogout.Enabled = false;
            btnProfileInfor.Enabled = false;

            currentUser = new CurrentPersion();

            btnLogin.Enabled = true;
        }

        private void btnProfileInfor_Click(object sender, EventArgs e)
        {
            ucProfileInfos ucItem = new ucProfileInfos();
            AddNewTab("Thông tin cá nhân", ucItem);

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
