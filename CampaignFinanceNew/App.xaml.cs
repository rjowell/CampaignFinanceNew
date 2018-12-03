using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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



        public CurrentUserInfo()
        {
            
        }
    }



    public partial class App : Application
    {

        public static CurrentUserInfo currentUser;

        public App()
        {
            InitializeComponent();
            currentUser = new CurrentUserInfo();
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
