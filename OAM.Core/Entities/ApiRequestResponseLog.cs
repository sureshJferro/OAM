using System;
using System.Collections.Generic;

namespace OAM.Core.Entities;

public partial class ApiRequestResponseLog
{
    public int LogId { get; set; }

    public string? RequestMethod { get; set; }

    public string? RequestPath { get; set; }

    public string? RequestBody { get; set; }

    public int? ResponseStatusCode { get; set; }

    public string? ResponseBody { get; set; }

    public DateTime? CreateTimeStamp { get; set; }

    public DateTime? UpdateTimeStamp { get; set; }

    public Guid? UserId { get; set; }
}
