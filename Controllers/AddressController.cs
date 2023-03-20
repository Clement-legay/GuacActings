using guacactings.Models;
using guacactings.Services;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : ControllerBase
{
    #region Fields

    private readonly IAddressService _addressService;

    #endregion

    #region Constructor

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    #endregion
    
    #region Methods

    /// <summary>
    /// Returns all addresses
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = "GetAddresses")]
    public async Task<IActionResult> GetAddresses(int page = 1, int rows = 10)
    {
        var result = await _addressService.GetAddresses(page, rows);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPost(Name = "AddAddress")]
    public async Task<IActionResult> AddAddress(AddressRegistryDto address)
    {
        var result = await _addressService.AddAddress(address);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut("{id:int}/update", Name = "UpdateAddress")]
    public async Task<IActionResult> UpdateAddress(AddressRegistryDto address, int id)
    {
        var result = await _addressService.UpdateAddress(address, id);

        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    #endregion
}