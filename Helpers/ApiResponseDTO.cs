using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinlistTestApi.Helpers
{
    public class ApiResponseDTO<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T PayLoad { get; set; }
       // public List<T> Response { get; set; }
    }
}
