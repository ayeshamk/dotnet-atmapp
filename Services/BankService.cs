using ATMApp.Models;

namespace ATMApp.Services;

public class BankService
{
    private readonly Dictionary<AccountType, Account> _accounts;
    private readonly List<Transaction> _transactions;

    public BankService()
    {
        _accounts = new Dictionary<AccountType, Account>
        {
            { AccountType.Checking, new Account { Type = AccountType.Checking, Balance = 0 } },
            { AccountType.Savings, new Account { Type = AccountType.Savings, Balance = 0 } }
        };
        _transactions = new List<Transaction>();
    }

    public IEnumerable<Account> GetAccounts() => _accounts.Values;

    public IEnumerable<Transaction> GetTransactions() => _transactions;

    public void Deposit(AccountType type, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");

        _accounts[type].Balance += amount;
        _transactions.Add(new Transaction
        {
            Timestamp = DateTime.Now,
            From = null,
            To = type,
            Amount = amount,
            Type = "Deposit"
        });
    }

    public void Withdraw(AccountType type, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");

        if (_accounts[type].Balance < amount)
            throw new InvalidOperationException("Insufficient funds");

        _accounts[type].Balance -= amount;
        _transactions.Add(new Transaction
        {
            Timestamp = DateTime.Now,
            From = type,
            To = null,
            Amount = amount,
            Type = "Withdraw"
        });
    }

    public void Transfer(AccountType from, AccountType to, decimal amount)
    {
        if (from == to)
            throw new ArgumentException("Cannot transfer to the same account");

        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");

        if (_accounts[from].Balance < amount)
            throw new InvalidOperationException("Insufficient funds");

        _accounts[from].Balance -= amount;
        _accounts[to].Balance += amount;

        _transactions.Add(new Transaction
        {
            Timestamp = DateTime.Now,
            From = from,
            To = to,
            Amount = amount,
            Type = "Transfer"
        });
    }
}