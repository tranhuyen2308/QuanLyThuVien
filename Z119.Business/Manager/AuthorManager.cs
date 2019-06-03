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
    public class AuthorManager : BaseManager
    {
        /// <summary>
        ///     mapper to map between database entity and api level models
        /// </summary>
        private readonly IMapper _entityMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Author, AuthorBindingModel>();
            cfg.CreateMap<AuthorBindingModel, Author>();
        }).CreateMapper();

        public ListResult<AuthorBindingModel> GetList(string name = "", int? pageNumber = 0, int? pageSize = 0)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Authors
                              select a;

                    if (!string.IsNullOrWhiteSpace(name))
                        qry = from a in qry
                              where a.Name.Contains(name)
                              select a;

                    var listRs = ListResult<Author>.GetDataPage(qry.OrderBy(x=>x.Name), pageNumber, pageSize);
                    if (listRs == null) throw new ArgumentNullException();

                    if (listRs.ListOfObjects == null) return new ListResult<AuthorBindingModel>();

                    var listResult = new ListResult<AuthorBindingModel>();
                    listResult.ListOfObjects = new List<AuthorBindingModel>();
                    listResult.TotalRecord = listRs.TotalRecord;

                    foreach (var item in listRs.ListOfObjects)
                    {
                        var author = _entityMapper.Map<Author, AuthorBindingModel>(item);
                        listResult.ListOfObjects.Add(author);
                    }
                    return listResult;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Create(AuthorBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // check duplicated
                    var currentAuthor = libraryDb.Authors.FirstOrDefault(s => s.Id == model.Id);
                    if (currentAuthor != null) throw new ArgumentNullException();
                    // Insert db
                    var authorInfo = _entityMapper.Map<AuthorBindingModel, Author>(model);
                    libraryDb.Authors.Add(authorInfo);
                    libraryDb.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(AuthorBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var author = libraryDb.Authors.FirstOrDefault(s => s.Id == model.Id);
                    if (author == null) throw new ArgumentNullException("No exist");
                    author.Name = model.Name;
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
                    var author = libraryDb.Authors.FirstOrDefault(s => s.Id == id);
                    if (author == null) throw new ArgumentNullException("No exist");

                    libraryDb.Authors.Remove(author);

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
