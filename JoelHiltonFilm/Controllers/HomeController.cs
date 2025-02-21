using System.Diagnostics;
using JoelHiltonFilm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JoelHiltonFilm.Controllers;

public class HomeController : Controller
{
    private MovieFormContext _context;
    
    public HomeController(MovieFormContext temp)
    {
        _context = temp;
    }
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult GetToKnow()
    {
        return View();
    }

    [HttpGet]
    public IActionResult AddMovie()
    {
        ViewBag.Categories = _context.Categories.ToList();
        
        return View("AddMovie", new MovieForm());
    }
    
    [HttpPost]
    public IActionResult AddMovie(MovieForm response)
    {
        if (ModelState.IsValid)
        {
             _context.Movies.Add(response);
             _context.SaveChanges();
             return View("Confirmation", response);
        }
        else
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View(response);
        }
    }

    public IActionResult MovieList()
    {
        var moviesInList = _context.Movies
            .Include(m => m.Category)  // <-- This ensures Category data is loaded
            .OrderBy(x => x.Title)
            .ToList();
    
        return View(moviesInList);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var movieRecord = _context.Movies
            .Single(x => x.MovieId == id);
        
        ViewBag.Categories = _context.Categories.ToList();
        return View("AddMovie", movieRecord);
    }

    [HttpPost]
    public IActionResult Edit(MovieForm updatedInfo)
    {
        _context.Update(updatedInfo);
        _context.SaveChanges();
        return RedirectToAction("MovieList");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var recordToDelete = _context.Movies
            .Single(x => x.MovieId == id);
        
        return View(recordToDelete);
    }

    [HttpPost]
    public IActionResult Delete(MovieForm deletedRecord)
    {
        _context.Movies.Remove(deletedRecord);
        _context.SaveChanges();
        
        return RedirectToAction("MovieList");
    }
}