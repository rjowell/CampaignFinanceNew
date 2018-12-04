using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Net;
using CampaignFinanceNew.Droid;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using Java.Lang;
using Android.Gms.Tasks;
//[assembly: Dependency(typeof(FirebaseAuthenticator))]
[assembly: Dependency (typeof(FirebaseAuthenticator))]

namespace CampaignFinanceNew.Droid
{

   



    public class FirebaseAuthenticator: Java.Lang.Object,IFirebaseAuthenticator, IOnCompleteListener 
    {
        public FirebaseAuthenticator()
        {
        }

        WebClient newClient = new WebClient();

        //public new IntPtr Handle => throw new NotImplementedException();

        public bool GetIdInfo()
        {
            var currentId = Firebase.Auth.FirebaseAuth.Instance.Uid;
            Console.WriteLine("Token is "+currentId);
            if(currentId != null)
            {
                
                return true;
            }
            else
            {
                return false;
            }
        }


        public void MoveOn()
        {

        }


        NameValueCollection mainUserData;
        bool isCreateUser;
        string userEmail;
        string userPassword;


        public string CreateNewUser(string email, string password, NameValueCollection userData)
        {
            //newClient.Encoding = System.Text.Encoding.UTF8;
            mainUserData = userData;
            userPassword = password;
            userEmail = email;
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            Console.WriteLine("system A1");
            //await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
            isCreateUser = true;
            Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPassword(email, password).AddOnCompleteListener(this);
            Console.WriteLine("system B");

            Console.WriteLine("system C");
            Console.WriteLine("your id is " + Firebase.Auth.FirebaseAuth.Instance.Uid);
            //userData.Add("isSupporter", "false");
           
            //Console.WriteLine(response);

            Console.WriteLine("it worked1233");
            //tcs.SetResult(true);
            return "hello";

            /* var sendingParameters = new System.Collections.Specialized.NameValueCollection();
                newClient.Encoding = System.Text.Encoding.UTF8;
                Console.WriteLine("ID is " + authResult.User.Uid);
                userData.Add("isSupporter", "false");
                userData.Add("firebaseID", authResult.User.Uid);
                Console.WriteLine(userData);
                var resultData = newClient.UploadValues("http://www.cvx4u.com/web_service/create_user.php", userData);
                Console.WriteLine(System.Text.Encoding.ASCII.GetString(resultData));*/
            //throw new NotImplementedException();
        }


        WebClient thisClient = new WebClient();

        public Task<bool> LoginWithEmailPassword(string email, string password)
        {
            //Firebase.Auth.FirebaseAuth.Instance.SignOut();
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            Console.WriteLine("Step 1");
            isCreateUser = false;
            Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPassword(email, password).AddOnCompleteListener(this);

            Console.WriteLine("point 1");
            var userJsonData = thisClient.DownloadString("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID=" + Firebase.Auth.FirebaseAuth.Instance.Uid);
            Console.WriteLine("point 2A "+ Firebase.Auth.FirebaseAuth.Instance.Uid);
            App.currentUser.userFirebaseID = Firebase.Auth.FirebaseAuth.Instance.Uid;
            Console.WriteLine("here we go");
            var currentUserData = JObject.Parse(userJsonData);
            Console.WriteLine("jeepers "+currentUserData.ContainsKey("CandidateId"));
            if(currentUserData.ContainsKey("CandidateId")==false)
            {
                Console.WriteLine("chickee");
                App.currentUser.systemID = JObject.Parse(userJsonData).GetValue("SupporterID").ToString();
            }
            else
            {
                Console.WriteLine("point 3" + JObject.Parse(userJsonData).GetValue("CandidateId"));
                App.currentUser.systemID = currentUserData.GetValue("CandidateId").ToString();
            }


            tcs.SetResult(true);

            return tcs.Task;
        }


        /*public Task<bool> LoginWithEmailPassword(string email, string password)
        {
            //var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);


            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();


            Auth.DefaultInstance.SignInWithPassword(email, password, (authResult, error) => {

                WebClient thisClient = new WebClient();
               

                //thisClient.DownloadString("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID=" + authResult.User.Uid);
                userJsonData = thisClient.DownloadString("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID=" + authResult.User.Uid);         //("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID=" + authResult.User.Uid);
                App.currentUser.userFirebaseID = authResult.User.Uid;
                App.currentUser.systemID = JObject.Parse(userJsonData).GetValue("CandidateId").ToString();
                Console.WriteLine("Your id is " + App.currentUser.systemID);
                tcs.SetResult(true);


            });

            return tcs.Task;

            //return await user.User.GetIdTokenAsync();
            //userJsonData = new WebClient().DownloadString("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID=" + currentToken);


        }*/

        public string GetCurrentUserInfo()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid;
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (isCreateUser == true)
            {

                Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPassword(userEmail, userPassword);

                mainUserData.Add("firebaseID", Firebase.Auth.FirebaseAuth.Instance.Uid);
                var response = newClient.UploadValues("http://www.cvx4u.com/web_service/create_user.php", mainUserData);
                Console.WriteLine(System.Text.Encoding.Default.GetString(response));
            }
            else
            {
                Console.WriteLine("point 2A " + Firebase.Auth.FirebaseAuth.Instance.Uid);
                App.currentUser.userFirebaseID = Firebase.Auth.FirebaseAuth.Instance.Uid;
                var userJsonData = thisClient.DownloadString("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID=" + Firebase.Auth.FirebaseAuth.Instance.Uid);
                var currentUserData = JObject.Parse(userJsonData);
                Console.WriteLine("jeepers " + currentUserData.ContainsKey("CandidateId"));
                if (currentUserData.ContainsKey("CandidateId") == false)
                {
                    Console.WriteLine("chickee");
                    App.currentUser.systemID = JObject.Parse(userJsonData).GetValue("SupporterID").ToString();
                    App.currentUser.isSupporter = true;
                    Console.WriteLine("Sys user id " + App.currentUser.systemID);
                }
                else
                {
                    Console.WriteLine("point 3" + JObject.Parse(userJsonData).GetValue("CandidateId"));
                    App.currentUser.systemID = currentUserData.GetValue("CandidateId").ToString();
                    App.currentUser.isSupporter = false;
                }
            }
        }

        public new void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}


