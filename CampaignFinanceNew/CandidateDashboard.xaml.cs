using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
//using System.Linq;

using Xamarin.Forms;
using CampaignFinanceNew;
using Plugin.Geolocator;
using System.Threading.Tasks;

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


        //AIzaSyDfeiCRXoUEb2ZNaq9WmgadSmeEKAiCIlw
        string googleCivicURL = "https://www.googleapis.com/civicinfo/v2/representatives?key=AIzaSyDfeiCRXoUEb2ZNaq9WmgadSmeEKAiCIlw&address=";



        public async Task<string> GetLocationInformation()
        {

            var locator = CrossGeolocator.Current;
            Console.WriteLine("this pos");
            var currentPosition = await locator.GetPositionAsync();
            Console.WriteLine("this pos-2");
            var address = await locator.GetAddressesForPositionAsync(currentPosition);
            Console.WriteLine("this pos-3");
            string currentUserString=""; 
            foreach(var item in address)
            {

                //Console.WriteLine(item.AdminArea + " " + item.CountryCode + " " + item.FeatureName + " " + item.Locality + " " + item.PostalCode + " " + item.SubAdminArea + " " + item.SubLocality + " " + item.SubThoroughfare + " " + item.Thoroughfare);

                // MD US 6707 Democracy Blvd Bethesda 20817 Montgomery  6707 Democracy Blvd

               currentUserString=item.FeatureName.Replace(" ", "%20") + "%20"+item.Locality.Replace(" ","%20")+"%20"+item.AdminArea;
                }
            Console.WriteLine(currentUserString);
            WebClient newClient = new WebClient();
            //var rawLocationData = newClient.DownloadString(googleCivicURL + currentUserString);
            Console.WriteLine("cheese-1");
            JObject parseData = JObject.Parse(newClient.DownloadString(googleCivicURL + currentUserString));
            Console.WriteLine("cheese-2");
            JObject divisionData = JObject.Parse(parseData.GetValue("divisions").ToString());
            Console.WriteLine("cheese-3");
            //JArray newData = JArray.Parse(parseData.GetValue("divisions").ToString());
            Console.WriteLine("cheese-43");
            var currKeys = divisionData.Properties();

            foreach(var things in currKeys)
            {
                string[] currentThings=things.Name.Split('/');

                string[] currentItem = currentThings[currentThings.Length - 1].Split(':');

                if(currentItem[0]=="state")
                {
                    App.currentLocation.state = currentItem[1];
                }
                else if(currentItem[0]=="county")
                {
                    App.currentLocation.getCountyType = 0;
                    App.currentLocation.countyName = currentItem[1];

                }
                else if(currentItem[0]=="parish")
                {
                    App.currentLocation.getCountyType = 1;
                    App.currentLocation.countyName = currentItem[1];
                }
                else if (currentItem[0] == "borough")
                {
                    App.currentLocation.getCountyType = 2;
                    App.currentLocation.countyName = currentItem[1];
                }
                else if (currentItem[0] == "cd")
                {
                    App.currentLocation.congressDistrict = Convert.ToInt32(currentItem[1]);
                }
                else if (currentItem[0] == "sldl")
                {
                    App.currentLocation.stateHouseDistrict = Convert.ToInt32(currentItem[1]);
                }
                else if (currentItem[0] == "sldu")
                {
                    App.currentLocation.stateSenateDistrict = Convert.ToInt32(currentItem[1]);
                }
                else if (currentItem[0] == "sldu")
                {
                    App.currentLocation.stateSenateDistrict = Convert.ToInt32(currentItem[1]);
                }
                else if (currentItem[0] == "place")
                {
                    App.currentLocation.cityName = currentItem[1].Replace('_', ' ');
                }


                //Console.WriteLine(currentThings[currentThings.Length - 1]);

            }



            return "hello";
        }




        public CandidateDashboard()
        {

            Console.WriteLine("helo george");


            GetLocationInformation();

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
