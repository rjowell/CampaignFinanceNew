﻿using System;
using System.Threading.Tasks;
using CampaignFinanceNew.iOS;
using Firebase.Auth;
using Firebase.Core;
using System.Net;
[assembly: Xamarin.Forms.Dependency(typeof(FirebaseAuthenticator))]
namespace CampaignFinanceNew.iOS
{
    public class FirebaseAuthenticator: IFirebaseAuthenticator
    {

        WebClient newClient = new WebClient();

        public bool GetIdInfo()
        {
            var currentToken = Auth.DefaultInstance.CurrentUser.Uid;
            Console.WriteLine("Token is " + currentToken);
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

            //TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            Auth.DefaultInstance.CreateUser(email, password, (authResult, error) => {

                //Console.WriteLine(bioInfo + "&firebaseID=" + authResult.User.Uid + "&isSupporter=false");
                var sendingParameters = new System.Collections.Specialized.NameValueCollection();
                newClient.Encoding = System.Text.Encoding.UTF8;
                Console.WriteLine("ID is " + authResult.User.Uid);
                //userData.Add("isSupporter", "false");
                userData.Add("firebaseID", authResult.User.Uid);
                Console.WriteLine(userData);
                var resultData = newClient.UploadValues("http://www.cvx4u.com/web_service/create_user.php", userData);
                Console.WriteLine(System.Text.Encoding.ASCII.GetString(resultData));
               
                //Console.WriteLine(resultData[0]+" "+resultData[1]+" "+resultData[2]);
                //newClient.Headers[HttpRequestHeader.ContentType]= "application/x-www-form-urlencoded";
                //newClient.UploadString("http://www.cvx4u.com/web_service/create_user.php", bioInfo + "&firebaseID=" + authResult.User.Uid + "&isSupporter=false");

            });




            /*Auth.DefaultInstance.CreateUser(email, password, (authResult, error) =>
            {
                Console.WriteLine("this is" + error.Description);
                //Console.WriteLine("idd is " + authResult.User.Uid);
                Console.WriteLine("info is " + bioInfo);
                //userId = authResult.User.Uid;
                //tcs.SetResult(authResult.User.Uid);
                //theThing.setNewID(authResult.User.Uid);
                newClient.UploadString("http://www.cvx4u.com/web_service/create_user.php", bioIn+"&firebaseID="+authResult.User.Uid+"&isSupporter=false");
                //theThing.setNewID("done");

            });*/
            Console.WriteLine("Your result lis");
            //Console.WriteLine(tcs.Task.Result);

            return "hello";
            //return tcs.Task.Result;
        }

        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
            return await user.User.GetIdTokenAsync();


        }


    }
}
