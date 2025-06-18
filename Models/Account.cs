namespace ATMApp.Models;

public enum AccountType { Checking, Savings }

public class Account
{
    public AccountType Type { get; set; }
    public decimal Balance { get; set; }
}