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
                        { "eMail", App.newUser.eMailAddress },
                        { "isSupporter", App.newUserIsSupporter.ToString() },
                        { "phone", App.newUser.phone },
                        { "website", App.newUser.website },
                        { "party", App.newUser.party },
                        {"mailingAddress", App.newUser.streetAddress},
                        {"city", App.newUser.city},
                        { "state", App.newUser.state},
                        {"zipCode", App.newUser.zipCode},
                        {"office", App.newUser.office}


            };
            Console.WriteLine(sendingParameters.AllKeys);
            //Console.WriteLine("jerkoff");
            DependencyService.Get<IFirebaseAuthenticator>().CreateNewUser(emailEntry.Text, passwordEntry.Text, sendingParameters);
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
