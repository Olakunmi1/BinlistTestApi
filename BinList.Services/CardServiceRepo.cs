using BinlistTestApi.Binlist.Data;
using BinlistTestApi.Binlist.Data.Entities;
using BinlistTestApi.ReadDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallet.Data.Dbcontext;

namespace BinlistTestApi.BinList.Services
{
    public class CardServiceRepo : ICardService
    {
        private readonly ApplicationDbContext _context;

        public CardServiceRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public void CreateHit(HitCount count)
        {
            _context.HitCounts.Add(count);
        }

        public void GetcardDetails(int cardNumber)
        {
           //call 3rd party API here
        }

        public HitCountsDTO getHitCounts(int cardNum) 
        {

            var hitCounts = _context.HitCounts.Where(x => x.CardNumber == cardNum).FirstOrDefault();
            var Hitcountss = new HitCountsDTO
            {
                CardNumber = hitCounts.CardNumber,
                Count = hitCounts.Count
            };

            return Hitcountss;

        }
    }
}
