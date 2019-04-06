using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
//using System.Linq;

using Xamarin.Forms;
using CampaignFinanceNew;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace CampaignFinanceNew
{

    public class CampaignCellSelector : DataTemplateSelector
    {

        public DataTemplate CampaignTemplate { get; set; }
        public DataTemplate CrowdfundTemplate { get; set; }
        public DataTemplate PendingTemplate { get; set; }
        public DataTemplate NoCampaignTemplate { get; set; }
        public DataTemplate WelcomeTemplate { get; set; }

        public int status { get; set; }

       

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {


            if (((Campaign)item).status==0)
            {
                
                Console.WriteLine("chappiedead");
                return PendingTemplate;
            }
            else if (((Campaign)item).status == 1)
            {
                Console.WriteLine("chappie");
                return NoCampaignTemplate;

            }
            else if (((Campaign)item).status == 2)
            {
                Console.WriteLine("chappie-killlleeed");
                return WelcomeTemplate;
            }
            else
            {
             if (((Campaign)item).startDate == "")
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
        public String isSupporter { get; set; }
        public String donorNoticeText { get; set; }
        public String lastDonationDate { get; set; }
        public String lastDonationText { get; set; }
        public bool isContribute { get; set; }
        public bool showGetDonors { get; set; }

        public int status { get; set; }


        public Campaign(int mode,string iD, string campName, string campDescription, string firstName, string lastName, string office, string state, string district, string start, string end, string campProgress, string goal)
        {
            Console.WriteLine("poop");
            campaignName = campName;
            Console.WriteLine("poop1");
            candidateDisplayName = firstName + " " + lastName + "|Running For: " + office + " " + state + "-" + district;
            Console.WriteLine("poop2");
            startDate = start;
            status = mode;
            Console.WriteLine("here russ");
            lastDonationDate = "";
            lastDonationText = "";
            Console.WriteLine("poop3");
            //campaignType = type;
            Console.WriteLine("poop4");
            isSupporter = App.currentUser.isSupporter.ToString().ToLower();

            if(isSupporter=="false")
            {
                showGetDonors = true;
            }
            else
            {
                showGetDonors = false;
            }
            endDate = end;
            if(startDate != "")
            {
                Console.WriteLine("crowedund");
                progress = campProgress;
                progressDisplay = progress + " Raised of $" + goal;
                progressFactor = Math.Round(Convert.ToDouble(campProgress) / Convert.ToDouble(goal), 2);
            }
            Console.WriteLine("poop5");

            Console.WriteLine("poop6");

           Console.WriteLine("poop7 "+goal+"hh");
           

           Console.WriteLine("poop8");
            campaignDescription = campDescription;
            Console.WriteLine("poop9");
            campaignID = iD;
            Console.WriteLine("poop10 "+start+"end date");




            fundGoal = goal;
            //Console.WriteLine("status is" + App.currentUser.isSupporter.ToString().ToLower());
            if (App.currentUser.isSupporter==true)
            {
                setEditButton = "Donate";
                Console.WriteLine("poop11 " + App.currentUser.campaignsSupported.Count);
                setInfoButton = "More Info";
                Console.WriteLine("poop12");
                isContribute = false;
                Console.WriteLine("popp 23");
                foreach (CampaignInfo current in App.currentUser.campaignsSupported)
                {
                    Console.WriteLine("popp 25");
                    if (current.campaignId == campaignID)
                    {
                        isContribute = true;
                        Console.WriteLine("popp 29");
                        Console.WriteLine(DateTime.Parse(current.dateGiven));
                        Console.WriteLine("dooby");
                        //Console.WriteLine(DateTime.Parse(lastDonationDate));

                        if(lastDonationDate == "")
                        {
                            Console.WriteLine("popp 31A");
                            if (start == "")
                            {
                                lastDonationDate = current.dateGiven;
                                lastDonationText= "Your most recent donation was on " + current.dateGiven;
                                Console.WriteLine("popp 33A");

                            }
                            else
                            {
                                Console.WriteLine("popp 29AA--");
                                lastDonationDate =  current.dateGiven;
                                lastDonationText= "You donated to this campaign on " + current.dateGiven;
                                setEditButton = "Change";
                                Console.WriteLine("popp 35AA");

                            }
                        }

                      
                        else
                        {

                            Console.WriteLine("cheezburgerAA "+lastDonationDate);
                            if (DateTime.Parse(current.dateGiven).CompareTo(DateTime.Parse(lastDonationDate)) >= 0)
                            {
                                Console.WriteLine("popp 31");
                                if (start == "")
                                {
                                    lastDonationDate = current.dateGiven;
                                    lastDonationText = "Your most recent donation was on " + current.dateGiven;
                                    Console.WriteLine("popp 33");

                                }
                                else
                                {
                                    lastDonationDate = current.dateGiven;
                                    lastDonationText = "You donated to this campaign on " + current.dateGiven;
                                    setEditButton = "Change";
                                    Console.WriteLine("popp 35");

                                }
                            }
                           
                        }



                        //
                    }
                }
            }
            else
            {
                setEditButton = "Edit";
                setInfoButton = "";

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



            private async void SendDonorList(object sender, EventArgs e)
            {
            Button current = (Button)sender;

            eMailWindow.IsVisible = true;
            confirmationLabel.IsVisible = false;
            okButton.IsVisible = false;

            NameValueCollection sendData = new NameValueCollection();
            sendData.Set("campaignName", current.Parent.Parent.ClassId);
            sendData.Set("eMail", App.currentUser.eMail);

            String result= System.Text.Encoding.Default.GetString(client.UploadValues("http://www.cvx4u.com/web_service/getDonors.php",sendData));

            Console.WriteLine("state is " + result);
            confirmationLabel.Text = "A list of donors to this campaign has been sent to '"+App.currentUser.eMail+"'";
            Console.WriteLine("thisis the song");
            confirmationLabel.IsVisible = true;
            Console.WriteLine("that doesn't end");
            okButton.IsVisible = true;
            eMailSendingLabel.IsVisible = false;
        }


        private void RefreshFieldData()
        {
            fieldData.Clear();
            String rawData;
            String locationData = "state=" + App.currentLocation.state + "&county=" + App.currentLocation.countyName + "&usCongressDist=" + App.currentLocation.congressDistrict + "&lowerStateHouse=" + App.currentLocation.stateHouseDistrict + "&upperStateHouse=" + App.currentLocation.stateSenateDistrict + "&cityName=" + App.currentLocation.cityName;
            //Console.WriteLine("test point1");
            Console.WriteLine(locationData);
                
            if (App.currentUser.isSupporter == true)
            {
                rawData = thisClient.DownloadString("http://www.cvx4u.com/web_service/getCampaigns.php?id=0&"+locationData);
            }
            else
            {
                rawData = thisClient.DownloadString("http://www.cvx4u.com/web_service/getCampaigns.php?id=" + App.currentUser.systemID);
            }
            Console.WriteLine("test poin2 "+rawData);

            NameValueCollection thisInfo = new NameValueCollection();

            thisInfo.Set("locationData", locationData);

            thisClient.UploadValues("http://www.cvx4u.com/web_service/locationTest.php",thisInfo);

            array = JArray.Parse(rawData);
            Console.WriteLine("test point3");


            // 5269 River Rd, Bethesda, MD

            /*if (App.currentUser.userApproved == "0")
            {
                fieldData.Add(new Campaign("", "", "", "", "", "", "", "", "", "", "",""));
            }
            else
            {*/
            foreach (JObject thisThing in array)
                {
                    Console.WriteLine("test poin4");
                    Campaign thisCampaign = new Campaign(3,thisThing.GetValue("CampaignID").ToString(), thisThing.GetValue("CampaignName").ToString(), thisThing.GetValue("CampaignDescription").ToString(), thisThing.GetValue("FirstName").ToString(), thisThing.GetValue("LastName").ToString(), thisThing.GetValue("CandidateOffice").ToString(), thisThing.GetValue("State").ToString(), thisThing.GetValue("District").ToString(), thisThing.GetValue("StartDate").ToString(), thisThing.GetValue("EndDate").ToString(), thisThing.GetValue("AmountRaised").ToString(), thisThing.GetValue("Goal").ToString());
                    Console.WriteLine("test point5");
                    thisCampaign.posIndex = (fieldData.Count).ToString();

                    fieldData.Add(thisCampaign);
                }
            //}
        }



        protected override void OnAppearing()
        {

            titleLabel.Text = "Welcome," + App.currentUser.firstName;
            menuBlock.IsVisible = false;
            Console.WriteLine("chocolate factory");
            RefreshFieldData();
            Console.WriteLine("chocolate factory-1");
            campaignDisplay.ItemsSource = null;

            List<String> singleList = new List<string>();
            singleList.Add("");

            //Console.WriteLine("football player");
            if(App.currentUser.userApproved=="0")
            {
                Console.WriteLine("not appo");
                //campaignCellPicker.status = 0;
                fieldData.Clear();
                fieldData.Add(new Campaign(0, "", "", "", "", "", "", "", "", "", "", "",""));
                campaignDisplay.ItemsSource = fieldData;
            }
            else if(fieldData.Count==0)
            {
                if (App.currentUser.isSupporter == true)
                {
                    Console.WriteLine("mcdonalds");
                    //campaignCellPicker.status = 1;
                    fieldData.Clear();
                    fieldData.Add(new Campaign(1, "", "", "", "", "", "", "", "", "", "", "", ""));
                    campaignDisplay.ItemsSource = fieldData;
                    Console.WriteLine("dominaosugar land");
                }
                else
                {
                    Console.WriteLine("burger king");
                    // campaignCellPicker.status = 2;
                    fieldData.Clear();
                    fieldData.Add(new Campaign(2, "", "", "", "", "", "", "", "", "", "", "", ""));
                    campaignDisplay.ItemsSource = fieldData;
                }
            }
            else
            {

                campaignDisplay.ItemsSource = fieldData;
            }


        }

        DonationWindow currentWindow;

        JArray array;

        WebClient thisClient = new WebClient();

        CampaignCellSelector campaignCellPicker;

        public CandidateDashboard()
        {

            campaignCellPicker = new CampaignCellSelector();
            campaignCellPicker.status = 1;
            InitializeComponent();

            Random rand = new Random();
            //fields = new Entry[] { eMailField, passwordField };
            backImage.Source = "img" + rand.Next(6).ToString() + ".png";
            //noticeFrame.IsVisible = false;

            if (App.currentUser.isSupporter==true || App.currentUser.userApproved=="0")
            {
                Console.WriteLine("step bobby-223");
                createCampaignButton.IsVisible = false;
            }


            okButton.Clicked += (sender, e) => {

                eMailWindow.IsVisible = false;
            };

            eMailWindow.IsVisible = false;

            refreshLocButton.Clicked += async (sender, e) => {

                await App.currentLocation.GetLocationInformation();

               


                NameValueCollection nvc = new NameValueCollection();

                String locationInfo = App.currentLocation.rawData+"    DCWard: "+App.currentLocation.dcWard+" DCANC: "+App.currentLocation.dcAnc+"  State: " + App.currentLocation.state + " County: " + App.currentLocation.countyType + " " + App.currentLocation.countyName + " City: " + App.currentLocation.cityName +
                 " City Council: " + App.currentLocation.cityCouncilDistrict + " County Council: " + App.currentLocation.countyCouncilDistrict + " State Senate: " + App.currentLocation.stateSenateDistrict + " State House: " + App.currentLocation.stateHouseDistrict;


                nvc.Set("locationData", locationInfo);


                client.UploadValues("http://www.cvx4u.com/web_service/locationTest.php", nvc);
           
            
             
               };

                        



            fieldData = new List<Campaign>();

           
            RefreshFieldData();

            NavigationPage.SetHasNavigationBar(this, false);




            donateWindow.IsVisible = false;
            titleLabel.Text = "Welcome," + App.currentUser.firstName;
           


            menuBlock.IsVisible = false;
           

            //campNameLabel.FontFamily="Times";
            menuButton.Source = ImageSource.FromResource("CampaignFinanceNew.menusandwich.png");
            searchButton.Source= ImageSource.FromResource("CampaignFinanceNew.searchglass.png");
            //menuButton.Image = (Xamarin.Forms.FileImageSource)ImageSource.FromResource("CampaignFinanceNew.MenuSandwich.png");      


            Console.WriteLine("look here doof");
            Console.WriteLine(App.currentUser.userApproved == "0");
            //campaignDisplay.ItemsSource = null;
            if(App.currentUser.userApproved=="0")
            {
                Console.WriteLine("football player");
                List<String> defaltList = new List<String>();
                defaltList.Add("");
                //campaignDisplay.ItemsSource = defaltList;
            }
            else
            {
                //campaignDisplay.ItemsSource = fieldData;
            }











        }
        String currentSelectedCampaign;
        String currentSelectedCampaignID;
        bool isChangeDonation;
        private void ShowDonateWindow(object sender, EventArgs e)
        {
            Button current = (Button)sender;

            Console.WriteLine("button text i "+ current.Parent.Parent.ClassId);
            currentSelectedCampaign = current.Parent.Parent.AutomationId.ToString();
            Console.WriteLine("Campaign is" + currentSelectedCampaign);
            if (current.Text=="Edit")
            {
                Navigation.PushAsync(new CreateEditCampaign(current.Parent.Parent.ClassId));
            }
            else if(current.Text=="Change")
            {
                Console.WriteLine("change123");
                foreach (CampaignInfo currentThing in App.currentUser.campaignsSupported)
                {
                    Console.WriteLine(currentThing.campaignId + " " + currentThing.dateGiven);
                }
                //Console.WriteLine("Id is "+sender.Parent.Parent.ClassId);
                currentWindow = new DonationWindow(donateWindow, current.Parent.Parent.ClassId, current.Parent.Parent.AutomationId, true, App.currentUser.campaignsSupported.Find(x => x.campaignId == current.Parent.Parent.ClassId).amount);
                currentWindow.openWindow();


            }
            else
            {
                Console.WriteLine("nochange");
                currentWindow = new DonationWindow(donateWindow, current.Parent.Parent.ClassId, current.Parent.Parent.AutomationId, false, "");
                currentWindow.openWindow();


            }



        }


        double confirmDonationAmount;
        private void ConfirmDonation(object sender, EventArgs e)
        {

            currentWindow.confirmDonation();


           


        }

        private async void OpenEditWindow(object sender, EventArgs e)
        {
            Button current = (Button)sender;

            if(current.Text=="More Info")
            {
                await Navigation.PushAsync(new CampaignDetails(current.ClassId));
            }
            else
            {
                await Navigation.PushAsync(new CreateEditCampaign(current.ClassId));
            }
            //Console.WriteLine("Camp no is " + sender.ClassId);
            //Navigation.PushAsync(new CreateCampaign(sender.ClassId));
        }

        private void CloseWindow(object sender, EventArgs e)
        {

            currentWindow.closeWindow();



        }



       


        private void ProcessDonation(object sender, EventArgs e)
        {

            currentWindow.ProcessDonation();


            RefreshFieldData();

            campaignDisplay.ItemsSource = null;
                campaignDisplay.ItemsSource = fieldData;






        }

        public void SignOutUser(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAuthenticator>().Logout();
            Navigation.PushAsync(new MainPage());

        }
       
        private void ShowMenu(object sender, EventArgs e)
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

        private void ShowUserInfo(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditUserInfo());
        }

        private void OpenCreateCampaign(object sender, EventArgs e)
        {

            Button current = (Button)sender;

            if (current.ClassId == null)
            {
                Navigation.PushAsync(new CreateEditCampaign(""));
            }
            else
            {
                Navigation.PushAsync(new CreateEditCampaign(current.ClassId));
            }

            //Console.WriteLine("The is is "+ sender.ClassId);


        }
    }
}
