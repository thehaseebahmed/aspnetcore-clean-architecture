using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Domain.Interfaces;

public interface IAuditable
{
    Guid CreatedById { get; set; }
    DateTimeOffset CreatedOn { get; set; }
    Guid UpdatedById { get; set; }
    DateTimeOffset UpdatedOn { get; set; }
}