using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ClientEmail
{
    public int ClientEmailId { get; set; }
    public string EmpEmail { get; set; }
    public int Email_type { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }

}
