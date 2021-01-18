using image_repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace image_repository.DBContext {
  public class ImageDbContext:DbContext {
    public ImageDbContext(DbContextOptions<ImageDbContext> options) : base(options) { }

    public DbSet<ImageModel> Images { get; set; }

    public ImageDbContext() : base() {
      var created = Database.EnsureCreated();
    }
  }
}
