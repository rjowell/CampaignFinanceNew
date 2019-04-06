using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class RegistrationNotice : ContentPage
    {

        String candidateInfo = "\n" +
        	
        	"-Candidate Name" + "\n"+
            "-Office and Jurisdiction" + "\n" +"-Your Campaign Contact Person"+"\n"+ "-Your Campaign Mailing Address"+"\n"+ "-Your Campaign Phone Number"+"\n"+ "-Your Campiagn Website"+"\n"+ "-Your party"+"\n"+ "-A link to your local election office to verify your candidacy"+"\n"+"-An active e-mail address for account creation";
        String supporterInfo = "&#x0a;&#x0a;-Your Name&#x0a;-Your Mailing Address&#x0a;-Your current job title and emoployer name&#x0a;-A valid credit card&#x0a;-An Active E-mail address";
        public RegistrationNotice()
        {
            InitializeComponent();

            startButton.Clicked+= (sender, e) => {
                Navigation.PushAsync(new CreateName());
            
            };

            cancelButton.Clicked+= (sender, e) => {

                Navigation.PushAsync(new MainPage());
             };

            if(App.newUser.isSupporter==true)
            {
                noticeLabel.Text = supporterInfo;
            }
            else
            {
                noticeLabel.Text = candidateInfo;
            }
        }
    }
}
