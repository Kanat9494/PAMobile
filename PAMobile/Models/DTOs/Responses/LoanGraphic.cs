namespace PAMobile.Models.DTOs.Responses;

internal class LoanGraphic
{
    public int Id { get; set; }
    public string DateRepayment { get; set; }
    public double PayMonth { get; set; }
    public double BasicSum { get; set; }
    public double PercentSum { get; set; }
    public double NCPSum { get; set; }
    public double CurrentSum { get; set; }
    public double AccumulatedSum { get; set; }
    public double OstatokCreditSum { get; set; }
    public double AccumulateOstatokSum { get; set; }
}
