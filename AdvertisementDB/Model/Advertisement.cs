using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdvertisementDB.Model
{
    public class Advertisement 
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required, MaxLength(30)]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public float price { get; set; }

        [Required]
        public string Status { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
