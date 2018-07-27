using Microsoft.EntityFrameworkCore;

namespace mywebapi {
    public class TicketItem{
        public long Id { get; set; }
        public string Consert { get; set; }
        public string Artist { get; set; }
        public bool Avaliable { get; set; }

    }

    public class TicketContext : DbContext{

        public TicketContext(DbContextOptions<TicketContext> options) : base(options){

        }

        public DbSet<TicketItem> TicketItems {get;set;}
    }
}