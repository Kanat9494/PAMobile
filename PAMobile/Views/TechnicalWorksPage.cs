namespace PAMobile.Views;

public class TechnicalWorksPage : ContentPage
{
	public TechnicalWorksPage()
	{
		Shell.SetNavBarIsVisible(this, false);
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Сервис временно не доступен"
				}.Font(size: 18, bold: true).Margins(10, 10, 10, 10),

				new Label
				{
                    HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center
                }.Text("Уважаемые клиенты, мы проводим технические работы в целях улучшения функциональности наших сервисов. Благодарим" +
				" за понимание и приносим извиниения за временные неудобства. Сервис будет доступен в ближайшее время").Margins(10, 10, 10, 10),
			}
		};
	}

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}