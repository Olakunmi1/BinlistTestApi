using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BinlistTestApi.WriteDTO
{
    public class CardDetailsDTOW
    {
        [Required]
       // [RegularExpression(@"^[0 - 9]{6, 8}$")] //validation to make user to enter 6 digit or 8 digit
       [Range(0, 9, ErrorMessage = "Value for {0} must be greater than {1}")]
        public int CardNumber { get; set; }

    }
}
