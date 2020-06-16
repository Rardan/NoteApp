using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Data;
using NoteApp.Models;
using NoteApp.ViewModels;

namespace NoteApp.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;
        private readonly UserManager<AppUser> _userManager;

        public NoteController(INoteRepository noteRepository, UserManager<AppUser> userManager)
        {
            _noteRepository = noteRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> ShowNotes()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(await _noteRepository.GetNotesByUserAsync(user));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ShowAllNotes()
        {
            return View(await _noteRepository.GetNotesAsync());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var note = new Note
                {
                    DateTime = DateTime.Now,
                    Message = model.Message,
                    User = await _userManager.FindByNameAsync(User.Identity.Name)
                };

                await _noteRepository.AddNoteAsync(note);
                await _noteRepository.SaveChangesAsync();

                return RedirectToAction("ShowNotes");
            }

            return View();
        }
    }
}