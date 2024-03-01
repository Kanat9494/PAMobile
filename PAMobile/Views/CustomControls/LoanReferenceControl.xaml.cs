namespace PAMobile.Views.CustomControls;

public class LoanTermsSelectedEventArgs : EventArgs
{
	public LoanTermsSelectedEventArgs(string loanTerm) => LoanTerm = loanTerm;
	public string LoanTerm { get; set; }
}

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoanReferenceControl : ContentView
{
	public LoanReferenceControl()
	{
		InitializeComponent();

		SelectLoanTermCommand = new Command(OnLoanTermsSelected);
    }

	public static readonly BindableProperty LoansProperty = BindableProperty.Create(nameof(Loans),
		typeof(IEnumerable<LoanResponse>), typeof(LoanReferenceControl), Enumerable.Empty<LoanResponse>());
	public IEnumerable<LoanResponse> Loans
	{
		get => (IEnumerable<LoanResponse>)GetValue(LoansProperty);
		set => SetValue(LoansProperty, value);
	}

	public static readonly BindableProperty SelectedLoanProperty = BindableProperty.Create(nameof(SelectedLoan),
		typeof(LoanResponse), typeof(LoanReferenceControl), null);
	public LoanResponse SelectedLoan
	{
		get => (LoanResponse)GetValue(SelectedLoanProperty);
		set => SetValue(SelectedLoanProperty, value);
	}

	public static readonly BindableProperty NextCommandProperty = BindableProperty.Create(nameof(NextCommand),
		typeof(ICommand), typeof(LoanReferenceControl), null);
	public ICommand NextCommand
	{
		get => (AsyncRelayCommand)GetValue(NextCommandProperty);
		set => SetValue(NextCommandProperty, value);
	}

	public static readonly BindableProperty ReferencePageTitleProperty = BindableProperty.Create(nameof(ReferencePageTitle),
		typeof(string), typeof(LoanReferenceControl), string.Empty);
	public string ReferencePageTitle
	{
		get => (string)GetValue(LoanReferenceControl.ReferencePageTitleProperty);
		set => SetValue(LoanReferenceControl.ReferencePageTitleProperty, value);
	}

	public static readonly BindableProperty PickerTitleProperty = BindableProperty.Create(nameof(PickerTitle),
		typeof(string), typeof(LoanReferenceControl), string.Empty);
	public string PickerTitle
	{
		get => (string)GetValue(PickerTitleProperty);
		set => SetValue(PickerTitleProperty, value);
	}

	public static readonly BindableProperty LoanTermsProperty = BindableProperty.Create(nameof(LoanTerms),
		typeof(IEnumerable<string>), typeof(LoanReferenceControl), Enumerable.Empty<string>());
	public IEnumerable<string> LoanTerms
	{
		get => (IEnumerable<string>)GetValue(LoanTermsProperty);
		set => SetValue(LoanTermsProperty, value);
	}

	public static readonly BindableProperty IsNextProperty = BindableProperty.Create(nameof(IsNext),
		typeof(bool), typeof(LoanReferenceControl), false);
	public bool IsNext
	{
		get => (bool)GetValue(IsNextProperty);
		set => SetValue(IsNextProperty, value);
	}

	public ICommand SelectLoanTermCommand { get; private set; }
	public event EventHandler<LoanTermsSelectedEventArgs> LoanTermsSelected;
	private void OnLoanTermsSelected(object parameter)
	{
		if (parameter is string loanTerm && loanTerm is not null)
		{
			LoanTermsSelected?.Invoke(this, new LoanTermsSelectedEventArgs(loanTerm));
		}
	}
}