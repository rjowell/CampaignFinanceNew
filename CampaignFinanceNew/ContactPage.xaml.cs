using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public partial class ContactPage : ContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
            cancelButton.Clicked += (sender, e) => {

                Navigation.PushAsync(new CandidateDashboard());
            };

            sendButton.Clicked += (sender, e) => {

                NameValueCollection sendData = new NameValueCollection();

                sendData.Set("id", App.currentUser.systemID);
                sendData.Set("firstName", App.currentUser.firstName);
                sendData.Set("lastName", App.currentUser.lastName);
                sendData.Set("messageText", messageBox.Text);

                WebClient client = new WebClient();

                client.UploadValues("http://www.cvx4u.com/web_service/sendSupportMessage.php", sendData);

                Navigation.PushAsync(new CandidateDashboard());


            };
        }
    }
}
