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

        public async void OnComplete(Android.Gms.Tasks.Task task)
        {

            //Console.WriteLine("here you are bill");
            bool moveOn;



            //
            //Console.WriteLine("Number is"+Firebase.Auth.FirebaseAuth.Instance.Uid);

            if (isCreateUser == true)
            {
                Console.WriteLine("this point coyote");
                 try
                 {

                Console.WriteLine("here is " + task.Result);
                    mainUserData.Add("firebaseID", Firebase.Auth.FirebaseAuth.Instance.Uid);
                    newClient.UploadValues("http://www.cvx4u.com/web_service/create_user.php", mainUserData);
                    Console.WriteLine("this point - Android");
                    Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPassword(mainEmail, mainPassword);
                }
            catch(Android.Gms.Tasks.RuntimeExecutionException fbe)
            {
                if(task.Exception.Message== "The email address is already in use by another account.")
                {
                    //Console.WriteLine("you won");
                    MessagingCenter.Send<IFirebaseAuthenticator>(this, "Go");
                }

            }



            }
            else
            {
                Console.WriteLine("point 2A " + Firebase.Auth.FirebaseAuth.Instance.Uid);
                App.currentUser.userFirebaseID = Firebase.Auth.FirebaseAuth.Instance.Uid;
               
            }
            await App.currentUser.SetUserInfo(Firebase.Auth.FirebaseAuth.Instance.Uid);
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new CandidateDashboard());
        }

        public new void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            Firebase.Auth.FirebaseAuth.Instance.SignOut();
        }

        public Task<string> CreateNewUserAsync(string email, string password, System.Collections.Specialized.NameValueCollection userData)
        {
            throw new NotImplementedException();
        }
    }
}


