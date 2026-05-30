using System;
using System.Collections.Generic;
using System.Text;

namespace Uber.Voyage.Domain.Enums;

public enum VoyageStatus
{
    Requested = 1, 
    Accepted = 2, 
    InProgress = 3,
    Completed = 4,
    Cancelled = 5  
}
