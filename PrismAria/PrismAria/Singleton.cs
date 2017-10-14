using PrismAria.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PrismAria
{
    public sealed class Singleton
    {

        private static volatile Singleton _instance;
        private static readonly object _syncLock = new object();

        private Singleton() {

        }

        public static Singleton Instance
        {
            get
            {
                if (_instance != null) return _instance;

                lock (_syncLock)
                {
                    if(_instance == null)
                    {
                        _instance = new Singleton();
                    }
                }

                return _instance;
            }
        }

        public ObservableCollection<DiscoverPageModel> DiscoverCollection { get; set; }
        public ObservableCollection<UserBandModel> UserBandCollection { get; set; }

    }
}
