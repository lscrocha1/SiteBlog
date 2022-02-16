using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteBlog.Repositories.Mongo;

public interface IMongoRepository<T> where T : class
{
    Task<List<T>> GetAsync(int page = 1, int limit = 10);
}