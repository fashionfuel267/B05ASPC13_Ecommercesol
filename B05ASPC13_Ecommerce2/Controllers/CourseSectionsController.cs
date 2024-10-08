﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using B05ASPC13_Ecommerce2.Data;
using B05ASPC13_Ecommerce2.Models;

namespace B05ASPC13_Ecommerce2.Controllers
{
    public class CourseSectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseSectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseSections
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CourseSections.Include(c => c.Course);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CourseSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSection = await _context.CourseSections
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseSection == null)
            {
                return NotFound();
            }

            return View(courseSection);
        }

        // GET: CourseSections/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            return View();
        }

        // POST: CourseSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CourseId")] CourseSection courseSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseSection.CourseId);
            return View(courseSection);
        }

        // GET: CourseSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSection = await _context.CourseSections.FindAsync(id);
            if (courseSection == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseSection.CourseId);
            return View(courseSection);
        }

        // POST: CourseSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CourseId")] CourseSection courseSection)
        {
            if (id != courseSection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseSectionExists(courseSection.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseSection.CourseId);
            return View(courseSection);
        }

        // GET: CourseSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSection = await _context.CourseSections
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseSection == null)
            {
                return NotFound();
            }

            return View(courseSection);
        }

        // POST: CourseSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseSection = await _context.CourseSections.FindAsync(id);
            if (courseSection != null)
            {
                _context.CourseSections.Remove(courseSection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseSectionExists(int id)
        {
            return _context.CourseSections.Any(e => e.Id == id);
        }
    }
}
