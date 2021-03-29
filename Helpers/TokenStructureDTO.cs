using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinlistTestApi.Helpers
{
    public class TokenStructureDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string TokenString { get; set; }
        public DateTime? ExpiresWhen { get; set; }
    }
}
