namespace Infrastructure.Model;

public partial class Client
{
    public long Id { get; }
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

public partial class Client
{
    public Client()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Number = 0;
        Address = string.Empty;
        Department = string.Empty;
        Location = string.Empty;
        CertifiedCompany = string.Empty;
        CertifiedName = string.Empty;
        ReportId = 0;
        
    }
    
    public Client(string firstName, string lastName, int number, string address, string department, string location, string certifiedCompany, string cetifiedName,long reportId)
    {
        FirstName = firstName;
        LastName = lastName;
        Number = number;
        Address = address;
        Department = department;
        Location = location;
        CertifiedCompany = certifiedCompany;
        CertifiedName = cetifiedName;
        ReportId = reportId;
    }
}