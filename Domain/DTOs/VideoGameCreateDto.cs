using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class VideoGameCreateDto
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public int Note { get; set; }
    }
}
