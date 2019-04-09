using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Geolocator;
using System.Net;
using Newtonsoft.Json.Linq;

namespace CampaignFinanceNew
{
    public partial class MainPage : ContentPage
    {

        Entry[] fields;

        public void OpenUserWindow(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PositionsAndIssues());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Console.WriteLine("this point");
            await App.currentLocation.GetLocationInformation();
            Console.WriteLine(App.currentLocation.state + " " + App.currentLocation.cityName);

        }


        public MainPage()
        {
            InitializeComponent();

            Console.WriteLine(App.currentLocation.cityName + " " + App.currentLocation.state);

            //App.currentLocation.GetLocationInformation();

            NavigationPage.SetHasNavigationBar(this, false);

           
            fields = new Entry[] { eMailField, passwordField };
            backImage.Source = App.currentUser.getBackImage();
            noticeFrame.IsVisible = false;

            // MessagingCenter.Send<IFirebaseAuthenticator, int>(this, "ResetError", 1);

            MessagingCenter.Subscribe<IFirebaseAuthenticator, int>(this, "ResetError", (sender, arg) => {

                passResetError.IsVisible = true;
            if(arg==0)
                {
                    passResetError.Text = "Your e-mail is not in the correct format.";
                }
            if(arg==1)
                {
                    passResetError.Text = "That e-mail is not in our system";
                }
            if(arg==2)
                {
                    noticeFrame.IsVisible = false;
                }

            });

            MessagingCenter.Subscribe<IFirebaseAuthenticator,int>(this, "MainPageError", (sender,arg) => {

                passResetLabel.IsVisible = false;
                passResetEntry.IsVisible = false;
                passResetSend.IsVisible = false;
                passResetError.IsVisible = false;
                noticeText.IsVisible = true;
                noticeButton.IsVisible = true;
                passResetCancel.IsVisible = false;

                if (arg==0)
                {
                    noticeText.Text = "Your e-mail is not in the correct format";
                   
                    noticeFrame.IsVisible = true;
                }
                if(arg==1)
                {
                    noticeText.Text = "That e-mail is not in our system";
                    noticeFrame.IsVisible = true;
                }
                if(arg==2)
                {
                    noticeText.Text = "Password Incorrect";
                    noticeFrame.IsVisible = true;
                }

            });

            foreach (Entry current in fields)
            {
                if (Array.IndexOf(fields, current) != fields.Length - 1)
                {
                    current.Completed += (s, e) =>
                    {

                        fields[Array.IndexOf(fields, current) + 1].Focus();

                    };
                }
            }


            titleImage.Source = ImageSource.FromResource("CampaignFinanceNew.ANMlogo1.png");
            eMailLabel.IsVisible = false;
            eMailField.IsVisible = false;
            passwordLabel.IsVisible = false;
            passwordField.IsVisible = false;
            forgotPassword.IsVisible = false;
            supporterButton.IsVisible = false;
            candidateButton.IsVisible = false;
            loginButton.IsVisible = false;




        }


        public void ShowOptions(object sender, EventArgs e)
        {
            Button current = (Button)sender;

            Console.WriteLine("Clas is " + current.ClassId);

            if(current.ClassId=="0")
            {
                eMailLabel.IsVisible = true;
                eMailField.IsVisible = true;
                passwordLabel.IsVisible = true;
                passwordField.IsVisible = true;
                forgotPassword.IsVisible = true;
                supporterButton.IsVisible = false;
                candidateButton.IsVisible = false;
                loginButton.IsVisible = true;
            }
            else
            {
                eMailLabel.IsVisible = false;
                eMailField.IsVisible = false;
                passwordLabel.IsVisible = false;
                passwordField.IsVisible = false;
                forgotPassword.IsVisible = false;
                supporterButton.IsVisible = true;
                candidateButton.IsVisible = true;
                loginButton.IsVisible = false;
            }

        }

        public void ShowPasswordReset(object sender, EventArgs e)
        {
            noticeFrame.IsVisible = true;
            noticeText.IsVisible = false;
            noticeButton.IsVisible = false;
            passResetLabel.IsVisible = true;
            passResetEntry.IsVisible = true;
            passResetSend.IsVisible = true;
            passResetError.IsVisible = true;
            passResetCancel.IsVisible = true;
        }








        public void CloseWindow(object sender, EventArgs e)
        {
            noticeFrame.IsVisible = false;
        }



        private async void ProcessLogin(object sender, EventArgs e)
        {

            noticeWindow.IsVisible = true;
            Console.WriteLine("dulles");
            DependencyService.Get<IFirebaseAuthenticator>().LoginWithEmailPassword(eMailField.Text, passwordField.Text);

            //var isLoggedIn = DependencyService.Get<IFirebaseAuthenticator>().GetIdInfo();
            //DependencyService.Get<IFirebaseAuthenticator>().CreateNewUser(eMailField.Text, passwordField.Text, sendingParameters);
            //Console.WriteLine(isLoggedIn);

            //await GetLocationInformation();




           

        }

        private void SendPasswordReset(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAuthenticator>().SendResetLink(passResetEntry.Text);
        }

        private void CancelReset(object sender, EventArgs e)
        {
            noticeFrame.IsVisible = false;
        }


        private async void ProcessButtons(object sender, EventArgs e)
        {
            Button current = (Button)sender;

            switch(current.ClassId)
            {
                case "returning":
                    Console.WriteLine("button returning");
                    eMailLabel.IsVisible = true;
                    eMailField.IsVisible = true;
                    passwordLabel.IsVisible = true;
                    passwordField.IsVisible = true;
                    forgotPassword.IsVisible = true;
                    supporterButton.IsVisible = false;
                    candidateButton.IsVisible = false;
                    loginButton.IsVisible = true;
                    break;
                case "newUser":
                    Console.WriteLine("button newUser");
                    eMailLabel.IsVisible = false;
                    eMailField.IsVisible = false;
                    passwordLabel.IsVisible = false;
                    forgotPassword.IsVisible = false;
                    passwordField.IsVisible = false;
                    supporterButton.IsVisible = true;
                    candidateButton.IsVisible = true;
                    loginButton.IsVisible = false;
                    break;
                case "login":
                    Console.WriteLine("button login");
                    DependencyService.Get<IFirebaseAuthenticator>().LoginWithEmailPassword(eMailField.Text, passwordField.Text);
                    //await Navigation.PushAsync(new CandidateDashboard());
                    break;
                case "supporter":
                    Console.WriteLine("button supporter");


                    App.newUser = new NewUserInfo();
                    App.newUser.isSupporter = true;
                    App.newUser.firstName = "";
                    App.newUser.lastName = "";
                    App.newUser.office = "";
                    //App.newUser.district = "";
                    App.newUser.officeCity = "";
                    App.newUser.officeState = "";
                    App.newUser.officeCounty = "";
                    App.newUser.officeDistrict = "";
                    App.newUser.party = "";
                    App.newUser.streetAddress = "";
                    App.newUser.city = "";
                    App.newUser.state = "";
                    App.newUser.zipCode = "";
                    App.newUser.phone = "";
                    App.newUser.website = "";
                    App.newUser.eMailAddress = "";

                    await Navigation.PushAsync(new IntroPage());

                    break;
                case "candidate":
                    Console.WriteLine("button candidate");


                    App.newUser = new NewUserInfo();
                    App.newUser.isSupporter = false;
                    App.newUser.firstName = "";
                    App.newUser.lastName = "";
                    App.newUser.office = "";
                    App.newUser.officeCity = "";
                    App.newUser.officeState = "";
                    App.newUser.officeCounty = "";
                    App.newUser.officeDistrict = "";
                    App.newUser.party = "";
                    App.newUser.streetAddress = "";
                    App.newUser.city = "";
                    App.newUser.state = "";
                    App.newUser.zipCode = "";
                    App.newUser.phone = "";
                    App.newUser.website = "";
                    App.newUser.eMailAddress = "";
                    

                    await Navigation.PushAsync(new IntroPage());
                    break;


            }
        }



       

    }
}
