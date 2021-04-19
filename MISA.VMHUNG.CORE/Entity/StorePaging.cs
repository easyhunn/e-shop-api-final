using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.VMHUNG.Core.Entity
{
    public class StorePaging
    {
        /// <summary>
        /// Tổng số lượng trang
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// Tổng số lượng bản ghi
        /// </summary>
        public int TotalRecord { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Store> Data { get; set; }
    }
}
