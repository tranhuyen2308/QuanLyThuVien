using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z119.Model.BaseModel;
using Z119.Model.Bindingmodel;
using Z119.Model.EntityModel;

namespace Z119.Business.Manager
{
    public class SpecializedManager : BaseManager
    {

        /// <summary>
        ///     mapper to map between database entity and api level models
        /// </summary>
        private readonly IMapper _entityMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Specialized, SpecializedBindingModel>();
            cfg.CreateMap<SpecializedBindingModel, Specialized > ();
        }).CreateMapper();

        public ListResult<SpecializedBindingModel> GetList(string name = "", int? pageNumber = 0, int? pageSize = 0)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Specializeds
                              select a;

                    if (!string.IsNullOrWhiteSpace(name))
                        qry = from a in qry
                              where a.Name.Contains(name)
                              select a;

                    var listRs = ListResult<Specialized>.GetDataPage(qry.OrderBy(x=>x.Name), pageNumber, pageSize);
                    if (listRs == null) throw new ArgumentNullException();

                    if (listRs.ListOfObjects == null) return new ListResult<SpecializedBindingModel>();

                    var listResult = new ListResult<SpecializedBindingModel>();
                    listResult.ListOfObjects = new List<SpecializedBindingModel>();
                    listResult.TotalRecord = listRs.TotalRecord;

                    foreach (var item in listRs.ListOfObjects)
                    {
                        var specialized = _entityMapper.Map<Specialized, SpecializedBindingModel>(item);
                        listResult.ListOfObjects.Add(specialized);
                    }
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Create(SpecializedBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // check duplicated
                    var currentSpecialized = libraryDb.Specializeds.FirstOrDefault(s => s.Name == model.Name);
                    if (currentSpecialized != null) throw new ArgumentNullException();
                    // Insert db
                    var specializeInfo = _entityMapper.Map<SpecializedBindingModel, Specialized>(model);
                    libraryDb.Specializeds.Add(specializeInfo);
                    libraryDb.SaveChanges();
                    
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(SpecializedBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var specialzed = libraryDb.Specializeds.FirstOrDefault(s => s.Id == model.Id);
                    if (specialzed == null) throw new ArgumentNullException("No exist");
                    specialzed.Name = model.Name;
                    libraryDb.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // When you delete specialized you have to delete (category, specialcategory, book, borrower)
        public bool Delete(int id)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var specialzed = libraryDb.Specializeds.FirstOrDefault(s => s.Id == id);
                    if (specialzed == null) throw new ArgumentNullException("No exist");
                    //// Check if have child
                    //// 1. Get category
                    //var listCategory = (from a in libraryDb.Categories
                    //                   where a.Id == model.Id
                    //                   select a).ToList();

                    //// 2. Get special category
                    //var listSpecialCategory = (from a in libraryDb.SpecialCategories
                    //                           where a.Id == model.Id
                    //                           select a).ToList();

                    //// 3. Get book by category
                    //List<Book> lstBook = new List<Book>();
                    //foreach (var item in listCategory)
                    //{
                    //    var book = (from a in libraryDb.Books
                    //                where a.CategoryId == item.Id
                    //                select a).FirstOrDefault();

                    //    if (book != null)
                    //        lstBook.Add(book);
                    //}
                    //foreach (var item in listSpecialCategory)
                    //{
                    //    var book = (from a in libraryDb.Books
                    //                where a.SpecialCategoryId == item.Id
                    //                select a).FirstOrDefault();

                    //    if (book != null)
                    //        lstBook.Add(book);
                    //}
                    //// 4. Get borrower
                    //List<Borrower> lstBorrower = new List<Borrower>();
                    //foreach (var item in lstBook)
                    //{
                    //    var borrower = (from a in libraryDb.Borrowers
                    //                where a.BookId == item.Id
                    //                select a).FirstOrDefault();

                    //    if (borrower != null)
                    //        lstBorrower.Add(borrower);
                    //}

                    //// Delete Borrowers => Delete books => Delete Category => Delete SpecialCategory => Delete Specialized

                    //libraryDb.Borrowers.RemoveRange(lstBorrower);
                    //libraryDb.Books.RemoveRange(lstBook);
                    //libraryDb.Categories.RemoveRange(listCategory);
                    //libraryDb.SpecialCategories.RemoveRange(listSpecialCategory);

                    libraryDb.Specializeds.Remove(specialzed);

                    libraryDb.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
