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
        Model = PrinterModel.b1;
        Connection = "usb";
        Density = PrintDensity._5;
        Rotation = PrintRotation._90;
        Verbose = false;
        // get address from environment variable
        Address = Environment.GetEnvironmentVariable("NIIMPRINT_B1_USB_ADDRESS") ?? throw new ArgumentNullException("NIIMPRINT_B1_USB_ADDRESS");
        MaxImageSizeX = 500;
        MaxImageSizeY = 320;
        BasePath = Environment.GetEnvironmentVariable("STAMP_PATH") ?? throw new ArgumentNullException("STAMP_PATH");
    }
}
