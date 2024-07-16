using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetCourse.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ITodoRepository TodoRepository { get;  }
        ICategoryRepository CategoryRepository { get; }

        IStatusRepository StatusRepository { get; }
        void Save();
   
    }
}
