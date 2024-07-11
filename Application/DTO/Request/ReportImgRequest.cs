namespace Application.DTO.Request;

public class ReportImgRequest
{
    public string FileName { get; set; }
    public string Image { get; set; }
    public long ReportId { get; set; }
}