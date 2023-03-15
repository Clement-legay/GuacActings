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
    public async Task<IEnumerable<Address>> GetAddresses()
    {
        return await _addressService.GetAddresses();
    }

    [HttpPost(Name = "AddAddress")]
    public async Task<Address> AddAddress(Address address)
    {
        return await _addressService.AddAddress(address);
    }

    #endregion
}