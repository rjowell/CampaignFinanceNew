using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class CampaignDetails : ContentPage
    {

        WebClient client = new WebClient();
        DonationWindow givingWindow;
        String lastDonationDate;
        String campaignIDNumber;

        public void HideDonateWindow(object sender, EventArgs e)
        {
            givingWindow.closeWindow();
          
        }

        public void ProcessDonation(object sender, EventArgs e)
        {


            givingWindow.ProcessDonation();
          
           


            donateWindow.IsVisible = false;
        }

        public void ConfirmDonation(Object sender, EventArgs e)
        {



            givingWindow.confirmDonation();


        }

        public void ShowDonateWindow(Object sender, EventArgs e)
        {
            //donateWindow.IsVisible = true;
          
                givingWindow = new DonationWindow(donateWindow, campaignIDNumber, campiagnTitle.Text, false, "");


            givingWindow.openWindow();
            // confirmLabel,confirmAmount,confirmTo,confirmCampaign,donateConfirm
        }




        public CampaignDetails(string campaignNumber)

        {
            InitializeComponent();

            lastDonationDate = "";

            donateWindow.IsVisible = false;
            campaignIDNumber = campaignNumber;

            //Init donate window
            /*
            confirmLabel.IsVisible = false;
            confirmAmount.IsVisible = false;
            confirmAmount.Text = "";
            confirmTo.IsVisible = false;
            donateConfirm.IsVisible = false;
            /*
            foreach(CampaignInfo things in App.currentUser.campaignsSupported)
            {
                if(things.campaignId==donateSubmit.ClassId)
                {
                    contributeDate = things.dateGiven;
                    break;
                }
            }*/

            

           

            string rawData = client.DownloadString("http://www.cvx4u.com/web_service/getCampaigns.php?campaignID="+campaignNumber);
            JObject sortedData = JObject.Parse(rawData);
            //Console.WriteLine(sortedData.GetValue("CampaignName")+" "+ sortedData.GetValue("CampaignDescription"));

            /*<Label x:Name="campiagnTitle"/>
            <Label x:Name="campaignDescription"/>
            <Label x:Name="candidateName"/>
            <Label x:Name="candidateOffice"/>*/

            campiagnTitle.Text = sortedData.GetValue("CampaignName").ToString();
            campaignDescription.Text= sortedData.GetValue("CampaignDescription").ToString();
            candidateName.Text=sortedData.GetValue("FirstName")+" "+sortedData.GetValue("LastName");
            candidateOffice.Text = sortedData.GetValue("Office").ToString();
            officeDistrict.Text = sortedData.GetValue("OfficeState") + "-" + sortedData.GetValue("District");
            campaignDonorNotice.IsVisible = false;
            foreach(CampaignInfo things in App.currentUser.campaignsSupported)
            {
                if(things.campaignId==campaignNumber)
                {
                    if(lastDonationDate=="" || DateTime.Parse(things.dateGiven).CompareTo(DateTime.Parse(lastDonationDate)) >= 0)
                    {
                        lastDonationDate = things.dateGiven;
                    }
                    campaignDonorNotice.Text = "Your last donation to this campaign was on " + lastDonationDate;
                    campaignDonorNotice.IsVisible = true;
                }
            }


        }
    }
}
