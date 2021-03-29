using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BinlistTestApi.Helpers
{
    public static class HandleErrors
    {
        public static List<string> HandeleModelState
        {
            get
            {
                return null;
                //return ModelState.Values
                //  .SelectMany(x => x.Errors
                //    .Select(ie => ie.ErrorMessage))
                //    .ToList();
            }
        }
    }
}
