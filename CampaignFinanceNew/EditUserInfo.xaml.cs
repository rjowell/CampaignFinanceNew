using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class EditUserInfo : ContentPage
    {
        String[] localFields = { "FirstName","LastName","ContactPerson","MailingAddress","City","State","Zip","Phone","Office","District","OfficeState","Affiliation","Website" };
        //other office


        String currentFieldValue;
        
        public void EditField(object sender, EventArgs e)
        {
            //Console.WriteLine("Message is "+(Xamarin.Forms.Entry)FindByName(sender.ClassId + "Entry"));
            //VisualElement currentElement;

            Button current = (Button)sender;

            Label currentLabel= (Xamarin.Forms.Label)FindByName(current.ClassId + "Display");

            Button cancelButton= (Xamarin.Forms.Button)FindByName(current.ClassId + "Cancel");
            Button submitButton= (Xamarin.Forms.Button)FindByName(current.ClassId + "Submit");
            cancelButton.IsVisible = true;
            submitButton.IsVisible = true;
            current.IsVisible = false;
            
            currentLabel.IsVisible = false;
            try
            {
                Entry currentElement = (Xamarin.Forms.Entry)FindByName(current.ClassId + "Entry");
                currentFieldValue = currentElement.Text;
                currentElement.IsVisible = true;
            }
            catch(System.InvalidCastException error)
            {
                Picker currentElement = (Xamarin.Forms.Picker)FindByName(current.ClassId + "Entry");
                currentElement.IsVisible = true;
                if(current.ClassId=="State" || current.ClassId == "OfficeState")
                {
                    currentFieldValue = App.stateAbbr[currentElement.SelectedIndex];
                }
                else if(current.ClassId=="Office")
                {
                    currentFieldValue = App.offices[currentElement.SelectedIndex];
                }
                else if(current.ClassId=="Affiliation")
                {

                    Console.WriteLine("Index is " + currentElement.SelectedIndex);
                    String[] parties = { "Republican", "Democratic", "Other" };
                    currentFieldValue = parties[currentElement.SelectedIndex];
                }
            }

            //currentElement.IsVisible = true;


        }

        System.Collections.Specialized.NameValueCollection postData;
        WebClient client = new WebClient();

        private void OpenUserPass(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserPassUpdate());
        }

        private void OpenCCWindow(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreditCardUpdate());
        }

        public void SubmitField(object sender, EventArgs e)
        {

            Button current = (Button)sender;

            postData.Set("property", current.ClassId);
            try
            {
                Entry currentElement = (Xamarin.Forms.Entry)FindByName(current.ClassId + "Entry");

                postData.Set("value", currentElement.Text);
            }
            catch(InvalidCastException error)
            {
                Picker currentElement = (Xamarin.Forms.Picker)FindByName(current.ClassId + "Entry");
                //currentElement.IsVisible = true;
                if (current.ClassId == "State" || current.ClassId == "OfficeState")
                {
                    postData.Set("value", App.stateAbbr[currentElement.SelectedIndex]);

                }
                else if (current.ClassId == "Office")
                {
                    postData.Set("value", App.offices[currentElement.SelectedIndex]);
                }
                else if (current.ClassId == "Affiliation")
                {


                    String[] parties = { "Republican", "Democratic", "Other" };
                    postData.Set("value", parties[currentElement.SelectedIndex]);
                }
            }




            client.UploadValues("http://www.cvx4u.com/web_service/updateUserInfo.php", postData);
            App.currentUser.firstName = FirstNameEntry.Text;

            Label currentLabel = (Xamarin.Forms.Label)FindByName(current.ClassId + "Display");

            Button cancelButton = (Xamarin.Forms.Button)FindByName(current.ClassId + "Cancel");
            Button submitButton = (Xamarin.Forms.Button)FindByName(current.ClassId + "Submit");
            Button editButton= (Xamarin.Forms.Button)FindByName(current.ClassId + "Edit");

            cancelButton.IsVisible = false;
            submitButton.IsVisible = false;
            editButton.IsVisible = true;

            try
            {
                Entry currentElement = (Xamarin.Forms.Entry)FindByName(current.ClassId + "Entry");

                currentLabel.Text = currentElement.Text;
                currentElement.IsVisible = false;
                currentLabel.IsVisible = true;
            }
            catch (InvalidCastException error)
            {
                Picker currentElement = (Xamarin.Forms.Picker)FindByName(current.ClassId + "Entry");
                Label infoLabel= (Xamarin.Forms.Label)FindByName(current.ClassId + "Display");
                //currentElement.IsVisible = true;
                if (current.ClassId == "State")
                {
                    infoLabel.Text = App.stateAbbr[currentElement.SelectedIndex];
                    //postData.Set("value", App.stateAbbr[currentElement.SelectedIndex]);

                }
                else if(current.ClassId == "OfficeState")
                {
                    infoLabel.Text = App.stateAbbr[currentElement.SelectedIndex];
                }
                else if (current.ClassId == "Office")
                {
                    infoLabel.Text= App.offices[currentElement.SelectedIndex];
                }
                else if (current.ClassId == "Affiliation")
                {


                    String[] parties = { "Republican", "Democratic", "Other" };
                    infoLabel.Text= parties[currentElement.SelectedIndex];
                }

                currentElement.IsVisible = false;
                infoLabel.IsVisible = true;
            }



        }

        public void CancelField(object sender, EventArgs e)
        {
            Button current = (Button)sender;

            Label currentLabel = (Xamarin.Forms.Label)FindByName(current.ClassId + "Display");

            Button editButton= (Xamarin.Forms.Button)FindByName(current.ClassId + "Edit");
            Button cancelButton = (Xamarin.Forms.Button)FindByName(current.ClassId + "Cancel");
            Button submitButton = (Xamarin.Forms.Button)FindByName(current.ClassId + "Submit");

            editButton.IsVisible = true;
            cancelButton.IsVisible = false;
            submitButton.IsVisible = false;

            currentLabel.Text = currentFieldValue;
            currentLabel.IsVisible = true;

            try
            {
                Entry currentElement = (Xamarin.Forms.Entry)FindByName(current.ClassId + "Entry");
               
                currentElement.IsVisible = false;


            }
            catch (InvalidCastException error)
            {
                Picker currentElement = (Xamarin.Forms.Picker)FindByName(current.ClassId + "Entry");
                currentElement.IsVisible = false;

               
                if (current.ClassId == "State" || current.ClassId == "OfficeState")
                {
                      currentElement.SelectedIndex= Array.IndexOf(App.stateAbbr,currentFieldValue);
                }
                else if (current.ClassId == "Office")
                {
                    currentElement.SelectedIndex = Array.IndexOf(App.offices, currentFieldValue);
                }
                else if (current.ClassId == "Affiliation")
                {
                    String[] parties = { "Republican", "Democratic", "Other" };
                    Console.WriteLine("Paryt is"+currentFieldValue);
                    currentElement.SelectedIndex = Array.IndexOf(parties, currentFieldValue);
                }
            }
        }

        String[] offices = { "Republican", "Democratic", "Other" };
        public EditUserInfo()
        {
            InitializeComponent();

           postData= new System.Collections.Specialized.NameValueCollection();

            postData.Set("userID", App.currentUser.systemID);
            postData.Set("isSupporter", App.currentUser.isSupporter.ToString().ToLower());

            foreach (String thing in localFields)
            {
                Console.WriteLine(thing);
                Button submitButton = (Xamarin.Forms.Button)FindByName(thing + "Submit");
                submitButton.IsVisible = false;
                Button cancelButton= (Xamarin.Forms.Button)FindByName(thing + "Cancel");
                cancelButton.IsVisible = false;
            }

            StateEntry.ItemsSource = App.stateAbbr;
            OfficeStateEntry.ItemsSource = App.stateAbbr;
            OfficeEntry.ItemsSource = App.offices;
            AffiliationEntry.ItemsSource = offices;

            FirstNameDisplay.Text = App.currentUser.firstName;
            FirstNameEntry.Text= App.currentUser.firstName;
            FirstNameEntry.IsVisible = false;

            LastNameDisplay.Text = App.currentUser.lastName;
            LastNameEntry.Text = App.currentUser.lastName;
            LastNameEntry.IsVisible = false;

            MailingAddressDisplay.Text = App.currentUser.mailingAddress;
            MailingAddressEntry.Text = App.currentUser.mailingAddress;
            MailingAddressEntry.IsVisible = false;

            CityDisplay.Text = App.currentUser.city;
            CityEntry.Text = App.currentUser.city;
            CityEntry.IsVisible = false;

            StateEntry.SelectedIndex = Array.IndexOf(App.stateAbbr, App.currentUser.mailState);
            StateDisplay.Text = App.currentUser.mailState;
            StateEntry.IsVisible = false;
            //stateEntry.IsEnabled = false;

            ZipDisplay.Text = App.currentUser.zipCode;
            ZipEntry.Text = App.currentUser.zipCode;
            ZipEntry.IsVisible = false;

            PhoneDisplay.Text = App.currentUser.phoneNumber;
            PhoneEntry.Text = App.currentUser.phoneNumber;
            PhoneEntry.IsVisible = false;

            OfficeEntry.SelectedIndex = Array.IndexOf(App.offices, App.currentUser.office);
            OfficeDisplay.Text = App.currentUser.office;
            OfficeEntry.IsVisible = false;
            otherOfficeEntry.IsVisible = false;
            if(App.currentUser.office=="Other")
            {
                otherOfficeLabel.IsVisible = true;
                otherOfficeEntry.Text = App.currentUser.office;
                otherPartyDisplay.Text= App.currentUser.office;
                otherOfficeEntry.IsVisible = true;
                otherPartyDisplay.IsVisible = true;
                
            }
            else
            {
                otherOfficeLabel.IsVisible = false;
                otherOfficeEntry.IsVisible = false;
                otherOfficeDisplay.IsVisible = false;
                otherPartyDisplay.IsVisible = false;
            }

            DistrictDisplay.Text = App.currentUser.district;
            DistrictEntry.Text = App.currentUser.district;
            DistrictEntry.IsVisible = false;

            OfficeStateDisplay.Text = App.currentUser.officeState;
            Console.WriteLine("Office state is" + App.currentUser.officeState);
            OfficeStateEntry.SelectedIndex= Array.IndexOf(App.stateAbbr, App.currentUser.officeState);
            OfficeStateEntry.IsVisible = false;
            //officeEntry.IsEnabled = false;

            AffiliationDisplay.Text = App.currentUser.party;

            Console.WriteLine("current index " + Array.IndexOf(offices, App.currentUser.party));
            AffiliationEntry.SelectedIndex=Array.IndexOf(offices,App.currentUser.party);
            AffiliationEntry.IsVisible = false;
            otherPartyEntry.IsVisible = false;
            if(AffiliationEntry.SelectedIndex==2)
            {

                otherPartyEntry.Text = App.currentUser.party;
                otherPartyLabel.IsVisible = true;
                otherPartyEntry.IsVisible = true;
                otherPartyDisplay.IsVisible = true;
                otherPartyDisplay.Text= App.currentUser.party;

            }
            else
            {
                otherPartyLabel.IsVisible = false;
                otherPartyEntry.IsVisible = false;
                otherPartyDisplay.IsVisible = false;
            }

            WebsiteDisplay.Text = App.currentUser.website;
            WebsiteEntry.Text = App.currentUser.website;
            WebsiteEntry.IsVisible = false;

            ContactPersonDisplay.Text = App.currentUser.contactPerson;
            ContactPersonEntry.Text = App.currentUser.contactPerson;
            ContactPersonEntry.IsVisible = false;
        }
    }
}
