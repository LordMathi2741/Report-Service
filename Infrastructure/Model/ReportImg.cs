namespace Infrastructure.Model;

public partial class ReportImg
{
    public long Id { get; }
    public string FileName { get; set; }
    
    public string Image { get; set; }
    
    public Report Report { get; private set; }
    
    public long ReportId { get; set; }
}

public partial class ReportImg
{
    public ReportImg()
    {
        FileName = string.Empty;
        Image = string.Empty;
    }

    public ReportImg(string fileName,string image, long reportId)
    {
        FileName = fileName;
        Image = image;
        ReportId = reportId;
    }
    
}