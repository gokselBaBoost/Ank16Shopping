using Microsoft.EntityFrameworkCore;
using Shopping.DAL.DataContext;
using Shopping.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.DAL.Repositories.Abstract
{
    public abstract class Repo<TEntity> : IRepo<TEntity> where TEntity : BaseEntity
    {
        protected ShoppingDbContext _dbContext;
        protected Repo(ShoppingDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
        }

        public int Add(TEntity entity)
        {
            entity.Created = DateTime.Now;

            _dbContext.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            _dbContext.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public virtual TEntity? Get(int id)
        {
            return _dbContext.Set<TEntity>().AsNoTracking().SingleOrDefault(e => e.Id == id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking().ToList();
        }

        public int Update(TEntity entity)
        {
            entity.Updated = DateTime.Now;

            _dbContext.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
