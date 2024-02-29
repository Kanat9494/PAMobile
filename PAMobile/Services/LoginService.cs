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
            KlPassword = password
        };

        //var devSslHelper = new 

        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri(PAConstants.SERVER_ROOT_URL);

            var content = new StringContent(JsonConvert.SerializeObject(requestUser), Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync("api/ILogin/AuthenticateUser", content);
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
}
