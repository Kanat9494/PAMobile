namespace PAMobile.Services;

internal class LoginService
{
    public LoginService()
    {
    }

    private static LoginService _instance;
    public static LoginService Instance()
    {
        if (_instance == null)
            _instance = new LoginService();

        return _instance;
    }

    public async Task<ILoginResponse> AuthenticateUser(string userName, string password)
    {
        var requestUser = new ILoginRequest
        {
            KlLogin = userName,
            KlPassword = password,
            FCMToken = "ios token",
            OS = 2
        };

        try
        {

            PAConstants.MSHC.DefaultRequestHeaders.Clear();
            var content = new StringContent(JsonConvert.SerializeObject(requestUser), Encoding.UTF8, "application/json");
            var response = await PAConstants.MSHC.PostAsync("api/ILogin/AuthenticateUser", content);
            var result = await response.Content.ReadAsStringAsync();
            var authenticatedUser = JsonConvert.DeserializeObject<ILoginResponse>(result);
            return authenticatedUser;
        }
        catch (Exception ex)
        {
            return new ILoginResponse
            {
                StatusCode = 500,
                ResponseMessage = ex.Message,
            };
        }
    }
}
