using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Paging<T> : CPagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        public Paging()
        {
            Results = new List<T>();
        }

        public Paging(IList<T> list, int rowCount, int pageIndex, int pageSize)
        {
            Results = list;
            CurrentPage = pageIndex;
            PageSize = pageSize;
            RowCount = rowCount;
            PageCount = (int)Math.Ceiling((double)RowCount / PageSize);
        }

        public void ToList(IList<T> list, int rowCount, int pageIndex, int pageSize)
        {
            Results = list;
            CurrentPage = pageIndex;
            PageSize = pageSize;
            RowCount = rowCount;
            PageCount = (int)Math.Ceiling((double)RowCount / PageSize);
        }
    }

    public abstract class CPagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int FirstRowOnPage
        {

            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }

    public static class IQueryable
    {
        public static Paging<T> GetPaged<T>(this IQueryable<T> query,
                                                    int page, int pageSize) where T : class
        {
            if (page <= 1)
                page = 1;

            var result = new Paging<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}
