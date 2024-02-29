namespace PAMobile.Models.DTOs.Requests;

public class LoanApplicationRequest
{
    public int LoanPositionalNumber { get; set; }
    public string ApplicationText { get; set; }
    public int ClientCode { get; set; }
    public string ClientPhoneNumber { get; set; }
}

public class LoanApplicationRequestForGettingOverPayment : LoanApplicationRequest
{
    public string NumberOfCard { get; set; }
    public string TypeOfCard { get; set; }
}
