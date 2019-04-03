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


    
        public void UpdateEMail(string email)
        {




                  
                    Auth.DefaultInstance.CurrentUser.UpdateEmail( email, (error) => {
                //Console.WriteLine("email mac");
                if (error == null)
                {
                    System.Collections.Specialized.NameValueCollection eMailData = new System.Collections.Specialized.NameValueCollection();
                    eMailData.Set("isSupporter", App.currentUser.isSupporter.ToString().ToLower());
                    eMailData.Set("systemID", App.currentUser.systemID);
                    eMailData.Set("eMail", email);
                    //await newClient.UploadValuesAsync("http://www.cvxu.com/web_service/updateEmailPassword.php", eMailData);
                    newClient.UploadValues("http://www.cvx4u.com/web_service/updateEmailPassword.php", eMailData);
                            App.currentUser.eMail = email;


                    MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "AuthError", 2);
                }

                else
                {

                    //Console.WriteLine(error);
                    if (error.ToString()  == "The email address is already in use by another account.")
                    {
                        MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "AuthError", 1);
                    }
                    if (error.ToString()  == "The email address is badly formatted.")
                    {

                        MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "AuthError", 0);
                    }
                }

                //Console.WriteLine(error==null);
            });
        }

        public void SendResetLink(string email)
        {
            Auth.DefaultInstance.SendPasswordReset(email,(error) => {
                Console.WriteLine("Reset erros "+error);

                if(error.ToString()  == "The email address is badly formatted.")
                {
                    MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "ResetError", 0);
                }
                if(error.ToString()== "There is no user record corresponding to this identifier. The user may have been deleted.")
                {
                    MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "ResetError", 1);
                }

                //The email address is badly formatted.
                //There is no user record corresponding to this identifier. The user may have been deleted.
            });
        }

        public void UpdatePassword(string password)
        {
            Auth.DefaultInstance.CurrentUser.UpdatePassword(password, (error) => {
                MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "AuthError", 3);
            });
        }

        public bool GetIdInfo()
        {
            var currentToken = Auth.DefaultInstance.CurrentUser.Uid;
            //var sendingParameters = new System.Collections.Specialized.NameValueCollection();
            
            userJsonData =new WebClient().DownloadString("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID="+currentToken);

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
           
            isCreateUser = true;
           
            Auth.DefaultInstance.CreateUser(email, password, HandleAuthDataResultHandler);






        }
        bool isCreateUser;

        async void HandleAuthDataResultHandler(AuthDataResult authResult, Foundation.NSError error)
        {
           
            if (error != null)
            {
                //Console.WriteLine(error.UserInfo["error_name"].Description);

                if(error.UserInfo["error_name"].ToString() == "ERROR_EMAIL_ALREADY_IN_USE")
                {
                    //Console.WriteLine("DUPLICATE_EMAIL_ERROR");
                    MessagingCenter.Send<IFirebaseAuthenticator>(this, "Go");
                }
                if (error.UserInfo["error_name"].Description == "ERROR_INVALID_EMAIL") 
                {
                    MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "MainPageError", 0);
                }
                if(error.UserInfo["error_name"].Description == "ERROR_USER_NOT_FOUND")
                {
                    MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "MainPageError", 1);
                }
                if(error.UserInfo["error_name"].Description=="ERROR_WRONG_PASSWORD")
                {
                    MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "MainPageError", 2);
                }


            }
          
            else
            {

                if (isCreateUser == true)
                {
                    localUserData.Add("firebaseID", authResult.User.Uid);

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
