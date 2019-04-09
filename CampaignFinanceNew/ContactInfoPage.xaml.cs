using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class ContactInfoPage : ContentPage
    {

        List<Entry> entries=new List<Entry>();        
        List<Label> labels = new List<Label>();
       

        public ContactInfoPage()
        {
            InitializeComponent();

            //Console.WriteLine("index is " + statePicker.SelectedIndex);
            NavigationPage.SetHasNavigationBar(this, false);

            backImage.Source = App.currentUser.getBackImage();

            entries.Add(contactPersonEntry);
           entries.Add(addressEntry);
            entries.Add(cityEntry);
            //entries.Add(statePicker);
            entries.Add(zipCodeEntry);
            entries.Add(phoneEntry);
            entries.Add(websiteEntry);
            labels.Add(contactPersonLabel);
            labels.Add(addressLabel);
            labels.Add(cityLabel);
            //labels.Add(stateLabel);
            labels.Add(zipLabel);
            labels.Add(phoneLabel);
            labels.Add(websiteLabel);
           
            addressEntry.Text = App.newUser.streetAddress;
            cityEntry.Text = App.newUser.city;
            statePicker.SelectedIndex = Array.IndexOf(App.states, App.newUser.state);
            zipCodeEntry.Text = App.newUser.zipCode;
            phoneEntry.Text = App.newUser.phone;
            if(App.newUser.isSupporter==true)
            {
                websiteEntry.IsVisible = false;
                websiteLabel.IsVisible = false;
                contactPersonLabel.IsVisible = false;
                contactPersonEntry.IsVisible = false;
                Console.WriteLine("still supporter");
            }
            else
            {
                websiteEntry.Text = App.newUser.website;
            }



            statePicker.ItemsSource = App.states;

            if(contactPersonEntry.IsVisible==true)
            {
                contactPersonEntry.Focus();
            }
            else
            {
                addressEntry.Focus();
            }
            foreach(Entry thisEntry in entries)
            {

                Console.WriteLine(entries.IndexOf(thisEntry));

                if (entries.IndexOf(thisEntry) != entries.Count-1)
                {
                    Console.WriteLine("Doo dee");
                    thisEntry.Completed += (s, e) =>
                    {
                        if (entries.IndexOf(thisEntry) == 2)
                        {
                            statePicker.Focus();
                        }

                        else
                        {

                            entries[entries.IndexOf(thisEntry) + 1].Focus();
                        }
                    };
                }

                

            }

        }

       

        public async void ProcessButton(object sender, EventArgs e)
        {

            Button current = (Button)sender;

            bool moveOn=true;

            foreach(Entry field in entries)
            {
                if(field.IsVisible==true &&  field.Text=="")
                {
                    labels[entries.IndexOf(field)].TextColor = Color.Red;
                    moveOn = false;

                }
                else
                {

                    labels[entries.IndexOf(field)].TextColor = Color.FromHex("#c5d8f7");

                }

            }
            Console.WriteLine("point 3");
            if (statePicker.SelectedIndex==-1)
            {
                stateLabel.TextColor = Color.Red;
                moveOn = false;

            }
            else
            {
                App.newUser.state = App.stateAbbr[statePicker.SelectedIndex];
                stateLabel.TextColor= Color.FromHex("#c5d8f7");
            }

            Console.WriteLine("point 2");

            App.newUser.streetAddress = addressEntry.Text;
            App.newUser.city = cityEntry.Text;

            App.newUser.zipCode = zipCodeEntry.Text;
            App.newUser.phone = phoneEntry.Text;
            App.newUser.website = websiteEntry.Text;
            App.newUser.contactPerson = contactPersonEntry.Text;


            if(current.ClassId=="Back")
            {

            
                if (App.newUser.isSupporter==true)
                 {
                     await Navigation.PushAsync(new CreateName());
                     }
                    else
                    {
                         await Navigation.PushAsync(new PositionsAndIssues());
                     }
                 }


            else
            {
                if (moveOn == true)
                {

                    if(App.newUser.isSupporter==true)
                    {
                        await Navigation.PushAsync(new OccupationPage());
                    }
                    else
                    {
                        await Navigation.PushAsync(new PaymentPage());
                    }


                }
            }

        }
    }
}
