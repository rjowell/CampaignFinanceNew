using System;
using System.Threading.Tasks;
using System.Text;
using CampaignFinanceNew.iOS;
using Firebase.Auth;
using Firebase.Core;
using Newtonsoft.Json.Linq;
using System.Net;
using Xamarin.Forms;
[assembly: Xamarin.Forms.Dependency(typeof(FirebaseAuthenticator))]
namespace CampaignFinanceNew.iOS
{
    public class FirebaseAuthenticator: IFirebaseAuthenticator
    {

        WebClient newClient = new WebClient();
        String userJsonData;


      


        public bool GetIdInfo()
        {
            var currentToken = Auth.DefaultInstance.CurrentUser.Uid;
            //var sendingParameters = new System.Collections.Specialized.NameValueCollection();
            
            userJsonData =new WebClient().DownloadString("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID="+currentToken);
            Console.WriteLine(currentToken);
            Console.WriteLine(userJsonData);
            if(currentToken != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public string CreateNewUser(string email, string password, System.Collections.Specialized.NameValueCollection userData)
        {
           
            Auth.DefaultInstance.CreateUser(email, password, async (authResult, error) => {

                Console.WriteLine(authResult.User.Uid);
                Console.WriteLine("test print");
                userData.Add("firebaseID", authResult.User.Uid);
                newClient.UploadValues("http://www.cvx4u.com/web_service/create_user.php", userData);
                App.currentUser.SetUserInfo(authResult.User.Uid);


            
            });

            
            return "True";

        }




        public Task<bool> LoginWithEmailPassword(string email, string password)
        {
            //var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);


            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();


            Auth.DefaultInstance.SignInWithPassword(email, password, (authResult, error) => {

                Console.WriteLine("You're in as " + authResult.User.Email);

                App.currentUser.SetUserInfo(authResult.User.Uid);

               
                tcs.SetResult(true);


            });

            return tcs.Task;




        }

        public string GetCurrentUserInfo()
        {
            return App.currentUser.systemID;
        }

        public void Logout()
        {
            Auth.DefaultInstance.SignOut(out Foundation.NSError error);
        }

        public async Task<string> CreateNewUserAsync(string email, string password, System.Collections.Specialized.NameValueCollection userData)
        {



            await Auth.DefaultInstance.CreateUserAsync(email, password);
            //await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
            Console.WriteLine("Current user is"+Auth.DefaultInstance.CurrentUser.Uid);
            Console.WriteLine("more info is" + Auth.DefaultInstance.CurrentUser.Email);
            userData.Add("firebaseID", Auth.DefaultInstance.CurrentUser.Uid);
            newClient.UploadValues("http://www.cvx4u.com/web_service/create_user.php", userData);
            Console.WriteLine("step three");
            await App.currentUser.SetUserInfo(Auth.DefaultInstance.CurrentUser.Uid);


            return "hello";
        }
    }
}
