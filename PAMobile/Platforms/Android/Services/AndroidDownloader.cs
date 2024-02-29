namespace PAMobile.Platforms.Android.Services;

internal class AndroidDownloader
{
    public event EventHandler<DownloadEventArgs> OnFileDownloaded;

    public async void DonwloadFile(string url, string folder)
    {
        string pathToNewFolder = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), folder);
        Directory.CreateDirectory(pathToNewFolder);

        try
        {
            //using (var httpClient = new HttpClient())
            //{
            //    var response = await httpClient.GetAsync(url);
            //    response.EnsureSuccessStatusCode();

            //    var contentStream = await response.Content.ReadAsStreamAsync();

            //    using 
            //}
        }
        catch
        {
            if (OnFileDownloaded != null)
                OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));
        }
    }
}
