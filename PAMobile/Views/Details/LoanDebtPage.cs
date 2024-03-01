namespace PAMobile.Views.Details;

public class LoanDebtPage : ContentPage
{
	public LoanDebtPage()
	{
        Shell.SetTabBarIsVisible(this, false);
        //Shell.SetNavBarIsVisible(this, false);
        Title = "������� �������";

        contentSL = new StackLayout
        {
            BackgroundColor = Color.FromArgb("#f2f2f2"),
        };
        Content = new ScrollView
        {
            Content = contentSL
        };

        GenerateUIAsync();

        BindingContext = _viewModel = new LoanDebtViewModel();
    }

	LoanDebtViewModel _viewModel;
	StackLayout contentSL;

	



    private void GenerateUIAsync()
    {
        App.Current.Dispatcher.Dispatch(() =>
        {
            contentSL.Add(new Label
            {
                HorizontalOptions = LayoutOptions.Start
            }.Text("����� ������������� �� �������").Font(bold: true, size: 18).Margins(10, 10, 10, 10));

            CreateLabel<string>("�������� �����", "CurrentLoanTotalDebt.LoanBalance");
            CreateLabel<string>("��������", "CurrentLoanTotalDebt.Percent");
            CreateLabel<string>("������", "CurrentLoanTotalDebt.Fine");
            CreateLabel<string>("�����", "CurrentLoanTotalDebt.Summa");

            contentSL.Add(new Label
            {
                HorizontalOptions = LayoutOptions.Start
            }.Text("������������ ������������� �� �������").Font(bold: true, size: 18).Margins(10, 30, 10, 10));
            CreateLabel<string>("�������� �����", "CurrentLoanOverdueDebt.Ostatok");
            CreateLabel<string>("��������", "CurrentLoanOverdueDebt.ProcPercent");
            CreateLabel<string>("������", "CurrentLoanOverdueDebt.Fine");
            CreateLabel<string>("�����", "CurrentLoanOverdueDebt.Summa");
        });
    }

    void CreateLabel<T>(string text, T value)
    {
        contentSL.Add(new Label
        {
            HorizontalOptions = LayoutOptions.Start
        }.Text(text).Font(size: 12).TextColor(Color.FromArgb("#212121")).Margins(10, 15, 10, 5));
        var label = new Label().Font(size: 15, bold: true, family: "OpenSansRegular").TextColor(Colors.Black).Margins(10, 0, 10, 0);
        label.SetBinding(Label.TextProperty, value.ToString(), stringFormat: "{0:N2}");
        contentSL.Add(label);
        contentSL.Add(new BoxView { Color = Color.FromArgb("#999999"), HeightRequest = 1, HorizontalOptions = LayoutOptions.Fill }.Margins(5, 5, 5, 0));
    }
}