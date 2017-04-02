using System.ComponentModel.DataAnnotations;

namespace TheWorld.ViewModels
{
    public class ContactVM
    {
        [Required]
        [StringLength(30, MinimumLength = 3,
                      ErrorMessage = "Full name character length must be between 3 and 30")]
        public string Name {get; set;}

        [Required]
        [EmailAddress]
        public string Email {get; set;}

        [Required]        
        [MinLength(10, ErrorMessage = "Message must be at least 10 characters long")]    
        public string Message {get; set;}
    }
}
