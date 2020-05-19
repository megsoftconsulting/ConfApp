using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfApp.ViewModels;
using DynamicData;

namespace ConfApp.Services
{
    public interface IDataService<T>
        where T : ModelBase
    {
        IObservable<IChangeSet<T, string>> ChangeSet { get; }

        Task<T> Get(string id);

        Task<IEnumerable<T>> Get();
    }

    public abstract class DataServiceBase<T> : IDataService<T>
        where T : ModelBase
    {
        private readonly IClient _client;
        protected SourceCache<T, string> SourceCache = new SourceCache<T, string>(x => x.Id);

        protected DataServiceBase(IClient client)
        {
            _client = client;
        }

        public IObservable<IChangeSet<T, string>> ChangeSet => SourceCache.Connect().RefCount();

        public async Task<T> Get(string id)
        {
            var result = await _client.Get<T>(id);
            SourceCache.AddOrUpdate(result);
            return SourceCache.Lookup(id).Value;
        }

        public async Task<IEnumerable<T>> Get()
        {
            var result = await _client.GetAll<T>();
            SourceCache.AddOrUpdate(result);
            return SourceCache.Items;
        }
    }
}