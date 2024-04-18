namespace PAMobile.Models.DTOs.Responses;

internal class DigitalDocument
{
    public int Id { get; set; }
    public int DG_POZN { get; set; }
    public string NameFile { get; set; }
    public int Status { get; set; }
    public int TypeDoc { get; set; }
    public string TextStatus { get; set; }
    public string TextTypeDoc { get; set; }
    public bool? CanSign { get; set; }
}
