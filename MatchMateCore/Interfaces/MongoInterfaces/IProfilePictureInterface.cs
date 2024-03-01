using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMateCore.Interfaces.MongoInterfaces
{
    public interface IProfilePictureInterface
    {
        public Task SaveProfilePictureToMongoAsync(string userId,string file);
        public Task<string> GetProfilePictureFromMongoAsync(string userId);
    }
}
