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
    public class PublisherManager : BaseManager
    {

        /// <summary>
        ///     mapper to map between database entity and api level models
        /// </summary>
        private readonly IMapper _entityMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Publisher, PublisherBindingModel>();
            cfg.CreateMap<PublisherBindingModel, Publisher>();
        }).CreateMapper();

        public ListResult<PublisherBindingModel> GetList(string name = "", int? pageNumber = 0, int? pageSize = 0)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    var qry = from a in libraryDb.Publishers
                              select a;

                    if (!string.IsNullOrWhiteSpace(name))
                        qry = from a in qry
                              where a.Name.Contains(name)
                              select a;

                    var listRs = ListResult<Publisher>.GetDataPage(qry.OrderBy(x=>x.Name), pageNumber, pageSize);
                    if (listRs == null) throw new ArgumentNullException();

                    if (listRs.ListOfObjects == null) return new ListResult<PublisherBindingModel>();

                    var listResult = new ListResult<PublisherBindingModel>();
                    listResult.ListOfObjects = new List<PublisherBindingModel>();
                    listResult.TotalRecord = listRs.TotalRecord;

                    foreach (var item in listRs.ListOfObjects)
                    {
                        var publisher = _entityMapper.Map<Publisher, PublisherBindingModel>(item);
                        listResult.ListOfObjects.Add(publisher);
                    }
                    return listResult;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Create(PublisherBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // check duplicated
                    var currentPublishers = libraryDb.Publishers.FirstOrDefault(s => s.Id == model.Id);
                    if (currentPublishers != null) throw new ArgumentNullException();
                    // Insert db
                    var publishersInfo = _entityMapper.Map<PublisherBindingModel, Publisher>(model);
                    libraryDb.Publishers.Add(publishersInfo);
                    libraryDb.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(PublisherBindingModel model)
        {
            try
            {
                using (var libraryDb = new LibraryManagementEntities())
                {
                    // Check first get item
                    var publishers = libraryDb.Publishers.FirstOrDefault(s => s.Id == model.Id);
                    if (publishers == null) throw new ArgumentNullException("No exist");
                    publishers.Name = model.Name;
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
                    var publishers = libraryDb.Publishers.FirstOrDefault(s => s.Id == id);
                    if (publishers == null) throw new ArgumentNullException("No exist");

                    libraryDb.Publishers.Remove(publishers);

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
