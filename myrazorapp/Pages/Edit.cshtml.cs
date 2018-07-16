using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace myrazorapp.Pages
{
    public class EditModel : PageModel
    {
        
        private readonly AppDbContext _db;

        public EditModel(AppDbContext db){
            this._db = db;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id){

            this.Customer = await _db.Customers.FindAsync(id);

            if (this.Customer == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(){

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Attach(this.Customer).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception($"Customer {Customer.Id} not found!", ex);
            }

            return RedirectToPage("/Index");;
        }
    }
}