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

    public class CampaignCellSelector : DataTemplateSelector
    {

        public DataTemplate CampaignTemplate { get; set; }
        public DataTemplate CrowdfundTemplate { get; set; }

        

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Console.WriteLine("this new point here "+((Campaign)item).campaignName);
            if(((Campaign)item).campaignType==0)
            {
                Console.WriteLine("point AA");
                return CampaignTemplate;
            }
            else
            {
                Console.WriteLine("point BB");
                return CrowdfundTemplate;
            }
        }
    }

    public class Campaign
    {
        public String campaignName { get; set; }
        public int campaignType { get; set; }
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
        public String setInfoButton { get; set; }



        public Campaign(int type, string iD, string campName, string campDescription, string firstName, string lastName, string office, string state, string district, string start, string end, string campProgress, string goal)
        {
            campaignName = campName;
            candidateDisplayName = firstName + " " + lastName + "|Running For: " + office + " " + state + "-" + district;
            startDate = start;
            campaignType = type;
            endDate = end;

            progress = campProgress;
            progressDisplay = progress + "/" + goal;
            progressFactor = Math.Round(Convert.ToDouble(campProgress) / Convert.ToDouble(goal),2);
            campaignDescription = campDescription;
            campaignID = iD;
            fundGoal = goal;
          
            if (App.currentUser.isSupporter==true)
            {
                setEditButton = "Donate";
                setInfoButton = "More Info";
                foreach (CampaignInfo current in App.currentUser.campaignsSupported)
                {
                    if (current.campaignId == campaignID)
                    {
                        setEditButton = "Change";
                    }
                }
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

            confirmTo.IsVisible = false;
            confirmLabel.IsVisible = false;
            confirmAmount.IsVisible = false;
            confirmCampaign.IsVisible = false;
            Console.WriteLine("this name is" + App.currentUser.firstName);

            WebClient thisClient = new WebClient();


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
                Console.WriteLine("point F "+thisThing.GetValue("CampaignType"));
                Campaign thisCampaign = new Campaign(Convert.ToInt32(thisThing.GetValue("CampaignType")),thisThing.GetValue("CampaignID").ToString(),thisThing.GetValue("CampaignName").ToString(), thisThing.GetValue("CampaignDescription").ToString(), thisThing.GetValue("FirstName").ToString(), thisThing.GetValue("LastName").ToString(), thisThing.GetValue("CandidateOffice").ToString(), thisThing.GetValue("State").ToString(), thisThing.GetValue("District").ToString(), thisThing.GetValue("StartDate").ToString(),thisThing.GetValue("EndDate").ToString(), thisThing.GetValue("AmountRaised").ToString(),thisThing.GetValue("Goal").ToString());
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
            donateWindow.IsVisible = false;
            titleLabel.Text = "Welcome," + App.currentUser.firstName;
            if (App.currentUser.isSupporter==false)
            {
                createCampaignButton.IsVisible = true;
               

            }
           

            menuBlock.IsVisible = false;
           

            //campNameLabel.FontFamily="Times";
            menuButton.Source = ImageSource.FromResource("CampaignFinanceNew.menusandwich.png");
            searchButton.Source= ImageSource.FromResource("CampaignFinanceNew.searchglass.png");
            //menuButton.Image = (Xamarin.Forms.FileImageSource)ImageSource.FromResource("CampaignFinanceNew.MenuSandwich.png");      
            CampaignCellSelector  campaignCellPicker = new CampaignCellSelector();
            Console.WriteLine("this is the song");
            campaignDisplay.ItemsSource = fieldData;
            Console.WriteLine("this is the song-1");
            /*campaignDisplay.ItemTemplate = new CampaignCellSelector {

                CampaignTemplate = campaignTemplate, 
                CrowdfundTemplate = crowdfundTemplate
            
            };*/
            Console.WriteLine("this is the song-2");





        }

        private void ShowDonateWindow(Button sender, EventArgs e)
        {
            Console.WriteLine(sender.ClassId);
            donateWindow.IsVisible = true;
            donateSubmit.ClassId = sender.ClassId;
            //currentSelectedCampaign = sender.ClassId;
        }


        double confirmDonationAmount;
        private void ConfirmDonation(Button sender, EventArgs e)
        {

            if(double.TryParse(donationField.Text,out confirmDonationAmount)==true)
            {
                confirmAmount.Text = "$ " + confirmDonationAmount;

                donateQuery.IsVisible = false;
                donationField.IsVisible = false;
                confirmTo.IsVisible = true;
                confirmLabel.IsVisible = true;
                confirmAmount.IsVisible = true;
                confirmCampaign.IsVisible = true;
            }
            else
            {
                //amount is not a number
            }


        }

        private void CloseWindow(Button sender, EventArgs e)
        {
            donateWindow.IsVisible = false;
            Console.WriteLine("Campaing ins "+sender.ClassId);
        }

        private void ProcessDonation(Button sender, EventArgs e)
        {


            if(sender.Text=="Donate")
            {
                donationData.Add("supporterID", App.currentUser.systemID);
                donationData.Add("donorAmount", donationField.Text);
                donationData.Add("campaignID", donateSubmit.ClassId);
                App.currentUser.ProcessCampDonations(client.UploadValues(new Uri("http://www.cvx4u.com/web_service/processDonation.php"), donationData).ToString());
                //App.currentUser.campaignsSupported = client.UploadValues(new Uri("http://www.cvx4u.com/web_service/processDonation.php"), donationData).ToString().Split(',');

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
