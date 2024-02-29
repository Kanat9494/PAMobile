namespace PAMobile.Utils;

internal class LocalNotificationHelper
{
    internal static async Task<string> DownloadAndNotify(string url, string accessToken)
    {
        try
        {
            await Shell.Current.DisplayAlert("", "Файл будет скачан на ваш телефон", "Ок");
            var excelBytes = await ContentService.Instance(accessToken).GetItemAsync2<byte[]>(url);

            var filePath = await FileHelper.SaveFileAsync(excelBytes, ".pdf");

            var request = new NotificationRequest
            {
                NotificationId = 1337,
                Title = "Файл скачан",
                Subtitle = "Можете посмотреть",
                BadgeNumber = 42,
                ReturningData = filePath,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(10)
                }
            };

            //await LocalNotificationCenter.Current.Show(request);


            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            string text = "Файл скачивается...";

            //var snackbar = Snackbar.Make("Файл скачивается...", null, "Ок", TimeSpan.FromSeconds(2), new SnackbarOptions
            //{
            //    BackgroundColor = Color.FromRgba(0, 255, 0, 185),
            //    TextColor = Colors.White
            //});

            //await snackbar.Show();
            return filePath;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("", $"Не удалось загрузить файл, попробуйте еще раз: {ex.Message}", "Ок");
            return "";
        }
    }
}
