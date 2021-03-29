using BinlistTestApi.Binlist.Data.Entities;
using BinlistTestApi.Helpers;
using BinlistTestApi.ReadDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinlistTestApi.Binlist.Data
{
   public interface ICardService
    {
        Task<MyRootClass> GetcardDetails(int cardNumber);
        void CreateHit(HitCount count);

        HitCountsDTO getHitCounts(int cardNum);   
    }
}
