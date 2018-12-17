using System;
using System.Collections.Generic;
using Stripe;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class CreditCardProcess : ContentPage
    {

        StripeClient paymentClient = new StripeClient("[test key here]");

        public CreditCardProcess()
        {
            InitializeComponent();
           
            CreditCard currentCard = new CreditCard();
            currentCard.Number = creditCardNumber.Text;
            currentCard.ExpMonth = Convert.ToInt32(ccExpiryMonth.Text);
            currentCard.ExpYear = Convert.ToInt32(ccExpiryYear.Text);

            StripeObject newToken = paymentClient.CreateCardToken(currentCard);



        }

       

       


    }
}
