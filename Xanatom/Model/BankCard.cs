namespace Xanatom.Model;

public class BankCard
{
    public string? CardNumber { get; set; }
    public string? OwnerFirstName { get; set; }
    public string? OwnerLastName { get; set; }
    public DateOnly ExpireDate { get; set; }
    public int Balance { get; set; }
    // ReSharper disable once InconsistentNaming
    public int PIN { get; set; }
}