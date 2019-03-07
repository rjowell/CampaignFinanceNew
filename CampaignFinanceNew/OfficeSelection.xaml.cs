using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class OfficeSelection : ContentPage
    {
        public OfficeSelection()
        {





            InitializeComponent();
            Random rand = new Random();
            backImage.Source = rand.Next(5).ToString() + ".png";
            Picker[] pickers = new Picker[] { officePicker, stateSelector};



            stateSelector.ItemsSource = App.stateAbbr;
            officePicker.ItemsSource = App.offices;
            officePicker.SelectedIndex = App.newUser.officePickerIndex;
            if(officePicker.SelectedIndex==9)
            {
                otherOfficeField.IsVisible = true;
                otherOfficeLabel.IsVisible = true;
                otherOfficeField.Text = App.newUser.office;
            }
            else
            {
                otherOfficeField.IsVisible = false;
                otherOfficeLabel.IsVisible = false;
            }

            if(App.newUser.party != "Republican" && App.newUser.party != "Democratic" && App.newUser.party != "")
            {
                otherPartyField.IsVisible = true;
                otherPartyLabel.IsVisible = true;
                otherPartyField.Text = App.newUser.party;
            }
            else
            {
                otherPartyField.IsVisible = false;
                otherPartyLabel.IsVisible = false;
            }


        }

        public void ShowOtherOffice(Picker sender, EventArgs e)
        {
            App.newUser.officePickerIndex = sender.SelectedIndex;
            if(sender.SelectedIndex==9)
            {
                otherOfficeField.IsVisible = true;
                otherOfficeLabel.IsVisible = true;
                districtLabel.Text = "";
            }
            else if(sender.SelectedIndex==8)
            {
                districtLabel.Text = "City";
                otherOfficeField.IsVisible = false;
                otherOfficeLabel.IsVisible = false;

            }
            else
            {
                districtLabel.Text = "District/Jurisdiction";
                otherOfficeField.IsVisible = false;
                otherOfficeLabel.IsVisible = false;

            }


        }

        public void ProcessPartyButton(Button sender, EventArgs e)
        {
            if(sender.ClassId=="D")
            {
                demButton.TextColor = Color.Blue;
                demButton.BackgroundColor = Color.CornflowerBlue;
                repButton.TextColor = Color.White;
                repButton.BackgroundColor = Color.Transparent;
                otherButton.TextColor = Color.White;
                otherButton.BackgroundColor = Color.Transparent;
                otherPartyField.IsVisible = false;
                otherPartyLabel.IsVisible = false;
                App.newUser.party = "Democratic";
            }
            else if(sender.ClassId=="R")
            {
                repButton.TextColor = Color.Blue;
                repButton.BackgroundColor = Color.CornflowerBlue;
                demButton.TextColor = Color.White;
                demButton.BackgroundColor = Color.Transparent;
                otherButton.TextColor = Color.White;
                otherButton.BackgroundColor = Color.Transparent;
                otherPartyField.IsVisible = false;
                otherPartyLabel.IsVisible = false;
                App.newUser.party = "Republican";
            }
            else
            {
                otherButton.TextColor = Color.Blue;
                otherButton.BackgroundColor = Color.CornflowerBlue;
                demButton.TextColor = Color.White;
                demButton.BackgroundColor = Color.Transparent;
                repButton.TextColor = Color.White;
                repButton.BackgroundColor = Color.Transparent;
                otherPartyField.IsVisible = true;
                otherPartyLabel.IsVisible = true;
                App.newUser.party = otherPartyField.Text;

            }
        }

        public async void ChangeWindows(Button thing, EventArgs e)
        {

            bool moveOn = true;
            if(officePicker.SelectedIndex==-1)
            {
                officePickerLabel.TextColor = Color.Red;
                moveOn = false;
            }
            else
            {
                officePickerLabel.TextColor = Color.FromHex("#c5d8f7");
            }

            if(officePicker.SelectedIndex==9 && otherPartyField.Text=="")
            {
                officePickerLabel.TextColor = Color.Red;
                moveOn = false;
            }
            else
            {
                officePickerLabel.TextColor = Color.FromHex("#c5d8f7");
            }

            if(jurisdictionLabel.Text=="")
            {
                districtLabel.TextColor = Color.Red;
                moveOn = false;
            }
            else
            {
                districtLabel.TextColor= Color.FromHex("#c5d8f7");

            }

            if(stateSelector.SelectedIndex==-1)
            {
                stateLabel.TextColor = Color.Red;
                moveOn = false;
            }
            else
            {
                stateLabel.TextColor= Color.FromHex("#c5d8f7");
            }

            if(App.newUser.party=="")
            {
                partyLabel.TextColor = Color.Red;
                moveOn = false;
            }
            else
            {
                partyLabel.TextColor= Color.FromHex("#c5d8f7");
            }

            if(otherButton.IsEnabled==true && otherPartyField.Text=="")
            {
                otherPartyLabel.TextColor = Color.Red;
                moveOn = false;
            }
            else
            {
                otherPartyLabel.TextColor= Color.FromHex("#c5d8f7");
            }



            if(moveOn==true)
            {
                if (otherOfficeField.IsVisible == true)
                {
                    App.newUser.office = otherOfficeField.Text;
                }
                else
                {
                    App.newUser.office = App.offices[officePicker.SelectedIndex];

                }
                App.newUser.district = jurisdictionLabel.Text;
                App.newUser.officeState = App.stateAbbr[stateSelector.SelectedIndex];

                if (thing.ClassId == "Back")
                {
                    await Navigation.PushAsync(new CreateName());
                }
                else
                {
                    await Navigation.PushAsync(new ContactInfoPage());
                }
            }


           



           


            //Console.WriteLine("office is "+App.newUser.office + " " + App.newUser.party + " " + App.newUser.district);



        }
    }
}
