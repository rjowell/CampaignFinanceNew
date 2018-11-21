using System;
using System.Threading.Tasks;

namespace CampaignFinanceNew
{
    public interface IFirebaseAuthenticator
    {
        Task<string> LoginWithEmailPassword(string email, string password);
        string CreateNewUser(string email, string password, System.Collections.Specialized.NameValueCollection userData);
        bool GetIdInfo();
    }
}
