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
    public class BookShelfManager : BaseManager 
    {
        /// <summary>
        ///     mapper to map between database entity and api level models
        /// </summary>
        private readonly IMapper _entityMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<BookShelf, BookShelfBindingModel>();
            cfg.CreateMap<BookShelfBindingModel, BookShelf>();
        }).CreateMapper();

        public ListResult<BookShelfBindingModel> GetList(string name = "", int? pageNumber = 0, int? pageSize = 0)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.BookShelves
                              select a;

                    if (!string.IsNullOrWhiteSpace(name))
                        qry = from a in qry
                              where a.Name.Contains(name)
                              select a;

                    var listRs = ListResult<BookShelf>.GetDataPage(qry.OrderBy(x=>x.Name), pageNumber, pageSize);
                    if (listRs == null) throw new ArgumentNullException();

                    if (listRs.ListOfObjects == null) return new ListResult<BookShelfBindingModel>();

                    var listResult = new ListResult<BookShelfBindingModel>();
                    listResult.ListOfObjects = new List<BookShelfBindingModel>();
                    listResult.TotalRecord = listRs.TotalRecord;

                    foreach (var item in listRs.ListOfObjects)
                    {
                        var bookshelf = _entityMapper.Map<BookShelf, BookShelfBindingModel>(item);
                        listResult.ListOfObjects.Add(bookshelf);
                    }
                    return listResult;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Create(BookShelfBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // check duplicated
                    var currentBookshelf = libraryDb.BookShelves.FirstOrDefault(s => s.Id == model.Id);
                    if (currentBookshelf != null) throw new ArgumentNullException();
                    // Insert db
                    var bookshelfInfo = _entityMapper.Map<BookShelfBindingModel, BookShelf>(model);
                    libraryDb.BookShelves.Add(bookshelfInfo);
                    libraryDb.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(BookShelfBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var bookshelf = libraryDb.BookShelves.FirstOrDefault(s => s.Id == model.Id);
                    if (bookshelf == null) throw new ArgumentNullException("No exist");
                    bookshelf.Name = model.Name;
                    libraryDb.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var bookshelf = libraryDb.BookShelves.FirstOrDefault(s => s.Id == id);
                    if (bookshelf == null) throw new ArgumentNullException("No exist");

                    libraryDb.BookShelves.Remove(bookshelf);

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
