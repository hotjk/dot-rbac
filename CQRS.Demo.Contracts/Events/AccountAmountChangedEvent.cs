﻿using Grit.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Demo.Contracts.Events
{
    public class AccountAmountChangedEvent : Event
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
