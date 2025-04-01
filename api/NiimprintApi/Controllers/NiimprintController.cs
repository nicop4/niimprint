using System.Net;
using Microsoft.AspNetCore.Mvc;
using NiimprintApi.Core;

namespace NiimprintApi.Controllers;

[Route("[controller]")]
public class NiimprintController : ControllerBase
{
    private readonly ILogger<NiimprintController> _logger;
    private readonly NiimbotB1_50x30 _printer50x30;
    private readonly NiimbotB1_60x40 _printer60x40;

    public NiimprintController(ILogger<NiimprintController> logger, NiimbotB1_50x30 printer50x30, NiimbotB1_60x40 printer60x40)
    {
        _printer50x30 = printer50x30;
        _printer60x40 = printer60x40;
        _logger = logger;
    }

    [HttpPost("PrintProductLabel/{imagePath}")]
    public ActionResult PrintProductLabel(string imagePath)
    {
        // Decode URL-encoded characters
        imagePath = Uri.UnescapeDataString(imagePath);
        
        _logger.LogInformation($"Print50x30 for product label- image {imagePath}");
        (bool result, string errorMessage) = _printer50x30.Print(imagePath);
        if (!result)
        {
            return BadRequest(errorMessage);
        }
        return Ok();
    }

    [HttpPost("PrintCustomerAddress/{imagePath}")]
    public ActionResult PrintCustomerAddress(string imagePath)
    {
        // Decode URL-encoded characters
        imagePath = Uri.UnescapeDataString(imagePath);
        
        _logger.LogInformation($"Print60x40 for customer address - image {imagePath}");
        (bool result, string errorMessage) = _printer60x40.Print(imagePath);
        if (!result)
        {
            return BadRequest(errorMessage);
        }
        return Ok();
    }
    [HttpPost("PrintStamp/{imagePath}")]
    public ActionResult PrintStamp(string imagePath)
    {
        // Decode URL-encoded characters
        imagePath = Uri.UnescapeDataString(imagePath);
        
        _logger.LogInformation($"Print60x40 for stamp - image {imagePath}");
        (bool result, string errorMessage) = _printer60x40.Print(imagePath);
        if (!result)
        {
            return BadRequest(errorMessage);
        }
        return Ok();
    }
}
