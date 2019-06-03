using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z119.Model.Bindingmodel;
using Z119.Model.EntityModel;

namespace Z119.Business.Manager
{
    public class ReportManager
    {
        public List<ReportModel> Report()
        {
            List<ReportModel> listRs = new List<ReportModel>();

            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Categories
                              select a;



                    foreach (var item in qry.ToList())
                    {
                        // Select cout sach where category
                        var s = from a in libraryDb.Books
                                where a.CategoryId == item.Id
                                select a;
                        

                        listRs.Add(new ReportModel() {
                            Category = item.Name,
                            BookCount = s.Count().ToString(),
                            PageCount = s.Sum(m => m.PageCount).ToString()
                        });
                    }


                }

                return listRs;
            }
            catch (Exception)
            {

                return listRs;
            }
                
        }
    }
}
