using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoteApp.ViewModels
{
    public class NoteViewModel
    {
        [Required]
        [MaxLength(150)]
        public string Message { get; set; }
    }
}
