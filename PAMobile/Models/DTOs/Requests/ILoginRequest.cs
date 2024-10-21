namespace PAMobile.Models.DTOs.Requests;

internal class ILoginRequest
{
    public string? KlLogin { get; set; }
    public string? KlPassword { get; set; }
    public string? FCMToken { get; set; }
    public int? OS { get; set; }
}

