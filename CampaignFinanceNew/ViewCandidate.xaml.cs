using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class ViewCandidate : ContentPage
    {
        private String candidateId;
        WebClient client = new WebClient();
        List<KeyValuePair<String,int>> issueList=new List<KeyValuePair<string, int>>();
        public ViewCandidate(String candidateIdInput)
        {
            InitializeComponent();
            candidateId = candidateIdInput;
            Console.WriteLine(candidateId);
            String candidateRawData = client.DownloadString("http://www.cvx4u.com/web_service/getCandidate.php?id="+candidateId);
            Console.WriteLine(candidateRawData);
            JObject candidateData = JObject.Parse(candidateRawData);
            candidateName.Text = candidateData.GetValue("FirstName") + " " + candidateData.GetValue("LastName") ;
            officeName.Text = candidateData.GetValue("Office")+"";

            String[] locales = { "OfficeCounty", "OfficeCity", "OfficeDistrict" };

            String[] issueText = candidateData.GetValue("Issues").ToString().Split(',');

            String issueDisplay="";

            foreach(String item in issueText)
            {
                if(item != "")
                {
                    String[] currentItem = item.Split('=');
                    int supportVal;
                    issueDisplay += currentItem[0]+": "+currentItem[1]+ '\r';
                    if(currentItem[1]=="Support")
                    {
                        supportVal = 1;

                    }
                    else
                    {
                        supportVal = 0;
                    }
                    issueList.Add(new KeyValuePair<string, int>(currentItem[0], supportVal));
                }
            }

            issues.Text = issueDisplay;

            foreach (String locale in locales)
            {
                Console.WriteLine("doo");
                Console.WriteLine(candidateData.GetValue(locale));
                if (candidateData.GetValue(locale).ToString() != "" && candidateData.GetValue(locale).ToString() != null)
                {
                    districtName.Text= candidateData.GetValue("OfficeState") + " - " + candidateData.GetValue(locale);
                    break;
                }
            }

            if(districtName.Text=="")
            {
                districtName.Text = candidateData.GetValue("OfficeState") + "";
            }



            //{"FirstName":"Hoover","LastName":"Jowell","Office":"County Board","OfficeCounty":"Montgomery","OfficeCity":"","OfficeDistrict":null,"OfficeState":"MD","Issues":"Guns=Support,Babies=Support,"}


        }
    }
}
