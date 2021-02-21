using image_repository.Interfaces;
using image_repository.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace image_repository.Controllers {
  public class ImageController : Controller {
    
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IImageService _imageService;

    public ImageController(IWebHostEnvironment webHostEnvironment, IImageService imageService) {
      _imageService = imageService;
      _webHostEnvironment = webHostEnvironment;
    }

    // GET: Image
    public async Task<IActionResult> Index(string searchString) {
        
      var images = await _imageService.Get(searchString);

      return View(images);
    }


    // GET: Image/Details/5
    public async Task<IActionResult> Details(int? id) {
      
      if (id == null) {
        return NotFound();
      }

      var imageModel = await _imageService.GetById((int)id);
          
      if (imageModel == null) {
        return NotFound();
      }

      return View(imageModel);
    }

    // GET: Image/Create
    public IActionResult Create() {
      
      return View();
    }

    // POST: Image/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ImageId,Title,ImageFile")] ImageModel imageModel) {
      
      if (ModelState.IsValid) {
        await _imageService.Create(imageModel);
        return RedirectToAction(nameof(Index));
      }
      
      return View(imageModel);
    }

    // GET: Image/Delete/5
    public async Task<IActionResult> Delete(int? id) {
      if (id == null) {
        return NotFound();
      }

      var imageModel = await _imageService.GetById((int)id);
      
      if (imageModel == null) {
        return NotFound();
      }

      return View(imageModel);
    }

    // POST: Image/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {

      await _imageService.Delete(id);

      return RedirectToAction(nameof(Index));
    }


    #region EDIT Image Under Development
    //// GET: Image/Edit/5
    //public async Task<IActionResult> Edit(int? id) {
    //  if (id == null) {
    //    return NotFound();
    //  }

    //  var imageModel = await _context.Images.FindAsync(id);
    //  if (imageModel == null) {
    //    return NotFound();
    //  }
    //  return View(imageModel);
    //}

    //// POST: Image/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(int id, [Bind("ImageId,Title,ImageName")] ImageModel imageModel) {
    //  if (id != imageModel.ImageId) {
    //    return NotFound();
    //  }

    //  if (ModelState.IsValid) {
    //    try {
    //      //imageModel.ImageData = ConvertIntoByteArray(imageModel.ImageFile);
    //      _context.Update(imageModel);
    //      await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateConcurrencyException) {
    //      if (!ImageModelExists(imageModel.ImageId)) {
    //        return NotFound();
    //      }
    //      else {
    //        throw;
    //      }
    //    }
    //    return RedirectToAction(nameof(Index));
    //  }
    //  return View(imageModel);
    //}
    #endregion
  }
}
