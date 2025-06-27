using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Abstractions.Interfaces.Redis
{
	public interface IRedisService
	{
		Task<string?> GetStringAsync(string key);

		Task<T?> GetObjectAsync<T>(string key);
		Task<bool> ExistsAsync(string key);
		
		Task<T?> GetAsync<T>(string hashKey, string id);

		Task<List<T>> GetAllAsync<T>(string hashKey);
	}
}
