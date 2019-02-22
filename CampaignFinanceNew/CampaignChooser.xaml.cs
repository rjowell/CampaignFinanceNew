using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class CampaignChooser : ContentPage
    {
        public CampaignChooser()
        {
            InitializeComponent();
        }

        public void ProcessButton(Button sender, EventArgs e)
        {
            if(sender.StyleId=="general")
            {
                App.newCampaign.isCrowdfund = false;
            }
            else if(sender.StyleId=="crowd")
            {
                App.newCampaign.isCrowdfund = true;
            }
            else if(sender.StyleId=="cancel")
            {

            }
            else
            {

            }
        }
    }
}
