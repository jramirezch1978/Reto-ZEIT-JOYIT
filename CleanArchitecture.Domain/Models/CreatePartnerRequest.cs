namespace CleanArchitecture.Domain.Models;

public class CreatePartnerRequest
{
    public string RazonSocial { get; set; }
    public string TaxId { get; set; }
    public string Type { get; set; }
    public string ContactName { get; set; }
    public string ContactEmail { get; set; }
    public string ContactPhone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    public bool IsActive { get; set; }
} 