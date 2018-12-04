using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
//using System.Linq;

using Xamarin.Forms;
using CampaignFinanceNew;

namespace CampaignFinanceNew
{
    public class Campaign
    {
        public String campaignName { get; set; }
        public String startDate { get; set; }
        public String endDate { get; set; }
        public String progress { get; set; }
        public String fundGoal { get; set; }
        public String campaignDescription { get; set; }
        public String campaignID { get; set; }
        public String posIndex { get; set; }

        public Campaign(string ID, string Name, string Description, string Start, string End, string Progress, string Goal)
        {
            campaignName = Name;
            startDate = Start;
            endDate = End;
            progress = Progress;
            campaignDescription = Description;
            campaignID = ID;
            fundGoal = Goal;
        }
    }




    public partial class CandidateDashboard : ContentPage
    {
        List<Campaign> fieldData;
        //String jsonData = new WebClient().DownloadString("http://www.cvx4u.com/web_service/getCampaigns.php");
       

        public CandidateDashboard()
        {
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

            //Console.WriteLine(array);
            foreach (JObject thisThing in array)
            {
                Console.WriteLine("point F");
                Campaign thisCampaign = new Campaign(thisThing.GetValue("CampaignID").ToString(),thisThing.GetValue("CampaignName").ToString(), thisThing.GetValue("CampaignDescription").ToString(), thisThing.GetValue("StartDate").ToString(),thisThing.GetValue("EndDate").ToString(), thisThing.GetValue("AmountRaised").ToString(),thisThing.GetValue("Goal").ToString());
                Console.WriteLine("point G");
                thisCampaign.posIndex = (fieldData.Count).ToString();
                Console.WriteLine("Current count is " + thisCampaign.posIndex);
                fieldData.Add(thisCampaign);
                Console.WriteLine("point H");


            }
        


       

            InitializeComponent();
            campaignDisplay.ItemsSource = fieldData;
            
           


        }

        private void ShowDonateWindow(Button sender, EventArgs e)
        {

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
