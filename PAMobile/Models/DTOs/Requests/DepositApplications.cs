namespace PAMobile.Models.DTOs.Requests;

internal class DepositApplications
{
}

public class DepositApplication
{
    public int DepositPostionalNumber { get; set; }
    public string ApplicationText { get; set; }
    public int ClientCode { get; set; }
    public string BICAccount { get; set; }
    public string PaymentAccount { get; set; }
    public string CardNumber { get; set; }
}
