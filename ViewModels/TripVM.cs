using System;
using System.ComponentModel.DataAnnotations;

namespace TheWorld.ViewModels
{
    public class TripVM
    {
        [RequiredAttribute]
        [StringLengthAttribute(100, MinimumLength=5)]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
