using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CampaignFinanceNew
{
    public partial class CreateCampaign : ContentPage
    {
        Uri submitCandidate = new Uri("http://www.cvx4u.com/web_service/create_campaign.php");
        WebClient sendClient = new WebClient();


        Campaign currentCampaign;


        InputView[] fields;
        bool isCreateCampaign;

        public CreateCampaign()
        {
            InitializeComponent();
            fields = new InputView[]{campaignName,campaignDescription,fundGoal};

            String currentEntry="";

            if(currentEntry!=null)
            {
                var currentString = sendClient.DownloadString("http://www.cvx4u.com/web_service/getCampaigns.php?campaignID=");
                JObject currentCampaignData = JObject.Parse(currentString);
            }





        }

        private void GoToCampaignWindow(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CandidateDashboard());
        }

        bool moveOn;
        private async Task<bool> CheckForBlanks(Button sender, EventArgs e)
        {
            if(campaignName.Text=="")
            {
                nameLabel.TextColor = Color.Red;
                moveOn = false;
            }
            if(campaignDescription.Text=="")
            {
                descLabel.TextColor = Color.Red;
                moveOn = false;
            }
            if(crowdfundOn.IsToggled==true && fundGoal.Text=="")
            {
                fundLabel.TextColor = Color.Red;
                moveOn = false;
            }
            if(crowdfundOn.IsToggled && DateTime.Compare(startDate.Date,endDate.Date) >=0 )
            {
                startDateLabel.TextColor = Color.Red;
                endDateLabel.TextColor = Color.Red;
                moveOn = false;

            }
            if(moveOn==true)
            {
                await SubmitCampaign();
                return true;
            }
            else
            {
                return false;
            }
        }






        private async Task<bool> SubmitCampaign()
        {
            var sendingParameters = new System.Collections.Specialized.NameValueCollection();
            sendingParameters.Add("campaignName", campaignName.Text);
            sendingParameters.Add("campaignDescription", campaignDescription.Text);
            sendingParameters.Add("goal", fundGoal.Text);
            sendingParameters.Add("isCrowdfund", crowdfundOn.IsToggled.ToString().ToLower());
            sendingParameters.Add("startDate", startDate.Date.ToShortDateString());
            sendingParameters.Add("endDate", endDate.Date.ToShortDateString());
            sendingParameters.Add("candidateId", App.currentUser.systemID);
            sendingParameters.Add("newCampaign", isCreateCampaign.ToString().ToLower());

            sendClient.UploadValues("http://www.cvx4u.com/web_service/create_campaign.php", sendingParameters);

            return true;


        }





        private void SubmitCampaignAsync(object sender, EventArgs e)
        {
            Console.WriteLine(campaignName.Text);
            Console.WriteLine(startDate.Date);
            Console.WriteLine("campaign is-- " + currentCampaign);
            var sendingParameters = new System.Collections.Specialized.NameValueCollection();
            sendingParameters.Add("campaignName", campaignName.Text);
            sendingParameters.Add("campaignDescription", campaignDescription.Text);
            sendingParameters.Add("goal", fundGoal.Text);
            sendingParameters.Add("startDate", startDate.Date.ToShortDateString());
            sendingParameters.Add("endDate", endDate.Date.ToShortDateString());
            sendingParameters.Add("candidateId", App.currentUser.systemID);
            if (currentCampaign == null)
            {
                Console.WriteLine("it is null");
                sendingParameters.Add("campaignID", "0");
            }
            else
            {
                sendingParameters.Add("campaignID", currentCampaign.campaignID);
            }
            /*
            Console.WriteLine("change is  " + sendingParameters.Get("campaignID"));
            Console.WriteLine("change isa  " + sendingParameters.Get("campaignName"));
            Console.WriteLine("change isb  " + sendingParameters.Get("campaignDescription"));
            Console.WriteLine("change isc  " + sendingParameters.Get("goal"));
            Console.WriteLine("change isd  " + sendingParameters.Get("startDate"));
            Console.WriteLine("change ise  " + sendingParameters.Get("endDate"));
            Console.WriteLine("change isf  " + sendingParameters.Get("candidateId"));*/

            var response=sendClient.UploadValues("http://www.cvx4u.com/web_service/create_campaign.php", sendingParameters);

            //sendClient.UploadValues("http://www.cvx4u.com/web_service/create_campaign.php", sendingParameters);
            Console.WriteLine(System.Text.Encoding.Default.GetString(response));
            GoToCampaignWindow(null, null);

           
        }

    }
}
