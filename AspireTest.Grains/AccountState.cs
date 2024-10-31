namespace AspireTest.Grains;

[GenerateSerializer]
public sealed class AccountState
{
    [Id(0)]
    public decimal Balance { get; set; }
}