using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class ContactInfoPage : ContentPage
    {
        public ContactInfoPage()
        {
            InitializeComponent();
            addressEntry.Text = App.newUser.streetAddress;
            cityEntry.Text = App.newUser.city;
            statePicker.SelectedIndex = Array.IndexOf(App.states, App.newUser.state);
            zipCodeEntry.Text = App.newUser.zipCode;
            phoneEntry.Text = App.newUser.phone;
            if(App.newUserIsSupporter==true)
            {
                websiteEntry.IsVisible = false;
                websiteLabel.IsVisible = false;
            }
            else
            {
                websiteEntry.Text = App.newUser.website;
            }



            statePicker.ItemsSource = App.states;
        }

       

        public async void ProcessButton(Button sender, EventArgs e)
        {
            App.newUser.streetAddress = addressEntry.Text;
            App.newUser.city = cityEntry.Text;
            App.newUser.state = App.stateAbbr[statePicker.SelectedIndex];
            App.newUser.zipCode = zipCodeEntry.Text;
            App.newUser.phone = phoneEntry.Text;
            App.newUser.website = websiteEntry.Text;


            if(sender.ClassId=="Back")
            {

            
            if (App.newUserIsSupporter==true)
            {
                await Navigation.PushAsync(new CreateName());
            }
            else
            {
                await Navigation.PushAsync(new OfficeSelection());
            }
            }
            else
            {
                await Navigation.PushAsync(new PaymentPage());
            }

        }
    }
}
