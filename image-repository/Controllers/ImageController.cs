﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using image_repository.DBContext;
using image_repository.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace image_repository.Controllers {
  public class ImageController : Controller {
    
    private readonly ImageDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ImageController(ImageDbContext context, IWebHostEnvironment webHostEnvironment) {
      _context = context;
      _webHostEnvironment = webHostEnvironment;
    }

    // GET: Image
    public async Task<IActionResult> Index(string searchString) {

      var images = from i in _context.Images select i;

      if (!String.IsNullOrEmpty(searchString)) {
        images = images.Where(i => i.Title.ToLower().Contains(searchString.ToLower()));
      }

      return View(await images.ToListAsync());
    }


    // GET: Image/Details/5
    public async Task<IActionResult> Details(int? id) {
      if (id == null) {
        return NotFound();
      }

      var imageModel = await _context.Images
          .FirstOrDefaultAsync(m => m.ImageId == id);
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

        string wwwRootPath = _webHostEnvironment.WebRootPath;
        string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
        string extension = Path.GetExtension(imageModel.ImageFile.FileName);
        imageModel.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

        // converting the image into byte array.
        imageModel.ImageData = ConvertIntoByteArray(imageModel.ImageFile);

        //Insert record
        _context.Add(imageModel);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(imageModel);
    }

    // GET: Image/Edit/5
    public async Task<IActionResult> Edit(int? id) {
      if (id == null) {
        return NotFound();
      }

      var imageModel = await _context.Images.FindAsync(id);
      if (imageModel == null) {
        return NotFound();
      }
      return View(imageModel);
    }

    // POST: Image/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ImageId,Title,ImageName")] ImageModel imageModel) {
      if (id != imageModel.ImageId) {
        return NotFound();
      }

      if (ModelState.IsValid) {
        try {
          imageModel.ImageData = ConvertIntoByteArray(imageModel.ImageFile);
          _context.Update(imageModel);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) {
          if (!ImageModelExists(imageModel.ImageId)) {
            return NotFound();
          }
          else {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(imageModel);
    }

    // GET: Image/Delete/5
    public async Task<IActionResult> Delete(int? id) {
      if (id == null) {
        return NotFound();
      }

      var imageModel = await _context.Images
          .FirstOrDefaultAsync(m => m.ImageId == id);
      if (imageModel == null) {
        return NotFound();
      }

      return View(imageModel);
    }

    // POST: Image/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
      var imageModel = await _context.Images.FindAsync(id);
      _context.Images.Remove(imageModel);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool ImageModelExists(int id) {
      return _context.Images.Any(e => e.ImageId == id);
    }

    private byte[] ConvertIntoByteArray(IFormFile Image) {
      MemoryStream ms = new MemoryStream();
      Image.CopyTo(ms);
      byte[] arr = ms.ToArray();
      return arr;
    }
  }
}
