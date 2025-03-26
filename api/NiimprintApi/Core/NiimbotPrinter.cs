using System;
using System.Diagnostics;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace NiimprintApi.Core;

public abstract class NiimbotPrinter
{    
    protected PrinterModel Model { get; set; }
    protected string Connection { get; set; }
    protected string Address { get; set; }
    protected PrintDensity Density { get; set; }
    protected PrintRotation Rotation { get; set; }
    protected bool Verbose { get; set; }
    protected int MaxImageSizeX { get; set; }
    protected int MaxImageSizeY { get; set; }

    protected ILogger<NiimbotPrinter> _logger;

    public (bool result, string errorMessage) Print(string imagePath){

        if(!File.Exists(imagePath)){
            return (false, $"Image file not found: {imagePath}");
        }

        using (Image<Rgba32> image = Image.Load<Rgba32>(imagePath))
        {
            _logger.LogInformation($"Image dimensions: {image.Width}x{image.Height}");
            if (image.Width > MaxImageSizeX || image.Height > MaxImageSizeY)
            {
                var errorMessage = $"Image size is too large. Dimensions are {image.Width}x{image.Height} Max size is {MaxImageSizeX}x{MaxImageSizeY}";
                _logger.LogError(errorMessage);
                return (false, errorMessage);
            }
        }
        
        var command = $"python niimprint -m {Model} -i {imagePath} -r {(int)Rotation} -c {Connection} -a {Address}";
        if (Verbose)
        {
            command += " -v";
        }

        _logger.LogInformation($"Executing command: {command}");
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = $"-c \"{command}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };

        var errorMessageBuilder = new StringBuilder();

        process.OutputDataReceived += (sender, args) => _logger.LogInformation(args.Data);
        process.ErrorDataReceived += (sender, args) => {
            _logger.LogError(args.Data);
            errorMessageBuilder.AppendLine(args.Data);
            } ;

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            return (false, errorMessageBuilder.ToString());
        }

        return (true, "");
    }
}
