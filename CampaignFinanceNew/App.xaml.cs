using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;

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

            offices = new string[] { "U.S. Senate", "U.S. House", "Governor", "State Senate", "State House", "County Board", "City Council", "Mayor", "Other" };
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
