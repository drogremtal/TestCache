using Microsoft.Extensions.Caching.Memory;

namespace TestCache.Service
{
    public class MemoryCacheService : ICacheService
    {

        private IMemoryCache _memoryCache;
        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public void Clear(string key)
        {
            throw new NotImplementedException();
        }

        public void ClearAll()
        {
        }

        public object GetData(string key, Func<object> func)
        {

            object data;

            if (!_memoryCache.TryGetValue(key, out data))
            {

                Console.WriteLine("Пишем в кеш");

                data = func();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSize(1)//Значение размера
                       // Приоритет на удаление при достижении предельного размера (давления на память)
               .SetPriority(CacheItemPriority.High)
                // Храним в кэше в течении этого времени, сбрасываем время при обращении.
                .SetSlidingExpiration(TimeSpan.FromSeconds(2))
               // Удаляем из кэша по истечении этого времени, независимо от скользящего таймера.
               .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

                _memoryCache.Set(key, data, cacheEntryOptions);
            }
            else
            {
                Console.WriteLine("Из кеша");
            }
            return data;
        }

        public object SetDate(string key, Func<object> func)
        {
            throw new NotImplementedException();
        }
    }
}
