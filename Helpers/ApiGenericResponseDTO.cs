using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinlistTestApi.Helpers
{
    public class ApiGenericResponseDTO<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<T> Response { get; set; }
    }
}
