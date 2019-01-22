using System;
using System.Threading.Tasks;
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
            Console.WriteLine("hhhhh");
            //string userId = "";
            //NewUserID theThing = new NewUserID();

            //TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            Auth.DefaultInstance.CreateUser(email, password, (authResult, error) => {

               
                userData.Add("firebaseID", authResult.User.Uid);
                Console.WriteLine("information is" + userData.Get("firebaseID")+" "+userData.Get("lastName"));
                Console.WriteLine(userData.AllKeys);
               

                newClient.UploadValues("http://www.cvx4u.com/web_service/create_user.php", userData);


            });



            Console.WriteLine("Your result lis");
            //Console.WriteLine(tcs.Task.Result);
            //tcs.SetResult(true);
            return "true";
            //return tcs.Task.Result;
        }

        public Task<bool> LoginWithEmailPassword(string email, string password)
        {
            //var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);


            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();


            Auth.DefaultInstance.SignInWithPassword(email, password, (authResult, error) => {


                App.currentUser.SetUserInfo(authResult.User.Uid);

                //thisClient.DownloadString("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID=" + authResult.User.Uid);
               /* userJsonData = newClient.DownloadString("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID=" + authResult.User.Uid);         //("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID=" + authResult.User.Uid);
                App.currentUser.userFirebaseID = authResult.User.Uid;
                JObject currentUserString = JObject.Parse(userJsonData);
                App.currentUser.firstName = currentUserString.GetValue("FirstName").ToString();
                App.currentUser.lastName = currentUserString.GetValue("LastName").ToString();
                /*{"FirstName":"Russell","LastName":"Jowell","MailingAddress":"4500 Connecticut Ave nw #203","City":"Washington ","State":"DC","Zip":"20008","EMail":"russ.jowell@gmail.com","Phone":"2814608568","CampaignsSupported":null,"SupporterID":"1000","FirebaseID":"h9s7cK7qoqSCxOaGDcftnnTwtS22","StripeCustomerID":"cus_ELQh45iGJf5gt9"}

                if (JObject.Parse(userJsonData).ContainsKey("CandidateId")==false)
                {

                    
                    App.currentUser.systemID = currentUserString.GetValue("SupporterID").ToString();
                    App.currentUser.isSupporter = true;
                    App.currentUser.campaignsSupported = currentUserString.GetValue("CampaignsSupported").ToString().Split(',');
                }
                else
                {
                    App.currentUser.systemID = currentUserString.GetValue("CandidateId").ToString();
                    App.currentUser.currentCampaigns = currentUserString.GetValue("CampaignIDs").ToString().Split(',');
                    App.currentUser.isSupporter = false;
                }*/

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
    }
}
