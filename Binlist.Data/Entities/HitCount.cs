using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BinlistTestApi.Binlist.Data.Entities
{
    public class HitCount
    {
        [Key]
        public int Id { get; set; }
        public int CardNumber { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; private set; } = DateTime.Now;
    }
}
