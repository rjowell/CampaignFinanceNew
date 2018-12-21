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

            emailEntry.Text = App.newUser.eMailAddress;

            statePicker.ItemsSource = App.states;
        }

        public void ProcessNewUser(Button sender, EventArgs e)
        {
            var sendingParameters = new System.Collections.Specialized.NameValueCollection
                    {
                        { "firstName", App.newUser.firstName },
                        { "lastName", App.newUser.lastName },
                        { "eMail", emailEntry.Text },
                        { "isSupporter", App.newUserIsSupporter.ToString() },
                        { "phone", phoneEntry.Text },
                        { "website", websiteEntry.Text },
                        { "party", App.newUser.party },
                        {"mailingAddress", addressEntry.Text},
                        {"city", cityEntry.Text},
                        { "state", App.states[statePicker.SelectedIndex]},
                        {"zipCode", zipCodeEntry.Text},
                        {"office", App.newUser.office}


            };
            foreach(var keys in sendingParameters.AllKeys)
            {
                Console.WriteLine(keys + " " + sendingParameters.Get(keys));
            }
            //Console.WriteLine("jerkoff");
            //DependencyService.Get<IFirebaseAuthenticator>().CreateNewUser(emailEntry.Text, passwordEntry.Text, sendingParameters);
        }

        public async void GoBack(Button sender, EventArgs e)
        {
            if(App.newUserIsSupporter==true)
            {
                await Navigation.PushAsync(new CreateName());
            }
            else
            {
                await Navigation.PushAsync(new OfficeSelection());
            }

        }
    }
}
