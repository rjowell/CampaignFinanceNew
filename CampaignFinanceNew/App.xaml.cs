﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CampaignFinanceNew
{

    public class CampaignInfo
    {
        public String campaignId { get; set; }
        public String amount { get; set; }
        public String dateGiven { get; set; }
    }


    public class CurrentUserInfo
    {
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String systemID { get; set; }
        public String userFirebaseID { get; set; }
        public bool isSupporter { get; set; }
        //public String[] campaignsSupported { get; set; }
        public String[] currentCampaigns { get; set; }
        public List<CampaignInfo> campaignsSupported { get; set; }

        WebClient newClient = new WebClient();


        public void ProcessCampDonations(String dataIn)
        {
            Console.WriteLine("point G");
            campaignsSupported = new List<CampaignInfo>();
            Console.WriteLine("point G-1");
            Console.WriteLine(dataIn[0]);
            String[] currentInfo = dataIn.Split(',');
            Console.WriteLine("point H");
            Console.WriteLine("liength is " + currentInfo.Length);
            foreach (String item in currentInfo)
            {
                Console.WriteLine("ITem is " + item + ".e");
                if (item != "")
                {
                    Console.WriteLine("point I");
                    String[] currentItem = item.Split(':');
                    Console.WriteLine("point J "+ currentItem[0]+" "+ currentItem[1]+" "+ currentItem[2]);

                    CampaignInfo currentCamp = new CampaignInfo();
                    Console.WriteLine("point K");
                    currentCamp.amount = currentItem[1];
                    Console.WriteLine("point L");
                    currentCamp.campaignId = currentItem[0];
                    Console.WriteLine("point M");
                    currentCamp.dateGiven = currentItem[2];
                    Console.WriteLine("point ZZ");
                    campaignsSupported.Add(currentCamp);
                    Console.WriteLine("point O");
                }
            }

        }


        public Task<string> SetUserInfo(String firebaseID)
        {

            String userJsonData = newClient.DownloadString("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID="+firebaseID);
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            userFirebaseID = firebaseID;
            Console.WriteLine(userJsonData);
            JObject currentUserString = JObject.Parse(userJsonData);
            firstName = currentUserString.GetValue("FirstName").ToString();
            lastName = currentUserString.GetValue("LastName").ToString();
            Console.WriteLine("Your name is " + firstName + " " + lastName);
            /*{"FirstName":"Russell","LastName":"Jowell","MailingAddress":"4500 Connecticut Ave nw #203","City":"Washington ","State":"DC","Zip":"20008","EMail":"russ.jowell@gmail.com","Phone":"2814608568","CampaignsSupported":null,"SupporterID":"1000","FirebaseID":"h9s7cK7qoqSCxOaGDcftnnTwtS22","StripeCustomerID":"cus_ELQh45iGJf5gt9"}
            */
            Console.WriteLine("point A");
            if (JObject.Parse(userJsonData).ContainsKey("CandidateId") == false)
            {
                Console.WriteLine("point B");

                systemID = currentUserString.GetValue("SupporterID").ToString();
                isSupporter = true;
                Console.WriteLine("point C");
                Console.WriteLine("Info si "+currentUserString.GetValue("CampaignsSupported"));
                campaignsSupported = new List<CampaignInfo>();
                if (currentUserString.GetValue("CampaignsSupported").ToString() != "")
                {
                    ProcessCampDonations(currentUserString.GetValue("CampaignsSupported").ToString());
                }
               
            }
            else
            {
                systemID = currentUserString.GetValue("CandidateId").ToString();
                Console.WriteLine("point E");

                currentCampaigns = currentUserString.GetValue("CampaignIDs").ToString().Split(',');
                Console.WriteLine("point F");
                isSupporter = false;
            }

            tcs.SetResult("true");
            return tcs.Task;

        }


    }

    public class NewUserInfo
    {
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String office { get; set; }
        public String district { get; set; }
        public String party { get; set; }
        public String streetAddress { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public int officePickerIndex { get; set; }
        public String zipCode { get; set; }
        public String phone { get; set; }
        public String website { get; set; }
        public String eMailAddress { get; set; }
        public bool isSupporter { get; set; }
        public String contactPerson { get; set; }
        public String officeState { get; set; }
    }

    public class CurrentLocationInfo
    {
        public String state { get; set; }
        public String[] countyType = new string[] { "county", "parish", "borough" };
        public int getCountyType { get; set; }
        public String countyName { get; set; }
        public String cityName { get; set; }
        public int cityCouncilDistrict { get; set; }
        public int countyCouncilDistrict { get; set; }
        public int congressDistrict { get; set; }
        public int stateSenateDistrict { get; set; }
        public int stateHouseDistrict { get; set; }
    }



    public partial class App : Application
    {

        public static CurrentUserInfo currentUser;
        public static CurrentLocationInfo currentLocation;
        public static bool newUserIsSupporter;
        public static NewUserInfo newUser;
        public static string[] states = new string[] { "Alaska", "Alabama", "Arkansas", "Arizona", "California", "Colorado", "Connecticut", "District of Columbia", "Delaware", "Florida", "Georgia", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky", "Louisiana", "Massachusetts", "Maryland", "Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina", "North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Vermont", "Washington", "Wisconsin", "West Virginia", "Wyoming" };
        public static string[] stateAbbr = new string[] { "AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DC", "DE", "FL", "GA", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VA", "VI", "VT", "WA", "WI", "WV", "WY" };
        public static string[] offices; 


        public App()
        {
            InitializeComponent();
           

            //Console.WriteLine(address);

            offices = new string[] {"Select Office", "U.S. Senate", "U.S. House", "Governor", "State Senate", "State House", "County Board", "City Council", "Mayor", "Other" };
            currentUser = new CurrentUserInfo();
            currentLocation = new CurrentLocationInfo();
            MainPage = new NavigationPage(new MainPage());
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


    }
}
