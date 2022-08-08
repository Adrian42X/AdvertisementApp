using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementDB.Model
{
    public class User 
    {
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(25)]
        public string Name { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; }
    }
}
