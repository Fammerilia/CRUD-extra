using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string EmpName { get; set; }

        public string EmpSurname { get; set; }

        public string EmpMiddlename { get; set; }
        public int Status { get; set; }
        public int DiscountType { get; set; }
        ICollection<Order> orders { get; set; }
        public List<ClientContact> ClientContacts { get; set; }
        public List<ClientAddresses> ClientAddresses { get; set; }
        public List<ClientEmail> ClientEmails { get; set; }


    }
}