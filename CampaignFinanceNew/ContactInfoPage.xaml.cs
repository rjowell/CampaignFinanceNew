using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class ContactInfoPage : ContentPage
    {

        List<Entry> entries = new List<Entry>();
        List<Label> labels = new List<Label>();

        public ContactInfoPage()
        {
            InitializeComponent();
            entries.Add(addressEntry);
            entries.Add(cityEntry);
            entries.Add(zipCodeEntry);
            entries.Add(phoneEntry);
            entries.Add(websiteEntry);
            labels.Add(addressLabel);
            labels.Add(cityLabel);
            labels.Add(stateLabel);
            labels.Add(zipLabel);
            labels.Add(phoneLabel);
            labels.Add(websiteLabel);
           
            addressEntry.Text = App.newUser.streetAddress;
            cityEntry.Text = App.newUser.city;
            statePicker.SelectedIndex = Array.IndexOf(App.states, App.newUser.state);
            zipCodeEntry.Text = App.newUser.zipCode;
            phoneEntry.Text = App.newUser.phone;
            if(App.newUserIsSupporter==true)
            {
                websiteEntry.IsVisible = false;
                websiteLabel.IsVisible = false;
            }
            else
            {
                websiteEntry.Text = App.newUser.website;
            }



            statePicker.ItemsSource = App.states;
        }

       

        public async void ProcessButton(Button sender, EventArgs e)
        {

            bool moveOn=true;

            foreach(Entry field in entries)
            {
                if(field.Text=="")
                {
                    labels[entries.IndexOf(field)].TextColor = Color.Red;
                    moveOn = false;
                }
            }


             
            App.newUser.streetAddress = addressEntry.Text;
            App.newUser.city = cityEntry.Text;
            App.newUser.state = App.stateAbbr[statePicker.SelectedIndex];
            App.newUser.zipCode = zipCodeEntry.Text;
            App.newUser.phone = phoneEntry.Text;
            App.newUser.website = websiteEntry.Text;


            if(sender.ClassId=="Back")
            {

            
                if (App.newUserIsSupporter==true)
                 {
                     await Navigation.PushAsync(new CreateName());
                     }
                    else
                    {
                         await Navigation.PushAsync(new OfficeSelection());
                     }
                 }


                //Console.WriteLine(App.newUser.firstName + " " + App.newUser.lastName + " " + App.newUser.office + " " + App.newUser.district+" " +App.newUser.party + " " + App.newUser.streetAddress+" "+App.newUser.city+" "+App.newUser.state+" "+App.newUser.zipCode);


                //await Navigation.PushAsync(new CandidateDashboard());
            


            else
            {
                if (moveOn == true)
                {


                    await Navigation.PushAsync(new PaymentPage());
                }
            }

        }
    }
}
