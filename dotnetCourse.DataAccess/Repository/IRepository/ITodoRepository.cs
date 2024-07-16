using dotnetCourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dotnetCourse.DataAccess.Repository.IRepository
{
    public interface ITodoRepository : IRepository<Todo> 
    {
        bool Update(Todo entity);
        IQueryable<Todo> GetAllWithCategoryAndStatus();

        bool MarkAsComplete(Todo todo);
    }
    
}
