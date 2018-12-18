using System;
using System.Collections.Generic;
using Stripe;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class CreditCardProcess : ContentPage
    {




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

            Console.WriteLine(stripeToken.Id);








        }

       

       


    }
}
