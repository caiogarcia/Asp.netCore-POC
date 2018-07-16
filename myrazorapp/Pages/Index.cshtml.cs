using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace myrazorapp.Pages
{
    public class IndexModel : PageModel
    {

        private readonly AppDbContext _db;

        public IndexModel(AppDbContext db){
            this._db = db;
        }

        public static IList<Customer> Customers { get; set; }

        public async Task OnGetAsync()
        {
            Customers = await _db.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id){
            var customer = await _db.Customers.FindAsync(id);

            if(customer != null)
            {
                _db.Customers.Remove(customer);

                await _db.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
