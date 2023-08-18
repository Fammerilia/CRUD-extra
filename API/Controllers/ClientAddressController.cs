using BLL.DTOS.Addresses;
using BLL;
using Microsoft.Extensions.Logging;
using NLog;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClientAddressController : ControllerBase
{
    private readonly IClientAddressService _clientService;
    private readonly Logger logger = LogManager.GetLogger("databaseUsers");

    public ClientAddressController(IClientAddressService clientService)
    {
        _clientService = clientService;
    }
    [HttpPost("{clientId}/addaddress")]
    public async Task<IActionResult> AddAddress(int clientId, ClientAddressCreateDTO clientAddressCreateDTO)
    {

            await _clientService.AddAddress(clientAddressCreateDTO, clientId);
            logger.Info($"Address added for client with ID {clientId}.");
            return Ok("Address added successfully.");
    }

    [HttpDelete("{clientId},{clientAddressId}/deleteaddress")]
    public async Task<IActionResult> DeleteClientAddress(int clientId, int clientAddressId)
    {
            await _clientService.DeleteClientAddress(clientId, clientAddressId);
            logger.Info($"Address with ID {clientAddressId} deleted for client with ID {clientId}.");
            return Ok($"Address {clientAddressId} deleted for client");
    }
}
