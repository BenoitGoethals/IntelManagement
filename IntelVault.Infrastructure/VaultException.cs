namespace IntelVault.Infrastructure;

public class VaultException : Exception
{
    public VaultException(string? message) : base(message)
    {
    }
}