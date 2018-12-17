using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
//using System.Linq;

using Xamarin.Forms;
using CampaignFinanceNew;
using System.Collections.Specialized;

namespace CampaignFinanceNew
{
    public class Campaign
    {
        public String campaignName { get; set; }
        public String startDate { get; set; }
        public String endDate { get; set; }
        public String progress { get; set; }
        public String fundGoal { get; set; }
        public double progressFactor { get; set; }
        public String progressDisplay { get; set; }
        public String campaignDescription { get; set; }
        public String campaignID { get; set; }
        public String posIndex { get; set; }
        public String candidateDisplayName { get; set; }
        public String office { get; set; }
        public String setEditButton { get; set; }

        public Campaign(string iD, string campName, string campDescription, string firstName, string lastName, string office, string state, string district, string start, string end, string campProgress, string goal)
        {
            campaignName = campName;
            candidateDisplayName = firstName + " " + lastName + "|Running For: " + office + " " + state + "-" + district;
            startDate = start;
            endDate = end;
            progress = campProgress;
            progressDisplay = progress + "/" + goal;
            progressFactor = Math.Round(Convert.ToDouble(campProgress) / Convert.ToDouble(goal),2);
            campaignDescription = campDescription;
            campaignID = iD;
            fundGoal = goal;
            if(App.currentUser.isSupporter==true)
            {
                setEditButton = "Donate";
            }
            else
            {
                setEditButton = "Edit";
            }
        }
    }




    public partial class CandidateDashboard : ContentPage
    {

        List<Campaign> fieldData;
        //String jsonData = new WebClient().DownloadString("http://www.cvx4u.com/web_service/getCampaigns.php");
        WebClient client = new WebClient();
        NameValueCollection donationData=new NameValueCollection();
        //AIzaSyDfeiCRXoUEb2ZNaq9WmgadSmeEKAiCIlw
       








        public CandidateDashboard()
        {

            //Console.WriteLine("helo georg "+App.currentUser.isSupporter);



            //GetLocationInformation();
            Console.WriteLine(App.currentLocation.cityName + " " + App.currentLocation.state+" "+App.currentLocation.congressDistrict);
            Console.WriteLine("helo george-1");
           


            WebClient thisClient = new WebClient();
            Console.WriteLine("point A");

            //var userJsonData=thisClient.DownloadString("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID=" + App.currentUser.userFirebaseID);
            Console.WriteLine("point B");
            //Console.WriteLine("idents iss"+App.currentUser.userFirebaseID);
            //App.currentUser.systemID = JObject.Parse(userJsonData).GetValue("CandidateId").ToString();
            Console.WriteLine("point C");
            String rawData;
            if(App.currentUser.isSupporter==true)
            {
                rawData = thisClient.DownloadString("http://www.cvx4u.com/web_service/getCampaigns.php?id=0");
            }
            else
            {
                rawData = thisClient.DownloadString("http://www.cvx4u.com/web_service/getCampaigns.php?id=" + App.currentUser.systemID);
            }
           
            Console.WriteLine("point D "+ App.currentUser.systemID);
            fieldData = new List<Campaign>();
            JArray array = JArray.Parse(rawData);
            Console.WriteLine("point E");

            //thisLabel.Text = thisThing.ToString();
            //public Campaign(string iD, string campName, string campDescription, string firstName, string lastName, string office, string state, string district, string start, string end, string campProgress, string goal)

            //Console.WriteLine(array);
            foreach (JObject thisThing in array)
            {
                Console.WriteLine("point F");
                Campaign thisCampaign = new Campaign(thisThing.GetValue("CampaignID").ToString(),thisThing.GetValue("CampaignName").ToString(), thisThing.GetValue("CampaignDescription").ToString(), thisThing.GetValue("FirstName").ToString(), thisThing.GetValue("LastName").ToString(), thisThing.GetValue("CandidateOffice").ToString(), thisThing.GetValue("State").ToString(), thisThing.GetValue("District").ToString(), thisThing.GetValue("StartDate").ToString(),thisThing.GetValue("EndDate").ToString(), thisThing.GetValue("AmountRaised").ToString(),thisThing.GetValue("Goal").ToString());
                Console.WriteLine("point G");
                thisCampaign.posIndex = (fieldData.Count).ToString();
                Console.WriteLine("Current count is " + thisCampaign.posIndex);
                fieldData.Add(thisCampaign);
                Console.WriteLine("point H");


            }

            foreach(Campaign theese in fieldData)
            {
                Console.WriteLine(theese.campaignID + " " + theese.campaignName + " " + theese.candidateDisplayName + " " + theese.progressFactor);
            }





            InitializeComponent();

            if(App.currentUser.isSupporter==false)
            {
                createCampaignButton.IsVisible = true;
               

            }
           

            menuBlock.IsVisible = false;
           

            //campNameLabel.FontFamily="Times";
            menuButton.Source = ImageSource.FromResource("CampaignFinanceNew.menusandwich.png");
            searchButton.Source= ImageSource.FromResource("CampaignFinanceNew.searchglass.png");
            //menuButton.Image = (Xamarin.Forms.FileImageSource)ImageSource.FromResource("CampaignFinanceNew.MenuSandwich.png");      
            campaignDisplay.ItemsSource = fieldData;
           
           


        }

        private void ShowDonateWindow(Button sender, EventArgs e)
        {
            Console.WriteLine(sender.ClassId);
            currentSelectedCampaign = sender.ClassId;
        }

        private void ProcessDonation(Button sender, EventArgs e)
        {

            bool donationExists=false;
            foreach(string currentDonations in App.currentUser.campaignsSupported)
            {
                string[] currentItem = currentDonations.Split(':');
                if(currentItem[0]==currentSelectedCampaign)
                {
                    donationExists = true;
                }

            }
            if (donationExists == false)
            {
                donationData.Add("supporterID", App.currentUser.systemID);
                donationData.Add("donorAmount", donationField.Text);
                donationData.Add("campaignID", currentSelectedCampaign);
                App.currentUser.campaignsSupported=client.UploadValues(new Uri("http://www.cvx4u.com/web_service/processDonation.php"), donationData).ToString().Split(',');
            }
            else
            {

            }


        }

        public void SignOutUser(Button sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAuthenticator>().Logout();

        }
        String currentSelectedCampaign;
        private void ShowMenu(Button sender, EventArgs e)
        {
            if(menuBlock.IsVisible==true)
            {
                menuBlock.IsVisible = false;
            }
            else
            {
                menuBlock.IsVisible = true;
            }
        }

        private void OpenCreateCampaign(Button sender, EventArgs e)
        {

            if (sender.ClassId == null)
            {
                Navigation.PushAsync(new CreateCampaign(null));
            }
            else
            {
                Navigation.PushAsync(new CreateCampaign(fieldData[Convert.ToInt32(sender.ClassId)]));
            }

            //Console.WriteLine("The is is "+ sender.ClassId);


        }
    }
}
