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
        //GET /todo/edit/<id>
        public async Task<IActionResult> Edit(int Id)
        {
            TodoListItem item = await context.ToDoList.FindAsync(Id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
        //POST /todo/edit/<id>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TodoListItem item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);

                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(item);
        }
        //GET /todo/delete/<id>
        public async Task<IActionResult> Delete(int Id)
        {
            TodoListItem item = await context.ToDoList.FindAsync(Id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                context.ToDoList.Remove(item);
                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        //GET /todo/details/<id>
        public async Task<IActionResult> Details(int Id)
        {
            TodoListItem item = await context.ToDoList.FindAsync(Id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return View(item);
            }
        }
        //GET /<id>
        [HttpGet("{Id}")]
        public async Task<ActionResult<TodoListItem>> GetTodoItem(int Id)
        {
            TodoListItem item = await context.ToDoList.FindAsync(Id);

            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
    }
}
