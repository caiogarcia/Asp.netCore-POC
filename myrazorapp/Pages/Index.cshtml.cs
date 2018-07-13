using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

//Video pausado em 0:36
namespace myrazorapp.Pages
{
    public class IndexModel : PageModel
    {

        private readonly AppDbContext _db;

        public IndexModel(AppDbContext db){
            this._db = db;
        }

        //Devia ser NÃO Estatica
        public static IList<Customer> Customers { get; set; }

        public async Task OnGetAsync()
        {
            Customers = await _db.Customers.AsNoTracking().ToListAsync();
        }
    }
}
