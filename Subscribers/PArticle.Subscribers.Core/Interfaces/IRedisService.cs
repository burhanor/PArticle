using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PArticle.Subscribers.Core.Interfaces
{
	public interface IRedisService
	{

		 Task SetStringAsync(string key, string value, TimeSpan? expiry = null);
		 Task<string?> GetStringAsync(string key);

		Task SetObjectAsync<T>(string key, T data, TimeSpan? expiry = null);

		 Task<T?> GetObjectAsync<T>(string key);

		Task<bool> RemoveKeyAsync(string key);

		Task<bool> ExistsAsync(string key);
		Task<bool> AddOrUpdateAsync<T>(string hashKey, string id, T obj);
		Task<bool> AddOrUpdateAsync<T>(string hashKey, int id, T obj);

		Task<T?> GetAsync<T>(string hashKey, string id);

		Task<bool> DeleteAsync(string hashKey, string id);
		Task<bool> DeleteAsync(string hashKey, int id);

		Task<List<T>> GetAllAsync<T>(string hashKey);



	}
}
