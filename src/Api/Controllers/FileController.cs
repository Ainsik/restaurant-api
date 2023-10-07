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
}