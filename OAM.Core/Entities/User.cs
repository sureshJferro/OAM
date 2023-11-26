using System;
using System.Collections.Generic;

namespace OAM.Core.Entities;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public int RoleId { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? CreatedTimeStamp { get; set; }

    public DateTime? UpdatedTimeStamp { get; set; }

    public DateTime? IsDeleted { get; set; }
}
