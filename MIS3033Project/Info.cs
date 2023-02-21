using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS3033Project
{
    public class Info
    {
        public string Title { get; set; }
        public string Genre { get; set; }

        public string Director { get; set; }

        public string StarActors { get; set; }

        public string IMBD { get; set; }

        public Info() 
        {
            Title = string.Empty;
            Genre = string.Empty;
            Director = string.Empty;
            StarActors = string.Empty;
            IMBD = string.Empty;
        }   

    }
}
