namespace Application.DTO.Responses;

public class ClientResponse
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    
    public long ReportId { get; set; }
    public string LastName { get; set; }
    public int Number { get; set; }
    public string Address { get; set; }
    public string Department { get; set; }
    public string Location { get; set; }
    public string CertifiedCompany { get; set; }
    public string CertifiedName { get; set; }
}