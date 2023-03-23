using guacactings.Services;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Controllers;

public class JobOfferController : ControllerBase
{
    #region Fields

    private readonly IJobOfferService _jobOfferService;

    #endregion

    #region Constructor

    public JobOfferController(IJobOfferService jobOfferService)
    {
        _jobOfferService = jobOfferService;
    }

    #endregion

    #region Methods

    // Get all jobs
    [HttpGet(Name = "GetAllJobOffers")]
    public async Task<IActionResult> GetAllJobOffers(int page = 1, int rows = 10)
    {
        var result = await _jobOfferService.GetJobOffers(page, rows);
        return Ok(result);
    }
    
    #endregion
}