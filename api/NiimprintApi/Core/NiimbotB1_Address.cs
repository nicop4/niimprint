using System;

namespace NiimprintApi.Core;

/// <summary>
/// This class prints labels on a Niimbot B1 50x30 printer.
/// </summary>
public class NiimbotB1_Address : NiimbotB1_60x40
{
    public NiimbotB1_Address(ILogger<NiimbotPrinter> logger) : base(logger)
    {
        BasePath = Environment.GetEnvironmentVariable("ADDRESS_PATH") ?? throw new ArgumentNullException("ADDRESS_PATH");
    }
}
