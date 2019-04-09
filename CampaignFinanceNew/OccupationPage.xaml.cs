using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class OccupationPage : ContentPage
    {
        public OccupationPage()
        {
            InitializeComponent();


            Entry[] entries = { jobTitleEntry, employerEntry, cityEntry };

            statePicker.ItemsSource = App.stateAbbr;

            foreach(Entry these in entries)
            {
                these.Completed += (sender, e) => { 
                
                    if(Array.IndexOf(entries,these)==2)
                    {
                        statePicker.Focus();
                    }
                    else
                    {
                        entries[Array.IndexOf(entries, these) + 1].Focus();
                    }
                
                };
            }

            jobTitleEntry.Text = App.newUser.jobTitle;
            employerEntry.Text = App.newUser.employerName;
            cityEntry.Text = App.newUser.employerCity;
            NavigationPage.SetHasNavigationBar(this, false);


            backImage.Source = App.currentUser.getBackImage();

            goBackButton.Clicked+= (sender, e) => {

                Navigation.PushAsync(new ContactInfoPage());

            };

            nextButton.Clicked += (sender, e) => {

                bool moveOn = true;

                if(employerEntry.Text=="")
                {
                    employerLabel.TextColor = Color.Red;
                    moveOn = false;
                }
                if(jobTitleEntry.Text=="")
                {
                    jobTitleLabel.TextColor = Color.Red;
                    moveOn = false;
                }
                if(cityEntry.Text=="")
                {
                    cityLabel.TextColor = Color.Red;
                    moveOn = false;
                }
                if(statePicker.SelectedIndex==-1)
                {
                    stateLabel.TextColor = Color.Red;
                    moveOn = false;
                }

                if(moveOn==true)
                {
                    App.newUser.jobTitle = jobTitleEntry.Text;
                    App.newUser.employerName = employerEntry.Text;
                    App.newUser.employerCity = cityEntry.Text;
                    App.newUser.employerState = App.stateAbbr[statePicker.SelectedIndex];



                    Navigation.PushAsync(new PaymentPage());
                }


            };

        }

        
    }
}
