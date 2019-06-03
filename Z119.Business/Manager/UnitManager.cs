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
    public class UnitManager : BaseManager
    {
        /// <summary>
        ///     mapper to map between database entity and api level models
        /// </summary>
        private readonly IMapper _entityMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Unit, UnitBindingModel>();
            cfg.CreateMap<UnitBindingModel, Unit>();
        }).CreateMapper();

        public ListResult<UnitBindingModel> GetList(string name = "", int? pageNumber = 0, int? pageSize = 0)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Units
                              select a;

                    if (!string.IsNullOrWhiteSpace(name))
                        qry = from a in qry
                              where a.Name.Contains(name)
                              select a;

                    var listRs = ListResult<Unit>.GetDataPage(qry.OrderBy(x=>x.Name), pageNumber, pageSize);
                    if (listRs == null) throw new ArgumentNullException();

                    if (listRs.ListOfObjects == null) return new ListResult<UnitBindingModel>();

                    var listResult = new ListResult<UnitBindingModel>();
                    listResult.ListOfObjects = new List<UnitBindingModel>();
                    listResult.TotalRecord = listRs.TotalRecord;

                    foreach (var item in listRs.ListOfObjects)
                    {
                        var unit = _entityMapper.Map<Unit, UnitBindingModel>(item);
                        listResult.ListOfObjects.Add(unit);
                    }
                    return listResult;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Create(UnitBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // check duplicated
                    var currentUnit = libraryDb.Units.FirstOrDefault(s => s.Name == model.Name);
                    if (currentUnit != null) throw new ArgumentNullException();
                    // Insert db
                    var unitInfo = _entityMapper.Map<UnitBindingModel, Unit>(model);
                    libraryDb.Units.Add(unitInfo);
                    libraryDb.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(UnitBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var unit = libraryDb.Units.FirstOrDefault(s => s.Id == model.Id);
                    if (unit == null) throw new ArgumentNullException("No exist");
                    unit.Name = model.Name;
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
                    var unit = libraryDb.Units.FirstOrDefault(s => s.Id == id);
                    if (unit == null) throw new ArgumentNullException("No exist");

                    libraryDb.Units.Remove(unit);

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

