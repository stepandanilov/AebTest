using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoContext context;
        public ToDoController(ToDoContext context)
        {
            this.context = context;
        }
        //GET /
        public async Task<IActionResult> Index()
        {
            IQueryable<TodoListItem> items = from i in context.ToDoList orderby i.Id select i;

            List<TodoListItem> todoList = await items.ToListAsync();

            return View(todoList);
        }
        //GET /todo/create
        public IActionResult Create() => View();
        //POST /todo/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoListItem item)
        {
            if (ModelState.IsValid)
            {
                context.Add(item);

                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(item);
        }
    }
}
