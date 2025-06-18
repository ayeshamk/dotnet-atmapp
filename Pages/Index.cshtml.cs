using ATMApp.Models;
using ATMApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATMApp.Pages;

public class IndexModel : PageModel
{
    private readonly BankService _bank;

    public IndexModel(BankService bank)
    {
        _bank = bank;
    }

    public List<Account> Accounts { get; set; } = new();
    public List<Transaction> Transactions { get; set; } = new();

    [BindProperty]
    public AccountType SelectedAccount { get; set; }

    [BindProperty]
    public AccountType FromAccount { get; set; }

    [BindProperty]
    public AccountType ToAccount { get; set; }

    [BindProperty]
    public decimal Amount { get; set; }

    [BindProperty]
    public string Action { get; set; } = "Deposit";

    public void OnGet()
    {
        Accounts = _bank.GetAccounts().ToList();
        Transactions = _bank.GetTransactions().OrderByDescending(t => t.Timestamp).ToList();
    }

    public IActionResult OnPost()
    {
        try
        {
            switch (Action)
            {
                case "Deposit":
                    _bank.Deposit(SelectedAccount, Amount);
                    break;
                case "Withdraw":
                    _bank.Withdraw(SelectedAccount, Amount);
                    break;
                case "Transfer":
                    _bank.Transfer(FromAccount, ToAccount, Amount);
                    break;
            }

            return RedirectToPage();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }

        OnGet();
        return Page();
    }
}
