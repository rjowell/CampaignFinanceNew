using System;
using System.Collections.Generic;
using Stripe;
using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class PaymentPage : ContentPage
    {
        string[] ccExpiryMonths = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
        string[] ccExpiryYears = new string[] { "19", "20", "21", "22", "23", "24", "25", "26" };
        string ccNotice = "*We do not store your credit card info directly on our servers. Rather, we use Stripe.com, a highly trusted payment processor, to securely store and process payments. Learn more at Stripe.com";
        List<Entry> entries = new List<Entry>();
        public PaymentPage()
        {
            InitializeComponent();
            entries.Add(ccNumber);
            entries.Add(cvcEntry);
            entries.Add(eMailField);
            entries.Add(passwordField);

            foreach (Entry thisEntry in entries)
            {

                if (entries.IndexOf(thisEntry) != entries.Count - 1)
                {
                    thisEntry.Completed += (s, e) =>
                    {
                        if (entries.IndexOf(thisEntry) == 0)
                        {
                            expiryMonth.Focus();
                        }

                        else
                        {

                            entries[entries.IndexOf(thisEntry) + 1].Focus();
                        }
                    };
                }

            }



            StripeConfiguration.SetApiKey("pk_live_HTl5JEEmjEbq772AbJ3N6Ahl");
            Console.WriteLine("final valu s"+App.newUser.isSupporter);
            if (App.newUser.isSupporter==false)
            {
                ccNumber.IsVisible = false;
                expiryMonth.IsVisible = false;
                expiryYear.IsVisible = false;
                ccDisclamer.IsVisible = false;
            }
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
                        { "isSupporter", App.newUser.isSupporter.ToString() },
                        { "contactPerson",App.newUser.contactPerson},
                        { "phone", App.newUser.phone },
                        { "website", App.newUser.website },
                        { "party", App.newUser.party },
                        {"mailingAddress", App.newUser.streetAddress},
                        {"city", App.newUser.city},
                        { "state", App.newUser.state},
                        {"zipCode", App.newUser.zipCode},
                        {"office", App.newUser.office},



            };


            if (App.newUser.isSupporter == true)
            {
                var tokenOptions = new Stripe.TokenCreateOptions()
                {
                    Card = new Stripe.CreditCardOptions()
                    {
                        Number = ccNumber.Text,
                        ExpYear = long.Parse(ccExpiryYears[expiryYear.SelectedIndex]),
                        ExpMonth = long.Parse(ccExpiryMonths[expiryMonth.SelectedIndex]),
                        Cvc = cvcEntry.Text
                    }
                };



                var tokenService = new Stripe.TokenService();
                Stripe.Token stripeToken = tokenService.Create(tokenOptions);
                Console.WriteLine(stripeToken.Id);
               /* var customerOptions = new CustomerCreateOptions();
                var customerService = new CustomerService();
                customerOptions.SourceToken = stripeToken.Id;
                customerOptions.Email = App.newUser.eMailAddress;
                Customer newCustomer = customerService.Create(customerOptions);
                Console.WriteLine(newCustomer.Id);*/
                sendingParameters.Set("stripeToken", stripeToken.Id);


            }





          




            foreach (var keys in sendingParameters.AllKeys)
            {
                Console.WriteLine(keys + " " + sendingParameters.Get(keys));
                Console.WriteLine(ccNumber.Text + " " + ccExpiryYears[expiryYear.SelectedIndex] + " " + ccExpiryMonths[expiryMonth.SelectedIndex] + " " + cvcEntry.Text);
            }

            //var CreditProcess=new CreditCardProcess("5466160369828262", "05", "21", "847");

            //Console.WriteLine("jerkoff");
            DependencyService.Get<IFirebaseAuthenticator>().CreateNewUser(eMailField.Text, passwordField.Text, sendingParameters);
        }
    }
}

/*StripeConfiguration.SetApiKey("pk_live_HTl5JEEmjEbq772AbJ3N6Ahl");

            var tokenOptions = new Stripe.TokenCreateOptions()
            {
                Card = new Stripe.CreditCardOptions()
                {
                    Number = cardNumber,
                    ExpYear = long.Parse(cardExpYear),
                    ExpMonth = long.Parse(cardExpMonth),
                    Cvc = cardCVC
                }
            };

            var tokenService = new Stripe.TokenService();
            Stripe.Token stripeToken = tokenService.Create(tokenOptions);

            var customerOptions = new CustomerCreateOptions();
            var customerService = new CustomerService();
            customerOptions.SourceToken = stripeToken.Id;
            customerOptions.Email = App.newUser.eMailAddress;
            Customer newCustomer = customerService.Create(customerOptions);
            Console.WriteLine(newCustomer.Id);

            //Console.WriteLine("Token is"+stripeToken.Id);
*/