using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MongoDatabaseExperiments.Models;

namespace MongoDatabaseExperiments.Services;

public class GenericRepository<TEntity> where TEntity : class
{
    internal ApplicationDatabaseContext Context;
    internal DbSet<TEntity> DbSet;

    public GenericRepository(ApplicationDatabaseContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public GenericRepository(IDbContextFactory<ApplicationDatabaseContext> factory)
    {
        Context = factory.CreateDbContext();
        DbSet = Context.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = DbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return orderBy != null ? orderBy(query).ToList() : query.ToList();
    }

    public virtual TEntity GetById(object id)
    {
        return DbSet.Find(id);
    }

    public virtual void Insert(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public virtual void Delete(params object[] keyValues)
    {
        Delete(DbSet.Find(keyValues));
    }

    public virtual void Delete(TEntity entity)
    {
        if (Context.Entry(entity).State == EntityState.Detached)
        {
            DbSet.Attach(entity);
        }

        DbSet.Remove(entity);
    }

    public virtual void Update(TEntity entity)
    {
        DbSet.Attach(entity);
        Context.Entry(entity).State = EntityState.Modified;
    }
}