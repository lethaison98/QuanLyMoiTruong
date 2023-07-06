using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Requests
{
    public class PagingRequest
    {
        public string FullTextSearch
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public int PageIndex
        {
            get;
            set;
        }

        public int TotalCount
        {
            get;
            set;
        }
    }
}
