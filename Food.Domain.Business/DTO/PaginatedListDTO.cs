using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Business.DTO
{
    public class PaginatedListDTO<T>
    {
        public List<T> Items { get; set; } = new();
        public int Total { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }

        //public bool HasItems { get { return Items != null && Items.Any(); } }
    }
}
