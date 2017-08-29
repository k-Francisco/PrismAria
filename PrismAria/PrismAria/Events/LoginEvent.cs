using Prism.Events;
using PrismAria.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAria.Events
{
    public class LoginEvent : PubSubEvent<FacebookProfile>
    {
    }
}
