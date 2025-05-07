using System;

namespace CleanArchitecture.Domain.Models
{
    public class Partner
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string TaxId { get; set; }
        public string Type { get; set; } // SUPPLIER, CUSTOMER, BOTH
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedById { get; set; }
        public User? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 