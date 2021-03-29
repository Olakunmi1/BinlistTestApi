using BinlistTestApi.Binlist.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wallet.Data.Dbcontext
{
    //inheriting from IdentityDB Context give sus acces to AspNet users table and d rest 
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options)
           : base(options)
        {
           
        }

        //have DB sets below 
        public DbSet<HitCount> HitCounts { get; set; }

    }
}
