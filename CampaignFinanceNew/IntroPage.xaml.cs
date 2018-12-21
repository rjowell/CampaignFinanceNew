using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class IntroPage : ContentPage
    {
        string supporterIntroText = "ActNearMe is a fundraising platform that lets you support candidates and causes near you...wherever you are in the United States. To comply with relevant regulations, we are required to collect various demographic information about you, inclduing your mailing address. We will also securely store your credit card information to make contributions easy. Click the button below to begin";
        string candidateIntroText = "ActNearMe is a fundraising platform that lets you reach supporters in your area, whether they are permanent residents or just passing through. You can set up a page to ask for general donations, or set up a crowdfunding campaign that people can contribute to. Click on the Start button to begin the registration process";

        public IntroPage()
        {
            InitializeComponent();
            if(App.newUserIsSupporter==true)
            {
                introLabel.Text = supporterIntroText;
            }
            else
            {
                introLabel.Text = candidateIntroText;
            }

        }

        public async void Start(Button butt, EventArgs e)
        {
            await Navigation.PushAsync(new CreateName());
        }
    }
}
