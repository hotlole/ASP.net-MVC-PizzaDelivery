using Dodic.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dodic.DAL
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public string Description { get; set; }
        public StatusCode StatusCode { get; set; }
    }
}
