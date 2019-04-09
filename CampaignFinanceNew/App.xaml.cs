using System;
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
        public String contactPerson { get; set; }
        public String mailingAddress { get; set; }
        public String city { get; set; }
        public String mailState { get; set; }
        public String ccLastFour { get; set; }
        public String ccExpiryDate { get; set; }

        public String zipCode { get; set; }
        public String eMail { get; set; }
        public String userApproved { get; set; }
        public String phoneNumber { get; set; }
        public String office { get; set; }
        public String officeState { get; set; }
        public String officeCounty { get; set; }
        public String officeCity { get; set; }
        public String officeDistrict { get; set; }

        public String district { get; set; }
        public String party { get; set; }
        public String website { get; set; }
        public String systemID { get; set; }
        public String userFirebaseID { get; set; }
        public bool isSupporter { get; set; }
        //public String[] campaignsSupported { get; set; }
        public String[] currentCampaigns { get; set; }
        public List<CampaignInfo> campaignsSupported { get; set; }

        WebClient newClient = new WebClient();

        Random rand = new Random();

        public String getBackImage()
        {

            return "img" + rand.Next(6).ToString() + ".png";
        }



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
                //Console.WriteLine("ITem is " + item + ".e");
                //Console.WriteLine(item.Length);
                if (item.Length!=0)
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
                Console.WriteLine("chsses");
            }

        }

       

        public Task<string> SetUserInfo(String firebaseID)
        {

            String userJsonData = newClient.DownloadString("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID="+firebaseID);
            Console.WriteLine("hello-222");
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            Console.WriteLine("hello-333");
            userFirebaseID = firebaseID;
            Console.WriteLine("hello-444");
            Console.WriteLine(userJsonData);
            Console.WriteLine("hello-555");
            JObject currentUserString = JObject.Parse(userJsonData);
            Console.WriteLine("hello-666");
            firstName = currentUserString.GetValue("FirstName").ToString();
            Console.WriteLine("hello-1");
            lastName = currentUserString.GetValue("LastName").ToString();
            mailingAddress = currentUserString.GetValue("MailingAddress").ToString();
            Console.WriteLine("hello-2");
            eMail = currentUserString.GetValue("EMail").ToString();
            city = currentUserString.GetValue("City").ToString();
            Console.WriteLine("hello-3");
            mailState = currentUserString.GetValue("State").ToString();
            Console.WriteLine("hello-");
            zipCode = currentUserString.GetValue("Zip").ToString();
            Console.WriteLine("hello-5");
            phoneNumber = currentUserString.GetValue("Phone").ToString();

            //contactPerson = currentUserString.GetValue("ContactPerson").ToString();
            Console.WriteLine("Your name is " + firstName + " " + lastName);
            /*{"FirstName":"Russell","LastName":"Jowell","MailingAddress":"4500 Connecticut Ave nw #203","City":"Washington ","State":"DC","Zip":"20008","EMail":"russ.jowell@gmail.com","Phone":"2814608568","CampaignsSupported":null,"SupporterID":"1000","FirebaseID":"h9s7cK7qoqSCxOaGDcftnnTwtS22","StripeCustomerID":"cus_ELQh45iGJf5gt9"}
            */
            Console.WriteLine("point A");
            if (JObject.Parse(userJsonData).ContainsKey("CandidateId") == false)
            {
                Console.WriteLine("point B");
                ccLastFour = currentUserString.GetValue("currentLastFour").ToString();
                ccExpiryDate = currentUserString.GetValue("ccExpiryDate").ToString();
                systemID = currentUserString.GetValue("SupporterID").ToString();
                isSupporter = true;
                userApproved = "1";
                Console.WriteLine("point C");
                Console.WriteLine("Info si "+currentUserString.GetValue("CampaignsSupported"));
                campaignsSupported = new List<CampaignInfo>();
                Console.WriteLine("cheese-burger");
                if (currentUserString.GetValue("CampaignsSupported").ToString() != "")
                {
                    Console.WriteLine("cheese-burger-pickles");
                    ProcessCampDonations(currentUserString.GetValue("CampaignsSupported").ToString());
                }

                Console.WriteLine("buttercup catt");
               
            }
            else
            {
                systemID = currentUserString.GetValue("CandidateId").ToString();
                office = currentUserString.GetValue("Office").ToString();
                //district = currentUserString.GetValue("District").ToString();
                officeCity= currentUserString.GetValue("OfficeCity").ToString();
                officeCounty= currentUserString.GetValue("OfficeCounty").ToString();
                officeDistrict= currentUserString.GetValue("OfficeDistrict").ToString();
                contactPerson = currentUserString.GetValue("ContactPerson").ToString();
                userApproved = currentUserString.GetValue("IsApproved").ToString();
                party = currentUserString.GetValue("Affiliation").ToString();
                website = currentUserString.GetValue("Website").ToString();
                Console.WriteLine("point E");
                officeState= currentUserString.GetValue("OfficeState").ToString();
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
        public String officeDistrict { get; set; }
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
        public String officeCity { get; set; }
        public String officeCounty { get; set; }
        public String ideology { get; set; }

        public String issues { get; set; }

        public String jobTitle { get; set; }
        public String employerName { get; set; }
        public String employerCity { get; set; }
        public String employerState { get; set; }


    }

    public class CurrentLocationInfo
    {
        public String state { get; set; }
        public String[] countyType = new string[] { "county", "parish", "borough" };
        public int getCountyType { get; set; }
        public String countyName { get; set; }
        public String cityName { get; set; }
        public String cityCouncilDistrict { get; set; }
        public String countyCouncilDistrict { get; set; }
        public String congressDistrict { get; set; }
        public String stateSenateDistrict { get; set; }
        public String stateHouseDistrict { get; set; }

        public String dcAnc { get; set;}
        public String dcWard { get; set;}

        public String rawData { get; set; }

       
       


        public async Task<string> GetLocationInformation()
        {

            var locator = CrossGeolocator.Current;



            var googleCivicURL = "https://www.googleapis.com/civicinfo/v2/representatives?key=AIzaSyAwpzFeq7FYHdVY__3vFL9QyTP2oCekkio&address=";
            Console.WriteLine("this pos");
            Console.WriteLine("next point");



            var currentPosition = await locator.GetPositionAsync();
            Console.WriteLine("position is");
            Console.WriteLine(currentPosition);
            var address = await locator.GetAddressesForPositionAsync(currentPosition);
            Console.WriteLine("this pos-3");
            string currentUserString = "";
            Console.WriteLine("address 1" + address);
            foreach (var item in address)
            {

                string feature;
                if(item.FeatureName.IndexOf('-')!=-1)
                {
                    feature= item.FeatureName.Substring(item.FeatureName.IndexOf('–') + 1).Replace(" ", "%20");
                }
                else
                {
                    feature = item.FeatureName.Replace(" ", "%20");
                }


                Console.WriteLine(item.FeatureName.Substring(item.FeatureName.IndexOf('–')+1).Replace(" ","%20")   );
                currentUserString = feature + "%20" + item.Locality.Replace(" ", "%20") + "%20" + item.AdminArea.Replace(" ", "%20");
                Console.WriteLine("String is "+currentUserString);
                rawData = currentUserString;
            }
            //Console.WriteLine(currentUserString);
            WebClient newClient = new WebClient();
            //var rawLocationData = newClient.DownloadString(googleCivicURL + currentUserString);
            //Console.WriteLine(googleCivicURL + currentUserString);
            JObject parseData = JObject.Parse(newClient.DownloadString(googleCivicURL + currentUserString));
            //Console.WriteLine("cheese-2");
            JObject divisionData = JObject.Parse(parseData.GetValue("divisions").ToString());
            //Console.WriteLine("cheese-3");
            //JArray newData = JArray.Parse(parseData.GetValue("divisions").ToString());
            //Console.WriteLine("cheese-43");
            var currKeys = divisionData.Properties();

            foreach (var things in currKeys)
            {
                string[] currentThings = things.Name.Split('/');

                string[] currentItem = currentThings[currentThings.Length - 1].Split(':');
                Console.WriteLine(currentItem[0] + " and-- " + currentItem[1]);

                if(currentItem[0]=="district" && currentItem[1]=="dc")
                {
                    Console.WriteLine("dc case state");
                   state = "DC";
                }
                if(currentItem[0]=="anc")
                {
                    Console.WriteLine("dc anc dist");
                    dcAnc = currentItem[1];
                }
                if(currentItem[0]== "ward")
                {
                    Console.WriteLine("dc ward case");
                    dcWard = currentItem[1];
                }





                if (currentItem[0] == "state")
                {
                    state = currentItem[1];
                    Console.WriteLine(currentItem[0] + " State is " + currentItem[1]);
                }
                else if (currentItem[0] == "county")
                {
                    getCountyType = 0;
                    countyName = currentItem[1];
                    Console.WriteLine(currentItem[0] + " County is " + currentItem[1]);

                }
                else if (currentItem[0] =="parsh" )
                {
                   getCountyType = 1;
                    countyName = currentItem[1];
                }
                else if (currentItem[0] == "borough")
                {
                    getCountyType = 2;
                    countyName = currentItem[1];
                }
                else if (currentItem[0] == "cd")
                {
                    congressDistrict = currentItem[1];

                }
                else if (currentItem[0] == "sldl")
                {
                    stateHouseDistrict = currentItem[1];
                    Console.WriteLine(currentItem[0] + " sldl is " + currentItem[1]);
                }
                else if (currentItem[0] == "sldu")
                {
                    stateSenateDistrict = currentItem[1];
                    Console.WriteLine(currentItem[0] + " sldu is " + currentItem[1]);
                }

                else if (currentItem[0] == "place")
                {
                    cityName = currentItem[1].Replace('_', ' ');
                    Console.WriteLine(currentItem[0] + " Place is " + currentItem[1]);
                }
                else if (currentThings[currentThings.Length - 2].Split(':')[0] == "place")
                {
                    cityCouncilDistrict = currentItem[1];
                }
                else if (currentThings[currentThings.Length - 2].Split(':')[0] == "county" || currentThings[currentThings.Length - 2].Split(':')[0] == "parish" || currentThings[currentThings.Length - 2].Split(':')[0] == "borough")
                {
                    countyCouncilDistrict = currentItem[1];
                }
                else
                {

                }

                //Console.WriteLine(currentThings[currentThings.Length - 1]);

            }



            return "hello";
        }







    }

    


    public class CurrentCampaign
    {
        public String campaignName { get; set; }
        public bool isCrowdfund { get; set; }
        public String campaignDescription { get; set; }
        public String campaignGoal { get; set; }
        public String endDate { get; set; }
    }



    public partial class App : Application
    {

        public static CurrentUserInfo currentUser;
        public static CurrentLocationInfo currentLocation;
        public static CurrentCampaign newCampaign;
        public static bool newUserIsSupporter;
        public static NewUserInfo newUser;
        public static string[] states = new string[] { "Alaska", "Alabama", "Arkansas", "Arizona", "California", "Colorado", "Connecticut", "District of Columbia", "Delaware", "Florida", "Georgia", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky", "Louisiana", "Massachusetts", "Maryland", "Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina", "North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Vermont", "Washington", "Wisconsin", "West Virginia", "Wyoming" };
        public static string[] stateAbbr = new string[] { "AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DC", "DE", "FL", "GA", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VA", "VI", "VT", "WA", "WI", "WV", "WY" };
        public static string[] offices;











        public App()
        {
            Console.WriteLine("poopyyy");
            InitializeComponent();
            Console.WriteLine("poopyyy-111");


            //Console.WriteLine(address);

            offices = new string[] {"Select Office", "U.S. Senate", "U.S. House", "Governor", "State Senate", "State House", "County Board", "City Council", "Mayor", "Other" };
            currentUser = new CurrentUserInfo();
            currentLocation = new CurrentLocationInfo();
            newCampaign = new CurrentCampaign();


            

            //

            MainPage = new NavigationPage(new MainPage());
        }


        protected override void OnStart()
        {
           
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected async override void OnResume()
        {
            // Handle when your app resumes
            await currentLocation.GetLocationInformation();
        }


    }
}
