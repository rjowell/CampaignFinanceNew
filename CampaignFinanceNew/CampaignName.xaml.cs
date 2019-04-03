using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class CampaignName : ContentPage
    {
        public CampaignName()
        {
            InitializeComponent();
            campaignTitle.Text = App.newCampaign.campaignName;
        }

        private void ProcessButton(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;

            if(currentButton.Text=="Cancel")
            {
                App.newCampaign = new CurrentCampaign();
                Navigation.PushAsync(new CandidateDashboard());

            }
            else
            {
                App.newCampaign.campaignName = campaignTitle.Text;
                Navigation.PushAsync(new CampaignChooser());
            }
        }
    }
}
