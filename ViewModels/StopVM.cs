using System;
using System.ComponentModel.DataAnnotations;

namespace TheWorld.ViewModels
{
    public class StopVM
    {
        [Required]
        [StringLengthAttribute(100, MinimumLength = 5)]
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [RequiredAttribute]
        public int Order { get; set; }
        [RequiredAttribute]
        public DateTime Arrival { get; set; }
    }
}