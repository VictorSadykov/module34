﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP34.Data.Queries
{
    public class EditRoomQuery
    {
        public string NewName { get; set; }
        public int NewArea { get; set; }
        public bool NewGasConnected { get; set; }
        public int NewVoltage { get; set; }
    }
}
