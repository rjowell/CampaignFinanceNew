using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class CreateEditCampaign : ContentPage
    {
        String[] elements = { "CampaignName", "CampaignDescription", "Goal", "StartDate", "EndDate" };

        String campaignNumber;
        WebClient client = new WebClient();

        System.Collections.Specialized.NameValueCollection sendingData = new System.Collections.Specialized.NameValueCollection();
        System.Collections.Specialized.NameValueCollection editData = new System.Collections.Specialized.NameValueCollection();

        private void GoBack(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CandidateDashboard());
        }


        private void EditCampaign(object sender, EventArgs e)
        {
            Button current = (Button)sender;

            if (current.ClassId == "CampaignDescription")
            {

                CampaignDescriptionEntry.IsVisible = true;
                CampaignDescriptionSubmit.IsVisible = true;
                CampaignDescriptionCancel.IsVisible = true;
                CampaignDescriptionDisplay.IsVisible = false;
                CampaignDescriptionEdit.IsVisible = false;


            }
            else if (current.ClassId == "CampaignName")
            {
                CampaignNameEntry.IsVisible = true;
                CampaignNameSubmit.IsVisible = true;
                CampaignNameCancel.IsVisible = true;
                CampaignNameDisplay.IsVisible = false;
                CampaignNameEdit.IsVisible = false;
            }

            else if (current.ClassId == "StartDate")
            {
                StartDateEntry.IsVisible = true;
                StartDateSubmit.IsVisible = true;
                StartDateCancel.IsVisible = true;
                StartDateDisplay.IsVisible = false;
                StartDateEdit.IsVisible = false;


            }
            else if (current.ClassId == "EndDate")
            {

                EndDateEntry.IsVisible = true;
                EndDateSubmit.IsVisible = true;
                EndDateCancel.IsVisible = true;
                EndDateDisplay.IsVisible = false;
                EndDateEdit.IsVisible = false;
            }
            else
            {
                GoalEntry.IsVisible = true;
                GoalSubmit.IsVisible = true;
                GoalCancel.IsVisible = true;
                GoalDisplay.IsVisible = false;
                GoalEdit.IsVisible = false;
            }
        }



        private void SubmitData(object sender, EventArgs e)
        {
            Button current = (Button)sender;

            if (current.ClassId == "CampaignDescription")
            {
                editData.Set("property", current.ClassId);
                editData.Set("value", CampaignDescriptionEntry.Text);
                editData.Set("campaign", campaignNumber);
                CampaignDescriptionDisplay.Text = CampaignDescriptionEntry.Text;
                CampaignDescriptionEntry.IsVisible = false;
                CampaignDescriptionSubmit.IsVisible = false;
                CampaignDescriptionCancel.IsVisible = false;
                CampaignDescriptionDisplay.IsVisible = true;
                CampaignDescriptionEdit.IsVisible = true;


            }
            else if(current.ClassId=="CampaignName")
            {
                editData.Set("property", current.ClassId);
                editData.Set("value", CampaignNameEntry.Text);
                editData.Set("campaign", campaignNumber);
                CampaignNameDisplay.Text = CampaignNameEntry.Text;
                CampaignNameEntry.IsVisible = false;
                CampaignNameSubmit.IsVisible = false;
                CampaignNameCancel.IsVisible = false;
                CampaignNameDisplay.IsVisible = true;
                CampaignNameEdit.IsVisible = true;
            }

            else if (current.ClassId == "StartDate")
            {
                editData.Set("property", current.ClassId);
                editData.Set("value",StartDateEntry.Date.ToShortDateString());
                editData.Set("campaign", campaignNumber);
                StartDateDisplay.Text = StartDateEntry.Date.ToShortDateString();
                StartDateEntry.IsVisible = false;
                StartDateSubmit.IsVisible = false;
                StartDateCancel.IsVisible = false;
                StartDateDisplay.IsVisible = true;
                StartDateEdit.IsVisible = true;


            }
            else if(current.ClassId == "EndDate")
            {


                editData.Set("property", current.ClassId);
                editData.Set("value", EndDateEntry.Date.ToShortDateString());
                editData.Set("campaign", campaignNumber);
                EndDateDisplay.Text = EndDateEntry.Date.ToShortDateString();
                EndDateEntry.IsVisible = false;
                EndDateSubmit.IsVisible = false;
                EndDateCancel.IsVisible = false;
                EndDateDisplay.IsVisible = true;
                EndDateEdit.IsVisible = true;
            }
            else
            {
                editData.Set("property", current.ClassId);
                editData.Set("value",GoalEntry.Text);
                editData.Set("campaign", campaignNumber);
                GoalDisplay.Text = GoalEntry.Text;
                GoalEntry.IsVisible = false;
                GoalSubmit.IsVisible = false;
                GoalCancel.IsVisible = false;
                GoalDisplay.IsVisible = true;
                GoalEdit.IsVisible = true;
            }

            if (StartDateEntry.Date.CompareTo(EndDateEntry.Date) > 0)
            {
                dateWarning.IsVisible = true;
            }
            else
            {
                dateWarning.IsVisible = false;
                client.UploadValues("http://www.cvx4u.com/web_service/create_campaign.php", editData);
            }
        }

        private void CancelEdit(object sender, EventArgs e)
        {

            Button current = (Button)sender;

            Label currentLabel = (Xamarin.Forms.Label)FindByName(current.ClassId + "Display");

            Button submitButton = (Button)FindByName(current.ClassId + "Submit");
            Button cancelButton = (Button)FindByName(current.ClassId + "Cancel");
            Button editButton = (Button)FindByName(current.ClassId + "Edit");

            submitButton.IsVisible = false;
            cancelButton.IsVisible = false;
            editButton.IsVisible = true;

            if (current.ClassId == "CampaignDescription")
            {

                 Editor currentInputView = (Xamarin.Forms.Editor)FindByName(current.ClassId + "Entry");

                currentInputView.Text = sendingData.Get("CampaignDescription").ToString();
                currentInputView.IsVisible = false;

               

            }
            else if (current.ClassId == "StartDate" || current.ClassId == "EndDate")
            {
                DatePicker currentItem = (DatePicker)FindByName(current.ClassId + "Entry");
                currentItem.Date = DateTime.Parse(sendingData.Get(current.ClassId));
                currentItem.IsVisible = false;


            }
            else
            {
                Entry currentInputView = (Xamarin.Forms.Entry)FindByName(current.ClassId + "Entry");
                currentInputView.Text = sendingData.Get(current.ClassId);
                currentInputView.IsVisible = false;
               
            }

            currentLabel.IsVisible = true;

        }

        private void ShowCrowdElements(Switch sender, ToggledEventArgs e)
        {
            GoalButtons.IsVisible = sender.IsToggled;
            GoalDisplay.IsVisible = sender.IsToggled;
            GoalEntry.IsVisible = sender.IsToggled;

            StartDateButtons.IsVisible = sender.IsToggled;
            StartDateDisplay.IsVisible = sender.IsToggled;
            StartDateEntry.IsVisible = sender.IsToggled;

            EndDateButtons.IsVisible = sender.IsToggled;
            EndDateDisplay.IsVisible = sender.IsToggled;
            EndDateEntry.IsVisible = sender.IsToggled;


        }

        private void SubmitCampaign(object sender, EventArgs e)
        {
            bool moveOn = true;
            if (CampaignNameEntry.Text == "")
            {
                CampaignNameLabel.TextColor = Color.Red;
                moveOn = false;
            }

            if (CampaignDescriptionEntry.Text == "")
            {
                CampaignDescriptionLabel.TextColor = Color.Red;
                moveOn = false;
            }
            if (isCrowdfund.IsToggled == true)
            {
                if (GoalEntry.Text == "")
                {
                    GoalLabel.TextColor = Color.Red;
                    moveOn = false;
                }

                if (StartDateEntry.Date.CompareTo(EndDateEntry.Date) >= 0)
                {
                    dateWarning.IsVisible = true;
                    moveOn = false;
                }


            }

            if (moveOn == true)
            {


                client.UploadValues("http://www.cvx4u.com/web_service/create_campaign.php", sendingData);
            }
        }



        public CreateEditCampaign(String campaignID)
        {
            InitializeComponent();

            campaignNumber = campaignID;

            dateWarning.IsVisible = false;

            CampaignNameEntry.IsVisible = false;
            CampaignDescriptionEntry.IsVisible = false;
            StartDateEntry.IsVisible = false;
            EndDateEntry.IsVisible = false;
            GoalEntry.IsVisible = false;

            sendingData.Set("candidateId", App.currentUser.systemID);

            GoalButtons.IsVisible = false;
            GoalDisplay.IsVisible = false;
            GoalEntry.IsVisible = false;

            StartDateButtons.IsVisible = false;
            StartDateDisplay.IsVisible = false;
            StartDateEntry.IsVisible = false;

            EndDateButtons.IsVisible = false;
            EndDateDisplay.IsVisible = false;
            EndDateEntry.IsVisible = false;

            if (campaignNumber == "")
            {
                titleLabel.Text = "Create Campaign";

                foreach (String thing in elements)
                {
                    Console.WriteLine("Item is " + thing);

                    Button editButton = (Xamarin.Forms.Button)FindByName(thing + "Edit");
                    Button submitButton = (Xamarin.Forms.Button)FindByName(thing + "Submit");
                    Button cancelButton = (Xamarin.Forms.Button)FindByName(thing + "Cancel");

                    editButton.IsVisible = false;
                    submitButton.IsVisible = false;
                    cancelButton.IsVisible = false;

                }

                isCrowdfund.IsToggled = false;
                isCrowdfund.IsEnabled = false;
                




            }
            else
            {
                titleLabel.Text = "Edit Campaign";

                String rawData = client.DownloadString("http://www.cvx4u.com/web_service/getCampaigns.php?campaignID=" + campaignNumber);

                formSubmit.IsVisible = false;

                JObject campaignData = JObject.Parse(rawData);

                CampaignNameDisplay.Text = campaignData.GetValue("CampaignName").ToString();
                CampaignNameEntry.Text = campaignData.GetValue("CampaignName").ToString();

                CampaignNameCancel.IsVisible = false;
                CampaignNameSubmit.IsVisible = false;
                CampaignNameEdit.IsVisible = true;

                isCrowdfund.IsToggled = false;
                isCrowdfund.IsEnabled = false;


                sendingData.Set("campaignName", CampaignNameEntry.Text);

                CampaignDescriptionDisplay.Text = campaignData.GetValue("CampaignDescription").ToString();
                CampaignDescriptionEntry.Text = campaignData.GetValue("CampaignDescription").ToString();
                CampaignDescriptionCancel.IsVisible = false;
                CampaignDescriptionSubmit.IsVisible = false;
                CampaignDescriptionEdit.IsVisible = true;
                sendingData.Set("campaignDescription",CampaignDescriptionEntry.Text);

                if (campaignData.GetValue("StartDate").ToString() != "")
                {
                    isCrowdfund.IsToggled = true;
                    isCrowdfund.IsEnabled = false;

                    GoalDisplay.Text = campaignData.GetValue("Goal").ToString();
                    GoalEntry.Text = campaignData.GetValue("Goal").ToString();
                    GoalButtons.IsVisible = true;
                    GoalCancel.IsVisible = false;
                    GoalSubmit.IsVisible = false;
                    GoalEdit.IsVisible = true;
                    sendingData.Set("goal", GoalEntry.Text);
                    GoalEntry.IsVisible = false;

                    StartDateDisplay.Text = campaignData.GetValue("StartDate").ToString();
                    StartDateEntry.Date = DateTime.Parse(campaignData.GetValue("StartDate").ToString());
                    StartDateButtons.IsVisible = true;
                    StartDateCancel.IsVisible = false;
                    StartDateSubmit.IsVisible = false;
                    StartDateEdit.IsVisible = true;
                    sendingData.Set("startDate", StartDateEntry.Date.ToShortDateString());
                    StartDateEntry.IsVisible = false;

                    EndDateDisplay.Text = campaignData.GetValue("EndDate").ToString();
                    EndDateEntry.Date = DateTime.Parse(campaignData.GetValue("EndDate").ToString());
                    EndDateButtons.IsVisible = true;
                    EndDateCancel.IsVisible = false;
                    EndDateSubmit.IsVisible = false;
                    EndDateEdit.IsVisible = true;
                    EndDateEntry.IsVisible = false;
                    sendingData.Set("endDate", EndDateEntry.Date.ToShortDateString());

                }



            }
        }
    }
}

