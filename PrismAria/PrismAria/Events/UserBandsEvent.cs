using Prism.Events;
using PrismAria.Models;
using PrismAria.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAria.Events
{
    public class UserBandsEvent : PubSubEvent<UserBandModelForEvent>
    {
    }
}
