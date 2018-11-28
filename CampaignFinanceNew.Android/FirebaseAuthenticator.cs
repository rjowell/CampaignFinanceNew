﻿using System;
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

   



    public class FirebaseAuthenticator: IFirebaseAuthenticator
    {
        public FirebaseAuthenticator()
        {
        }

        WebClient newClient = new WebClient();

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
        public string CreateNewUser(string email, string password, NameValueCollection userData)
        {
            newClient.Encoding = System.Text.Encoding.UTF8;
            Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPassword(email, password);
            Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPassword(email, password);
            Console.WriteLine("your id is " + Firebase.Auth.FirebaseAuth.Instance.Uid);
            //userData.Add("isSupporter", "false");
            userData.Add("firebaseID", Firebase.Auth.FirebaseAuth.Instance.Uid);
            newClient.UploadValues("http://www.cvx4u.com/web_service/create_user.php", userData);


            Console.WriteLine("it worked1233");
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

        public Task<bool> LoginWithEmailPassword(string email, string password)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPassword(email, password);
            WebClient thisClient = new WebClient();
            Console.WriteLine("point 1");
            var userJsonData = thisClient.DownloadString("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID=" + Firebase.Auth.FirebaseAuth.Instance.Uid);
            Console.WriteLine("point 2");
            App.currentUser.userFirebaseID = Firebase.Auth.FirebaseAuth.Instance.Uid;
            Console.WriteLine("point 3");
            App.currentUser.systemID=JObject.Parse(userJsonData).GetValue("CandidateId").ToString();
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
    }
}

/*
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * using System;
using System.Threading.Tasks;

using CampaignFinanceNew.Droid;
using Xamarin.Forms;
[assembly: Dependency(typeof(FirebaseAuthenticator))]

namespace CampaignFinanceNew.Droid
{
    public class FirebaseAuthenticator: IFirebaseAuthenticator
    {
        public FirebaseAuthenticator()
        {
        }

        public string CreateNewUser(string email, string password, System.Collections.Specialized.NameValueCollection userData)
        {
            Console.WriteLine("hhhhh");
            //var thing=Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPassword(email, password);
            bool userCreateSuccessful = Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password).IsCompletedSuccessfully;
            if (userCreateSuccessful == true)
            {
                //var user = await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                //var theToken = await user.User.GetIdTokenAsync(false);
                //Console.WriteLine(theToken.Token);
            }
            else
            {
                Console.WriteLine("it failed");
            }

            //Firebase.Auth.UserProfileChangeRequest request = new Firebase.Auth.UserProfileChangeRequest.Builder().SetDisplayName("John Smith").Build();

            //thing.User.UpdateProfile(request);
            //return "Hello";
            return "hello";
        }

        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            var user = await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
            var userID = user.User.Uid;
            Console.WriteLine(userID);
            var token = await user.User.GetTokenAsync(false);
            return token.Token;
        }
    }
}*/
