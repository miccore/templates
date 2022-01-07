using System.Collections.Generic;

namespace  Miccore.Net.webapi_template.Sample.Api.Entities
{
    public class PaginationEntity<TModel>
    {
        const int MaxPageSize = 100;
        private int _pageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public IList<TModel> Items { get; set; }
        public string Prev {get; set;}
        public string Next {get; set;}

        public PaginationEntity()
        {
            Items = new List<TModel>();
        }
    }
}
