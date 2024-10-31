namespace AspireTest.Grains;

public sealed class AccountActor : Grain<AccountState>, IAccountActor
{
    public Task<decimal> Balance()
    {
        return Task.FromResult(State.Balance);
    }

    public async Task<decimal> Debit(decimal amount)
    {
        State.Balance -= amount;
        await this.WriteStateAsync();
        return State.Balance;
    }

    public async Task<decimal> Credit(decimal amount)
    {
        State.Balance += amount;
        await WriteStateAsync();
        return State.Balance;
    }
}