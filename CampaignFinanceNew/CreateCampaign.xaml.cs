using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using System.Net;
namespace CampaignFinanceNew
{
    public partial class CreateCampaign : ContentPage
    {
        Uri submitCandidate = new Uri("http://www.cvx4u.com/web_service/create_campaign.php");
        WebClient sendClient = new WebClient();


        Campaign currentCampaign;


        public CreateCampaign(Campaign currentData)
        {
            InitializeComponent();


            if(currentData==null)
            {
                currentCampaign = null;
                titleLabel.Text = "Create Campaign";
                submitButton.Text = "Submit Campaign";
            }
            else
            {
                titleLabel.Text = "Edit Campaign";
                submitButton.Text = "Submit Changes";
                currentCampaign = currentData;
                campaignName.Text = currentData.campaignName;
                campaignDescription.Text = currentData.campaignDescription;
                fundGoal.Text = currentData.fundGoal;
                String[] startDateRaw = currentData.startDate.Split('/');
                String[] endDateRaw = currentData.endDate.Split('/');
                Console.WriteLine(startDateRaw[0]+" "+startDateRaw[1]+" "+startDateRaw[2]);
                startDate.Date = new DateTime().AddMonths(Convert.ToInt32(startDateRaw[0])-1).AddDays(Convert.ToInt32(startDateRaw[1])-1).AddYears(Convert.ToInt32(startDateRaw[2])-1);
                endDate.Date = new DateTime().AddMonths(Convert.ToInt32(endDateRaw[0])-1).AddDays(Convert.ToInt32(endDateRaw[1])-1).AddYears(Convert.ToInt32(endDateRaw[2])-1);
            }


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
            Console.WriteLine("change is  " + sendingParameters.Get("campaignID"));
            Console.WriteLine("change isa  " + sendingParameters.Get("campaignName"));
            Console.WriteLine("change isb  " + sendingParameters.Get("campaignDescription"));
            Console.WriteLine("change isc  " + sendingParameters.Get("goal"));
            Console.WriteLine("change isd  " + sendingParameters.Get("startDate"));
            Console.WriteLine("change ise  " + sendingParameters.Get("endDate"));
            Console.WriteLine("change isf  " + sendingParameters.Get("candidateId"));
            var response=sendClient.UploadValues("http://www.cvx4u.com/web_service/create_campaign.php", sendingParameters);
            Console.WriteLine(System.Text.Encoding.Default.GetString(response));
           
        }

    }
}
