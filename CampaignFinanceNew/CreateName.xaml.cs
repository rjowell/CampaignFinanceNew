using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class CreateName : ContentPage
    {
        public CreateName()
        {
            InitializeComponent();
            firstNameField.Text = App.newUser.firstName;
            lastNameField.Text = App.newUser.lastName;
            if(App.newUserIsSupporter==true)
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
                    if (App.newUserIsSupporter == false)
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
