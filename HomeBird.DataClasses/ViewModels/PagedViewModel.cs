using HomeBird.DataClasses.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses.ViewModels
{
    public class PagedViewModel<T, U> where U : PagingForm
    {
        public PagedViewModel(IEnumerable<T> list, U form)
        {
            List = list;
            Form = form;
        }

        public U Form { get; set; }

        public IEnumerable<T> List { get; set; }
    }
}
