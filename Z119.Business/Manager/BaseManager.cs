using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z119.Model.EntityModel;

namespace Z119.Business.Manager
{
    public class BaseManager : IDisposable
    {
        protected LibraryManagementEntities _dbLibraryManagement;

        public BaseManager()
        {
            this._dbLibraryManagement = new LibraryManagementEntities();
        }

        public BaseManager(LibraryManagementEntities dbLibraryManagement)
        {
            this._dbLibraryManagement = dbLibraryManagement;
        }

        public virtual void Dispose()
        {
            this._dbLibraryManagement.Dispose();
        }
    }
}
