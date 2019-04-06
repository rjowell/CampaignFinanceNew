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

            //{0-"Select Office", 1-"U.S. Senate", 2-"U.S. House", 3-"Governor", 4-"State Senate", 5-"State House", 6-"County Board", 7-"City Council",8-"Mayor", 9-"Other" };


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

        public void ShowOtherOffice(object sender, EventArgs e)
        {
            Picker current = (Picker)sender;

            App.newUser.officePickerIndex = current.SelectedIndex;

            if(current.SelectedIndex==1 || current.SelectedIndex==3)
            {
                //US Senate & Governor
                districtLabel.IsVisible = false;
                jurisdictionLabel.IsVisible = false;

                otherOfficeField.IsVisible = false;
                otherOfficeLabel.IsVisible = false;
            }
           else if(current.SelectedIndex==2 || current.SelectedIndex == 4 || current.SelectedIndex == 5 || current.SelectedIndex == 6)
            {
                districtLabel.Text = "District";
                districtLabel.IsVisible = true;
                jurisdictionLabel.IsVisible = true;

                otherOfficeField.IsVisible = false;
                otherOfficeLabel.IsVisible = false;
            }

            else if (current.SelectedIndex==9)
            {
                otherOfficeField.IsVisible = true;
                otherOfficeLabel.IsVisible = true;
                districtLabel.IsVisible = true;
                districtLabel.Text = "District";

            }
            else if(current.SelectedIndex==8 || current.SelectedIndex == 7)
            {
                districtLabel.Text = "City";
                otherOfficeField.IsVisible = false;
                otherOfficeLabel.IsVisible = false;

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

        public void ProcessPartyButton(object sender, EventArgs e)
        {
            Button current = (Button)sender;

            if(current.ClassId=="D")
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
            else if(current.ClassId=="R")
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
                Console.WriteLine("other party picked");
                otherButton.TextColor = Color.Blue;
                otherButton.BackgroundColor = Color.CornflowerBlue;
                demButton.TextColor = Color.White;
                demButton.BackgroundColor = Color.Transparent;
                repButton.TextColor = Color.White;
                repButton.BackgroundColor = Color.Transparent;
                otherPartyField.IsVisible = true;
                otherPartyLabel.IsVisible = true;
                //App.newUser.party = otherPartyField.Text;

            }
        }

        public async void ChangeWindows(object thing, EventArgs e)
        {

            Button current = (Button)thing;

            bool moveOn = true;

            Console.WriteLine("pary is " + App.newUser.office);

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

            if(otherPartyField.IsVisible==true && otherPartyField.Text=="")
            {
                partyLabel.TextColor = Color.Red;
                moveOn = false;
            }
            else
            {
                partyLabel.TextColor= Color.FromHex("#c5d8f7");
                App.newUser.party = otherPartyField.Text;
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


            Console.WriteLine("office is " + App.newUser.party);

            if(moveOn==true)
            {
                if (otherOfficeField.IsVisible == true)
                {
                    App.newUser.office = otherOfficeField.Text;
                }
                else
                {
                    App.newUser.office = App.offices[officePicker.SelectedIndex];

                    if(officePicker.SelectedIndex==2 || officePicker.SelectedIndex == 4 || officePicker.SelectedIndex == 5)
                    {
                        App.newUser.officeDistrict = jurisdictionLabel.Text;
                    }
                    if(officePicker.SelectedIndex==6)
                    {
                        App.newUser.officeCounty = jurisdictionLabel.Text;
                    }
                    if(officePicker.SelectedIndex==7 || officePicker.SelectedIndex == 8)
                    {
                        App.newUser.officeCity = jurisdictionLabel.Text;
                    }

                    //{0-"Select Office", 1-"U.S. Senate", 2-"U.S. House", 3-"Governor", 4-"State Senate", 5-"State House", 6-"County Board", 7-"City Council",8-"Mayor", 9-"Other" };




                }


                Console.WriteLine("field visivile " + otherOfficeField.IsVisible);
                Console.WriteLine("ofc si " + App.newUser.office);

                //App.newUser.district = jurisdictionLabel.Text;
                App.newUser.officeState = App.stateAbbr[stateSelector.SelectedIndex];
                Console.WriteLine("distrcit s " + App.newUser.officeCounty);
                if (current.ClassId == "Back")
                {
                    await Navigation.PushAsync(new CreateName());
                }
                else
                {
                    await Navigation.PushAsync(new PositionsAndIssues());
                }
            }


           



           


            //Console.WriteLine("office is "+App.newUser.office + " " + App.newUser.party + " " + App.newUser.district);



        }
    }
}
