using System;

namespace NiimprintApi.Core;

/// <summary>
/// This class prints labels on a Niimbot B1 50x30 printer.
/// </summary>
public class NiimbotB1_Stamp : NiimbotB1_60x40
{
    public NiimbotB1_Stamp(ILogger<NiimbotPrinter> logger) : base(logger)
    {
        BasePath = Environment.GetEnvironmentVariable("STAMP_PATH") ?? throw new ArgumentNullException("STAMP_PATH");
    }
}
