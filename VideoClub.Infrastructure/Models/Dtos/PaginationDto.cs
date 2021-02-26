using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Infrastructure.Models.Dtos
{
    public class PaginationDto
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public PaginationDto(int? current, int? size)
        {
            if (current == null)
                current = 1;
            if (current < 1)
                current = 1;
            CurrentPage = (int)current;

            if (size == null)
                size = 10;
            if (size < 1)
                size = 10;
            PageSize = (int)size;
        }

        public int SkipTo()
        {
            return (CurrentPage - 1) * PageSize;
        }

        public PaginationDto()
        { }
    }
}
