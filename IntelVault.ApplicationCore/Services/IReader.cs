namespace IntelVault.ApplicationCore.Services;

public interface IReader
{
    public byte[] GetBytes(string file);
}