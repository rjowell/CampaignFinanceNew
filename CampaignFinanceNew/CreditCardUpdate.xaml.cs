using System;
using System.Collections.Generic;
using System.Net;
using Stripe;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class CreditCardUpdate : ContentPage
    {
        string[] months = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
        string[] years = { "20", "21", "22", "23", "24", "25", "26" };

        public void ShowEditButtons(object sender, EventArgs e)
        {
            editButton.IsVisible = false;
            submit.IsVisible = true;
            cancel.IsVisible = true;
            ccNumber.IsEnabled = true;
            expMonth.IsEnabled = true;
            expYear.IsEnabled = true;
            cvcLabel.IsEnabled = true;
        }

        public void Cancel(object sender, EventArgs e)
        {
            ccNumber.Text= "**** **** **** " + App.currentUser.ccLastFour;
            ccNumber.IsEnabled = false;
            cvcLabel.Text = "***";
            cvcLabel.IsEnabled = false;
            expMonth.ItemsSource = months;
            expMonth.IsEnabled = false;

            expYear.ItemsSource = years;
            expYear.IsEnabled = false;
            submit.IsVisible = false;
            cancel.IsVisible = false;

            expMonth.SelectedIndex = Convert.ToInt32(expiryDate[0]) - 1;
            expYear.SelectedIndex = Array.IndexOf(years, expiryDate[1]);

        }

        string[] expiryDate;

        public CreditCardUpdate()
        {
            InitializeComponent();
             expiryDate= App.currentUser.ccExpiryDate.Split('/');

            ccNumber.Text = "**** **** **** " + App.currentUser.ccLastFour;
            ccNumber.IsEnabled = false;
            cvcLabel.Text = "***";
            cvcLabel.IsEnabled = false;
            expMonth.ItemsSource = months;
            expMonth.IsEnabled = false;
            
            expYear.ItemsSource = years;
            expYear.IsEnabled = false;
            submit.IsVisible = false;
            cancel.IsVisible = false;

            expMonth.SelectedIndex = Convert.ToInt32(expiryDate[0]) - 1;
            expYear.SelectedIndex = Array.IndexOf(years, expiryDate[1]);

    


        }

        public void GoBack(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CandidateDashboard());


        }

        public void ProcessNewCard()
        {
            StripeConfiguration.SetApiKey("pk_live_HTl5JEEmjEbq772AbJ3N6Ahl");

            WebClient newClient = new WebClient();

            var tokenOptions = new Stripe.TokenCreateOptions()
            {
                Card = new Stripe.CreditCardOptions()
                {
                    Number = ccNumber.Text,
                    ExpYear = long.Parse(years[expYear.SelectedIndex]),
                    ExpMonth = long.Parse(months[expMonth.SelectedIndex]),
                    Cvc = cvcLabel.Text
                }
            };

            var tokenService = new Stripe.TokenService();

            try
            {
                Stripe.Token stripeToken = tokenService.Create(tokenOptions);
                var sendingParameters = new System.Collections.Specialized.NameValueCollection();
                sendingParameters.Set("newToken", stripeToken.Id);
                sendingParameters.Set("userID", App.currentUser.systemID);
                sendingParameters.Set("newLastFour", ccNumber.Text.Substring(12));
                sendingParameters.Set("newExpiryDate", months[expMonth.SelectedIndex] + "/" + years[expYear.SelectedIndex]);
                newClient.UploadValues("http://www.cvx4u.com/web_service/updateCreditCard.php", sendingParameters);
                App.currentUser.ccLastFour = ccNumber.Text.Substring(12);
                App.currentUser.ccExpiryDate = months[expMonth.SelectedIndex] + "/" + years[expYear.SelectedIndex];


            }
            catch (StripeException stripeExcept)
            {
                Console.WriteLine("errros is " + stripeExcept.StripeError.Message + " " + stripeExcept.StripeError.Code + " " + stripeExcept.StripeError.ErrorDescription);

            }

        }
    }
}
