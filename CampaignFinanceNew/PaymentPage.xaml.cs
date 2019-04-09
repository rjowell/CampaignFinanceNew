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
            NavigationPage.SetHasNavigationBar(this, false);


            backImage.Source = App.currentUser.getBackImage();

            //errorWindow.IsVisible = false;
            entries.Add(ccNumber);
            entries.Add(cvcEntry);
            entries.Add(eMailField);
            entries.Add(passwordField);
            //paymentForm.HeightRequest= Xamarin.Forms.Application.Current.MainPage.Height;

            noticeFrame.IsVisible = false;
           

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
                titleLabel.Text = "Verificaiton & Login Info";
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
            //Console.WriteLine("final valu s"+App.newUser.isSupporter);
            if (App.newUser.isSupporter==false)
            {
                ccNumber.IsVisible = false;
                ccNumLabel.IsVisible = false;
                ccGrid.IsVisible = false;
                spacer.IsVisible = false;
                expiryMonth.IsVisible = false;
                expYearLabel.IsVisible = false;
                expMonthLabel.IsVisible = false;
                cvcEntry.IsVisible = false;
                cvcLabel.IsVisible = false;
                expiryYear.IsVisible = false;
                ccDisclamer.IsVisible = false;
            }
            else
            {
                verificationURL.IsVisible = false;
                verifyNotice.IsVisible = false;
                verificationDescription.IsVisible = false;
                verficationLabel.IsVisible = false;
            }
            expiryMonth.ItemsSource = ccExpiryMonths;
            expiryYear.ItemsSource = ccExpiryYears;
            Console.WriteLine("select " + expiryYear.SelectedIndex);
            ccDisclamer.Text = ccNotice;
        }

        public async void GoBack(object buttOff, EventArgs e)
        {
            await Navigation.PushAsync(new ContactInfoPage());
        }

        public void CloseWindow(object button, EventArgs e)
        {
            noticeFrame.IsVisible = false;
        }


        public void ProcessNewUser(object sender, EventArgs e)
        {

            bool moveOn = true;

            if (App.newUser.isSupporter == true)
            {
                if (ccNumber.Text.Length != 16)
                {
                    ccNumLabel.TextColor = Color.Red;
                    moveOn = false;
                }

                if (expiryMonth.SelectedIndex == -1)
                {
                    expMonthLabel.TextColor = Color.Red;
                    moveOn = false;
                }
                if (expiryYear.SelectedIndex == -1)
                {
                    expYearLabel.TextColor = Color.Red;
                    moveOn = false;
                }
                if (cvcEntry.Text.Length != 3)
                {
                    cvcLabel.TextColor = Color.Red;
                    moveOn = false;
                }
            }
            else
            {
                if (verificationURL.Text == "")
                {
                    verficationLabel.TextColor = Color.Red;
                    moveOn = false;
                }
            }


            if (moveOn == true)
            {



                noticeFrame.IsVisible = true;
                noticeButton.IsVisible = false;
                noticeText.Text = "Sending...";



                var sendingParameters = new System.Collections.Specialized.NameValueCollection
                    {
                        { "firstName", App.newUser.firstName },
                        { "lastName", App.newUser.lastName },
                        { "eMail", eMailField.Text },
                        { "isSupporter", App.newUser.isSupporter.ToString().ToLower() },
                        { "contactPerson",App.newUser.contactPerson},
                        { "phone", App.newUser.phone },
                        { "website", App.newUser.website },
                        { "party", App.newUser.party },
                        {"mailingAddress", App.newUser.streetAddress},
                        {"city", App.newUser.city},
                        { "state", App.newUser.state},
                        {"zipCode", App.newUser.zipCode},
                        {"office", App.newUser.office},
                        {"officeDistrict",App.newUser.officeDistrict},
                        {"officeState",App.newUser.officeState},
                        {"officeCity",App.newUser.officeCity},
                        {"officeCounty",App.newUser.officeCounty},
                        {"issues",App.newUser.issues},
                        {"ideology",App.newUser.ideology},
                        {"verificationLink",verificationURL.Text},
                        {"jobTitle",App.newUser.jobTitle},
                        {"employerName",App.newUser.employerName},
                        {"employerCity",App.newUser.employerCity},
                        {"employerState",App.newUser.employerState}





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
                        sendingParameters.Set("ccExpiry", ccExpiryMonths[expiryMonth.SelectedIndex] + "/" + ccExpiryYears[expiryYear.SelectedIndex]);
                    }
                    catch (StripeException stripeExcept)
                    {
                        Console.WriteLine("errros is " + stripeExcept.StripeError.Message + " " + stripeExcept.StripeError.Code + " " + stripeExcept.StripeError.ErrorDescription);
                        noticeText.Text = ccWarning;
                        noticeButton.IsVisible = true;
                        moveOn = false;
                        //errorWindow.IsVisible = true;
                    }



                }


                //Console.WriteLine("send county is"+App.newUser.officeCounty);
                   DependencyService.Get<IFirebaseAuthenticator>().CreateNewUser(eMailField.Text, passwordField.Text, sendingParameters);


            }
        }
    }
}

