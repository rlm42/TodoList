using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Web.Constants;
using Web.Entities;
using Web.Models;

namespace Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase  // : means inherits from ControllerBase (base class)
    {


        //--------------------------------------
        //             Properties
        //--------------------------------------

        private readonly TodoListDbContext _ctx;     // Declare property first, don't declare and initialise at same time, don't need readonly


        //--------------------------------------
        //             Constructor
        //--------------------------------------

        public TodosController(TodoListDbContext ctx)   // Dependency injection, ctx field is initiated
        {
            _ctx = ctx;
        }


        //--------------------------------------
        //             Methods
        //--------------------------------------

        // Post

        [HttpPost]
        public async Task<IActionResult> AddItem(TodoModel model)  // Create an IActionResult interface called PostTodo
        {
            try
            {
                var entity = new Todo { Name = model.Name };
                await _ctx.Todos.AddAsync(entity);
                await _ctx.SaveChangesAsync();
                model.Id = entity.Id;
                return Ok(model);

                //return Ok("Post works...");

                //return CreatedAtAction(nameof(GetItem), new { id = name.Id }, name);
            }
            catch (Exception ex)
            {
                return BadRequest("Bad Request Error");
            }
        }
        
        // Read - List of Items

        [HttpGet]
        public IActionResult GetList()  // Get the whole list of Todos
        {
           // var todos = new List<Todo>();  // Declare and initialize todos, same as List<Todo> todos = new List<Todo>();
           // todos = _ctx.Todos.ToList();

            var todos = _ctx.Todos.ToList();  // Declare and initialize todos, same as List<Todo> todos = new List<Todo>();

            return Ok(todos);
        }

        // Read - One Item

        [HttpGet("{id}")]
        public IActionResult GetItem(int id)  // Get a single Todo
        {
            // var todos = new List<Todo>();  // Declare and initialize todos, same as List<Todo> todos = new List<Todo>();
            // todos = _ctx.Todos.ToList();

            var todo = _ctx.Todos.Find(id);  // Declare and initialize todos, same as List<Todo> todos = new List<Todo>();

            return Ok(todo);
        }

        // Update

        [HttpPut("{id}")]
        public IActionResult PutTodo(int id, Todo name)
        {
            // return Ok("Put works..");

            if (id != name.Id)
            {
                return BadRequest();
            }

            _ctx.Entry(name).State = EntityState.Modified;
            _ctx.SaveChanges();

            return NoContent();
        }

        // Update Status  

        [HttpPut("{id}/update-status")]
        public async Task<IActionResult> UpdateStatus(int id, TodoStatusModel model)
        {
            try
            {
                var entity = await _ctx.Todos.FindAsync(id);
                entity.IsDone = model.IsDone;

                await _ctx.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest("Bad Request Error");
            }
        }

        // Delete 

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            //return Ok("Delete works..");

            var todo = _ctx.Todos.Find(id);  // Declare and initialize todos, same as List<Todo> todos = new List<Todo>();

            if (todo == null)
            {
                return NotFound();
            }

            _ctx.Todos.Remove(todo);
            _ctx.SaveChanges();

            return NoContent();

        }

    }
}
