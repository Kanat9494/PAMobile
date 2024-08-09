namespace PAMobile.Models.DTOs.Responses;

public class LoanResponse
{
    public int DG_POZN { get; set; }
    public string? DG_NOM { get; set; }
    public decimal? DG_SUM { get; set; }
    public int? KODB { get; set; }
    public string? DG_KODV { get; set; }
    public DateTime? DG_DATE1 { get; set; }
    public bool CanSign { get; set; }
    public decimal? PricipalBalance { get; set; }
    public decimal? PercentBalance { get; set; }
}
