using DanaCopilot.Domain.DTOs;
using DanaCopilot.Domain.Interfaces.Command;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DanaCopilot.Infrastructure.DataAccess.Command
{
    public class OtpRedisRepository : IOtpRedisRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConfiguration _configuration;
        public OtpRedisRepository(IDistributedCache distributedCache, IConfiguration configuration)
        {
            _distributedCache = distributedCache;
            _configuration = configuration;
        }

        public async Task<bool> Delete(Otp entity)
        {
            _distributedCache.RemoveAsync(entity.UserId.ToString());

            //TODO set entity
            return true;
        }

        public async Task<bool> Insert(Otp entity)
        {
            int time=Convert.ToInt32(_configuration.GetSection("Otp:OtpTime").Value);
            _distributedCache.SetString(entity.UserId.ToString(),JsonConvert.SerializeObject(entity),new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(time))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(time)));

            //TODO set entity
            return true;
            
        }

        public Task<bool> Update(Otp entity)
        {
            throw new NotImplementedException();
        }
    }
}
