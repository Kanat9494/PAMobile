namespace PAMobile.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoansPage : ContentPage
{
	public LoansPage()
	{
		InitializeComponent();

		BindingContext = new LoansViewModel();

        //Task.Run(async () =>
        //{
        //    await LoadComponentsAsync();
        //});
	}

	private async Task LoadComponentsAsync()
	{
		await Task.Delay(1000);
		

		App.Current.Dispatcher.Dispatch(() =>
		{

            mainContent.Add(new CollectionView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,

                ItemTemplate = new DataTemplate(() =>
                {
                    Grid itemGrid = new Grid
                    {
                        RowDefinitions =
                        {
                            new RowDefinition {Height = new GridLength(40)},
                            new RowDefinition {Height = new GridLength(40)},
                            new RowDefinition {Height = new GridLength(40)},
                        },
                        ColumnDefinitions =
                        {
                            new ColumnDefinition {Width = new GridLength(50, GridUnitType.Star)},
                            new ColumnDefinition {Width = new GridLength(50, GridUnitType.Star)},
                        }
                    };

                    itemGrid.Add(new Label().Text("Номер договора").Font(size: 16, bold: true).Margins(10, 10, 0, 0), 0, 0);
                    itemGrid.Add(new Label()
                    {
                        HorizontalOptions = LayoutOptions.End
                    }.Font(size: 16, bold: true).Margins(0, 10, 10, 0).Bind(Label.TextProperty, "DG_NOM"), 1, 0);

                    itemGrid.Add(new Label().Text("Сумма кредита").Font(size: 16, bold: true).Margins(10, 10, 0, 0), 0, 1);
                    itemGrid.Add(new Label()
                    {
                        HorizontalOptions = LayoutOptions.End
                    }.Font(size: 16, bold: true).Margins(0, 10, 10, 0).Bind(Label.TextProperty, "DG_SUM"), 1, 1);

                    itemGrid.Add(new Label().Text("Валюта кредита").Font(size: 16, bold: true).Margins(10, 10, 0, 0), 0, 2);
                    itemGrid.Add(new Label()
                    {
                        HorizontalOptions = LayoutOptions.End
                    }.Font(size: 16, bold: true).Margins(0, 10, 10, 0).Bind(Label.TextProperty, "DG_KODV"), 1, 2);

                    

                    Frame itemFrame = new Frame
                    {
                        BorderColor = Colors.Transparent,
                        CornerRadius = 10,
                        BackgroundColor = Colors.White,
                        Content = itemGrid,
                    }.Height(120).Margins(0, 10, 0, 0).Paddings(0, 0, 0, 0);

                    //TapGestureRecognizer tap = new TapGestureRecognizer().Bind(TapGestureRecognizer.CommandProperty, (LoansViewModel vm) =>
                    //vm.LoanCommand).Bind(TapGestureRecognizer.CommandParameterProperty, "DG_POZN");
                    var tap = new TapGestureRecognizer();
                    tap.SetBinding(TapGestureRecognizer.CommandProperty, new Binding("TestCommand"));

                    itemFrame.GestureRecognizers.Add(tap);
                    return itemFrame;
                }),
            }.Margins(10, 10, 10, 10).Bind(CollectionView.ItemsSourceProperty, static (LoansViewModel vm) => vm.Loans));
        });//App.Current.Dispatcher.Dispatch
	}
}