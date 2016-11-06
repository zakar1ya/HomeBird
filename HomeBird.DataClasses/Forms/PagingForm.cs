using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses.Forms
{
    public abstract class PagingForm
    {
        public int Offset { get; set; }

        public int Count { get; set; }

        public int Total { get; set; }
    }
}
