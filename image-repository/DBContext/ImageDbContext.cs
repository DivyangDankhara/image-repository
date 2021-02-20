using image_repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace image_repository.DBContext {
  public class ImageDbContext:DbContext {
    public ImageDbContext(DbContextOptions<ImageDbContext> options) : base(options) { }

    public DbSet<Image> Images { get; set; }

    public ImageDbContext() : base() {
      var created = Database.EnsureCreated();
    }
  }
}
