﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses.Forms
{
    public class UpdateSaleForm : CreateSaleForm
    {
        [Required]
        public int Id { get; set; }
    }
}
