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


        public void MoveOn()
        {

        }


        NameValueCollection mainUserData;
        bool isCreateUser;
        bool branch=false;
        bool changeEMail;
        int actionTaken;
        //0 - Create User 
        string mainPassword;
        string mainEmail;


        public void CreateNewUser(string email, string password, NameValueCollection userData)
        {
            //newClient.Encoding = System.Text.Encoding.UTF8;
            mainUserData = userData;
            mainEmail = email;
            mainPassword = password;
            Console.WriteLine("Android make user");
            foreach(var keys in userData.AllKeys)
            {
                Console.WriteLine(keys + " " + userData.Get(keys));
            }

            isCreateUser = true;


            Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPassword(email, password).AddOnCompleteListener(this);



           
           
        }


        WebClient thisClient = new WebClient();

        public void LoginWithEmailPassword(string email, string password)
        {
            //Firebase.Auth.FirebaseAuth.Instance.SignOut();
           
            Console.WriteLine("Step 1");
            isCreateUser = false;
            Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPassword(email, password).AddOnCompleteListener(this);



          
        }


     

        public string GetCurrentUserInfo()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid;
        }
        bool password;
        public void SendResetLink(string email)
        {
            password = true;
            Firebase.Auth.FirebaseAuth.Instance.SendPasswordResetEmail(email).AddOnCompleteListener(this);
        }

       
       
     public async Task<bool> UploadUserData(string url, NameValueCollection input)
        {
            newClient.UploadValues(new Uri(url), input);
            return true;

        }


        public async void OnComplete(Android.Gms.Tasks.Task task)
        {

            Console.WriteLine("babby");

            if (task.Exception != null)
            {

                Console.WriteLine("your problem isw" + task.Exception);

                if (task.Exception.Message == "The email address is badly formatted.")
                {
                    if (password == true)
                    {
                        MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "ResetError", 0);
                    }
                    else
                    { 
                        MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "MainPageError", 0);
                    }
                }
                if (task.Exception.Message == "There is no user record corresponding to this identifier. The user may have been deleted.")
                {
                    if (password == true)
                    {
                        MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "ResetError", 1);
                    }
                    else
                    {
                        MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "MainPageError", 1);
                    }
                }
                if(task.Exception.Message=="The password is invalid or the user does not have a password.")
                {
                    MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "MainPageError", 2);
                }
                if (task.Exception.Message.Contains("The email address is already in use by another account"))
                {
                    MessagingCenter.Send<IFirebaseAuthenticator>(this, "Go");
                }

                //"There is no user record corresponding to this identifier. The user may have been deleted.";
                //The email address is badly formatted.
                //Console.WriteLine(task.Exception.Message);

                //The email address is badly formatted.
                //There is no user record corresponding to this identifier. The user may have been deleted.
            }



            else
            {

                Console.WriteLine("babby-22");
                if (branch == true)
                {
                    Console.WriteLine("modify");


                    if (changeEMail == true)
                    {


                        if (task.Exception.Message != null)
                        {
                            if (task.Exception.Message == "The email address is badly formatted.")
                            {
                                MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "AuthError", 0);
                            }
                            if (task.Exception.Message == "The email address is already in use by another account.")
                            {
                                MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "AuthError", 1);
                            }
                        }
                        else
                        {
                            NameValueCollection eMailData = new NameValueCollection();
                            eMailData.Set("isSupporter", App.currentUser.isSupporter.ToString().ToLower());
                            eMailData.Set("systemID", App.currentUser.systemID);
                            eMailData.Set("eMail", newEmail);

                            //newClient.UploadValuesAsync(new Uri("http://www.cvx4u.com/web_service/updateEmailPassword.php"), mainUserData);
                           
                           newClient.UploadValues("http://www.cvx4u.com/web_service/updateEmailPassword.php", eMailData);
                            App.currentUser.eMail = newEmail;
                            MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "AuthError", 2);
                        }


                        //The email address is badly formatted.
                        //The email address is already in use by another account.
                    }
                    else
                    {
                        MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "AuthError", 3);
                    }


                }
                else
                {


                    if (isCreateUser == true)
                    {
                        Console.WriteLine("this point coyote");
                        try
                        {

                            Console.WriteLine("here is " + task.Result);
                            mainUserData.Add("firebaseID", Firebase.Auth.FirebaseAuth.Instance.Uid);
                            foreach(var keys in mainUserData.AllKeys)
                            {
                                Console.WriteLine(keys + "=" + mainUserData.Get(keys) + "&");
                            }

                            //newClient.UploadValuesAsync(new Uri("http://www.cvx4u.com/web_service/create_user.php"), mainUserData);
                           var returnData=newClient.UploadValues("http://www.cvx4u.com/web_service/create_user.php", mainUserData);
                            Console.WriteLine("Info is" + System.Text.Encoding.UTF8.GetString(returnData));
                            //await newClient.UploadValues("http://www.cvx4u.com/web_service/create_user.php", mainUserData);
                            Console.WriteLine("this point - Android");
                            Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPassword(mainEmail, mainPassword);
                            Console.WriteLine("ID is " + Firebase.Auth.FirebaseAuth.Instance.Uid);
                        }
                        catch (Android.Gms.Tasks.RuntimeExecutionException fbe)
                        {
                            if (task.Exception.Message == "The email address is already in use by another account.")
                            {
                                //Console.WriteLine("you won");
                                MessagingCenter.Send<IFirebaseAuthenticator>(this, "Go");
                            }

                        }



                    }
                    else
                    {
                       
                        App.currentUser.userFirebaseID = Firebase.Auth.FirebaseAuth.Instance.Uid;
                       

                    }

                    await App.currentUser.SetUserInfo(Firebase.Auth.FirebaseAuth.Instance.Uid);
                    Console.WriteLine("hess piece");
                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new CandidateDashboard());
                }
            }


        }

        public new void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            Firebase.Auth.FirebaseAuth.Instance.SignOut();
            Console.WriteLine("Logout");
        }

        string newEmail;
        public void UpdateEMail(string email)
        {
            branch = true;
            changeEMail = true;
            newEmail = email;
            try
            {
                Firebase.Auth.FirebaseAuth.Instance.CurrentUser.UpdateEmail(email).AddOnCompleteListener(this);
            }
            catch (Android.Gms.Tasks.RuntimeExecutionException fbe)
            {
                Console.WriteLine("error email is" + fbe.Message);
            }

            Console.WriteLine("Email worked");
        }

        public void UpdatePassword(string password)
        {
            branch = true;
            changeEMail = false;
            Firebase.Auth.FirebaseAuth.Instance.CurrentUser.UpdatePassword(password).AddOnCompleteListener(this);
            Console.WriteLine("Password Worked");
        }
    }
}


