using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z119.Model.BaseModel
{
    public class ObjectResult<T>
    {
        public T DataObject { get; set; }
    }

    public class ListResult<T>
    {
        private int _totalRecord = 0;

        /// <summary>
        /// List data object
        /// </summary>
        public List<T> ListOfObjects { get; set; }

        /// <summary>
        /// Total record
        /// </summary>
        public int TotalRecord
        {
            get
            {
                if (_totalRecord == 0 && ListOfObjects != null)
                {
                    return ListOfObjects.Count;
                }
                else
                {
                    return _totalRecord;
                }
            }

            set { _totalRecord = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="returnTotalRecord"></param>
        /// <returns></returns>
        public static ListResult<T> GetDataPage(IQueryable<T> query, int? pageNumber, int? pageSize, bool returnTotalRecord = true)
        {
            if (query == null) return null;
            var result = new ListResult<T>();
            if (pageNumber != null && pageSize != null
                && pageNumber > 0 && pageSize > 0)
            {
                if (returnTotalRecord) result.TotalRecord = query.Count();
                result.ListOfObjects = query.Skip((int)pageSize * ((int)pageNumber - 1)).Take((int)pageSize).ToList();
            }
            else
            {
                if (returnTotalRecord) result.TotalRecord = query.Count();
                result.ListOfObjects = query.ToList();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="returnTotalRecord"></param>
        /// <returns></returns>
        public static async Task<ListResult<T>> GetDataPageAsync(IQueryable<T> query, int? pageNumber, int? pageSize, bool returnTotalRecord = true)
        {
            if (query == null) return null;
            var result = new ListResult<T>();
            if (pageNumber != null && pageSize != null
                && pageNumber > 0 && pageSize > 0)
            {
                if (returnTotalRecord) result.TotalRecord = query.Count();
                result.ListOfObjects = await query.Skip((int)pageSize * ((int)pageNumber - 1)).Take((int)pageSize).ToListAsync();
            }
            else
            {
                var rCount = query.Count();
                if (returnTotalRecord) result.TotalRecord = query.Count();
                result.ListOfObjects = await query.ToListAsync();
            }
            return result;
        }

        //=================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="returnTotalRecord"></param>
        /// <returns></returns>
        public static ListResult<T> GetDataPage(IOrderedEnumerable<T> query, int? pageNumber, int? pageSize, bool returnTotalRecord = true)
        {
            if (query == null) return null;
            var result = new ListResult<T>();
            if (pageNumber != null && pageSize != null
                && pageNumber > 0 && pageSize > 0)
            {
                if (returnTotalRecord) result.TotalRecord = query.Count();
                result.ListOfObjects = query.Skip((int)pageSize * ((int)pageNumber - 1)).Take((int)pageSize).ToList();
            }
            else
            {
                if (returnTotalRecord) result.TotalRecord = query.Count();
                result.ListOfObjects = query.ToList();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="returnTotalRecord"></param>
        /// <returns></returns>
        public static ListResult<T> GetDataPage(IOrderedQueryable<T> query, int? pageNumber, int? pageSize, bool returnTotalRecord = true)
        {
            if (query == null) return null;
            var result = new ListResult<T>();
            if (pageNumber != null && pageSize != null
                && pageNumber > 0 && pageSize > 0)
            {
                if (returnTotalRecord) result.TotalRecord = query.Count();
                result.ListOfObjects = query.Skip((int)pageSize * ((int)pageNumber - 1)).Take((int)pageSize).ToList();
            }
            else
            {
                if (returnTotalRecord) result.TotalRecord = query.Count();
                result.ListOfObjects = query.ToList();
            }
            return result;
        }
    }
}