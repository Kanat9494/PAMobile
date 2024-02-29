namespace PAMobile.Models.DTOs.Responses;

public class ILoginResponse : BaseResponse
{
    public int? KlKod { get; set; }
    public string? Pin { get; set; }
    public string? KlLogin { get; set; }
    public string? Fio { get; set; }
    public string AccessToken { get; set; }
    public string? KlPhone { get; set; }
}
