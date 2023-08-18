using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Security;
using AutoMapper;
using API;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using DAL;
using BLL.DTOS.Order;
using BLL;
using NLog;
using API.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ApplicationDbContext _dbContext;
    private readonly Logger logger = LogManager.GetLogger("databaseUsers");


    public OrderController(IOrderService orderService, ApplicationDbContext dbContext)
    {
        _orderService = orderService;
        _dbContext = dbContext;
    }

    [HttpGet("{clientId}/orders")]
    public IActionResult GetOrdersByClient(int clientId)
    {
            var orders = _orderService.GetOrdersByClient(clientId);
            logger.Info($"Successfully found client {clientId} orders");
            return Ok(orders);
    }

    [HttpPost("addorder")]
    public async Task<IActionResult> AddOrder(OrderCreateDTO orderCreateDTO, int clientId)
    {
            var client = await _dbContext.Clients.FindAsync(clientId);
            await _orderService.AddOrder(orderCreateDTO, clientId);
            logger.Info($"Successfully added Order for client {clientId}");
            return Ok("Order added successfully.");
}
    [HttpPut("Change status")]

    public async Task<IActionResult> ChangeStatus(StatusChangeDTO statusChangeDTO) {
            var order = _orderService.GetOrderById(statusChangeDTO.OrderId);

            order.OrderStatus = statusChangeDTO.OrderStatus;
            await _orderService.ChangeStatus(order);
            logger.Info($"Status changed for order {statusChangeDTO.OrderId} to status {statusChangeDTO.OrderStatus})");
            return Ok("Status updated");
    
    }

}