using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class PaymentPage : ContentPage
    {
        string[] ccExpiryMonths = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
        string[] ccExpiryYears = new string[] { "19", "20", "21", "22", "23", "24", "25", "26" };
        string ccNotice = "*We do not store your credit card info directly on our servers. Rather, we use Stripe.com, a highly trusted payment processor, to securely store and process payments. Learn more at Stripe.com";

        public PaymentPage()
        {
            InitializeComponent();
            expiryMonth.ItemsSource = ccExpiryMonths;
            expiryYear.ItemsSource = ccExpiryYears;
            ccDisclamer.Text = ccNotice;
        }

        public async void GoBack(Button buttOff, EventArgs e)
        {
            await Navigation.PushAsync(new ContactInfoPage());
        }


        public void ProcessNewUser(Button sender, EventArgs e)
        {
            var sendingParameters = new System.Collections.Specialized.NameValueCollection
                    {
                        { "firstName", App.newUser.firstName },
                        { "lastName", App.newUser.lastName },
                        { "eMail", eMailField.Text },
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
            foreach (var keys in sendingParameters.AllKeys)
            {
                Console.WriteLine(keys + " " + sendingParameters.Get(keys));
            }
            //Console.WriteLine("jerkoff");
            //DependencyService.Get<IFirebaseAuthenticator>().CreateNewUser(eMailField.Text, passwordField.Text, sendingParameters);
        }
    }
}
