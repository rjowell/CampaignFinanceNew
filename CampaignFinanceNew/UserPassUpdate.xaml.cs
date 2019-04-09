using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class UserPassUpdate : ContentPage
    {
        public UserPassUpdate()
        {
            InitializeComponent();
            eMailSubmit.IsVisible = false;
            eMailCancel.IsVisible = false;
            eMailLabel.Text = App.currentUser.eMail;
            eMailField.Text = App.currentUser.eMail;
            eMailField.IsVisible = false;
            passwordSubmit.IsVisible = false;
            passwordCancel.IsVisible = false;
            noticeWindow.IsVisible = false;
            eMailField.IsEnabled = false;
            passwordField.IsEnabled = false;
            passwordField.Text = "*************";


            NavigationPage.SetHasNavigationBar(this, false);

            backImage.Source = App.currentUser.getBackImage();


            MessagingCenter.Subscribe<IFirebaseAuthenticator,int>(this, "AuthError", (sender,arg) => {

                if(arg==0)
                {
                    noticeWindow.IsVisible = true;
                    statusLabel.Text = "This is not a valid e-mail address. Please try again";

                }
                else if(arg==1)
                {
                    noticeWindow.IsVisible = true;
                    statusLabel.Text = "This E-Mail is already in use by another user. Please try again";
                }
                else if (arg == 2)
                {
                    noticeWindow.IsVisible = true;
                    statusLabel.Text = "E-Mail successfully changed.";
                }
                else
                {
                    noticeWindow.IsVisible = true;
                    statusLabel.Text = "Password successfully changed.";
                }

            });



        }

        public void CloseWindow(object sender, EventArgs e)
        {
            noticeWindow.IsVisible = false;
        }

        public void GoBack(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CandidateDashboard());
        }

        public void EditEMail(object sender, EventArgs e)
        {
            eMailSubmit.IsVisible = true;
            eMailCancel.IsVisible = true;
            changeEmail.IsVisible = false;
            eMailField.IsEnabled = true;
            eMailField.IsVisible = true;
            eMailLabel.IsVisible = false;
        }

        public void EditPassword(object sender, EventArgs e)
        {
            passwordSubmit.IsVisible = true;
            passwordCancel.IsVisible = true;
            changePassword.IsVisible = false;
            passwordField.IsEnabled = true;
            passwordField.Text = "";
        }

        public void CancelEMail(object sender, EventArgs e)
        {
            eMailSubmit.IsVisible = false;
            eMailCancel.IsVisible = false;
            changeEmail.IsVisible = true;
            eMailField.IsEnabled = false;
            eMailLabel.Text = App.currentUser.eMail;
            eMailField.IsVisible = false;
            eMailField.Text = App.currentUser.eMail;
        }

        public void CancelPassword(object sender, EventArgs e)
        {
            passwordSubmit.IsVisible = false;
            passwordCancel.IsVisible = false;
            changePassword.IsVisible = true;
            passwordField.IsEnabled = false;
            passwordField.Text = "*************";
        }

        public void UpdateEmail(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAuthenticator>().UpdateEMail(eMailField.Text);
            App.currentUser.eMail = eMailField.Text;
            eMailSubmit.IsVisible = false;
            eMailCancel.IsVisible = false;
            changeEmail.IsVisible = true;
            eMailField.IsVisible = false;
            eMailLabel.Text = eMailField.Text;
            eMailLabel.IsVisible = true;
        }

        public void UpdatePassword(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAuthenticator>().UpdatePassword(passwordField.Text);
            passwordField.Text = "*************";
            passwordSubmit.IsVisible = false;
            passwordCancel.IsVisible = false;
            changePassword.IsVisible = true;
        }
    }
}
