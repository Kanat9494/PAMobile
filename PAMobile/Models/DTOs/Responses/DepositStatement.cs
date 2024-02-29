namespace PAMobile.Models.DTOs.Responses;

internal class DepositStatement : BaseResponse
{
    public DateTime DV_DATE { get; set; }
    public decimal DepositAmount { get; set; }
    public decimal PayDeposit { get; set; }
    public decimal OstatokDeposit { get; set; }
}
