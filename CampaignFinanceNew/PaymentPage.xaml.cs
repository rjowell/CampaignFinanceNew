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
        string ccWarning = "Credit Card Info is invalid.\nPlease Try Again";
        string userNotice = "A User already exsits with that e-mail. Please Try Again--";

        List<Entry> entries = new List<Entry>();
        public PaymentPage()
        {
            InitializeComponent();
            //errorWindow.IsVisible = false;
            entries.Add(ccNumber);
            entries.Add(cvcEntry);
            entries.Add(eMailField);
            entries.Add(passwordField);
            //paymentForm.HeightRequest= Xamarin.Forms.Application.Current.MainPage.Height;

            noticeWindow.IsVisible = false;
            noticeWindow.TranslationY = 200;
            noticeWindow.TranslationX = 40;

            MessagingCenter.Subscribe<IFirebaseAuthenticator>(this,"Go",(sender) => {

                noticeText.Text = "A user already exists\nwith that e-mail\nPlease Try Again";
                noticeButton.IsVisible = true;
            
            });

            if (App.newUser.isSupporter==true)
            {
                titleLabel.Text = "Payment Info & Login";
            }
            else
            {
                titleLabel.Text = "Login Info";
            }

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
                ccNumLabel.IsVisible = false;
                expiryMonth.IsVisible = false;
                expYearLabel.IsVisible = false;
                expMonthLabel.IsVisible = false;
                cvcEntry.IsVisible = false;
                cvcLabel.IsVisible = false;
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

        public void CloseWindow(Button button, EventArgs e)
        {
            noticeWindow.IsVisible = false;
        }


        public void ProcessNewUser(Button sender, EventArgs e)
        {

            bool moveOn = true;
            noticeWindow.IsVisible = true;
            noticeButton.IsVisible = false;
            noticeText.Text = "Sending...";

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
                        {"district",App.newUser.district},
                        {"officeState",App.newUser.officeState}


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

                try
                {
                    Stripe.Token stripeToken = tokenService.Create(tokenOptions);
                    Console.WriteLine(stripeToken.StripeResponse);
                    sendingParameters.Set("stripeToken", stripeToken.Id);
                    sendingParameters.Set("lastFour", ccNumber.Text.Substring(12, 4));
                }
                catch(StripeException stripeExcept)
                {
                    Console.WriteLine("errros is " + stripeExcept.StripeError.Message+" "+stripeExcept.StripeError.Code+" "+stripeExcept.StripeError.ErrorDescription);
                    noticeText.Text = ccWarning;
                    noticeButton.IsVisible = true;
                    moveOn = false;
                    //errorWindow.IsVisible = true;
                }
               


            }





          




            foreach (var keys in sendingParameters.AllKeys)
            {
                Console.WriteLine(keys + " " + sendingParameters.Get(keys));
                //Console.WriteLine(ccNumber.Text + " " + ccExpiryYears[expiryYear.SelectedIndex] + " " + ccExpiryMonths[expiryMonth.SelectedIndex] + " " + cvcEntry.Text);
            }

            //var CreditProcess=new CreditCardProcess("5466160369828262", "05", "21", "847");

            //Console.WriteLine("jerkoff");
            if (moveOn == true)
            {
                Console.WriteLine("hello rge");




                    DependencyService.Get<IFirebaseAuthenticator>().CreateNewUser(eMailField.Text, passwordField.Text, sendingParameters);

               
            }
            //await DependencyService.Get<IFirebaseAuthenticator>().CreateNewUserAsync(eMailField.Text, passwordField.Text, sendingParameters);

           
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