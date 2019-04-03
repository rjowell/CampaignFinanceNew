using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class CreateName : ContentPage
    {

        Entry[] fields;
        Label[] labels;

        public CreateName()
        {
            InitializeComponent();
            Random rand = new Random();
            backImage.Source = rand.Next(5).ToString() + ".png";
            labels = new Label[] {firstNameLabel, lastNameLabel};
            fields = new Entry[] {firstNameField, lastNameField};
            Console.WriteLine("false is"+App.newUser.isSupporter);
            if(App.newUser.isSupporter==true)
            {
                Console.WriteLine("troo");
            }
            else
            {
                Console.WriteLine("flase");
            }
            firstNameField.Text = App.newUser.firstName;
            lastNameField.Text = App.newUser.lastName;
            firstNameField.Focus();


            foreach(Entry current in fields)
            {
                if (Array.IndexOf(fields, current) != fields.Length-1)
                {
                    current.Completed += (s, e) =>
                    {

                        fields[Array.IndexOf(fields, current) + 1].Focus();

                    };
                }
            }



            firstNameField.Completed += (s,e) => {
            
             

               };

            if (App.newUser.isSupporter==true)
            {
                titleLabel.Text = "What is your name?";
            }
            else
            {
                titleLabel.Text = "What is the candidate's name?";
            }
        }

        public async void ChangeWindows(object thing, EventArgs e)
        {
            Button current = (Button)thing;

            bool moveOn=true;
            if(current.ClassId=="Back")
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {

               foreach(Entry things in fields)
                {
                    if(things.Text=="")
                    {
                        labels[Array.IndexOf(fields, things)].TextColor = Color.Red;
                        moveOn = false;
                    }
                    else
                    {
                        labels[Array.IndexOf(fields, things)].TextColor = Color.FromHex("#c5d8f7");
                    }

                }


                if(moveOn==true)
                {
                    App.newUser.firstName = firstNameField.Text;
                    App.newUser.lastName = lastNameField.Text;

                    if (App.newUser.isSupporter == false)
                    {
                        await Navigation.PushAsync(new OfficeSelection());
                    }
                    else
                    {
                        await Navigation.PushAsync(new ContactInfoPage());
                    }
                }

            }
        }
    }
}
