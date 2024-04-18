using System;
using System.Collections.Generic;

namespace Chizh;

public partial class Train
{
    public int Id { get; set; }

    public string? TrTittle { get; set; }

    public string? TrDescription { get; set; }

    public int? IdPoze { get; set; }

    public int? IdMuscle { get; set; }

    public decimal? TrTime { get; set; }

    public virtual Muscle? IdMuscleNavigation { get; set; }

    public virtual Poze? IdPozeNavigation { get; set; }
}
