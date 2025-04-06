using System;

namespace NiimprintApi.Core;

/// <summary>
/// This class prints labels on a Niimbot B1 50x30 printer.
/// </summary>
public class NiimbotB1_60x40 : NiimbotPrinter
{
    public NiimbotB1_60x40(ILogger<NiimbotPrinter> logger) : base(logger)
    {
        _logger = logger;
        Rotation = PrintRotation._90;
        Verbose = false;
        MaxImageSizeX = 500;
        MaxImageSizeY = 320;
    }
}
