﻿using DataModel.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyAkkaNetWeb.ViewModel
{
    public class CoilScheduleViewModel
    {
        public IEnumerable<CoilSchedule> CoilSchedules { get; set; }
        public decimal CoilScheduleTotal { get; set; }
    }
}
