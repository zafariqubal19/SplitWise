namespace SplitWiseAPI.Services.Interface
{
    public interface IEncryptionService
    {
        string EcncryptPassword(string password, string salt);
        string GenerateSalt(string password);
    }
}
