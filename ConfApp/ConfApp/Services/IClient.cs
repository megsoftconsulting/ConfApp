using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfApp.ViewModels;

namespace ConfApp.Services
{
    public interface IClient
    {
        Task<T> Get<T>(string id) where T : ModelBase;

        Task<IEnumerable<T>> GetAll<T>() where T : ModelBase;

        Task<T> Post<T>(T entity) where T : ModelBase;

        Task<T> Delete<T>(string id) where T : ModelBase;

        Task<T> Delete<T>(T entity) where T : ModelBase;
    }

    public abstract class ClientMock<TModel> : IClient
        where TModel : ModelBase
    {
        protected List<TModel> Items { get; set; } = new List<TModel>();

        public Task<T> Get<T>(string id)
            where T : ModelBase
        {
            return Task.FromResult(Items.FirstOrDefault(x => x.Id == id) as T);
        }

        public Task<IEnumerable<T>> GetAll<T>()
            where T : ModelBase => Task.FromResult(Items.Cast<T>());

        public Task<T> Post<T>(T entity)
            where T : ModelBase
        {
            Items.Add(entity as TModel);
            return Task.FromResult(entity);
        }

        public Task<T> Delete<T>(string id)
            where T : ModelBase
        {
            var item = Items.FirstOrDefault(x => x.Id == id);
            Items.Remove(item);
            return Task.FromResult(item as T);
        }

        public Task<T> Delete<T>(T entity)
            where T : ModelBase
        {
            Items.Remove(entity as TModel);
            return Task.FromResult(entity);
        }
    }
}