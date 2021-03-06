﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class CreateUser : ContentPage
    {


        string[] states = new string[] { "Alaska", "Alabama", "Arkansas", "Arizona", "California", "Colorado", "Connecticut", "District of Columbia", "Delaware", "Florida", "Georgia", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky", "Louisiana", "Massachusetts", "Maryland", "Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina", "North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Vermont", "Washington", "Wisconsin", "West Virginia", "Wyoming" };
        string[] stateAbbr = new string[] { "AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DC", "DE", "FL", "GA", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VA", "VI", "VT", "WA", "WI", "WV", "WY" };
        string[] offices = new string[] { "U.S. Senate", "U.S. House", "Governor", "State Senate", "State House", "County Board", "City Council", "Mayor", "Other" };
        public void GetNewKey(string strIin)
        {

        }

        Button[] partyButtons;
        Uri submitCandidate = new Uri("http://www.cvx4u.com/web_service/create_user.php");
        HttpClient connClient = new HttpClient();
        Entry[] entryArray;         Label[] entryLabels;
        String partySelection;

        bool isSupporter;

        public void CreateNewUser(object sender, EventArgs e)
        {

        }

       
        
            public void setSupporter(bool supporterBool)
        {
            isSupporter = supporterBool;
        }

        public CreateUser(bool supporter)
        {

            InitializeComponent();
            Random rand = new Random();
            backImage.Source = rand.Next(5).ToString() + ".png";
            isSupporter = supporter;
            Console.WriteLine("supporter is" + isSupporter);
            stateSelection.ItemsSource = states;
            officeRunningFor.ItemsSource = offices;

            //passwordField.Focus();

            otherPartyLabel.IsVisible = false;
           
              successFrame.IsVisible = false;

            if (isSupporter == true)
            {
                partyLabel.IsVisible = false;
                candidateParty.IsVisible = false;
                demButton.IsVisible = false;
                gopButton.IsVisible = false;
                otherPartyButton.IsVisible = false;
                websiteLabel.IsVisible = false;
                contactPerson.IsVisible = false;
                contactPersonField.IsVisible = false;
                candidateWebsite.IsVisible = false;
                createWindowLabel.Text = "Tell us about yourself as a supporter";

                entryArray = new Entry[] { firstNameField, lastNameField, eMailField, phoneNumberField, passwordField };
                entryLabels = new Label[] { firstNameLabel, lastNameLabel, eMailLabel, phoneLabel, passwordLabel };
            }
            else
            {
                createWindowLabel.Text = "Tell us about yourself as a candidate";
                partyButtons = new Button[] { demButton, gopButton, otherPartyButton };


                entryArray = new Entry[] { firstNameField, lastNameField, eMailField, phoneNumberField, candidateWebsite, passwordField };
                entryLabels = new Label[] { firstNameLabel, lastNameLabel, eMailLabel, phoneLabel, websiteLabel, passwordLabel };

            }




        }

        private void ReturnToLogin(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }




        private void SubmitDataAsync(object sender, EventArgs e)
        {
            int count = 0;
            Console.WriteLine("step A");
            bool formComplete = true;
            foreach (Entry entries in entryArray)
            {
                if (entries.Text == "")
                {
                    entryLabels[count].TextColor = Color.Red;
                    formComplete = false;
                }
                count++;
                Console.WriteLine("step B");
            }
           
            Console.WriteLine("step C");
            if (formComplete == true)
            {

                Console.WriteLine("step D");

                //String content = "firstName=" + firstNameField.Text + "&lastName=" + lastNameField.Text + "&eMail=" + eMailField.Text + "&phone=" + phoneNumberField.Text + "&website=" + candidateWebsite.Text + "&party=" + partySelection;
                Console.WriteLine("success");
                    //Console.WriteLine(content);
                    //var sentString = new StringContent(content, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                    var sendingParameters = new System.Collections.Specialized.NameValueCollection
                    {
                        { "firstName", firstNameField.Text },
                        { "lastName", lastNameField.Text },
                        { "eMail", eMailField.Text },
                        { "isSupporter", isSupporter.ToString() },
                        { "phone", phoneNumberField.Text },
                        { "website", candidateWebsite.Text },
                        { "party", partySelection },
                        {"mailingAddress", addressField.Text},
                        {"city", cityField.Text},
                        { "state", stateAbbr[stateSelection.SelectedIndex]},
                        {"zipCode", zipCodeField.Text},
                        {"contactPerson", contactPersonField.Text}
            };
                    Console.WriteLine(sendingParameters.AllKeys);
                    //Console.WriteLine("jerkoff");
                    DependencyService.Get<IFirebaseAuthenticator>().CreateNewUser(eMailField.Text, passwordField.Text, sendingParameters);
                    //await connClient.PostAsync(submitCandidate, sentString);

                    //Console.WriteLine("your result is "+thisUserId);
                    //Console.WriteLine(whoDe);
                    //var thisObject = await DependencyService.Get<IFirebaseAuthenticator>().LoginWithEmailPassword(eMailField.Text, passwordField.Text);

                    //Console.WriteLine(thisObject.Length);
                
                successFrame.IsVisible = true;
            }
        }

        private void ProcessPartyButton(object sender, EventArgs e)
        {
            Button current = (Button)sender;

            current.BackgroundColor = Color.Blue;
            current.TextColor = Color.White;


            foreach (Button thisButton in partyButtons)
            {
                if (thisButton.Text != current.Text)
                {
                    thisButton.BackgroundColor = Color.White;
                    thisButton.TextColor = Color.Default;
                }
            }



            if (current.Text == "Other")
            {
                otherPartyLabel.IsVisible = true;
                candidateParty.IsVisible = true;
                partySelection = candidateParty.Text;
            }
            else
            {
                otherPartyLabel.IsVisible = false;
                candidateParty.IsVisible = false;
                partySelection = current.Text;
            }
        }

    }
}
