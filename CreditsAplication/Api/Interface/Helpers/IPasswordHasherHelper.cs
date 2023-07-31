using System.Security.Cryptography;

namespace CreditsAplication.Api.Interface.Helpers
{
    public interface IPasswordHasherHelper
    {
        public string Hash(string password);

        public bool Verify(string passwordhash, string inputpassword);
    }
}
