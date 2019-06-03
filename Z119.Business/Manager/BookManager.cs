using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z119.Common;
using Z119.Model.BaseModel;
using Z119.Model.Bindingmodel;
using Z119.Model.EntityModel;

namespace Z119.Business.Manager
{
    public class BookManager : BaseManager
    {
        /// <summary>
        ///     mapper to map between database entity and api level models
        /// </summary>
        private readonly IMapper _entityMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Book, BookBindingModel>();
            cfg.CreateMap<BookBindingModel, Book>();
        }).CreateMapper();

        //public ListResult<BookBindingModel> GetList(string id, string name, int? categoryId, int? specialCategoryId, int? bookShelfId,int? phblisherId,
        //    int? authorId, int? pageNumber = 0, int? pageSize = 0)
        //{
        //    try
        //    {
        //        using (var libraryDb = new LibraryManagementEntities())
        //        {
        //            var qry = from a in libraryDb.Books
        //                      select a;

        //            if (!string.IsNullOrWhiteSpace(id))
        //                qry = from a in qry
        //                      where a.Id.Equals(id)
        //                      select a;

        //            if (!string.IsNullOrWhiteSpace(name))
        //                qry = from a in qry
        //                      where a.Name.Contains(name)
        //                      select a;

        //            if (categoryId != null)
        //                qry = from a in qry
        //                      where a.CategoryId == categoryId
        //                      select a;

        //            if (specialCategoryId != null)
        //                qry = from a in qry
        //                      where a.SpecialCategoryId == specialCategoryId
        //                      select a;

        //            if (bookShelfId != null)
        //                qry = from a in qry
        //                      where a.BookShelfId == bookShelfId
        //                      select a;

        //            if (phblisherId != null)
        //                qry = from a in qry
        //                      where a.PublisherId == phblisherId
        //                      select a;

        //            if (authorId != null)
        //                qry = from a in qry
        //                      where a.AuthorId == authorId
        //                      select a;

        //            var listRs = ListResult<Book>.GetDataPage(qry.OrderBy(x=>x.Id), pageNumber, pageSize);
        //            if (listRs == null) throw new ArgumentNullException();

        //            if (listRs.ListOfObjects == null) return new ListResult<BookBindingModel>();

        //            var listResult = new ListResult<BookBindingModel>();
        //            listResult.ListOfObjects = new List<BookBindingModel>();
        //            listResult.TotalRecord = listRs.TotalRecord;

        //            foreach (var item in listRs.ListOfObjects)
        //            {
        //                var book = _entityMapper.Map<Book, BookBindingModel>(item);
        //                listResult.ListOfObjects.Add(book);
        //            }
        //            return listResult;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public BookBindingModel GetBook(string bookId)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Books
                              where a.Id.Equals(bookId)
                              select a;
                    var bookInfo = _entityMapper.Map<Book, BookBindingModel>(qry.FirstOrDefault());
                    return bookInfo;

                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ListResult<BookBindingViewModel> GetList(string searching, int? categoryId, int? specialCategoryId, int? bookShelfId, int? phblisherId,
        int? authorId, int? pageNumber = 0, int? pageSize = 0)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Books
                              join b in libraryDb.Categories on a.CategoryId equals b.Id
                              join c in libraryDb.SpecialCategories on a.SpecialCategoryId equals c.Id
                              join d in libraryDb.BookShelves on a.BookShelfId  equals d.Id
                              join e in libraryDb.Publishers on a.PublisherId equals e.Id
                              join f in libraryDb.Authors on a.AuthorId equals f.Id
                              select new {
                                  Id = a.Id,
                                  Name = a.Name,
                                  Language = a.Language,
                                  ReleaseYear = a.ReleaseYear,
                                  PageCount = a.PageCount,
                                  Price = a.Price,
                                  Note = a.Note,
                                  Status = a.Status,
                                  AuthorId = a.AuthorId,
                                  Author = f.Name,
                                  PublisherId = a.PublisherId,
                                  Publisher = e.Name,
                                  CategoryId = a.CategoryId,
                                  Category = b.Name,
                                  SpecialCategoryId = a.SpecialCategoryId,
                                  SpecialCategory = c.Name,
                                  BookShelfId = a.BookShelfId,
                                  BookShelf = d.Name

                              };


                    if (!string.IsNullOrWhiteSpace(searching))
                        qry = from a in qry
                              where a.Name.Contains(searching) || a.Id.Contains(searching) || a.Status.Contains(searching)
                              select a;

                    if (categoryId != null && categoryId >= 1)
                        qry = from a in qry
                              where a.CategoryId == categoryId
                              select a;

                    if (specialCategoryId != null && specialCategoryId >= 1)
                        qry = from a in qry
                              where a.SpecialCategoryId == specialCategoryId
                              select a;

                    if (bookShelfId != null && bookShelfId >= 1)
                        qry = from a in qry
                              where a.BookShelfId == bookShelfId
                              select a;

                    if (phblisherId != null && phblisherId >= 1)
                        qry = from a in qry
                              where a.PublisherId == phblisherId
                              select a;

                    if (authorId != null && authorId >= 1)
                        qry = from a in qry
                              where a.AuthorId == authorId
                              select a;

                    List<BookBindingViewModel> LR = new List<BookBindingViewModel>();
                    foreach (var item in qry.ToList())
                    {

                        LR.Add(new BookBindingViewModel()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Author = item.Author,
                            AuthorId = item.AuthorId,
                            BookShelf = item.BookShelf,
                            BookShelfId = item.BookShelfId,
                            Category = item.Category,
                            CategoryId = item.CategoryId,
                            Language = item.Language,
                            Note = item.Note,
                            PageCount = item.PageCount,
                            Price = item.Price,
                            Publisher = item.Publisher,
                            PublisherId = item.PublisherId,
                            ReleaseYear = item.ReleaseYear,
                            SpecialCategory = item.SpecialCategory,
                            SpecialCategoryId = item.SpecialCategoryId,
                            Status = item.Status
                        });
                    }




                    var listRs = ListResult<BookBindingViewModel>.GetDataPage(LR.OrderBy(x => x.Name), pageNumber, pageSize);
                    if (listRs == null) throw new ArgumentNullException();

                    if (listRs.ListOfObjects == null) return new ListResult<BookBindingViewModel>();

                    return listRs;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Create(BookBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // check duplicated
                    var currentBook = libraryDb.Books.FirstOrDefault(s => s.Id.Equals(model.Id));
                    if (currentBook != null) throw new ArgumentNullException();
                    // Insert db
                    var bookInfo = _entityMapper.Map<BookBindingModel, Book>(model);
                    libraryDb.Books.Add(bookInfo);
                    libraryDb.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(BookBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var book = libraryDb.Books.FirstOrDefault(s => s.Id.Equals(model.Id));
                    if (book == null) throw new ArgumentNullException("No exist");
                    book.Name = model.Name;
                    book.Language = model.Language;
                    book.ReleaseYear = model.ReleaseYear;
                    book.PageCount = model.PageCount;
                    //book.AuthorId = model.AuthorId;
                    //book.PublisherId = model.PulisherId;
                    book.Note = model.Note;
                    book.Price = model.Price;
                    //book.CategoryId = model.CategoryId;
                    //book.SpecialCategoryId = model.SpecialCategoryId;
                    //book.BookShelfId = model.BookShelfId;

                    libraryDb.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateStatusForPayment(BookBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var book = libraryDb.Books.FirstOrDefault(s => s.Id.Equals(model.Id));
                    if (book == null) throw new ArgumentNullException("No exist");

                    book.Status = SystemConstants.NotBorrow;

                    libraryDb.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateStatusForBorrow(string bookId)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var book = libraryDb.Books.FirstOrDefault(s => s.Id.Equals(bookId));
                    if (book == null) throw new ArgumentNullException("No exist");

                    book.Status = SystemConstants.Borrowed;

                    libraryDb.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var book = libraryDb.Books.FirstOrDefault(s => s.Id.Equals(id));
                    if (book == null) throw new ArgumentNullException("No exist");

                    libraryDb.Books.Remove(book);

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

