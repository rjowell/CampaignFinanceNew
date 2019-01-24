using System;
using System.Threading.Tasks;

namespace CampaignFinanceNew
{
    public interface IFirebaseAuthenticator
    {
       void LoginWithEmailPassword(string email, string password);
        void CreateNewUser(string email, string password, System.Collections.Specialized.NameValueCollection userData);
       Task<string> CreateNewUserAsync(string email, string password, System.Collections.Specialized.NameValueCollection userData);
        string GetCurrentUserInfo();
        bool GetIdInfo();
        void Logout();
    }
}
