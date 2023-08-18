using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ClientContact
{

    public int ClientContactId { get; set; }
    public string EmpContact { get; set; }
    public int Contact_type { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }

}
