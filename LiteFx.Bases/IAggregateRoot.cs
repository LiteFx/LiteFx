﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Bases
{
    public interface IAggregateRoot<TId>
    {
        TId GlobalIdentity { get; }
    }
}
