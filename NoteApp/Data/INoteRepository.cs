using NoteApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteApp.Data
{
    public interface INoteRepository
    {
        Task AddNoteAsync(Note note);
        Task<IEnumerable<Note>> GetNotesAsync();
        Task<IEnumerable<Note>> GetNotesByUserAsync(AppUser appUser);
        Task SaveChangesAsync();
    }
}