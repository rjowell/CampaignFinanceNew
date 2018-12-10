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



        public App()
        {
            InitializeComponent();
           
            //Console.WriteLine(address);
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
