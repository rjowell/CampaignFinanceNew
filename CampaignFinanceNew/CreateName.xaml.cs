using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class CreateName : ContentPage
    {

        Entry[] fields;

        public CreateName()
        {
            InitializeComponent();
            fields = new Entry[] {firstNameField, lastNameField };
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

        public async void ChangeWindows(Button thing, EventArgs e)
        {
            if(thing.ClassId=="Back")
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                if(firstNameField.Text=="")
                {
                    firstNameLabel.TextColor = Color.Red;
                }
                else if(lastNameField.Text=="")
                {
                    lastNameLabel.TextColor = Color.Red;
                }
                else
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
