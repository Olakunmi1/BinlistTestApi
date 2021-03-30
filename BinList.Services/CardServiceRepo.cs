using BinlistTestApi.Binlist.Data;
using BinlistTestApi.Binlist.Data.Entities;
using BinlistTestApi.Helpers;
using BinlistTestApi.ReadDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Wallet.Data.Dbcontext;

namespace BinlistTestApi.BinList.Services
{
    public class CardServiceRepo : ICardService
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public CardServiceRepo(ApplicationDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }
        public void CreateHit(HitCount count)
        {
            _context.HitCounts.Add(count);
        }

        public List<HitCountsDTO_GetAll> getAllCardHits()
        {
            var collections = _context.HitCounts as IQueryable<HitCount>;
            collections.ToList();

            var listOfHits_ReadDTO = collections
                 .Select(x => new HitCountsDTO_GetAll
                 {
                      Size = x.CardNumber.ToString().Count(),
                      Response = x.CardNumber.ToString() + ":" + " " + x.Count.ToString()
                 }).ToList();
            return listOfHits_ReadDTO;
        }

        //make a call to An External Api service
        public async Task<MyRootClass> GetcardDetails(int cardNumber)
        {
            var IIN = cardNumber.ToString();
            var JsonResponse =  await _httpClient.GetAsync(IIN);
            var responseStream = await JsonResponse.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<MyRootClass>(responseStream);

            return results;
        }

        public HitCount getHitCounts(int cardNum) 
        {
            var hitCounts = _context.HitCounts.Where(x => x.CardNumber == cardNum).FirstOrDefault();

            return hitCounts;

        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();

        }
    }
}
