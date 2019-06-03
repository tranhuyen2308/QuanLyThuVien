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
    public class ReaderManager : BaseManager
    {
        /// <summary>
        ///     mapper to map between database entity and api level models
        /// </summary>
        private readonly IMapper _entityMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Reader, ReaderBindingModel>();
            cfg.CreateMap<ReaderBindingModel, Reader>();
        }).CreateMapper();

        //public ListResult<ReaderBindingModel> GetList(string name = "", string gender = "", string phoneNumber = "", int? positionId = null, int? unitId = null, int? pageNumber = 0, int? pageSize = 0)
        //{
        //    try
        //    {
        //        using (var libraryDb = new LibraryManagementEntities())
        //        {
        //            var qry = from a in libraryDb.Readers
        //                      select a;

        //            if (!string.IsNullOrWhiteSpace(name))
        //                qry = from a in qry
        //                      where a.Name.Contains(name)
        //                      select a;

        //            if (!string.IsNullOrWhiteSpace(gender))
        //                qry = from a in qry
        //                      where a.Gender.Contains(gender)
        //                      select a;

        //            if (phoneNumber != null)
        //                qry = from a in qry
        //                      where a.PhoneNumber.Contains(phoneNumber)
        //                      select a;

        //            if (positionId != null && positionId >= 1)
        //                qry = from a in qry
        //                      where a.PositionId == positionId
        //                      select a;

        //            if (unitId != null && unitId >= 1)
        //                qry = from a in qry
        //                      where a.UnitId == unitId
        //                      select a;

        //            var listRs = ListResult<Reader>.GetDataPage(qry.OrderBy(x=>x.Name), pageNumber, pageSize);
        //            if (listRs == null) throw new ArgumentNullException();

        //            if (listRs.ListOfObjects == null) return new ListResult<ReaderBindingModel>();

        //            var listResult = new ListResult<ReaderBindingModel>();
        //            listResult.ListOfObjects = new List<ReaderBindingModel>();
        //            listResult.TotalRecord = listRs.TotalRecord;

        //            foreach (var item in listRs.ListOfObjects)
        //            {
        //                var reader = _entityMapper.Map<Reader, ReaderBindingModel>(item);
        //                listResult.ListOfObjects.Add(reader);
        //            }
        //            return listResult;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public List<ReaderBindingforBorrower> GetListReaderForBorrowerByUnit(int? unitId = null)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Readers
                              select a;

                    if (unitId != null && unitId >= 1)
                        qry = from a in libraryDb.Readers
                              where a.UnitId == unitId
                              select a;

                    List<ReaderBindingforBorrower> LR = new List<ReaderBindingforBorrower>();
                    foreach (var item in qry.ToList())
                    {
                        LR.Add(new ReaderBindingforBorrower()
                        {
                            Id = item.Id,
                            Name = item.Id + " - " + item.Name
                        });
                    }

                    return LR;
                }
            }
            catch (Exception)
            {
                return new List<ReaderBindingforBorrower>();
            }
        }

        public ListResult<ReaderBindingViewModel> GetList(string name = "", string gender = "", string phoneNumber = "", int? positionId = null, int? unitId = null, int? pageNumber = 0, int? pageSize = 0)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Readers
                              join b in libraryDb.Positions on a.PositionId equals b.Id
                              join c in libraryDb.Units on a.UnitId equals c.Id
                              select new {
                                  Id = a.Id,
                                  Name = a.Name,
                                  Gender = a.Gender,
                                  DayOfBirth = a.DayOfBirth,
                                  Address = a.Address,
                                  PhoneNumber = a.PhoneNumber,
                                  Email = a.Email,
                                  Position = b.Name,
                                  Unit = c.Name,
                                  Note = a.Note,
                                  PositionId = a.PositionId,
                                  UnitId = a.UnitId
                              } ;

                    if (!string.IsNullOrWhiteSpace(name))
                        qry = from a in qry
                              where a.Name.Contains(name)
                              select a;

                    if (!string.IsNullOrWhiteSpace(gender))
                        qry = from a in qry
                              where a.Gender.Contains(gender)
                              select a;

                    if (!string.IsNullOrWhiteSpace(phoneNumber))
                        qry = from a in qry
                              where a.PhoneNumber.Contains(phoneNumber)
                              select a;

                    if (positionId != null && positionId >= 1)
                        qry = from a in qry
                              where a.PositionId == positionId
                              select a;

                    if (unitId != null && unitId >= 1)
                        qry = from a in qry
                              where a.UnitId == unitId
                              select a;


                    List<ReaderBindingViewModel> LR = new List<ReaderBindingViewModel>();
                    foreach (var item in qry.ToList())
                    {

                        LR.Add(new ReaderBindingViewModel() {
                            Id = item.Id,
                            Name = item.Name,
                            Address = item.Address,
                            DayOfBirth = item.DayOfBirth,
                            Email = item.Email,
                            Gender = item.Gender,
                            Note = item.Note,
                            PhoneNumber = item.PhoneNumber,
                            Position = item.Position,
                            PositionId = item.PositionId,
                            Unit = item.Unit,
                            UnitId = item.UnitId
                        });
                    }

                    


                    var listRs = ListResult<ReaderBindingViewModel>.GetDataPage(LR.OrderBy(x => x.Name), pageNumber, pageSize);
                    if (listRs == null) throw new ArgumentNullException();

                    if (listRs.ListOfObjects == null) return new ListResult<ReaderBindingViewModel>();

                    return listRs;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetName(int? readerId)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Readers
                              where a.Id == readerId
                              select a;
                    return qry.FirstOrDefault().Name;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Create(ReaderBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // check duplicated
                    var readerAuthor = libraryDb.Readers.FirstOrDefault(s => s.Id == model.Id);
                    if (readerAuthor != null) throw new ArgumentNullException();
                    // Insert db
                    var readerInfo = _entityMapper.Map<ReaderBindingModel, Reader>(model);
                    libraryDb.Readers.Add(readerInfo);
                    libraryDb.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(ReaderBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var reader = libraryDb.Readers.FirstOrDefault(s => s.Id == model.Id);
                    if (reader == null) throw new ArgumentNullException("No exist");
                    reader.Name = model.Name;
                    reader.Gender = model.Gender;
                    reader.Address = model.Address;
                    reader.PhoneNumber = model.PhoneNumber;
                    reader.Email = model.Email;
                    reader.DayOfBirth = model.DayOfBirth;
                    //reader.PositionId = model.PositionId;
                    //reader.UnitId = model.UnitId;
                    reader.Note = model.Note;

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
                    var reader = libraryDb.Readers.FirstOrDefault(s => s.Id == id);
                    if (reader == null) throw new ArgumentNullException("No exist");

                    libraryDb.Readers.Remove(reader);

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
