namespace PAMobile.Views.OnlineLoans;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class OLDetailsPage : ContentPage
{
	public OLDetailsPage()
	{
		InitializeComponent();

		BindingContext = _viewModel = new OLDetailsViewModel();
    }

	OLDetailsViewModel _viewModel;

	void OnSliderLoanSumValueChanged(object sender, ValueChangedEventArgs args)
	{
		_viewModel.OnSliderLoanSumValueChanged(args);
	}

    void OnSliderLoanTermValueChanged(object sender, ValueChangedEventArgs args)
    {
        _viewModel.OnSliderLoanTermValueChanged(args);
    }

	//async void TakePassport_Clicked(object sender, EventArgs e)
	//{
	//	if (MediaPicker.Default.IsCaptureSupported)
	//	{
	//		FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
	//		if (photo != null)
	//		{
	//			string localFilePath = System.IO.Path.Combine(FileSystem.CacheDirectory, photo.FileName);
	//			using (Stream sourceStream = await photo.OpenReadAsync())
	//			{
	//				using (FileStream localFileStream = File.OpenWrite(localFilePath))
	//				{
	//					await sourceStream.CopyToAsync(localFileStream);
	//					byte[] photoBytes;
	//					using (FileStream fileStream = File.OpenRead(localFilePath))
	//					{
	//						photoBytes = new byte[fileStream.Length];
	//						fileStream.Read(photoBytes, 0, photoBytes.Length);
	//					}
	//					//ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(photoBytes));
	//					TestImg.Source = ImageSource.FromFile(localFilePath);
	//				}
	//			}
	//		}
	//	}
	//}
}