namespace AspireTest.Grains;

public interface IAccountActor : IGrainWithStringKey
{
    Task<decimal> Balance();
    
    Task<decimal> Debit(decimal amount);
 
    Task<decimal> Credit(decimal amount);
    
    Task<AccountState> GetState();
}