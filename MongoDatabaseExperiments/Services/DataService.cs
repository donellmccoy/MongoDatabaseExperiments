using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDatabaseExperiments.Models;
using MongoDatabaseExperiments.Options;
using MongoDatabaseExperiments.Services.Interfaces;
using MongoDB.Bson;

namespace MongoDatabaseExperiments.Services;

public class DataService : IDataService
{
    private readonly IDbContextFactory<ApplicationDatabaseContext> _factory;

    public DataService(IDbContextFactory<ApplicationDatabaseContext> factory, IOptions<AppSettings> options)
    {
        _factory = factory;
    }

    #region Methods

    public async Task<TEntity> UpsertEntityAsync<TEntity>(TEntity entity, CancellationToken token = default) where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(entity);

        await using var context = await _factory.CreateDbContextAsync(token);

        await context.AddAsync(entity, token);

        await context.SaveChangesAsync(token);

        return entity;
    }

    public async Task<IReadOnlyList<TEntity>> GetEntitiesWhereAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(predicate);

        await using var context = await _factory.CreateDbContextAsync(token);

        return await context.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken: token);
    }

    public async Task<Restaurant> GetRestaurantByIdAsync(string id, CancellationToken token = default)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(id));
        }

        if (ObjectId.TryParse(id, out var objectId) is false)
        {
            throw new ArgumentException("Value cannot parsed as an ObjectId.", nameof(id));
        }

        return await GetRestaurantByIdAsync(objectId, token);
    }

    public async Task<Restaurant> GetRestaurantByIdAsync(ObjectId id, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(id);
        
        await using var context = await _factory.CreateDbContextAsync(token);

        return await context.Restaurants.SingleOrDefaultAsync(r => r.Id == id, cancellationToken: token);
    }

    #endregion
}