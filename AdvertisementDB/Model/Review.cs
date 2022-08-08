using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementDB.Model
{
    public class Review
    {
        public int Id { get; set; }
        public int AdvertisementId { get; set; }
        public float Stars { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
