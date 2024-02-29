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
        using (HttpClient httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri(PAConstants.SERVER_ROOT_URL);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);

            try
            {
                var response = await httpClient.GetStringAsync(httpClient.BaseAddress + requestUrl);
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
    }

    public async Task<TResponse> GetItemAsync2<TResponse>(string requestUrl)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri(PAConstants.SERVER_ROOT_URL);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);

            try
            {
                var response = await httpClient.GetStringAsync(httpClient.BaseAddress + requestUrl);
                TResponse result = JsonConvert.DeserializeObject<TResponse>(response);

                return result;
            }
            catch (Exception ex)
            {
                var result = Activator.CreateInstance<TResponse>();

                return result;
            }
        }
    }

    public async Task<IEnumerable<TResponse>> GetItemsAsync<TResponse>(string requestUrl)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri(PAConstants.SERVER_ROOT_URL);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress + requestUrl);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await Shell.Current.GoToAsync("//MainPage");
                    return null;
                }
                var content = await response.Content.ReadAsStringAsync();
                if (content == null || string.IsNullOrEmpty(content))
                {

                }
                IEnumerable<TResponse> result = JsonConvert.DeserializeObject<IEnumerable<TResponse>>(content);
                return result;
            }
            catch
            {
                return null;
            }
        }
    }

    public async Task<int> PostItemAsync<TReqeust>(TReqeust request, string url)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri(PAConstants.SERVER_ROOT_URL);
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);

            try
            {
                var response = await httpClient.PostAsync(url, content);
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<int>(jsonResult);

                return result;
            }
            catch
            {
                return 0;
            }
        }
    }

    public async Task<int> PostStreamAsync<TRequest>(TRequest request, string url)
    {
        try
        {
            var longString = JsonConvert.SerializeObject(request);

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(longString)))
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);

                    httpClient.BaseAddress = new Uri(PAConstants.SERVER_ROOT_URL);

                    // Создание HttpRequestMessage с содержимым стрима
                    //using (HttpRequestMessage request2 = new HttpRequestMessage(HttpMethod.Post, url))
                    //{
                    //    request2.Content = new StreamContent(stream);

                    //    // Установка Content-Type и других заголовков при необходимости
                    //    request2.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                    //    // Отправка запроса на сервер
                    //    HttpResponseMessage response = await client.SendAsync(request2);

                    //    // Обработка ответа от сервера
                    //    if (response.IsSuccessStatusCode)
                    //    {
                    //        return 1;
                    //    }
                    //    else
                    //    {
                    //        return 0;
                    //    }
                    //}
                    var response = await httpClient.PostAsync(url, new StreamContent(stream));
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<int>(jsonResult);

                    return result;
                }
            }
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
}
