using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{

    public class ClientAddresses
    {

        public int ClientAddressId { get; set; }
        public int ClientId { get; set; }

        public string EmpAddress { get; set; }

        public int Address_type { get; set; }

        public Client Client { get; set; }


    }
}