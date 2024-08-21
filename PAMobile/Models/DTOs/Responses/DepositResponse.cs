namespace PAMobile.Models.DTOs.Responses;

public class DepositResponse
{
    public DateTime DV_DATE1 { get; set; }
    public DateTime? DV_DATE2 { get; set; }
    public string DV_NOM { get; set; }
    public decimal? DV_SUM { get; set; }
    public int DV_POZN { get; set; }
    public string DV_LC { get; set; }
    public string V_SIMV { get; set; }
    public int? DV_KODB { get; set; }
    public string DV_VIDVKL { get; set; }
    public short? DV_SROK { get; set; }
    public decimal? VK_STAVVKL { get; set; }
    public string VK_NAME { get; set; }
    public decimal OstatokDeposit { get; set; }
    public decimal SumPersent { get; set; }
}
