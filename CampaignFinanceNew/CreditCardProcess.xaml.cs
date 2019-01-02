using System;
using System.Collections.Generic;
using Stripe;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class CreditCardProcess : ContentPage
    {



        public string CreateNewCustomer()
        {

        }



        public CreditCardProcess(string cardNumber, string cardExpMonth, string cardExpYear, string cardCVC)
        {
            InitializeComponent();
            StripeConfiguration.SetApiKey("pk_live_HTl5JEEmjEbq772AbJ3N6Ahl");

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








        }

       

       


    }
}
