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
    public class BorrowerManager : BaseManager
    {
        /// <summary>
        ///     mapper to map between database entity and api level models
        /// </summary>
        private readonly IMapper _entityMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Borrower, BorrowerBindingModel>();
            cfg.CreateMap<BorrowerBindingModel, Borrower>();
        }).CreateMapper();

        //public ListResult<BorrowerBindingModel> GetList(int? readerId, string bookId, DateTime? borrowDate, DateTime? payDate, string note,
        //      int? pageNumber = 0, int? pageSize = 0)
        //{
        //    try
        //    {
        //        using (var libraryDb = new LibraryManagementEntities())
        //        {
        //            var qry = from a in libraryDb.Borrowers
        //                      select a;

        //            if (!string.IsNullOrWhiteSpace(note))
        //                qry = from a in qry
        //                      where a.Note.Contains(note)
        //                      select a;

        //            if (readerId != null && readerId >= 1)
        //                qry = from a in qry
        //                      where a.ReaderId == readerId
        //                      select a;

        //            if (!string.IsNullOrWhiteSpace(bookId))
        //                qry = from a in qry
        //                      where a.BookId.Contains(bookId)
        //                      select a;

        //            if (borrowDate != null)
        //                qry = from a in qry
        //                      where a.BorrowDate >= borrowDate
        //                      select a;

        //            if (payDate != null)
        //                qry = from a in qry
        //                      where a.PayDate <= payDate
        //                      select a;

        //            var listRs = ListResult<Borrower>.GetDataPage(qry.OrderByDescending(x=>x.PayDate), pageNumber, pageSize);
        //            if (listRs == null) throw new ArgumentNullException();

        //            if (listRs.ListOfObjects == null) return new ListResult<BorrowerBindingModel>();

        //            var listResult = new ListResult<BorrowerBindingModel>();
        //            listResult.ListOfObjects = new List<BorrowerBindingModel>();
        //            listResult.TotalRecord = listRs.TotalRecord;

        //            foreach (var item in listRs.ListOfObjects)
        //            {
        //                var borrower = _entityMapper.Map<Borrower, BorrowerBindingModel>(item);
        //                listResult.ListOfObjects.Add(borrower);
        //            }
        //            return listResult;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        


        public ListResult<BorrowerBindingViewModel> GetList(string searching = "", 
            int? positionId = null, int? unitId = null, int? pageNumber = 0, int? pageSize = 0)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Borrowers
                              join b in libraryDb.Books on a.BookId equals b.Id
                              join c in libraryDb.Readers on a.ReaderId equals c.Id
                              join d in libraryDb.Positions on c.PositionId equals d.Id
                              join g in libraryDb.Units on c.UnitId equals g.Id
                              where b.Status.Equals(SystemConstants.Borrowed)
                              select new {
                                  ReaderId = c.Id,
                                  ReaderName = c.Name,
                                  BookId = b.Id,
                                  BookName = b.Name,
                                  BorrowDate = a.BorrowDate,
                                  Note = a.Note,
                                  CreatedBy = a.CreatedBy,
                                  Unit = g.Name,
                                  UnitId = g.Id,
                                  Position = d.Name,
                                  PositionId = d.Id

                              };

                    if (!string.IsNullOrWhiteSpace(searching))
                        qry = from a in qry
                              where a.ReaderName.Contains(searching) || a.BookId.Contains(searching) 
                              || a.BookName.Contains(searching) || a.Note.Contains(searching)
                              select a;


                    
                    if (positionId != null && positionId >= 1)
                        qry = from a in qry
                              where a.PositionId == positionId
                              select a;

                    if (unitId != null && unitId >= 1)
                        qry = from a in qry
                              where a.UnitId == unitId
                              select a;

                    


                    List<BorrowerBindingViewModel> LR = new List<BorrowerBindingViewModel>();
                    foreach (var item in qry.ToList())
                    {

                        LR.Add(new BorrowerBindingViewModel()
                        {
                            BookId = item.BookId,
                            BookName = item.BookName,
                            ReaderId = item.ReaderId,
                            ReaderName = item.ReaderName,
                            BorrowDate = item.BorrowDate,
                            PositionId = item.PositionId,
                            Position = item.Position,
                            UnitId = item.UnitId,
                            Unit = item.Unit,
                            Note = item.Note,
                            CreatedBy = item.CreatedBy
                        });
                    }

                    var listRs = ListResult<BorrowerBindingViewModel>.GetDataPage(LR.OrderBy(x => x.ReaderName), pageNumber, pageSize);
                    if (listRs == null) throw new ArgumentNullException();

                    if (listRs.ListOfObjects == null) return new ListResult<BorrowerBindingViewModel>();

                    return listRs;
                }
            }
            catch (Exception ex)
            {
                return new ListResult<BorrowerBindingViewModel>();
            }
        }

        public bool Create(BorrowerBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // check duplicated
                    var borrwerAuthor = libraryDb.Borrowers.FirstOrDefault(s => s.Id == model.Id);
                    if (borrwerAuthor != null) throw new ArgumentNullException();
                    // Insert db
                    var borrwerInfo = _entityMapper.Map<BorrowerBindingModel, Borrower>(model);
                    libraryDb.Borrowers.Add(borrwerInfo);
                    libraryDb.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetBorrowerId(string bookId)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Borrowers
                              where a.BookId.Equals(bookId)
                              select a;
                    return qry.FirstOrDefault().Id;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Update(BorrowerBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var borrower = libraryDb.Borrowers.FirstOrDefault(s => s.Id == model.Id);
                    if (borrower == null) throw new ArgumentNullException("No exist");

                    borrower.ReaderId = model.ReaderId;
                    borrower.BookId = model.BookId;
                    borrower.BorrowDate = model.BorrowDate;
                    borrower.PayDate = model.PayDate;
                    borrower.Note = model.Note;

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
                    var borrower = libraryDb.Borrowers.FirstOrDefault(s => s.Id == id);
                    if (borrower == null) throw new ArgumentNullException("No exist");

                    libraryDb.Borrowers.Remove(borrower);

                    libraryDb.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public BookManager bookManager = new BookManager();
        

        public bool PaymentBook(string bookId)
        {
            try
            {
                // update status for book
                BookBindingModel book = bookManager.GetBook(bookId);
                bookManager.UpdateStatusForPayment(book);

                // delete
                int borrowerId = GetBorrowerId(book.Id);
                Delete(borrowerId);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}

