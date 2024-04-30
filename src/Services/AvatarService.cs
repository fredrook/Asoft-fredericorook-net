using Alfasoft.Interface;
using System;
using System.Threading.Tasks;

namespace Alfasoft.Services
{
    public class AvatarService : IAvatarService
    {
        public Task<string> GetAvatarUrlAsync(string userId)
        {
            string defaultAvatarUrl = "https://example.com/default-avatar.jpg";

            return Task.FromResult(defaultAvatarUrl);
        }

        public Task<string> GetRandomAvatarUrl()
        {
            string randomAvatarUrl = "https://example.com/random-avatar.jpg";

            return Task.FromResult(randomAvatarUrl);
        }
    }
}
