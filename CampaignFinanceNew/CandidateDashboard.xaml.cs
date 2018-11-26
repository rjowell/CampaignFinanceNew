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
        public String fundGoal { get; set; }
        public String campaignDescription { get; set; }
        public String campaignID { get; set; }
        public String posIndex { get; set; }

        public Campaign(string ID, string Name, string Description, string Start, string End, string Progress, string Goal)
        {
            campaignName = Name;
            startDate = Start;
            endDate = End;
            progress = Progress;
            campaignDescription = Description;
            campaignID = ID;
            fundGoal = Goal;
        }
    }




    public partial class CandidateDashboard : ContentPage
    {
        List<Campaign> fieldData;
        String jsonData = new WebClient().DownloadString("http://www.cvx4u.com/web_service/getCampaigns.php");
       

        public CandidateDashboard()
        {
            //JObject thisJunk = JObject.Parse(jsonData);
          
            fieldData = new List<Campaign>();
            //var thisThing = DependencyService.Get<IFirebaseAuthenticator>().GetIdInfo();
            //Console.WriteLine(thisThing.ToString());

            InitializeComponent();
            //thisLabel.Text = thisThing.ToString();
            JArray array = JArray.Parse(jsonData);
            //Console.WriteLine(array);
            foreach(JObject thisThing in array)
            {
                Campaign thisCampaign = new Campaign(thisThing.GetValue("CampaignID").ToString(),thisThing.GetValue("CampaignName").ToString(), thisThing.GetValue("CampaignDescription").ToString(), "11/12/18", thisThing.GetValue("EndDate").ToString(), thisThing.GetValue("AmountRaised").ToString(),thisThing.GetValue("Goal").ToString());
                thisCampaign.posIndex = (fieldData.Count).ToString();
                fieldData.Add(thisCampaign);

                //Console.WriteLine(thisThing.GetValue("CampaignName"));
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
            


            //Navigation.PushAsync(new CreateCampaign());
        }
    }
}
