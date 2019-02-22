using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class CampaignDescription : ContentPage
    {
        public CampaignDescription()
        {
            InitializeComponent();

            if(App.newCampaign.isCrowdfund==false)
            {

                goalLabel.IsVisible = false;
                dollarEntry.IsVisible = false;
                endLabel.IsVisible = false;
                endDateEntry.IsVisible = false;

            }
        }

        public void ProcessButton(Button button, EventArgs e)
        {
            var sendingParameters = new System.Collections.Specialized.NameValueCollection
            {
                {"title",App.newCampaign.campaignName},
                {"isCrowdfund",App.newCampaign.isCrowdfund.ToString()},
            };
        }
    }
}
