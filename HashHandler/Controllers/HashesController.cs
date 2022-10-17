using HashHandler.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HashHandler.Controllers;

[ApiController]
[Route("[controller]")]
public class HashesController : ControllerBase
{
    private readonly IHashService _hashService;

    public HashesController(IHashService hashService)
    {
        _hashService = hashService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var hashes = await _hashService.GetHashesAsync(cancellationToken);
        return Ok(hashes);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CancellationToken cancellationToken)
    {
        await _hashService.GenerateAndSendAsync(cancellationToken);
        return Ok();
    }
}