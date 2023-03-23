using guacactings.Services;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Controllers;

public class JobController : ControllerBase
{
    #region Fields

    private readonly IJobService _jobService;

    #endregion

    #region Constructor

    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }

    #endregion

    #region Methods

    // Get all jobs
    [HttpGet(Name = "GetAllJobs")]
    public async Task<IActionResult> GetAllJobs(int page = 1, int rows = 10)
    {
        var result = await _jobService.GetJobs(page, rows);
        return Ok(result);
    }
    
    #endregion
}