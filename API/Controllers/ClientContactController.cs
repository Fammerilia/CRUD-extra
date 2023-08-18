using BLL.DTOS.Contacts;
using BLL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

[ApiController]
[Route("api/[controller]")]
public class ClientContactController : ControllerBase
{
    private readonly IClientContactService _clientService;
    private readonly Logger logger = LogManager.GetLogger("databaseUsers");

    public ClientContactController(IClientContactService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost("{clientId}/addContact")]
    public async Task<IActionResult> AddContact(int clientId, ClientContactCreateDTO clientContactCreateDTO)
    {
            await _clientService.AddContact(clientContactCreateDTO, clientId);
            logger.Info($"Contact added for client {clientId}");
            return Ok("Contact added successfully.");
    }

    [HttpDelete("{clientId},{clientContactId}/deletecontact")]
    public async Task<IActionResult> DeleteClientContact(int clientId, int clientContactId)
    {
            await _clientService.DeleteClientContact(clientId, clientContactId);
            logger.Info($"successfully deleted contact {clientContactId} for client {clientId}");
            return Ok("Deleted");
    }
    
    }
