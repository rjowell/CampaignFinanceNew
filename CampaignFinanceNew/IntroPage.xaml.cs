using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class IntroPage : ContentPage
    {
        string supporterIntroText = "ActNearMe is a fundraising platform that lets you support candidates and causes near you...wherever you are in the United States. \n\n To comply with relevant regulations, we need to collect some demographic information about you. \n\n We will also securely store your credit card information to make contributing easy. \n\n Click the button below to begin";
        string candidateIntroText = "ActNearMe is a fundraising platform that lets you reach supporters in your area, whether they are residents or just passing through. \n\n You can set up a Campaign Page to ask for general donations, or set up a crowdfunding campaign to allow supporters to contribute toward a goal. \n\n All contributions are subject to a 5% app fee (This is how we are compensated). \n\n Click on the Start button to begin the registration process";

        public IntroPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            backImage.Source = App.currentUser.getBackImage();
            if (App.newUser.isSupporter==true)
            {
                introLabel.Text = supporterIntroText;
            }
            else
            {
                introLabel.Text = candidateIntroText;
            }


           cancelButton.Clicked += (sender, e) => {

               Navigation.PushAsync(new MainPage());

           };
          

        }

        public async void Start(object butt, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationNotice());
        }
    }
}
