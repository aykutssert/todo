using dotnetCourse.DataAccess.Data;
using dotnetCourse.DataAccess.Repository.IRepository;
using dotnetCourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetCourse.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        public CategoryRepository(ApplicationDbContext applicationDbContext) :base(applicationDbContext)
        {
            
        }
    }
}
