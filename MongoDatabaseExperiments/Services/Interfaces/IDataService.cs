using System.Linq.Expressions;
using MongoDatabaseExperiments.Models;
using MongoDB.Bson;

namespace MongoDatabaseExperiments.Services.Interfaces;

public interface IDataService
{
    Task<Restaurant> GetRestaurantByIdAsync(ObjectId id, CancellationToken token = default);
    Task<Restaurant> GetRestaurantByIdAsync(string id, CancellationToken token = default);
    Task<IReadOnlyList<TEntity>> GetEntitiesWhereAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) where TEntity : class;
    Task<TEntity> UpsertEntityAsync<TEntity>(TEntity entity, CancellationToken token = default) where TEntity : class;
}