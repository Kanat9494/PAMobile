namespace PAMobile.Models.DTOs.Responses;

public class DepositBalance : BaseResponse
{
    public decimal OstatokDeposit { get; set; }
    public DateTime DateLS { get; set; }
    public decimal SumPersent { get; set; }
    public string DepositNom { get; set; }
    public decimal ChangedPercent { get; set; }
}
