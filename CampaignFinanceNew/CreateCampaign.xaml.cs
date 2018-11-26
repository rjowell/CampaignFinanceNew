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

        bool isEditing;
        Campaign currentCampaign;


        public CreateCampaign(Campaign currentData)
        {
            InitializeComponent();


            if(currentData==null)
            {
                currentCampaign = null;
            }
            else
            {
                currentCampaign = currentData;
                campaignName.Text = currentData.campaignName;
                campaignDescription.Text = currentData.campaignDescription;
                fundGoal.Text = currentData.fundGoal;
                startDate.Date = new DateTime().AddMonths(Convert.ToInt32(currentData.startDate.Substring(0, 2))).AddDays(Convert.ToInt32(currentData.startDate.Substring(4,2))).AddYears(Convert.ToInt32(currentData.startDate.Substring(8)));
                endDate.Date = new DateTime().AddMonths(Convert.ToInt32(currentData.endDate.Substring(0, 2))).AddDays(Convert.ToInt32(currentData.endDate.Substring(4, 2))).AddYears(Convert.ToInt32(currentData.endDate.Substring(8)));
            }
        }

        private void SubmitCampaignAsync(object sender, EventArgs e)
        {
            Console.WriteLine(campaignName.Text);
            Console.WriteLine(startDate.Date);
            var sendingParameters = new System.Collections.Specialized.NameValueCollection();
            sendingParameters.Add("campaignName", campaignName.Text);
            sendingParameters.Add("campaignDescription", campaignDescription.Text);
            sendingParameters.Add("goal", fundGoal.Text);
            sendingParameters.Add("startDate", startDate.Date.ToShortDateString());
            sendingParameters.Add("endDate", endDate.Date.ToShortDateString());
            if (currentCampaign == null)
            {
                sendingParameters.Add("campaignToUpdate", "0");
            }
            else
            {
                sendingParameters.Add("campaignToUpdate", currentCampaign.campaignID);
            }
            sendClient.UploadValues("http://www.cvx4u.com/web_service/create_campaign.php", sendingParameters);
            //String submitString = "campaignName=" + campaignName.Text.Replace(" ", "%20") + "&goal=" + fundGoal.Text + "&startDate=" + startDate.Date.ToShortDateString() + "&endDate=" + endDate.Date.ToShortDateString();
            //Console.WriteLine(submitString);
            //var sentString = new StringContent(submitString, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
            //await connClient.PostAsync(submitCandidate, sentString);
        }

    }
}
