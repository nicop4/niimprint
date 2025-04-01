using System;

namespace NiimprintApi.Core;

/// <summary>
/// This class prints labels on a Niimbot B1 50x30 printer.
/// </summary>
public class NiimbotB1_Product : NiimbotB1_50x30
{
    public NiimbotB1_Product(ILogger<NiimbotPrinter> logger) : base(logger)
    {
        BasePath = Environment.GetEnvironmentVariable("PRODUCT_PATH") ?? throw new ArgumentNullException("PRODUCT_PATH");
    }
}
