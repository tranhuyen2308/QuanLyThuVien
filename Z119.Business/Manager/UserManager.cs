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
    public class UserManager : BaseManager
    {
        /// <summary>
        ///     mapper to map between database entity and api level models
        /// </summary>
        private readonly IMapper _entityMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserBindingModel>();
            cfg.CreateMap<UserBindingModel, User>();
        }).CreateMapper();

        //public ListResult<UserBindingModel> GetList(string name = "", int? positionId = null, int? unitId = null, int? roleId = null, int? pageNumber = 0, int? pageSize = 0)
        //{
        //    try
        //    {
        //        using (var libraryDb = new LibraryManagementEntities())
        //        {
        //            var qry = from a in libraryDb.Users
        //                      select a;

        //            if (!string.IsNullOrWhiteSpace(name))
        //                qry = from a in qry
        //                      where a.Name.Contains(name)
        //                      select a;

        //            if (positionId != null)
        //                qry = from a in qry
        //                      where a.PositionId == positionId
        //                      select a;

        //            if (unitId != null)
        //                qry = from a in qry
        //                      where a.UnitId == unitId
        //                      select a;

        //            if (roleId != null)
        //                qry = from a in qry
        //                      where a.RoleId == roleId
        //                      select a;

        //            var listRs = ListResult<User>.GetDataPage(qry.OrderBy(x=>x.Name), pageNumber, pageSize);
        //            if (listRs == null) throw new ArgumentNullException();

        //            if (listRs.ListOfObjects == null) return new ListResult<UserBindingModel>();

        //            var listResult = new ListResult<UserBindingModel>();
        //            listResult.ListOfObjects = new List<UserBindingModel>();
        //            listResult.TotalRecord = listRs.TotalRecord;

        //            foreach (var item in listRs.ListOfObjects)
        //            {
        //                var user = _entityMapper.Map<User, UserBindingModel>(item);
        //                listResult.ListOfObjects.Add(user);
        //            }
        //            return listResult;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public UserBindingViewModel getUser(string account, string password)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Users
                              join b in libraryDb.Positions on a.PositionId equals b.Id
                              join c in libraryDb.Units on a.UnitId equals c.Id
                              join d in libraryDb.Roles on a.RoleId equals d.Id
                              where a.Account == account && a.Password == password
                              select new
                              {
                                  Id = a.Id,
                                  Account = a.Account,
                                  Password = a.Password,
                                  Name = a.Name,
                                  Gender = a.Gender,
                                  DayOfBirth = a.DayOfBirth,
                                  Address = a.Address,
                                  PhoneNumber = a.PhoneNumber,
                                  PositionId = a.PositionId,
                                  Position = b.Name,
                                  UnitId = a.UnitId,
                                  Unit = c.Name,
                                  RoleId = a.RoleId,
                                  Role = d.Name,
                                  Email = a.Email
                              };

                    List<UserBindingViewModel> LR = new List<UserBindingViewModel>();
                    foreach (var item in qry.ToList())
                    {

                        LR.Add(new UserBindingViewModel()
                        {
                            Id = item.Id,
                            Account = item.Account,
                            Password = item.Password,
                            Name = item.Name,
                            Gender = item.Gender,
                            DayOfBirth = item.DayOfBirth,
                            Address = item.Address,
                            PhoneNumber = item.PhoneNumber,
                            PositionId = item.PositionId,
                            Position = item.Position,
                            UnitId = item.UnitId,
                            Unit = item.Unit,
                            RoleId = item.RoleId,
                            Role = item.Role,
                            Email = item.Email
                        });
                    }

                    return LR.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ListResult<UserBindingViewModel> GetList(string name = "", int? positionId = null, int? unitId = null, int? roleId = null, int? pageNumber = 0, int? pageSize = 0)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Users
                              join b in libraryDb.Positions on a.PositionId equals b.Id
                              join c in libraryDb.Units on a.UnitId equals c.Id
                              join d in libraryDb.Roles on a.RoleId equals d.Id
                              select new {
                                  Id = a.Id,
                                  Account = a.Account,
                                  Password = a.Password,
                                  Name = a.Name,
                                  Gender = a.Gender,
                                  DayOfBirth = a.DayOfBirth,
                                  Address = a.Address,
                                  PhoneNumber = a.PhoneNumber,
                                  PositionId = a.PositionId,
                                  Position = b.Name,
                                  UnitId = a.UnitId,
                                  Unit = c.Name,
                                  RoleId = a.RoleId,
                                  Role = d.Name,
                                  Email = a.Email
                              };

                    if (!string.IsNullOrWhiteSpace(name))
                        qry = from a in qry
                              where a.Name.Contains(name)
                              select a;

                    if (positionId != null && positionId >= 1)
                        qry = from a in qry
                              where a.PositionId == positionId
                              select a;

                    if (unitId != null && unitId >= 1)
                        qry = from a in qry
                              where a.UnitId == unitId
                              select a;

                    if (roleId != null && roleId >= 1)
                        qry = from a in qry
                              where a.RoleId == roleId
                              select a;

                    List<UserBindingViewModel> LR = new List<UserBindingViewModel>();
                    foreach (var item in qry.ToList())
                    {

                        LR.Add(new UserBindingViewModel()
                        {
                            Id = item.Id,
                            Account = item.Account,
                            Password = item.Password,
                            Name = item.Name,
                            Gender = item.Gender,
                            DayOfBirth = item.DayOfBirth,
                            Address = item.Address,
                            PhoneNumber = item.PhoneNumber,
                            PositionId = item.PositionId,
                            Position = item.Position,
                            UnitId = item.UnitId,
                            Unit = item.Unit,
                            RoleId = item.RoleId,
                            Role = item.Role,
                            Email = item.Email
                        });
                    }

                    var listRs = ListResult<UserBindingViewModel>.GetDataPage(LR.OrderBy(x => x.Name), pageNumber, pageSize);
                    if (listRs == null) throw new ArgumentNullException();

                    if (listRs.ListOfObjects == null) return new ListResult<UserBindingViewModel>();
                    
                    return listRs;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool Create(UserBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // check duplicated
                    var currentUser = libraryDb.Users.FirstOrDefault(s => s.Name == model.Name);
                    if (currentUser != null) throw new ArgumentNullException();

                    // Check account for user
                    currentUser = libraryDb.Users.FirstOrDefault(s => s.Account == model.Account);
                    if (currentUser != null) throw new ArgumentNullException();

                    // Insert db
                    var userInfo = _entityMapper.Map<UserBindingModel, User>(model);
                    libraryDb.Users.Add(userInfo);
                    libraryDb.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(UserBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var user = libraryDb.Users.FirstOrDefault(s => s.Id == model.Id);
                    if (user == null) throw new ArgumentNullException("No exist");
                    user.Name = model.Name;
                    user.Account = model.Account;
                    user.Password = model.Password;
                    user.Gender = model.Gender;
                    user.DayOfBirth = model.DayOfBirth;
                    user.Address = model.Address;
                    user.PhoneNumber = model.PhoneNumber;
                    //user.PositionId = model.PositionId;
                    //user.UnitId = model.UnitId;
                    //user.RoleId = model.RoleId;
                    user.Email = model.Email;

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
                    var user = libraryDb.Users.FirstOrDefault(s => s.Id == id);
                    if (user == null) throw new ArgumentNullException("No exist");

                    libraryDb.Users.Remove(user);

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