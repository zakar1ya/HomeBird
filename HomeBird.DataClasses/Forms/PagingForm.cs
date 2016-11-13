using HomeBird.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses.Forms
{
    public abstract class PagingForm
    {
        public PagingForm()
        {
            Count = Consts.PageCount;
        }

        private int _offset;
        public int Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                if (value >= 0)
                    _offset = value;
            }
        }

        private int _count;
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                if (value > 0)
                    _count = value;
            }
        }

        public int Total { get; set; }
    }
}
