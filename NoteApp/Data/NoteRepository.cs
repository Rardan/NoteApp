using Microsoft.EntityFrameworkCore;
using NoteApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteApp.Data
{
    public class NoteRepository : INoteRepository
    {
        private readonly NoteDbContext _noteDbContext;

        public NoteRepository(NoteDbContext noteDbContext)
        {
            _noteDbContext = noteDbContext;
        }

        public async Task<IEnumerable<Note>> GetNotesAsync()
        {
            return await _noteDbContext.Notes.Include(u => u.User).OrderByDescending(n => n.DateTime).ToListAsync();
        }

        public async Task<IEnumerable<Note>> GetNotesByUserAsync(AppUser appUser)
        {
            return await _noteDbContext.Notes.Where(n => n.User == appUser).OrderByDescending(n => n.DateTime).ToListAsync();
        }

        public async Task AddNoteAsync(Note note)
        {
            await _noteDbContext.AddAsync(note);
        }

        public async Task SaveChangesAsync()
        {
            await _noteDbContext.SaveChangesAsync();
        }
    }
}
