﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class OfficeSelection : ContentPage
    {
        public OfficeSelection()
        {
            InitializeComponent();


            officePicker.SelectedIndex = App.newUser.officePickerIndex;
            if(officePicker.SelectedIndex==8)
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

            officePicker.ItemsSource = App.offices;
        }

        public void ShowOtherOffice(Picker sender, EventArgs e)
        {
            App.newUser.officePickerIndex = sender.SelectedIndex;
            if(sender.SelectedIndex==8)
            {
                otherOfficeField.IsVisible = true;
                otherOfficeLabel.IsVisible = true;
            }
            else
            {
                otherOfficeField.IsVisible = false;
                otherOfficeLabel.IsVisible = false;
            }
        }

        public void ProcessPartyButton(Button sender, EventArgs e)
        {
            if(sender.ClassId=="D")
            {
                demButton.TextColor = Color.White;
                demButton.BackgroundColor = Color.Green;
                repButton.TextColor = Color.Default;
                repButton.BackgroundColor = Color.Default;
                otherButton.TextColor = Color.Default;
                otherButton.BackgroundColor = Color.Default;
                otherPartyField.IsVisible = false;
                otherPartyLabel.IsVisible = false;
                App.newUser.party = "Democratic";
            }
            else if(sender.ClassId=="R")
            {
                repButton.TextColor = Color.White;
                repButton.BackgroundColor = Color.Green;
                demButton.TextColor = Color.Default;
                demButton.BackgroundColor = Color.Default;
                otherButton.TextColor = Color.Default;
                otherButton.BackgroundColor = Color.Default;
                otherPartyField.IsVisible = false;
                otherPartyLabel.IsVisible = false;
                App.newUser.party = "Republican";
            }
            else
            {
                otherButton.TextColor = Color.White;
                otherButton.BackgroundColor = Color.Green;
                demButton.TextColor = Color.Default;
                demButton.BackgroundColor = Color.Default;
                repButton.TextColor = Color.Default;
                repButton.BackgroundColor = Color.Default;
                otherPartyField.IsVisible = true;
                otherPartyLabel.IsVisible = true;

            }
        }

        public async void ChangeWindows(Button thing, EventArgs e)
        {

            if (otherOfficeField.IsVisible == true)
            {
                App.newUser.office = otherOfficeField.Text;
            }
            else
            {
                App.newUser.office = App.offices[officePicker.SelectedIndex];

            }
            App.newUser.party = otherPartyField.Text;
            App.newUser.district = jurisdictionLabel.Text;
            
           

            if (thing.ClassId == "Back")
            {
                await Navigation.PushAsync(new CreateName());
            }
            else
            {
                await Navigation.PushAsync(new ContactInfoPage());
            }
        }
    }
}
