using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class PaymentPage : ContentPage
    {
        string[] ccExpiryMonths = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
        string[] ccExpiryYears = new string[] { "19", "20", "21", "22", "23", "24", "25", "26" };


        public PaymentPage()
        {
            InitializeComponent();
            expiryMonth.ItemsSource = ccExpiryMonths;
            expiryYear.ItemsSource = ccExpiryYears;
        }
    }
}
