using Microsoft.EntityFrameworkCore;
using VirtualLibrary.Api.Models;

namespace VirtualLibrary.Api.Data
    {
    public class VirtualLibraryContext : DbContext
        {
        public VirtualLibraryContext(DbContextOptions<VirtualLibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        }
    }
