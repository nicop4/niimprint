using System.Net;
using Microsoft.AspNetCore.Mvc;
using NiimprintApi.Core;

namespace NiimprintApi.Controllers;

[Route("[controller]")]
public class NiimprintController : ControllerBase
{
    private readonly ILogger<NiimprintController> _logger;
    private readonly NiimbotB1_Address _printerAddress;
    private readonly NiimbotB1_Product _printerProduct;
    private readonly NiimbotB1_Stamp _printerStamp;

    public NiimprintController(ILogger<NiimprintController> logger, NiimbotB1_Address printerAddress, NiimbotB1_Product printerProduct, NiimbotB1_Stamp printerStamp)
    {
        _printerAddress = printerAddress;
        _printerProduct = printerProduct;
        _printerStamp = printerStamp;
        _logger = logger;
    }

    [HttpPost("PrintProductLabel/{imagePath}")]
    public ActionResult PrintProductLabel(string imagePath)
    {
        // Decode URL-encoded characters
        imagePath = Uri.UnescapeDataString(imagePath);
        
        _logger.LogInformation($"Print50x30 for product label- image {imagePath}");
        (bool result, string errorMessage) = _printerProduct.Print(imagePath);
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
        (bool result, string errorMessage) = _printerAddress.Print(imagePath);
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
        (bool result, string errorMessage) = _printerStamp.Print(imagePath);
        if (!result)
        {
            return BadRequest(errorMessage);
        }
        return Ok();
    }
}
