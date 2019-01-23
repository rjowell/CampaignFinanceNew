using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CampaignFinanceNew
{

    public class CurrentUserInfo
    {
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String systemID { get; set; }
        public String userFirebaseID { get; set; }
        public bool isSupporter { get; set; }
        public String[] campaignsSupported { get; set; }
        public String[] currentCampaigns { get; set; }

        WebClient newClient = new WebClient();

        public async Task<string> SetUserInfo(String firebaseID)
        {

            String userJsonData = newClient.DownloadString("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID=" + firebaseID);         //("http://www.cvx4u.com/web_service/getUserInfo.php?firebaseID=" + authResult.User.Uid);
            userFirebaseID = firebaseID;
            Console.WriteLine(userJsonData);
            JObject currentUserString = JObject.Parse(userJsonData);
            firstName = currentUserString.GetValue("FirstName").ToString();
            lastName = currentUserString.GetValue("LastName").ToString();
            Console.WriteLine("Your name is " + firstName + " " + lastName);
            /*{"FirstName":"Russell","LastName":"Jowell","MailingAddress":"4500 Connecticut Ave nw #203","City":"Washington ","State":"DC","Zip":"20008","EMail":"russ.jowell@gmail.com","Phone":"2814608568","CampaignsSupported":null,"SupporterID":"1000","FirebaseID":"h9s7cK7qoqSCxOaGDcftnnTwtS22","StripeCustomerID":"cus_ELQh45iGJf5gt9"}
            */
            if (JObject.Parse(userJsonData).ContainsKey("CandidateId") == false)
            {


                systemID = currentUserString.GetValue("SupporterID").ToString();
                isSupporter = true;
                campaignsSupported = currentUserString.GetValue("CampaignsSupported").ToString().Split(',');
            }
            else
            {
                systemID = currentUserString.GetValue("CandidateId").ToString();
                currentCampaigns = currentUserString.GetValue("CampaignIDs").ToString().Split(',');
                isSupporter = false;
            }
            return "true";
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
