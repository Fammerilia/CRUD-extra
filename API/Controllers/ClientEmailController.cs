using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DAL.Interfaces;
using BLL.DTOS.Emails;
using BLL;
using NLog;


[ApiController]
[Route("api/[controller]")]
public class ClientEmailController : ControllerBase
{
    private readonly IClientEmailService _clientService;
    private readonly Logger logger = LogManager.GetLogger("databaseUsers");

    public ClientEmailController(IClientEmailService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost("{clientId}/addEmail")]
    public async Task<IActionResult> AddEmail(int clientId, ClientEmailCreateDTO clientEmailCreateDTO)
    {
            await _clientService.AddEmail(clientEmailCreateDTO, clientId);
            logger.Info($"Email added for client {clientId}");
            return Ok("Email added successfully.");
    }

    [HttpDelete("{clientId},{clientEmailId}/deleteemail")]
    public async Task<IActionResult> DeleteClientEmail(int clientId, int clientEmailId)
    {
            logger.Info($"Successfully deleted Email {clientEmailId} for client {clientId}");
            await _clientService.DeleteClientEmail(clientId, clientEmailId);
            return NoContent();
    }
}
