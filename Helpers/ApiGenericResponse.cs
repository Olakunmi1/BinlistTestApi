using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinlistTestApi.Helpers
{
    public class ApiGenericResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Results { get; set; }

    }
}
