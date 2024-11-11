namespace First_Step.Models;

public class Employee
{
    public int Id { get; set; };
    public string Payroll_Number { get; set; } = string.Empty;
    public string Forenames { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public DateTime Date_of_Birth { get; set; }
    public string Telephone { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Address_2 { get; set; } = string.Empty;
    public string Postcode { get; set; } = string.Empty;
    public string EMail_Home { get; set; } = string.Empty;
    public DateTime Start_Date { get; set; }
}
