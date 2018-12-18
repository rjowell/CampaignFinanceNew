using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Geolocator;
using System.Net;
using Newtonsoft.Json.Linq;

namespace CampaignFinanceNew
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            titleImage.Source = ImageSource.FromResource("CampaignFinanceNew.ANMlogo1.png");

        }





        public async Task<string> GetLocationInformation()
        {

            var locator = CrossGeolocator.Current;

           

            var googleCivicURL = "https://www.googleapis.com/civicinfo/v2/representatives?key=AIzaSyAwpzFeq7FYHdVY__3vFL9QyTP2oCekkio&address=";
            Console.WriteLine("this pos");
            var currentPosition= await locator.GetPositionAsync();var address = await locator.GetAddressesForPositionAsync(currentPosition);
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
            Console.WriteLine(googleCivicURL + currentUserString);
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
                Console.WriteLine(currentItem[0]+" "+currentItem[1]);
                if(currentItem[0]=="state")
                {
                    App.currentLocation.state = currentItem[1];
                    Console.WriteLine(currentItem[0]+" State is "+currentItem[1]);
                }
                else if(currentItem[0]=="county")
                {
                    App.currentLocation.getCountyType = 0;
                    App.currentLocation.countyName = currentItem[1];
                    Console.WriteLine(currentItem[0]+" County is "+currentItem[1]);

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
                    Console.WriteLine(currentItem[0]+" Cd is "+currentItem[1]);
                }
                else if (currentItem[0] == "sldl")
                {
                    App.currentLocation.stateHouseDistrict = Convert.ToInt32(currentItem[1]);
                    Console.WriteLine(currentItem[0]+" sldl is "+currentItem[1]);
                }
                else if (currentItem[0] == "sldu")
                {
                    App.currentLocation.stateSenateDistrict = Convert.ToInt32(currentItem[1]);
                    Console.WriteLine(currentItem[0]+" sldu is "+currentItem[1]);
                }
              
                else if (currentItem[0] == "place")
                {
                    App.currentLocation.cityName = currentItem[1].Replace('_', ' ');
                    Console.WriteLine(currentItem[0]+" Place is "+currentItem[1]);
                }
                else if (currentThings[currentThings.Length-2].Split(':')[0]=="place")
                {
                    App.currentLocation.cityCouncilDistrict =   Convert.ToInt32(currentItem[1]);
                }
                else if (currentThings[currentThings.Length - 2].Split(':')[0] == "county" || currentThings[currentThings.Length - 2].Split(':')[0] == "parish" || currentThings[currentThings.Length - 2].Split(':')[0] == "borough")
                {
                    App.currentLocation.countyCouncilDistrict = Convert.ToInt32(currentItem[1]);
                }
                else
                {

                }

                //Console.WriteLine(currentThings[currentThings.Length - 1]);

            }



            return "hello";
        }









        private async void ProcessLogin(object sender, EventArgs e)
        {

            await DependencyService.Get<IFirebaseAuthenticator>().LoginWithEmailPassword(usernameField.Text, passwordField.Text);

            //var isLoggedIn = DependencyService.Get<IFirebaseAuthenticator>().GetIdInfo();
            //DependencyService.Get<IFirebaseAuthenticator>().CreateNewUser(eMailField.Text, passwordField.Text, sendingParameters);
            //Console.WriteLine(isLoggedIn);

            //await GetLocationInformation();
            await Navigation.PushAsync(new CandidateDashboard());



            Console.WriteLine(usernameField.Text);
            Console.WriteLine(passwordField.Text);

        }

        private void CreateUser(object sender, EventArgs e)
        {

            Button currentButton = sender as Button;
            CreateUser newWindow = new CreateUser();

            if (currentButton.Text == "I am a candidate")
            {
                //Console.WriteLine("candiate button clicked");
                newWindow.setSupporter(false);

                Navigation.PushAsync(newWindow);
            }
            else
            {
                //Console.WriteLine("candiate button clicked--1");

                newWindow.setSupporter(true);
                Navigation.PushAsync(newWindow);
            }
        }

    }
}
