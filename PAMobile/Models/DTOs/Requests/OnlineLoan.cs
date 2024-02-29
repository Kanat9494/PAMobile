namespace PAMobile.Models.DTOs.Requests;

internal class OnlineLoan
{
    public int ClientCode { get; set; }
    public string? FrontPassportPhotoPath { get; set; }
    public string? BackPassportPhotoPath { get; set; }
    public string? SelfiePath { get; set; }
    public byte[] FrontPassportData { get; set; }
    public byte[] BackPassportData { get; set; }
    public byte[] SelfieData { get; set; }
    public string? WhatsAppNumber { get; set; }
    public string? CardNumber { get; set; }
    public string PaymentType { get; set; }
    public int PaymentId { get; set; }
    public decimal? LoanAmount { get; set; }
    public int? LoanTerm { get; set; }
}
