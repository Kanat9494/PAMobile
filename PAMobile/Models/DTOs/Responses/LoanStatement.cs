namespace PAMobile.Models.DTOs.Responses;

internal class LoanStatement
{
    //Фактически оплачено
    public DateTime PayDate { get; set; }
    public decimal PayBasicSum { get; set; }
    public decimal PayPercentSum { get; set; }
    public decimal PayFineSum { get; set; }
    public decimal TotalPaySum { get; set; }
    //Остаток просроченной суммы по кредиту
    public decimal OverdueRemBasicSum { get; set; }
    public decimal OverdueRemPercentSum { get; set; }
    //Всего задолженность по кредиту
    public decimal TotalLoanBasicSum { get; set; }
    public decimal TotalLoandPerSum { get; set; }
}
