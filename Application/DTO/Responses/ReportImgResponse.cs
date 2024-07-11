namespace Application.DTO.Responses;

public class ReportImgResponse
{
    public long Id { get; set; }
    public string FileName { get; set; }
    
    public string Image { get; set; }
    public long ReportId { get; set; }
}