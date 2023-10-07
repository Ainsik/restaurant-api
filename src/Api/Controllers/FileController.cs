using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Api.Controllers;

[ApiController]
[Route("file")]
[Authorize]
public class FileController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetFile([FromQuery] string fileName)
    {
        var rootPath = Directory.GetCurrentDirectory();
        var filePath = $"{rootPath}/PrivateFiles/{fileName}";
        var fileExists = System.IO.File.Exists(filePath);

        if (!fileExists)
        {
            return NotFound();
        }

        var contentTypeProvider = new FileExtensionContentTypeProvider();
        contentTypeProvider.TryGetContentType(fileName, out var contentType);

        var fileContents = await System.IO.File.ReadAllBytesAsync(filePath);

        return File(fileContents, contentType, fileName);
    }

    [HttpPost]
    public async Task<ActionResult> Upload([FromForm] IFormFile file)
    {
        if (file.Length <= 0) return BadRequest();

        var rootPath = Directory.GetCurrentDirectory();
        var filePath = $"{rootPath}/PrivateFiles/{file.FileName}";

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return Ok();
    }
}