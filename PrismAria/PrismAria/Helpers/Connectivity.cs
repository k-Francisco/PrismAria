﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismAria.Helpers
{
    public class ConnectivityChangedEventArgs : EventArgs
    {
        public bool IsConnected { get; set; }
    }

    public delegate void ConnectivityChangedEventHandler(object sender, ConnectivityChangedEventArgs e);
}
