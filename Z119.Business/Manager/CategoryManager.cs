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
    public class CategoryManager : BaseManager
    {
        /// <summary>
        ///     mapper to map between database entity and api level models
        /// </summary>
        private readonly IMapper _entityMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Category, CategoryBindingModel>();
            cfg.CreateMap<CategoryBindingModel, Category>();
        }).CreateMapper();

        //public ListResult<CategoryBindingModel> GetList(int? specializedId = null, string name = "", int? pageNumber = 0, int? pageSize = 0)
        //{
        //    try
        //    {
        //        using (var libraryDb = new LibraryManagementEntities())
        //        {
        //            var qry = from a in libraryDb.Categories
        //                      select a;

        //            if (!string.IsNullOrWhiteSpace(name))
        //                qry = from a in qry
        //                      where a.Name.Contains(name)
        //                      select a;

        //            if (specializedId != null && specializedId >= 1)
        //                qry = from a in qry
        //                      where a.SpecializedId == specializedId
        //                      select a;

        //            var listRs = ListResult<Category>.GetDataPage(qry.OrderBy(x=>x.Name), pageNumber, pageSize);
        //            if (listRs == null) throw new ArgumentNullException();

        //            if (listRs.ListOfObjects == null) return new ListResult<CategoryBindingModel>();

        //            var listResult = new ListResult<CategoryBindingModel>();
        //            listResult.ListOfObjects = new List<CategoryBindingModel>();
        //            listResult.TotalRecord = listRs.TotalRecord;

        //            foreach (var item in listRs.ListOfObjects)
        //            {
        //                var category = _entityMapper.Map<Category, CategoryBindingModel>(item);
        //                listResult.ListOfObjects.Add(category);
        //            }
        //            return listResult;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        public ListResult<CategoryViewModel> GetList(int? specializedId = null, string name = "", int? pageNumber = 0, int? pageSize = 0)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Categories join b in libraryDb.Specializeds
                              on a.SpecializedId equals b.Id
                              select new {
                                  Id = a.Id,
                                  Name = a.Name,
                                  SpecializedId = a.SpecializedId,
                                  Specialized = b.Name

                              };

                    if (!string.IsNullOrWhiteSpace(name))
                        qry = from a in qry
                              where a.Name.Contains(name)
                              select a;

                    if (specializedId != null && specializedId >= 1)
                        qry = from a in qry
                              where a.SpecializedId == specializedId
                              select a;

                    List<CategoryViewModel> LR = new List<CategoryViewModel>();
                    foreach (var item in qry.ToList())
                    {

                        LR.Add(new CategoryViewModel()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            SpecializedId = item.SpecializedId,
                            Specialized = item.Specialized
                        });
                    }

                    var listRs = ListResult<CategoryViewModel>.GetDataPage(LR.OrderBy(x => x.Name), pageNumber, pageSize);
                    if (listRs == null) throw new ArgumentNullException();

                    if (listRs.ListOfObjects == null) return new ListResult<CategoryViewModel>();

                    return listRs;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool Create(CategoryBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // check duplicated
                    var currentCategory = libraryDb.Categories.FirstOrDefault(s => s.Name == model.Name);
                    if (currentCategory != null) throw new ArgumentNullException();
                    // Insert db
                    var categoryInfo = _entityMapper.Map<CategoryBindingModel, Category>(model);
                    libraryDb.Categories.Add(categoryInfo);
                    libraryDb.SaveChanges();
                    
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(CategoryBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var category = libraryDb.Categories.FirstOrDefault(s => s.Id == model.Id);
                    if (category == null) throw new ArgumentNullException("No exist");
                    category.Name = model.Name;
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
                    var category = libraryDb.Categories.FirstOrDefault(s => s.Id == id);
                    if (category == null) throw new ArgumentNullException("No exist");

                    libraryDb.Categories.Remove(category);

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