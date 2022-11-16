using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using People.WebApp.Data;
using People.WebApp.Helpers;
using People.WebApp.Models;

namespace People.WebApp.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PeopleDbContext context;

        public PeopleController(PeopleDbContext context)
        {
            this.context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
              return context.People != null ? 
                          View(await context.People.ToListAsync()) :
                          Problem("Entity set 'PeopleDbContext.People'  is null.");
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || context.People == null)
            {
                return NotFound();
            }

            var person = await context.People
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,BirthDate")] Person person)
        {
            if (ModelState.IsValid)
            {
                context.Add(person);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || context.People == null)
            {
                return NotFound();
            }

            var person = await context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName,BirthDate")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(person);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeopleDbContextHelper.PersonExists(context, person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || context.People == null)
            {
                return NotFound();
            }

            var person = await context.People
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (context.People == null)
            {
                return Problem("Entity set 'PeopleDbContext.People'  is null.");
            }
            var person = await context.People.FindAsync(id);
            if (person != null)
            {
                context.People.Remove(person);
            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }        
    }
}
