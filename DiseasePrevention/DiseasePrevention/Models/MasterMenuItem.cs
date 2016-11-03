﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasePrevention.Models
{
    public class MenuItem
    {
        public string Title { get; set; }

        public string Icon { get; set; }

        public Func<Task> ActionAsync { get; set; }
    }
}
