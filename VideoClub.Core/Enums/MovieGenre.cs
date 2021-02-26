using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Core.Enums
{
    public enum MovieGenre
    {
        Δράση = 1,
        Κωμωδία,
        Αστυνομική,
        Δράμα,
        Φαντασία,
        Τρόμου,
        Θρίλλερ,
        [Display(Name = "Επιστημονικής φαντασίας")]
        Επιστημονικής_φαντασίας
    }
}
