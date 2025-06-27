using Microsoft.Extensions.Options;
using PArticle.Subscribers.Core.Interfaces;
using PArticle.Subscribers.Core.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace PArticle.Subscribers.Core.Concretes
{
	public class RedisService:IRedisService
	{
		private readonly ConnectionMultiplexer _redis;
		private readonly IDatabase _db;
		private readonly RedisModel _options;

		public RedisService(IOptions<RedisModel> options)
		{
			_options = options.Value;
			var config = new ConfigurationOptions
			{
				EndPoints = { $"{_options.Host}:{_options.Port}" },
				Password = _options.Password
			};

			const int delayMilliseconds = 2000;
			while (true)
			{
				try
				{
					Console.WriteLine($"Redis'e bağlanılıyor: {_options.Host}:{_options.Port}");
					_redis = ConnectionMultiplexer.Connect(config);
					break;
				}
				catch (Exception ex) 
				{
					Console.WriteLine($"Redis bağlantısı başarısız: {ex.Message}");
					Thread.Sleep(delayMilliseconds);
				}
				
			}
			Console.WriteLine($"Redis'e bağlandı: {_options.Host}:{_options.Port}");

			_db = _redis.GetDatabase();
		}

		// String değer ayarla
		public async Task SetStringAsync(string key, string value, TimeSpan? expiry = null)
		{
			await _db.StringSetAsync(key, value, expiry);
		}

		// String değer oku
		public async Task<string?> GetStringAsync(string key)
		{
			return await _db.StringGetAsync(key);
		}

		// Nesne olarak sakla
		public async Task SetObjectAsync<T>(string key, T data, TimeSpan? expiry = null)
		{
			var json = JsonSerializer.Serialize(data);
			await _db.StringSetAsync(key, json, expiry);
		}

		// Nesne olarak oku
		public async Task<T?> GetObjectAsync<T>(string key)
		{
			var json = await _db.StringGetAsync(key);
			return json.HasValue ? JsonSerializer.Deserialize<T>(json) : default;
		}

		// Key sil
		public async Task<bool> RemoveKeyAsync(string key)
		{
			return await _db.KeyDeleteAsync(key);
		}

		// Key var mı?
		public async Task<bool> ExistsAsync(string key)
		{
			return await _db.KeyExistsAsync(key);
		}

		#region Hash

		public async Task<bool> AddOrUpdateAsync<T>(string hashKey,string id, T obj)
		{
			var json = JsonSerializer.Serialize(obj);
			return await _db.HashSetAsync(hashKey, id, json);
		}
		public async Task<bool> AddOrUpdateAsync<T>(string hashKey, int id, T obj)=> await AddOrUpdateAsync<T>(hashKey, id.ToString(), obj);
		// Tek kayıt getirir
		public async Task<T?> GetAsync<T>(string hashKey,string id)
		{
			var json = await _db.HashGetAsync(hashKey, id);
			if (json.IsNullOrEmpty)
				return default;

			return JsonSerializer.Deserialize<T>(json);
		}

		// Tek kaydı siler
		public async Task<bool> DeleteAsync(string hashKey, string id)
		{
			return await _db.HashDeleteAsync(hashKey, id);
		}
		public async Task<bool> DeleteAsync(string hashKey, int id) => await DeleteAsync(hashKey, id.ToString());
		

		// Tüm kayıtları listeler
		public async Task<List<T>> GetAllAsync<T>(string hashKey)
		{
			var entries = await _db.HashGetAllAsync(hashKey);
			return entries
				.Select(e => JsonSerializer.Deserialize<T>(e.Value))
				.Where(x => x != null)
				.Select(x => x!)
				.ToList();
		}

		#endregion
	}
}
