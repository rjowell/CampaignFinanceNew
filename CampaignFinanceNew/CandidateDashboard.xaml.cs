using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
//using System.Linq;

using Xamarin.Forms;
using CampaignFinanceNew;

namespace CampaignFinanceNew
{
    public class Campaign
    {
        public String campaignName { get; set; }
        public String startDate { get; set; }
        public String endDate { get; set; }
        public String progress { get; set; }
        public String campaignDescription { get; set; }

        public Campaign(string Name, string Description, string Start, string End, string Goal)
        {
            campaignName = Name;
            startDate = Start;
            endDate = End;
            progress = Goal;
            campaignDescription = Description;
        }
    }

    public class InputObject
    {
        public String CampaignName { get; set; }
        public String FundGoal { get; set; }
        public String EndDate { get; set; }
        public String AmountRaised { get; set; }

        public InputObject(string Campaign, string Goal, string CampEndDate, string FundsRaised)
        {
            CampaignName = Campaign;
            FundGoal = Goal;
            EndDate = CampEndDate;
            AmountRaised = FundsRaised;
        }
    }


    public partial class CandidateDashboard : ContentPage
    {
        List<Campaign> fieldData;
        String jsonData = new WebClient().DownloadString("http://www.cvx4u.com/web_service/getCampaigns.php");
        List<InputObject> junkStuff;

        public CandidateDashboard()
        {
            //JObject thisJunk = JObject.Parse(jsonData);
            junkStuff = new List<InputObject>();
            fieldData = new List<Campaign>();
            //var thisThing = DependencyService.Get<IFirebaseAuthenticator>().GetIdInfo();
            //Console.WriteLine(thisThing.ToString());

            InitializeComponent();
            //thisLabel.Text = thisThing.ToString();
            JArray array = JArray.Parse(jsonData);
            //Console.WriteLine(array);
            foreach(JObject thisThing in array)
            {
                Campaign thisCampaign = new Campaign(thisThing.GetValue("CampaignName").ToString(), thisThing.GetValue("CampaignDescription").ToString(), "11/12/18", thisThing.GetValue("EndDate").ToString(), thisThing.GetValue("AmountRaised").ToString());
               
                fieldData.Add(thisCampaign);
                Console.WriteLine(thisThing.GetValue("CampaignName"));
            }
        


            //Console.WriteLine(stuff.Count);

            //Console.WriteLine(stuff);     

            //JArray newStuff = (Newtonsoft.Json.Linq.JArray)stuff["C1001_1"];
            //Console.WriteLine(newStuff.Count);


            //Object stuff = thisJunk;

            //List<InputObject> inObjects = JsonConvert.DeserializeObject<List<InputObject>>(jsonData);
            //Console.WriteLine(inObjects);


            campaignDisplay.ItemsSource = fieldData;

        }

        private void OpenCreateCampaign(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateCampaign());
        }
    }
}
