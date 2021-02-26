using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Core.Entities
{
    public class DVD
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public bool IsAvailable { get; set; } = true;

        public DVD()
        { }

        public DVD(Movie movie)
        {
            Movie = movie;
        }

        public void MakeAvailable()
        {
            IsAvailable = true;
        }
    }

}
