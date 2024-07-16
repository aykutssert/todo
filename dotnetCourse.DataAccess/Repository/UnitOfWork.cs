using dotnetCourse.DataAccess.Data;
using dotnetCourse.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetCourse.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext ApplicationDbContext { get; set; }
        public ITodoRepository TodoRepository {  get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }

        public IStatusRepository StatusRepository { get; private set; }
        public UnitOfWork(ApplicationDbContext applicationDbContext) {
        this.ApplicationDbContext = applicationDbContext;
            TodoRepository = new TodoRepository(ApplicationDbContext);
            CategoryRepository = new CategoryRepository(ApplicationDbContext);
            StatusRepository = new StatusRepository(ApplicationDbContext);
        }

        

        public void Save()
        {
            ApplicationDbContext.SaveChanges();
        }

    }
}
