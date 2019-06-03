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

namespace Z119.Shell.UcControl
{
    public partial class ucReportAll : UserControl
    {
        ReportManager manager = new ReportManager();
        public ucReportAll()
        {
            InitializeComponent();

            dgvData.DataSource = manager.Report();
        }
    }
}
