using guacactings.Models;
using guacactings.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/document-type")]
[ApiVersion("1")]
public class DocumentTypeController : ControllerBase
{
    #region Fields

    private readonly IDocumentTypeService _documentTypeService;

    #endregion
    
    #region Constructor

    public DocumentTypeController(IDocumentTypeService documentTypeService)
    {
        _documentTypeService = documentTypeService;
    }
    
    #endregion

    #region Methods
    
    // Get all document types
    [HttpGet(Name = "GetAllDocumentTypes")]
    [Authorize(Roles = "visitor, admin")]
    public async Task<IActionResult> GetDocumentTypes(int page = 1, int rows = 10)
    {
        var result = await _documentTypeService.GetDocumentTypes(page, rows);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    // Get document type by id
    [HttpGet("{id:int}", Name = "GetDocumentTypeById")]
    [Authorize(Roles = "visitor, admin")]
    public async Task<IActionResult> GetDocumentTypeById(int id)
    {
        var result = await _documentTypeService.GetDocumentTypeById(id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    [HttpPost(Name = "AddDocumentType")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> AddDocumentType([FromForm] DocumentTypeRegistryDto documentType)
    {
        var result = await _documentTypeService.AddDocumentType(documentType);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    // Get document type by id
    [HttpPut("{id:int}/update", Name = "UpdateDocumentType")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateDocumentType([FromForm] DocumentTypeRegistryDto documentType, int id)
    {
        var result = await _documentTypeService.UpdateDocumentType(documentType, id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    // Delete a document type
    [HttpDelete("{id:int}/delete", Name = "DeleteDocumentType")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteDocumentType(int id)
    {
        var result = await _documentTypeService.DeleteDocumentType(id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    

    #endregion
}