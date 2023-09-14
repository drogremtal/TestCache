namespace TestCache.Service
{
    public interface ICacheService
    {
        public object GetData(string key, Func<object> func);
        public void ClearAll();
        public void Clear(string key);

        public object SetDate(string key, Func<object> func);
    }
}
