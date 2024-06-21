namespace PAMobile.Services;

internal class ContentService
{
    public ContentService(string token)
    {
        _accessToken = token;
    }

    string _accessToken;
    private static ContentService _instance;

    public static ContentService Instance(string token)
    {
        if (_instance == null)
            _instance = new ContentService(token);

        return _instance;
    }

    public async Task<TResponse> GetItemAsync<TResponse>(string requestUrl) where TResponse : BaseResponse
    {
        //using (HttpClient httpClient = new HttpClient())
        //{
        //    if (_accessToken == null)
        //    {
        //        _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
        //    }
        //    httpClient.BaseAddress = new Uri(PAConstants.SERVER_ROOT_URL);
        //    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);

        //    try
        //    {
        //        var response = await httpClient.GetStringAsync(httpClient.BaseAddress + requestUrl);
        //        TResponse result = JsonConvert.DeserializeObject<TResponse>(response);

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        var result = Activator.CreateInstance<TResponse>();
        //        result.StatusCode = 500;
        //        result.ResponseMessage = ex.Message;

        //        return result;
        //    }
        //}
        try
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
            {
                _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken") ?? "";
            }
            PAConstants.MSHC.DefaultRequestHeaders.Clear();
            PAConstants.MSHC.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            PAConstants.MSHC.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);

            var response = await PAConstants.MSHC.GetStringAsync(requestUrl);

            TResponse result = JsonConvert.DeserializeObject<TResponse>(response);

            return result;
        }
        catch (Exception ex)
        {
            var result = Activator.CreateInstance<TResponse>();
            result.StatusCode = 500;
            result.ResponseMessage = ex.Message;

            return result;
        }
    }

    public async Task<TResponse> GetItemAsync2<TResponse>(string requestUrl)
    {
        //using (HttpClient httpClient = new HttpClient())
        //{
        //    if (_accessToken == null)
        //    {
        //        _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
        //    }
        //    httpClient.BaseAddress = new Uri(PAConstants.SERVER_ROOT_URL);
        //    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);

        //    try
        //    {
        //        var response = await httpClient.GetStringAsync(httpClient.BaseAddress + requestUrl);
        //        TResponse result = JsonConvert.DeserializeObject<TResponse>(response);

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        var result = Activator.CreateInstance<TResponse>();

        //        return result;
        //    }
        //}
        try
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
            {
                _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken") ?? "";
            }
            PAConstants.MSHC.DefaultRequestHeaders.Clear();
            PAConstants.MSHC.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            PAConstants.MSHC.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);

            var response = await PAConstants.MSHC.GetStringAsync(requestUrl);

            TResponse result = JsonConvert.DeserializeObject<TResponse>(response);

            return result;
        }
        catch (Exception ex)
        {
            var result = Activator.CreateInstance<TResponse>();

            return result;
        }
    }

    public async Task<IEnumerable<TResponse>> GetItemsAsync<TResponse>(string requestUrl)
    {
        //using (HttpClient httpClient = new HttpClient())
        //{
        //    if (_accessToken == null)
        //    {
        //        _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
        //    }
        //    httpClient.BaseAddress = new Uri(PAConstants.SERVER_ROOT_URL);
        //    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);

        //    try
        //    {
        //        HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress + requestUrl);
        //        if (response.StatusCode == HttpStatusCode.Unauthorized)
        //        {
        //            //await Shell.Current.GoToAsync("PinPage");
        //            return null;
        //        }
        //        var content = await response.Content.ReadAsStringAsync();
        //        if (content == null || string.IsNullOrEmpty(content))
        //        {

        //        }
        //        IEnumerable<TResponse> result = JsonConvert.DeserializeObject<IEnumerable<TResponse>>(content);
        //        return result;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        try
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
            {
                _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken") ?? "";
            }
            PAConstants.MSHC.DefaultRequestHeaders.Clear();
            PAConstants.MSHC.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            PAConstants.MSHC.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);

            HttpResponseMessage response = await PAConstants.MSHC.GetAsync(requestUrl);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                //await Shell.Current.GoToAsync("PinPage");
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            IEnumerable<TResponse> result = JsonConvert.DeserializeObject<IEnumerable<TResponse>>(content);
            return result;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<int> PostItemAsync<TReqeust>(TReqeust request, string url)
    {
        //using (var httpClient = new HttpClient())
        //{
        //    if (_accessToken == null)
        //    {
        //        _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken");
        //    }
        //    httpClient.BaseAddress = new Uri(PAConstants.SERVER_ROOT_URL);
        //    var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        //    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);

        //    try
        //    {
        //        var response = await httpClient.PostAsync(url, content);
        //        var jsonResult = await response.Content.ReadAsStringAsync();
        //        var result = JsonConvert.DeserializeObject<int>(jsonResult);

        //        return result;
        //    }
        //    catch 
        //    {
        //        return 0;
        //    }
        //}
        try
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
            {
                _accessToken = await SecureStorage.Default.GetAsync("UserAccessToken") ?? "";
            }
            PAConstants.MSHC.DefaultRequestHeaders.Clear();
            PAConstants.MSHC.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            PAConstants.MSHC.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await PAConstants.MSHC.PostAsync(url, content);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(jsonResult);

            return result;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public async Task<int> PostStreamAsync<TRequest>(TRequest request, string url)
    {
        try
        {
            var longString = JsonConvert.SerializeObject(request);

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(longString)))
            {
                //using (HttpClient httpClient = new HttpClient())
                //{
                //    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);

                //    httpClient.BaseAddress = new Uri(PAConstants.SERVER_ROOT_URL);
                //    var response = await httpClient.PostAsync(url, new StreamContent(stream));
                //    var jsonResult = await response.Content.ReadAsStringAsync();
                //    var result = JsonConvert.DeserializeObject<int>(jsonResult);

                //    return result;
                //}
                PAConstants.MSHC.DefaultRequestHeaders.Clear();
                PAConstants.MSHC.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                PAConstants.MSHC.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);
                var response = await PAConstants.MSHC.PostAsync(url, new StreamContent(stream));
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<int>(jsonResult);

                return result;
            }
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
}
