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
    public class PositionManager : BaseManager
    {
        /// <summary>
        ///     mapper to map between database entity and api level models
        /// </summary>
        private readonly IMapper _entityMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Position, PositionBindingModel>();
            cfg.CreateMap<PositionBindingModel, Position>();
        }).CreateMapper();

        public ListResult<PositionBindingModel> GetList(string name = "", int? pageNumber = 0, int? pageSize = 0)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Positions
                              select a;

                    if (!string.IsNullOrWhiteSpace(name))
                        qry = from a in qry
                              where a.Name.Contains(name)
                              select a;

                    var listRs = ListResult<Position>.GetDataPage(qry.OrderBy(x=>x.Name), pageNumber, pageSize);
                    if (listRs == null) throw new ArgumentNullException();

                    if (listRs.ListOfObjects == null) return new ListResult<PositionBindingModel>();

                    var listResult = new ListResult<PositionBindingModel>();
                    listResult.ListOfObjects = new List<PositionBindingModel>();
                    listResult.TotalRecord = listRs.TotalRecord;

                    foreach (var item in listRs.ListOfObjects)
                    {
                        var position = _entityMapper.Map<Position, PositionBindingModel>(item);
                        listResult.ListOfObjects.Add(position);
                    }
                    return listResult;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Create(PositionBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // check duplicated
                    var currentPosition = libraryDb.Positions.FirstOrDefault(s => s.Name == model.Name);
                    if (currentPosition != null) throw new ArgumentNullException();
                    // Insert db
                    var positionInfo = _entityMapper.Map<PositionBindingModel, Position>(model);
                    libraryDb.Positions.Add(positionInfo);
                    libraryDb.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(PositionBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var position = libraryDb.Positions.FirstOrDefault(s => s.Id == model.Id);
                    if (position == null) throw new ArgumentNullException("No exist");
                    position.Name = model.Name;
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
                    var position = libraryDb.Positions.FirstOrDefault(s => s.Id == id);
                    if (position == null) throw new ArgumentNullException("No exist");

                    libraryDb.Positions.Remove(position);

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
