using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class HowToGetPayment : ContentPage
    {
        String[] paymentMethods = { "PayPal", "Certified Check Mailed", "Other" };

        public HowToGetPayment( ) 
        {
            InitializeComponent();
            paymentSelect.ItemsSource = paymentMethods;
            statePicker.ItemsSource = App.stateAbbr;
            backImage.Source = App.currentUser.getBackImage();
            NavigationPage.SetHasNavigationBar(this, false);
            if (App.newUser.payoutEMail!="" && App.newUser.payoutEMail != null)
            {
                eMailEntry.Text = App.newUser.payoutEMail;
            }
            if(App.newUser.payoutAddress!="" && App.newUser.payoutAddress!= null)
            {
                String[] addressElements = App.newUser.payoutAddress.Split(':');
                addressEntry.Text = addressElements[0];
                cityEntry.Text = addressElements[1];
                statePicker.SelectedIndex = Array.IndexOf(App.stateAbbr, addressElements[2]);
                zipCodeEntry.Text = addressElements[3];
            }

            addressBox.IsVisible = false;
            otherBox.IsVisible = false;
            eMailBox.IsVisible = false;

            goBackButton.Clicked += (sender, e) => {

                Navigation.PushAsync(new ContactInfoPage());

            };

            submitButton.Clicked += (sender, e) => {

                if(addressBox.IsVisible==true)
                {
                    App.newUser.payoutAddress = addressEntry.Text + ":" + cityEntry.Text + ":" + App.stateAbbr[statePicker.SelectedIndex] + ":" + zipCodeEntry.Text;
                }
                if(eMailBox.IsVisible==true)
                {
                    App.newUser.payoutEMail = eMailEntry.Text;
                }
                if(otherBox.IsVisible==true)
                {
                    App.newUser.payoutOther = otherEntry.Text;
                }

                Navigation.PushAsync(new PaymentPage());


             };

            paymentSelect.SelectedIndexChanged += (sender, e) => { 
            
            if(((Picker)sender).SelectedIndex==0)
                {
                    addressBox.IsVisible = false;
                    otherBox.IsVisible = false;
                    eMailBox.IsVisible = true;
                }
            else if(((Picker)sender).SelectedIndex==1)
                {
                    addressBox.IsVisible = true;
                    otherBox.IsVisible = false;
                    eMailBox.IsVisible = false;
                }
            else
                {
                    addressBox.IsVisible = false;
                    otherBox.IsVisible = true;
                    eMailBox.IsVisible = false;
                }
            
            };
        }
    }
}
