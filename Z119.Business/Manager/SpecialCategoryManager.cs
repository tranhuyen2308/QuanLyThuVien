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
    public class SpecialCategoryManager : BaseManager
    {
        /// <summary>
        ///     mapper to map between database entity and api level models
        /// </summary>
        private readonly IMapper _entityMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SpecialCategory, SpecialCategoryBindingModel>();
            cfg.CreateMap<SpecialCategoryBindingModel, SpecialCategory>();
        }).CreateMapper();

        public ListResult<SpecialCategoryBindingModel> GetList(string name = "", int? pageNumber = 0, int? pageSize = 0)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.SpecialCategories
                              select a;

                    if (!string.IsNullOrWhiteSpace(name))
                        qry = from a in qry
                              where a.Name.Contains(name)
                              select a;


                    var listRs = ListResult<SpecialCategory>.GetDataPage(qry.OrderBy(x=>x.Name), pageNumber, pageSize);
                    if (listRs == null) throw new ArgumentNullException();

                    if (listRs.ListOfObjects == null) return new ListResult<SpecialCategoryBindingModel>();

                    var listResult = new ListResult<SpecialCategoryBindingModel>();
                    listResult.ListOfObjects = new List<SpecialCategoryBindingModel>();
                    listResult.TotalRecord = listRs.TotalRecord;

                    foreach (var item in listRs.ListOfObjects)
                    {
                        var specialCategory = _entityMapper.Map<SpecialCategory, SpecialCategoryBindingModel>(item);
                        listResult.ListOfObjects.Add(specialCategory);
                    }
                    return listResult;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Create(SpecialCategoryBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // check duplicated
                    var currentSpecialCategory = libraryDb.SpecialCategories.FirstOrDefault(s => s.Name == model.Name);
                    if (currentSpecialCategory != null) throw new ArgumentNullException();
                    // Insert db
                    var specialCategoryInfo = _entityMapper.Map<SpecialCategoryBindingModel, SpecialCategory>(model);
                    libraryDb.SpecialCategories.Add(specialCategoryInfo);
                    libraryDb.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(SpecialCategoryBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var specialCategory = libraryDb.SpecialCategories.FirstOrDefault(s => s.Id == model.Id);
                    if (specialCategory == null) throw new ArgumentNullException("No exist");
                    specialCategory.Name = model.Name;
                    specialCategory.Description = model.Description;
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
                    var specialCategory = libraryDb.SpecialCategories.FirstOrDefault(s => s.Id == id);
                    if (specialCategory == null) throw new ArgumentNullException("No exist");

                    libraryDb.SpecialCategories.Remove(specialCategory);

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
