
using dotnetCourse.DataAccess;
using dotnetCourse.DataAccess.Data;
using dotnetCourse.DataAccess.Repository.IRepository;
using dotnetCourse.Models;
using dotnetCourse.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace dotnetCourse.Controllers
{
    public class TodoController : Controller
    {

        private readonly IUnitOfWork unitOfWork;

        public TodoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }



        public IActionResult Index(string id,int pageNumber=1,int pageSize=5)
        {

            Console.WriteLine("index yüklendi.");
        
            var filters = new Filter(id);
            ViewBag.Filters = filters;


            TodoIndexVM todoIndexVM = new TodoIndexVM();
            todoIndexVM.Statues = GetStatusSelectList(filters.StatusId);
            todoIndexVM.Categories = GetCategorySelectList(filters.CategoryId);
            todoIndexVM.DueFilterValues = GetDueDatesSelectList(filters.Due);

            IQueryable<Todo> query = unitOfWork.TodoRepository.GetAllWithCategoryAndStatus();



            if (filters.HasCategory)
            {
                query = query.Where(e => e.CategoryId == filters.CategoryId);

            }
            if (filters.HasStatus)
            {
                query = query.Where(e => e.StatusId == filters.StatusId);

            }
            if (filters.HasCategory)
            {
                query = query.Where(e => e.CategoryId == filters.CategoryId);

            }
            if (filters.HasDue)
            {
                var today = DateTime.Today;
                if (filters.IsPast)
                {
                    query = query.Where(t => t.DueDate < today);
                }
                if (filters.IsToday)
                {
                    query = query.Where(t => t.DueDate == today);
                }
                if (filters.IsFuture)
                {
                    query = query.Where(t => t.DueDate > today);
                }

            }

            todoIndexVM.Todos = query.OrderBy(t => t.DueDate).ToList();

            var count = todoIndexVM.Todos.Count();
            var items = todoIndexVM.Todos.Skip((pageNumber-1)* pageSize).Take(pageSize).ToList();
            todoIndexVM.Todos = items;
            todoIndexVM.PageNumber = pageNumber;
            todoIndexVM.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            return View(todoIndexVM);
        }
        
       
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            var viewmodel = new TodoAddVM();
            viewmodel.Categories = GetCategorySelectList(null);
            viewmodel.Statuses = GetStatusSelectList(null);
            if (id == null)
            {
               

                var task = new Todo() { StatusId = "open" };
                viewmodel.Todo = task;
                return View(viewmodel);
            }
            else
            {
                

                var task = unitOfWork.TodoRepository.Get(i => i.Id == id);
               
                viewmodel.Todo = task;

                return View(viewmodel);
            }
            

        }
        [HttpPost]
        public IActionResult Upsert(TodoAddVM todoAddVM)
        {


            if (ModelState.IsValid)
            {   
                if(todoAddVM.Todo.Id == 0)
                {
                    unitOfWork.TodoRepository.Add(todoAddVM.Todo);
                    TempData["success"] = "Add successfully";
                }
                else
                {
                    Console.WriteLine("update");
                    unitOfWork.TodoRepository.Update(todoAddVM.Todo);
                    TempData["success"] = "Update successfully";
                }

                
                unitOfWork.Save();
                
                return RedirectToAction("Index");
                
               
            }
            else
            {
                todoAddVM.Categories = GetCategorySelectList(null);
                todoAddVM.Statuses = GetStatusSelectList(null);
                return View(todoAddVM);
            }

        }

        [HttpPost]
        public IActionResult Filters(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { id = id });

        }
        [HttpPost]
        public IActionResult MarkComplete([FromRoute] string id, Todo selected)
        {

            bool isMarkedComplete = unitOfWork.TodoRepository.MarkAsComplete(selected);
            if (isMarkedComplete)
            {
                unitOfWork.Save();
                TempData["success"] = "Mark complete successfully";
            }
            else
            {
                TempData["error"] = "Failed to mark complete";
            }
            return RedirectToAction("Index", new { id = id });
        }
        [HttpPost]
        public IActionResult DeleteComplete(string id)
        {
            Console.WriteLine("deleteocmplete");

            var toDeleteList = unitOfWork.TodoRepository.GetTodos(t => t.StatusId == "closed");
            if (toDeleteList != null && toDeleteList.Any())
            {
                unitOfWork.TodoRepository.RemoveRange(toDeleteList);
                unitOfWork.Save();
                TempData["success"] = "Delete successfully";
            }
            else
            {
                Console.WriteLine("girdi");
                TempData["error"] = "No delete items";
            }

            return RedirectToAction("Index", new { id = id });

        }
        #region API CALLS
        [Route("todo/delete/{id}/{filterId}")]
        [HttpDelete]
        public IActionResult Delete(int id, string filterId)
        {
            Console.WriteLine("filter:" + filterId);
            var toDelete = unitOfWork.TodoRepository.Get(i => i.Id == id);
            if (toDelete != null)
            {
                unitOfWork.TodoRepository.Remove(toDelete);
                unitOfWork.Save();
            }
            TempData["success"] = "Delete successfully";
            return Json(new { success = true, redirectUrl = Url.Action("Index", new { id = filterId }) });
        }


        #endregion

        private IEnumerable<SelectListItem> GetDueDatesSelectList(string DueDate)
        {
            return new List<SelectListItem>
                {

                    new SelectListItem { Value = "future", Text = "Future",Selected = DueDate == "future" },
                    new SelectListItem { Value = "past", Text = "Past",Selected = DueDate == "past" },
                    new SelectListItem { Value = "today", Text = "Today",Selected = DueDate == "today" }
                };
        }
        private IEnumerable<SelectListItem> GetCategorySelectList(string categoryId)
        {
            if (categoryId == null)
            {
                return unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.CategoryId
                    

                });
            }
            else
            {
                return unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.CategoryId,
                    Selected = u.CategoryId == categoryId

                });
            }

        }

        private IEnumerable<SelectListItem> GetStatusSelectList(string statusId)
        {
            if (statusId == null)
            {
                return unitOfWork.StatusRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.StatusName,
                    Value = u.StatusId
                });
            }
            else
            {
                return unitOfWork.StatusRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.StatusName,
                    Value = u.StatusId,
                    Selected = u.StatusId == statusId
                });

            }

        }
    }
}
