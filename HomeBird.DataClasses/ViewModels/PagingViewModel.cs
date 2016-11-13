using HomeBird.DataClasses.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses.ViewModels
{
    public class PagingViewModel : PagingForm
    {
        public int Start { get; set; }

        public int Stop { get; set; }

        public int LastPage { get; set; }

        public int Current { get; set; }

        public Func<int, string> GetUrl { get; set; }

    }
}
