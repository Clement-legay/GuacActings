using guacactings.Models;
using guacactings.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/address")]
[ApiVersion("1")]
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
    [Authorize(Roles = "visitor, admin")]
    public async Task<IActionResult> GetAddresses(int page = 1, int rows = 10)
    {
        var result = await _addressService.GetAddresses(page, rows);
        return Ok(result);
    }

    [HttpGet("{id:int}", Name = "GetAddressById")]
    [Authorize(Roles = "visitor, admin")]
    public async Task<IActionResult> GetAddressById(int id)
    {
        var result = await _addressService.GetAddressById(id);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost(Name = "AddAddress")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> AddAddress([FromForm] AddressRegistryDto address)
    {
        var result = await _addressService.AddAddress(address);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut("{id:int}/update", Name = "UpdateAddress")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateAddress([FromForm] AddressRegistryDto address, int id)
    {
        var result = await _addressService.UpdateAddress(address, id);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}/delete", Name = "DeleteAddress")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        var result = await _addressService.DeleteAddress(id);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    #endregion
}