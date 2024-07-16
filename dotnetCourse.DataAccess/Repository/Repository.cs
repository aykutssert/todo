using dotnetCourse.DataAccess.Data;
using dotnetCourse.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dotnetCourse.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        internal DbSet<T> Set;
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            this.Set = context.Set<T>();

        }
        public bool Add(T entity)
        {
            try
            {
                Set.Add(entity);
                return true;
            }
            catch (Exception ex)
            {
                // Hata yönetimi, loglama vb. işlemler yapılabilir
                return false; // Başarısız olursa null döndür
            }
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = Set;
            query = query.Where(predicate);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = Set;
            return query.ToList();
        }

        public IEnumerable<T> GetTodos(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = Set;
            query = query.Where(filter);
            return query.ToList();
        }

        public bool Remove(T entity)
        {
            try
            {
                Set.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                // Hata yönetimi, loglama vb. işlemler yapılabilir
                return false; // Başarısız olursa null döndür
            }
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Set.RemoveRange(entities);
        }
        
    }
}
