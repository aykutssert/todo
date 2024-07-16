using dotnetCourse.DataAccess.Data;
using dotnetCourse.DataAccess.Repository.IRepository;
using dotnetCourse.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dotnetCourse.DataAccess.Repository
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public TodoRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        {
            this.applicationDbContext = applicationDbContext;
        }

        public bool Update(Todo entity) //individual repo'da yazdık çünkü manual mapping gerekebilir.
        {
            try
            {
                  applicationDbContext.Todos.Update(entity);
                  return true;
            }
            catch(Exception ex)
            {
                return false;
            }
          
        }

        public IQueryable<Todo> GetAllWithCategoryAndStatus()
        {
            IQueryable<Todo> query = applicationDbContext.Todos.Include(i=>i.Category).Include(i=>i.Status);

            return query;
        }
        public bool MarkAsComplete(Todo todo)
        {
            var selectedItem = applicationDbContext.Todos.FirstOrDefault(t => t.Id == todo.Id);
            if (selectedItem != null)
            {
                selectedItem.StatusId = "closed";
                return true; // Başarılı
            }
            return false; // Başarısız
        }

    }
}
