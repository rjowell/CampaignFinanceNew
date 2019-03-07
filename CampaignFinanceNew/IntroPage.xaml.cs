using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class IntroPage : ContentPage
    {
        string supporterIntroText = "ActNearMe is a fundraising platform that lets you support candidates and causes near you...wherever you are in the United States. \n\n To comply with relevant regulations, we need to collect some demographic information about you. \n\n We will also securely store your credit card information to make contributing easy. \n\n Click the button below to begin";
        string candidateIntroText = "ActNearMe is a fundraising platform that lets you reach supporters in your area, whether they are residents or just passing through. \n\n You can set up a page to ask for general donations, or set up a crowdfunding campaign to allow supporters to contribute toward a goal. \n\n Click on the Start button to begin the registration process";

        public IntroPage()
        {
            InitializeComponent();
            Random rand = new Random();
            backImage.Source = rand.Next(5).ToString() + ".png";
            if (App.newUser.isSupporter==true)
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
