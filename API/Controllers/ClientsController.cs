
        using Microsoft.AspNetCore.Authorization;
        using Microsoft.AspNetCore.Mvc;
        using System.ComponentModel.DataAnnotations;
        using System.Net.Security;
        using AutoMapper;
        using API;
        using DAL.Interfaces;
        using BLL.DTOS.Contacts;
        using BLL;
using NLog;
using BLL.DTOS.Client;
using FluentValidation;

[ApiController]
        [Route("api/[controller]")]
        public class ClientsController : ControllerBase
    {
    private readonly IValidator<ClientCreateDTO> _clientValidator;
    private readonly IClientService _clientService;
        private readonly Logger logger = LogManager.GetLogger("databaseUsers");

    public ClientsController(IClientService clientService, IValidator<ClientCreateDTO> clientValidator)
        {
            _clientService = clientService;
        _clientValidator = clientValidator;

    }

    [HttpPost("add")]
        public IActionResult AddClient(ClientCreateDTO clientCreateDTO)
        {
            var client = _clientService.AddClient(clientCreateDTO);
            logger.Info("Client succesfully created");
            return Ok(client);
        }

    [HttpGet]
    public IActionResult SearchClients(
        [FromQuery] string? empName,
        [FromQuery] string? empSurname,
        [FromQuery] string? empMiddlename,
        [FromQuery] string? contact,
        [FromQuery] string? address,
        [FromQuery] string? email,
        [FromQuery] string? orderIdPartial,
        [FromQuery] string? orderDescription,
        [FromQuery] int? empSex,
        [FromQuery] int? status,
        [FromQuery] int? discountType)
    {
            var searchResults = _clientService.SearchClients(
                empName,
                empSurname,
                empMiddlename,
                contact,
                address,
                email,
                orderDescription,
                orderIdPartial,
                empSex,
                status,
                discountType);
            logger.Info("Successfully found client by parameters");

            return Ok(searchResults);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteClient(int clientId)
        {
            var client = new Client { ClientId = clientId };
            await _clientService.DellClient(client);
            logger.Info($"Successfully deleted client {clientId}");
            return Ok("Client Deleted");
        }

        [HttpGet("{clientId}")]
        public IActionResult GetClient(int clientId)
        {

            var clientDTO = _clientService.GetClient(clientId);
            logger.Info($"Successfully found client {clientId}");
            return Ok(clientDTO);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateClient(ClientUpdateDTO clientUpdateDTO)
        {
        var client = _clientService.GetClientById(clientUpdateDTO.ClientId);


            client.EmpName = clientUpdateDTO.EmpName;
            client.EmpMiddlename = clientUpdateDTO.EmpMiddlename;
            client.EmpSurname = clientUpdateDTO.EmpSurname;
            client.EmpSex = clientUpdateDTO.EmpSex;
            client.Status = clientUpdateDTO.Status;
            client.DiscountType = clientUpdateDTO.DiscountType;
            await _clientService.UpdateClient(client);
            logger.Info($"Successfully updated client {clientUpdateDTO.ClientId}");
            return Ok("Client updated successfully.");
        }
    }
