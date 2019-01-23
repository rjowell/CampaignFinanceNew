using System;
using System.Threading.Tasks;

namespace CampaignFinanceNew
{
    public interface IFirebaseAuthenticator
    {
        Task<bool> LoginWithEmailPassword(string email, string password);
        string CreateNewUser(string email, string password, System.Collections.Specialized.NameValueCollection userData);
       Task<string> CreateNewUserAsync(string email, string password, System.Collections.Specialized.NameValueCollection userData);
        string GetCurrentUserInfo();
        bool GetIdInfo();
        void Logout();
    }
}
