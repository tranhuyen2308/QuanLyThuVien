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
    public class RoleManager : BaseManager
    {
        /// <summary>
        ///     mapper to map between database entity and api level models
        /// </summary>
        private readonly IMapper _entityMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Role, RoleBindingModel>();
            cfg.CreateMap<RoleBindingModel, Role>();
        }).CreateMapper();

        public ListResult<RoleBindingModel> GetList(string name = "", int? pageNumber = 0, int? pageSize = 0)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Roles
                              select a;

                    if (!string.IsNullOrWhiteSpace(name))
                        qry = from a in qry
                              where a.Name.Contains(name)
                              select a;

                    var listRs = ListResult<Role>.GetDataPage(qry.OrderBy(x=>x.Name), pageNumber, pageSize);
                    if (listRs == null) throw new ArgumentNullException();

                    if (listRs.ListOfObjects == null) return new ListResult<RoleBindingModel>();

                    var listResult = new ListResult<RoleBindingModel>();
                    listResult.ListOfObjects = new List<RoleBindingModel>();
                    listResult.TotalRecord = listRs.TotalRecord;

                    foreach (var item in listRs.ListOfObjects)
                    {
                        var role = _entityMapper.Map<Role, RoleBindingModel>(item);
                        listResult.ListOfObjects.Add(role);
                    }
                    return listResult;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Create(RoleBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // check duplicated
                    var currentRole = libraryDb.Roles.FirstOrDefault(s => s.Name == model.Name);
                    if (currentRole != null) throw new ArgumentNullException();
                    // Insert db
                    var roleInfo = _entityMapper.Map<RoleBindingModel, Role>(model);
                    libraryDb.Roles.Add(roleInfo);
                    libraryDb.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(RoleBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var role = libraryDb.Roles.FirstOrDefault(s => s.Id == model.Id);
                    if (role == null) throw new ArgumentNullException("No exist");
                    role.Name = model.Name;
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
                    var role = libraryDb.Roles.FirstOrDefault(s => s.Id == id);
                    if (role == null) throw new ArgumentNullException("No exist");

                    libraryDb.Roles.Remove(role);

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


