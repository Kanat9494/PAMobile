namespace PAMobile.Views;

public class LoansPage2 : ContentPage
{
	public LoansPage2()
	{
        var loanInfoSL = new StackLayout
        {
            new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label{HorizontalOptions = LayoutOptions.Start}.Text("Номер договора").Font(size: 16, bold: true)
                    .Margin(new Thickness(10, 10, 0, 0)),
                    new Label{ HorizontalOptions = LayoutOptions.EndAndExpand}.Bind(Label.TextProperty, "DG_NOM")
                    .Font(size: 16, bold: true).Margin(new Thickness(0, 10, 10, 0)),
                }
            },
            new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label{HorizontalOptions = LayoutOptions.Start}.Text("Сумма кредита").Font(size: 16, bold: true)
                    .Margin(new Thickness(10, 10, 0, 0)),
                    new Label{ HorizontalOptions = LayoutOptions.EndAndExpand}.Bind(Label.TextProperty, "DG_SUM")
                    .Font(size: 16, bold: true).Margin(new Thickness(0, 10, 10, 0)),
                }
            },
            new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label{HorizontalOptions = LayoutOptions.Start}.Text("Валюта кредита").Font(size: 16, bold: true)
                    .Margin(new Thickness(10, 10, 0, 0)),
                    new Label{ HorizontalOptions = LayoutOptions.EndAndExpand}.Bind(Label.TextProperty, "DG_KODV")
                    .Font(size: 16, bold: true).Margin(new Thickness(0, 10, 10, 0)),
                }
            },
        };
        loanInfoSL.GestureRecognizers.Add(new TapGestureRecognizer().Bind(TapGestureRecognizer.CommandProperty, static (LoansViewModel vm) 
        => vm.LoanCommand)
            );

        Content = new StackLayout
		{
			BackgroundColor = Color.FromArgb("#f2f2f2"),
			Children =
			{
                new Label().Text("Кредиты").Margins(0, 25, 0, 0).CenterHorizontal().Font(size: 22, bold: true),
                new ActivityIndicator
				{
					Color = Color.FromArgb("00e600"),
				}.Margins(0, 20, 0, 0).Bind(ActivityIndicator.IsRunningProperty, static (LoansViewModel vm) =>
				vm.IsBusy)
				.Bind(ActivityIndicator.IsVisibleProperty, static (LoansViewModel vm) => vm.IsBusy)
				.Bind(ActivityIndicator.IsRunningProperty, static (LoansViewModel vm) => vm.IsBusy),//ActivityIndicator
				new CollectionView
				{
					VerticalOptions = LayoutOptions.FillAndExpand,
					ItemTemplate = new DataTemplate(() =>
					{
						return new Border
						{
							BackgroundColor = Color.FromArgb("#fff"),
							HeightRequest = 120,
							StrokeShape = new RoundRectangle
							{
								CornerRadius = new CornerRadius(10)
							},
							StrokeThickness = 0,
							Content = loanInfoSL,//StackLayout
                        }.Margin(new Thickness(10));
					}),
				}.Bind(CollectionView.ItemsSourceProperty, static (LoansViewModel vm) => vm.Loans).Margin(new Thickness(10)),
			}
		};//StackLayout

		BindingContext = new LoansViewModel();
	}
}