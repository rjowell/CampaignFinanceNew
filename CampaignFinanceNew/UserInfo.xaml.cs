using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class UserInfo : ContentPage
    {
        System.Collections.Specialized.NameValueCollection sendingParameters;

        WebClient client = new WebClient();

        public UserInfo()
        {
            sendingParameters = new System.Collections.Specialized.NameValueCollection();
            sendingParameters.Set("userID", App.currentUser.systemID);
            sendingParameters.Set("isSupporter", App.currentUser.isSupporter.ToString().ToLower());
            InitializeComponent();
            String[] parties = { "Republican", "Democratic", "Other" };


            firstNameEntry.Text = App.currentUser.firstName;
            firstNameEntry.IsEnabled = false;
            firstNameSubmit.IsVisible = false;
            firstNameCancel.IsVisible = false;
             
            lastNameEntry.Text = App.currentUser.lastName;
            lastNameEntry.IsEnabled = false;
            lastNameSubmit.IsVisible = false;
            lastNameCancel.IsVisible = false;
          
            addressEntry.Text = App.currentUser.mailingAddress;
            addressEntry.IsEnabled = false;
            addressSubmit.IsVisible = false;
            addressCancel.IsVisible = false;

            cityEntry.Text = App.currentUser.city;
            cityEntry.IsEnabled = false;
            citySubmit.IsVisible = false;
            cityCancel.IsVisible = false;

            statePicker.ItemsSource = App.states;
            statePicker.IsEnabled = false;
            stateSubmit.IsVisible = false;
            stateCancel.IsVisible = false;
            statePicker.SelectedIndex = Array.IndexOf(App.stateAbbr, App.currentUser.mailState);

            zipCodeEntry.Text = App.currentUser.zipCode;
            zipCodeEntry.IsEnabled = false;
            zipCodeCancel.IsVisible = false;
            zipCodeSubmit.IsVisible = false;

            phoneEntry.Text = App.currentUser.phoneNumber;
            phoneEntry.IsEnabled = false;
            phoneCancel.IsVisible = false;
            phoneSubmit.IsVisible = false;

            if (App.currentUser.isSupporter == true)
            {
                contactPersonLabel.IsVisible = false;
                contactPersonEntry.IsVisible = false;
                contactPersonCancel.IsVisible = false;
                contactPersonSubmit.IsVisible = false;
                contactPersonEdit.IsVisible = false;

                officeLabel.IsVisible = false;
                officePicker.IsVisible = false;
                officeCancel.IsVisible = false;
                officeSubmit.IsVisible = false;
                officeEdit.IsVisible = false;

                officeState.IsVisible = false;
                officeStateLabel.IsVisible = false;
                officeStateCancel.IsVisible = false;
                officeStateSubmit.IsVisible = false;
                officeStateEdit.IsVisible = false;

                districtLabel.IsVisible = false;
                districtEntry.IsVisible = false;
                districtCancel.IsVisible = false;
                districtSubmit.IsVisible = false;
                districtEdit.IsVisible = false;
              
                partyLabel.IsVisible = false;
                partyPicker.IsVisible = false;
                otherPartyEntry.IsVisible = false;
                otherPartyLabel.IsVisible = false;
                partyCancel.IsVisible = false;
                partySubmit.IsVisible = false;
                partyEdit.IsVisible = false;

                websiteLabel.IsVisible = false;
                websiteEntry.IsVisible = false;
                websiteCancel.IsVisible = false;
                websiteSubmit.IsVisible = false;
                websiteEdit.IsVisible = false;
            }
            else
            {
                contactPersonEntry.Text = App.currentUser.contactPerson;
                contactPersonEntry.IsEnabled = false;
                contactPersonSubmit.IsVisible = false;
                contactPersonCancel.IsVisible = false;

                officePicker.ItemsSource = App.offices;
                officePicker.IsEnabled = false;
                officePicker.SelectedIndex = Array.IndexOf(App.offices, App.currentUser.office);

                officeState.ItemsSource = App.stateAbbr;
                officeState.IsEnabled = false;
                officeState.SelectedIndex = Array.IndexOf(App.stateAbbr, App.currentUser.officeState);

                districtEntry.Text = App.currentUser.district;
                districtEntry.IsEnabled = false;
                districtCancel.IsVisible = false;
                districtSubmit.IsVisible = false;

                partyPicker.ItemsSource = parties;
                partyPicker.IsEnabled = false;
                if (App.currentUser.party == "Republican")
                {
                    partyPicker.SelectedIndex = 0;
                    otherPartyEntry.IsVisible = false;
                    otherPartyLabel.IsVisible = false;
                }
                else if (App.currentUser.party == "Democratic")
                {
                    partyPicker.SelectedIndex = 1;
                    otherPartyEntry.IsVisible = false;
                    otherPartyLabel.IsVisible = false;
                }
                else
                {
                    partyPicker.SelectedIndex = 2;
                    otherPartyEntry.IsVisible = true;
                    otherPartyLabel.IsVisible = true;
                    otherPartyEntry.Text = App.currentUser.party;
                }



                websiteEntry.Text = App.currentUser.website;
                websiteEntry.IsEnabled = false;
                websiteCancel.IsVisible = false;
                websiteSubmit.IsVisible = false;

            }

        }

        public void OpenUserInfo(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserPassUpdate());
        }

        public void OpenPaymentInfo(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreditCardUpdate());
        }

        public void ProcessEdit(Button editButton, Button cancelButton, Button submitButton,VisualElement entry, int condition)
        {
            if (condition == 0)
            {
                editButton.IsVisible = false;
                cancelButton.IsVisible = true;
                submitButton.IsVisible = true;
                entry.IsEnabled = true;
            }
            else if (condition == 1)
            {
                entry.IsEnabled = false;
                submitButton.IsVisible = false;
                editButton.IsVisible = true;
                cancelButton.IsVisible = false;
            }
            else
            {
                entry.IsEnabled = false;
                submitButton.IsVisible = false;
                editButton.IsVisible = true;
                cancelButton.IsVisible = false;
            }

        }





        public void ProcessButton(object sender, EventArgs e)
        {
            Button current = (Button)sender;

            switch(current.ClassId)
            {
                //FirstName
                case "0":
                    if(current.StyleId=="firstNameEdit")
                    {
                        ProcessEdit(firstNameEdit, firstNameCancel, firstNameSubmit, firstNameEntry,0);
                        Console.WriteLine("First Name 0");

                    }
                    else if(current.StyleId=="firstNameCancel")
                    {
                        ProcessEdit(firstNameEdit, firstNameCancel, firstNameSubmit, firstNameEntry, 1);

                        firstNameEntry.Text = App.currentUser.firstName;
                        Console.WriteLine("First Name 1");
                    }
                    else
                    {

                        Console.WriteLine("First Name 2");

                        sendingParameters.Set("property", "FirstName");
                        sendingParameters.Set("value", firstNameEntry.Text);

                                                  
                       client.UploadValues("http://www.cvx4u.com/web_service/updateUserInfo.php", sendingParameters);
                        App.currentUser.firstName = firstNameEntry.Text;
                        Console.WriteLine("First Name 2A");
                        ProcessEdit(firstNameEdit, firstNameCancel, firstNameSubmit, firstNameEntry, 2);
                         


                    }
                    break;
                    //Last Name
                case "1":
                    if (current.StyleId == "lastNameEdit")
                    {
                        ProcessEdit(lastNameEdit, lastNameCancel, lastNameSubmit, lastNameEntry, 0);
                    }
                    else if (current.StyleId == "lastNameCancel")
                    {
                        ProcessEdit(lastNameEdit, lastNameCancel, lastNameSubmit, lastNameEntry, 1);
                        lastNameEntry.Text = App.currentUser.lastName;
                    }
                    else
                    {

                        sendingParameters.Set("property", "LastName");
                        sendingParameters.Set("value", lastNameEntry.Text);

                        client.UploadValues("http://www.cvx4u.com/web_service/updateUserInfo.php", sendingParameters);
                        App.currentUser.lastName = lastNameEntry.Text;
                        ProcessEdit(lastNameEdit, lastNameCancel, lastNameSubmit, lastNameEntry, 2);
                    }

                    break;
                    //Contact Person
                case "2":
                    if (current.StyleId == "contactPersonEdit")
                    {
                        ProcessEdit(contactPersonEdit, contactPersonCancel, contactPersonSubmit, contactPersonEntry, 0);
                    }
                    else if (current.StyleId == "contactPersonCancel")
                    {
                       
                        contactPersonEntry.Text = App.currentUser.contactPerson;
                        ProcessEdit(contactPersonEdit, contactPersonCancel, contactPersonSubmit, contactPersonEntry, 1);
                    }
                    else
                    {

                        sendingParameters.Set("property", "ContactPerson");
                        sendingParameters.Set("value", contactPersonEntry.Text);

                        client.UploadValues("http://www.cvx4u.com/web_service/updateUserInfo.php", sendingParameters);
                        App.currentUser.contactPerson = contactPersonEntry.Text;
                        ProcessEdit(contactPersonEdit, contactPersonCancel, contactPersonSubmit, contactPersonEntry, 2);
                    }
                    break;
                    //Address
                case "3":
                    if (current.StyleId == "addressEdit")
                    {

                        ProcessEdit(addressEdit, addressCancel, addressSubmit, addressEntry, 0);

                    }
                    else if (current.StyleId == "addressCancel")
                    {

                        addressEntry.Text = App.currentUser.mailingAddress;
                        ProcessEdit(addressEdit, addressCancel, addressSubmit, addressEntry, 1);
                    }
                    else
                    {

                        sendingParameters.Set("property", "MailingAddress");
                        sendingParameters.Set("value", addressEntry.Text);
                       
                        client.UploadValues("http://www.cvx4u.com/web_service/updateUserInfo.php", sendingParameters);
                        App.currentUser.mailingAddress = addressEntry.Text;
                        ProcessEdit(addressEdit, addressCancel, addressSubmit, addressEntry, 2);
                    }
                    break;
                    //City
                case "4":
                    if (current.StyleId == "cityEdit")
                    {
                        ProcessEdit(cityEdit, cityCancel, citySubmit, cityEntry, 0);
                    }
                    else if (current.StyleId == "cityCancel")
                    {

                        cityEntry.Text = App.currentUser.city;
                        ProcessEdit(cityEdit, cityCancel, citySubmit, cityEntry, 1);
                    }
                    else
                    {

                        sendingParameters.Set("property", "City");
                        sendingParameters.Set("value", cityEntry.Text);
                       
                        client.UploadValues("http://www.cvx4u.com/web_service/updateUserInfo.php", sendingParameters);
                        App.currentUser.city = cityEntry.Text;
                        ProcessEdit(cityEdit, cityCancel, citySubmit, cityEntry, 2);
                    }
                    break;
                    //mailingstate
                case "5":
                    if (current.StyleId == "stateEdit")
                    {

                        ProcessEdit(stateEdit, stateCancel, stateSubmit, statePicker, 0);

                       
                    }
                    else if (current.StyleId == "stateCancel")
                    {

                        statePicker.SelectedIndex = Array.IndexOf(App.stateAbbr, App.currentUser.mailState);
                        ProcessEdit(stateEdit, stateCancel, stateSubmit, statePicker, 1);
                    }
                    else
                    {

                        sendingParameters.Set("property", "State");
                        sendingParameters.Set("value", App.stateAbbr[statePicker.SelectedIndex]);
                       
                        client.UploadValues("http://www.cvx4u.com/web_service/updateUserInfo.php", sendingParameters);
                        App.currentUser.mailState = App.states[statePicker.SelectedIndex];
                        ProcessEdit(stateEdit, stateCancel, stateSubmit, statePicker, 2);
                    }
                    break;
                    //zipcode
                case "6":
                    if (current.StyleId == "zipCodeEdit")
                    {
                        ProcessEdit(zipCodeEdit, zipCodeCancel, zipCodeSubmit, zipCodeEntry, 0);


                    }
                    else if (current.StyleId == "zipCodeCancel")
                    {

                        zipCodeEntry.Text = App.currentUser.zipCode;
                        ProcessEdit(zipCodeEdit, zipCodeCancel, zipCodeSubmit, zipCodeEntry, 1);
                    }
                    else
                    {

                        sendingParameters.Set("property", "Zip");
                        sendingParameters.Set("value", zipCodeEntry.Text);

                        client.UploadValues("http://www.cvx4u.com/web_service/updateUserInfo.php", sendingParameters);
                        App.currentUser.zipCode = zipCodeEntry.Text;
                        ProcessEdit(zipCodeEdit, zipCodeCancel, zipCodeSubmit, zipCodeEntry, 2);
                    }
                    break;
                case "7":
                    if (current.StyleId == "phoneEdit")
                    {
                        ProcessEdit(phoneEdit, phoneCancel, phoneSubmit, phoneEntry, 0);

                  
                    }
                    else if (current.StyleId == "phoneCancel")
                    {

                        phoneEntry.Text = App.currentUser.phoneNumber;
                        ProcessEdit(phoneEdit, phoneCancel, phoneSubmit, phoneEntry, 1);
                    }
                    else
                    {
                       
                        sendingParameters.Set("property", "Phone");
                        sendingParameters.Set("value", phoneEntry.Text);
                       
                        client.UploadValues("http://www.cvx4u.com/web_service/updateUserInfo.php", sendingParameters);
                        App.currentUser.phoneNumber = phoneEntry.Text;
                        ProcessEdit(phoneEdit, phoneCancel, phoneSubmit, phoneEntry, 2);
                    }
                    break;
                case "8":
                    if (current.StyleId == "officeEdit")
                    {

                        ProcessEdit(officeEdit, officeCancel, officeSubmit, officePicker, 0);

                    }
                    else if (current.StyleId == "officeCancel")
                    {
                        ProcessEdit(officeEdit, officeCancel, officeSubmit, officePicker, 1);
                        if (App.currentUser.office=="Republican")
                        {
                            officePicker.SelectedIndex = 0;
                        }
                        else if(App.currentUser.office=="Democratic")
                        {
                            officePicker.SelectedIndex = 1;
                        }
                        else
                        {
                            officePicker.SelectedIndex = 2;
                        }
                    }
                    else
                    {

                        sendingParameters.Set("property", "Office");
                        if (App.offices[officePicker.SelectedIndex] == "Other")
                        {
                            sendingParameters.Set("value", otherPartyEntry.Text);
                            App.currentUser.office = otherPartyEntry.Text;
                        }
                        else
                        {

                            sendingParameters.Set("value", App.offices[officePicker.SelectedIndex]);
                            App.currentUser.office = App.offices[officePicker.SelectedIndex];
                        }
                       client.UploadValues("http://www.cvx4u.com/web_service/updateUserInfo.php", sendingParameters);

                        ProcessEdit(officeEdit, officeCancel, officeSubmit, officePicker, 2); 
                    }
                    break;
                case "9":
                    if (current.StyleId == "districtEdit")
                    {

                        ProcessEdit(districtEdit, districtCancel, districtSubmit, districtEntry, 0);


                    }
                    else if (current.StyleId == "districtCancel")
                    {

                        districtEntry.Text = App.currentUser.district;
                        ProcessEdit(districtEdit, districtCancel, districtSubmit, districtEntry, 1);
                    }
                    else
                    {
                       
                        sendingParameters.Set("property", "District");
                        sendingParameters.Set("value", districtEntry.Text);

                        client.UploadValues("http://www.cvx4u.com/web_service/updateUserInfo.php", sendingParameters);
                        App.currentUser.district = districtEntry.Text;
                        ProcessEdit(districtEdit, districtCancel, districtSubmit, districtEntry, 2);
                    }
                    break;
                case "10":
                    if (current.StyleId == "officeStateEdit")
                    {

                        ProcessEdit(officeStateEdit, officeStateCancel, officeStateSubmit, officeState, 0);



                    }
                    else if (current.StyleId == "officeStateCancel")
                    {

                        officeState.SelectedIndex = Array.IndexOf(App.stateAbbr, App.currentUser.officeState);
                        ProcessEdit(officeStateEdit, officeStateCancel, officeStateSubmit, officeState, 1);

                    }
                    else
                    {

                        sendingParameters.Set("property", "District");
                        sendingParameters.Set("value", App.stateAbbr[officeState.SelectedIndex]);

                        client.UploadValues("http://www.cvx4u.com/web_service/updateUserInfo.php", sendingParameters);
                        App.currentUser.officeState = App.stateAbbr[officeState.SelectedIndex];
                        ProcessEdit(officeStateEdit, officeStateCancel, officeStateSubmit, officeState, 2);
                    }

                    break;
                case "11":
                    if (current.StyleId == "partyEdit")
                    {

                        ProcessEdit(partyEdit, partyCancel, partySubmit, partyPicker, 0);
                      


                    }
                    else if (current.StyleId == "partyCancel")
                    {
                        ProcessEdit(partyEdit, partyCancel, partySubmit, partyPicker, 1);
                        if (App.currentUser.party=="Republican")
                        {
                            partyPicker.SelectedIndex = 0;
                        }
                        else if(App.currentUser.party=="Democratic")
                        {
                            partyPicker.SelectedIndex = 1;
                        }
                        else
                        {
                            partyPicker.SelectedIndex = 2;
                            otherPartyEntry.IsVisible = true;
                            otherPartyEntry.Text = App.currentUser.party;
                        }



                    }
                    else
                    {

                        sendingParameters.Set("property", "Affiliation");
                        if(partyPicker.SelectedIndex==0)
                        {
                            sendingParameters.Set("value", "Republican");
                            App.currentUser.party = "Republican";  
                        }
                        else if(partyPicker.SelectedIndex==1)
                        {
                            sendingParameters.Set("value", "Democratic");
                            App.currentUser.party = "Democratic";
                        }
                        else
                        {
                            sendingParameters.Set("value", otherPartyEntry.Text);
                            App.currentUser.party = otherPartyEntry.Text;
                        }


                        client.UploadValues("http://www.cvx4u.com/web_service/updateUserInfo.php", sendingParameters);
                        ProcessEdit(partyEdit, partyCancel, partySubmit, partyPicker, 2);
                    }

                    break;
                case "12":
                    if (current.StyleId == "websiteEdit")
                    {

                        ProcessEdit(websiteEdit, websiteCancel, websiteSubmit, websiteEntry, 0);



                    }
                    else if (current.StyleId == "websiteCancel")
                    {

                        websiteEntry.Text = App.currentUser.website;
                        ProcessEdit(websiteEdit, websiteCancel, websiteSubmit, websiteEntry, 1);



                    }
                    else
                    {

                        sendingParameters.Set("property", "Website");
                        sendingParameters.Set("value", websiteEntry.Text);

                        client.UploadValues("http://www.cvx4u.com/web_service/updateUserInfo.php", sendingParameters);
                        App.currentUser.website = websiteEntry.Text;
                        
                        ProcessEdit(websiteEdit, websiteCancel, websiteSubmit, websiteEntry, 2);
                    }

                    break;
            }
        }
    }
}
