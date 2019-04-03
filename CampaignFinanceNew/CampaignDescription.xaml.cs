using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class CampaignDescription : ContentPage
    {
        public CampaignDescription()
        {
            InitializeComponent();

            if(App.newCampaign.isCrowdfund==false)
            {

                goalLabel.IsVisible = false;
                dollarEntry.IsVisible = false;
                endLabel.IsVisible = false;
                endDateEntry.IsVisible = false;

            }
        }


        WebClient newClient = new WebClient();

       

        public void ProcessButton(object button, EventArgs e)
        {
            Button currentButton = (Button)button;


            if (currentButton.Text == "Cancel")
            {
                Navigation.PushAsync(new CandidateDashboard());
            }
            else
            {


                var sendingParameters = new System.Collections.Specialized.NameValueCollection
                {
                    {"campaignName",App.newCampaign.campaignName},
                    {"isCrowdfund",App.newCampaign.isCrowdfund.ToString()},
                    {"campaignDescription",campDescription.Text},
                    {"candidateId",App.currentUser.systemID}
                };

                if(App.newCampaign.isCrowdfund==true)
                {
                    sendingParameters.Add("endDate", endDate.Date.ToString());
                    sendingParameters.Add("goal", goalInput.Text);
                }

                newClient.UploadValues("http://www.cvxu.com/web_service/create_campaign.php", sendingParameters);

            }
        }
    }
}
