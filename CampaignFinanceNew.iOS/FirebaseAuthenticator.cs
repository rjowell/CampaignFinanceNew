using System;
using System.Threading.Tasks;
using System.Text;
using CampaignFinanceNew.iOS;
using Firebase.Auth;
using Firebase.Core;
using Newtonsoft.Json.Linq;
using System.Net;
using Xamarin.Forms;
using CampaignFinanceNew;
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

        System.Collections.Specialized.NameValueCollection localUserData;




        public void CreateNewUser(string email, string password, System.Collections.Specialized.NameValueCollection userData)
        {



            localUserData = userData;
            Console.WriteLine("russell shrimp");
            isCreateUser = true;
            Console.WriteLine("russell shrimp1");
            Auth.DefaultInstance.CreateUser(email, password, HandleAuthDataResultHandler);






        }
        bool isCreateUser;

        async void HandleAuthDataResultHandler(AuthDataResult authResult, Foundation.NSError error)
        {
            Console.WriteLine("rolller coaster");
            if (error != null)
            {
                Console.WriteLine(error.UserInfo.Keys);
                Console.WriteLine(error.UserInfo["error_name"].Description);
            }
            if (error != null && error.UserInfo["error_name"].ToString() == "ERROR_EMAIL_ALREADY_IN_USE")
            {
                Console.WriteLine("DUPLICATE_EMAIL_ERROR");
                MessagingCenter.Send<IFirebaseAuthenticator>(this, "Go");
            }
            else
            {
                Console.WriteLine(authResult);
                Console.WriteLine("It s" + authResult.User.Uid);
                if (isCreateUser == true)
                {
                    localUserData.Add("firebaseID", authResult.User.Uid);
                    Console.WriteLine("russell shrim3");
                    newClient.UploadValues("http://www.cvx4u.com/web_service/create_user.php", localUserData);
                }
                await App.currentUser.SetUserInfo(authResult.User.Uid);
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new CandidateDashboard());



            }

        }




        public void LoginWithEmailPassword(string email, string password)
        {
            //var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);




            isCreateUser = false;
            Auth.DefaultInstance.SignInWithPassword(email, password, HandleAuthDataResultHandler);






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
            App.currentUser.SetUserInfo(Auth.DefaultInstance.CurrentUser.Uid);


            return "hello";
        }
    }
}
