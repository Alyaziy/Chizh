﻿using System;
using System.Collections.Generic;

namespace Chizh;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }

    public decimal? Weight { get; set; }

    public int? Height { get; set; }

    public virtual ICollection<Check> Checks { get; set; } = new List<Check>();
}
