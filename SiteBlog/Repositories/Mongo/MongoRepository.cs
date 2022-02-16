using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteBlog.Repositories.Mongo;

public class MongoRepository<T> : IMongoRepository<T> where T : class
{
    public Task<List<T>> GetAsync(
        int page = 1, 
        int limit = 10)
    {
        throw new NotImplementedException();
    }
}