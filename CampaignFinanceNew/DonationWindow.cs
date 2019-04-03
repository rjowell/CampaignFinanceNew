using System;
using System.Net;
using Xamarin.Forms;

namespace CampaignFinanceNew
{
    public class DonationWindow
    {
        WebClient client = new WebClient();

        Frame localWindow { get; set; }
        string campaignNumber { get; set; }
        string localCampaignName;
        StackLayout itemLayout { get; set; }
        StackLayout entryLayout { get; set; }
        StackLayout buttonLayout { get; set; }

        Label confirmLabel { get; set; }
        Label confirmAmount { get; set; }
        Label confirmTo { get; set; }
        Label confirmCampaign { get; set; }
        Label donateQuery { get; set; }
        Label numberIncorrect { get; set; }
        Label thankYouText { get; set; }

        Label dollarSign { get; set; }
        Entry donationField { get; set; }

        Button donateSubmit { get; set; }
        Button donateCancel { get; set; }
        Button donateConfirm { get; set; }
        Button thankYouButton { get; set; }

        String thankYouDonation;
        String thankYouCrowd;

        bool isChangeAmount { get; set; }
        String previousAmount { get; set; }

        public DonationWindow(Frame window, String campaignID, String campaignName, bool isChange, String previousDonation)
        {
            Console.WriteLine("Previsou amount is " + previousDonation);


            localWindow = window;
            campaignNumber = campaignID;
            localCampaignName = campaignName;

            isChangeAmount = isChange;
            previousAmount = previousDonation;

            thankYouDonation = "Thank You. Your donation has been processed";
            thankYouCrowd = "Thank You. Your donation has been recorded and will be processed at the end of the campaign";

            itemLayout = (Xamarin.Forms.StackLayout)window.FindByName("noticeWindow");
            entryLayout = (Xamarin.Forms.StackLayout)itemLayout.FindByName("entryLayout");
            buttonLayout = (Xamarin.Forms.StackLayout)itemLayout.FindByName("buttonLayout");

            confirmLabel = (Xamarin.Forms.Label)itemLayout.FindByName("confirmLabel");
            confirmAmount = (Xamarin.Forms.Label)itemLayout.FindByName("confirmAmount");
            confirmTo= (Xamarin.Forms.Label)itemLayout.FindByName("confirmTo");
            confirmCampaign= (Xamarin.Forms.Label)itemLayout.FindByName("confirmCampaign");
            donateQuery= (Xamarin.Forms.Label)itemLayout.FindByName("donateQuery");
            numberIncorrect= (Xamarin.Forms.Label)itemLayout.FindByName("numberIncorrect");

            thankYouText= (Xamarin.Forms.Label)itemLayout.FindByName("thankYouText");
            thankYouButton= (Xamarin.Forms.Button)itemLayout.FindByName("thankYouButton");

           

            dollarSign = (Xamarin.Forms.Label)entryLayout.FindByName("dollarSign");
            donationField = (Xamarin.Forms.Entry)entryLayout.FindByName("donationField");


            donateSubmit = (Xamarin.Forms.Button)buttonLayout.FindByName("donateSubmit");
            donateCancel = (Xamarin.Forms.Button)buttonLayout.FindByName("donateCancel");
            donateConfirm = (Xamarin.Forms.Button)buttonLayout.FindByName("donateConfirm");

        }

        public void openWindow()
        {
            localWindow.IsVisible = true;

            confirmTo.IsVisible = false;

            numberIncorrect.IsVisible = false;

            confirmLabel.IsVisible = false;

            confirmAmount.IsVisible = false;

            confirmCampaign.IsVisible = false;

            donateConfirm.IsVisible = false;

            dollarSign.IsVisible = true;

            donateSubmit.IsVisible = true;

            donationField.IsVisible = true;

            donateCancel.IsVisible = true;
           
            if (isChangeAmount == true)
            {
                donateQuery.Text = "Do you want to change your doantion to this campaign?";
                donationField.Text = previousAmount;

            }
            else
            {
                donationField.Text = "";
                donateQuery.Text = "How much would you like to donate to this campaign?";
            }
            confirmAmount.Text = "";

            thankYouText.IsVisible = false;
            thankYouButton.IsVisible = false;


        }

        public string ShowCampaignID()
        {
            return campaignNumber;
        }

        public void ProcessDonation()
        {
            Console.WriteLine("processor");

            System.Collections.Specialized.NameValueCollection donationData = new System.Collections.Specialized.NameValueCollection();

            donationData.Add("supporterID", App.currentUser.systemID);

            if (confirmAmount.Text.Contains(".") == true)
            {
                String[] amount = confirmAmount.Text.Split('.');
                if(amount[1].Length==1)
                {
                    Console.WriteLine(amount[0] + "" + amount[1] + "0");
                    donationData.Add("donorAmount", amount[0] +""+ amount[1]+"0");
                }
                else if(amount[1].Length == 2)
                {
                    Console.WriteLine(amount[0] + "" + amount[1]);
                    donationData.Add("donorAmount", amount[0] + "" + amount[1]);

                }

            }
            else
            {
                donationData.Add("donorAmount", confirmAmount.Text+"00");
            }

            donationData.Add("campaignID", campaignNumber);
            donationData.Add("isChangeDonation", isChangeAmount.ToString().ToLower());

            Console.WriteLine("Amount is" + donationData.GetValues("donorAmount")[0]);

            var data = client.UploadValues(new Uri("http://www.cvx4u.com/web_service/processDonation.php"), donationData);
            //Console.WriteLine(System.Text.Encoding.UTF8.GetString(data));
            App.currentUser.ProcessCampDonations(System.Text.Encoding.UTF8.GetString(data));
            //Console.WriteLine(App.currentUser.campaignsSupported);

            confirmLabel.IsVisible = false;
            confirmAmount.IsVisible = false;
            donateCancel.IsVisible = false;
            //confirmAmount.Text = donationField.Text;
            confirmTo.IsVisible = false;
            confirmCampaign.IsVisible = false;
            confirmCampaign.Text = "";
            donateSubmit.IsVisible = false;
            donateConfirm.IsVisible = false;
            donateQuery.IsVisible = false;
            dollarSign.IsVisible = false;
            donationField.IsVisible = false;

            thankYouText.IsVisible = true;
            if(isChangeAmount==true)
            {
                thankYouText.Text = thankYouCrowd;
            }
            else
            {
                thankYouText.Text = thankYouDonation;
            }

            thankYouButton.IsVisible = true;


            //localWindow.IsVisible = false;
        }
        public void confirmDonation()
        {

            Console.WriteLine("confirm donation");

            if (double.TryParse(donationField.Text, out double testNum) == false)
            {
                numberIncorrect.IsVisible = true;
                Console.WriteLine("confirm donation11");
            }
            else if (donationField.Text.Contains("."))
            {
                String[] amounts = donationField.Text.Split('.');
                Console.WriteLine("confirm donation22");
                if (amounts[1].Length > 2)
                {
                    numberIncorrect.IsVisible = true;
                    Console.WriteLine("confirm donatio33");
                }
                else
                {
                    confirmLabel.IsVisible = true;
                    confirmAmount.IsVisible = true;
                    confirmAmount.Text = donationField.Text;
                    confirmTo.IsVisible = true;
                    confirmCampaign.IsVisible = true;
                    confirmCampaign.Text = localCampaignName + " ?";
                    donateSubmit.IsVisible = false;
                    donateConfirm.IsVisible = true;
                    donateQuery.IsVisible = false;
                    dollarSign.IsVisible = false;
                    donationField.IsVisible = false;
                }
            }
            else
            {
                confirmLabel.IsVisible = true;
                confirmAmount.IsVisible = true;
                confirmAmount.Text = donationField.Text;
                confirmTo.IsVisible = true;
                confirmCampaign.IsVisible = true;
                confirmCampaign.Text = localCampaignName + " ?";
                donateSubmit.IsVisible = false;
                donateConfirm.IsVisible = true;
                donateQuery.IsVisible = false;
                dollarSign.IsVisible = false;
                donationField.IsVisible = false;
            }
        }

        public void closeWindow()
        {
            localWindow.IsVisible = false;
            confirmLabel.IsVisible = false;
            confirmAmount.IsVisible = false;
            confirmTo.IsVisible = false;
            confirmCampaign.IsVisible = false;
            confirmAmount.Text = "";
            confirmCampaign.Text = "";
            donationField.Text = "";

            donateQuery.IsVisible = true;
            entryLayout.IsVisible = true;
            donateConfirm.IsVisible = false;
        }
    }
}


/* <Frame x:Name="donateWindow" VerticalOptions="CenterAndExpand" HorizontalOptions="Center" RelativeLayout.XConstraint="{ConstraintExpression Type=RelativeToParent,Property=Width, Factor=0,Constant=50}"   RelativeLayout.WidthConstraint="{ConstraintExpression Type=RelativeToParent,Property=Width,Factor=.7}" RelativeLayout.HeightConstraint="{ConstraintExpression Type=RelativeToParent,Property=Height}" Padding="5" BackgroundColor="Navy">
           
            <StackLayout x:Name="noticeWindow" BackgroundColor="#d1e1fc" Padding="3">
        <Label TextColor="Navy" FontSize="17" HorizontalTextAlignment="Center" x:Name="confirmLabel" Text="Are you sure you want to donate"/>
                    <Label TextColor="Navy" FontSize="17" HorizontalTextAlignment="Center" x:Name="confirmAmount" Text=""/>
                    <Label TextColor="Navy" FontSize="17" HorizontalTextAlignment="Center" x:Name="confirmTo" Text="To"/>
                    <Label TextColor="Navy" FontSize="17" HorizontalTextAlignment="Center" x:Name="confirmCampaign" Text=""/>
                    <Label x:Name="donateQuery" TextColor="Navy" HorizontalTextAlignment="Center" Text="How much would you like to donate to this campaign?" FontSize="Medium"/>
                 <StackLayout Orientation="Horizontal" HorizontalOptions="Center">
                        <Label x:Name="dollarSign" VerticalOptions="Center" Text="$" FontSize="Large" HorizontalOptions="Center"/>
                    <Entry HorizontalTextAlignment="Start" HorizontalOptions="Center" Keyboard="Numeric" x:Name="donationField" MaxLength="5" WidthRequest="70">
                         <Entry.TextColor>
            <OnPlatform x:TypeArguments="Color" Android="Black" iOS="Black"/>
            </Entry.TextColor>
                </Entry>
                        </StackLayout> 
                    <StackLayout Orientation="Horizontal" HorizontalOptions="Center">
                          <Button FontSize="Medium" x:Name="donateSubmit" BackgroundColor="Transparent" TextColor="Navy" Clicked="ConfirmDonation" Text="Submit" HorizontalOptions="Start"  />
                         <Button FontSize="Medium" x:Name="donateConfirm" BackgroundColor="Transparent" TextColor="Navy" Clicked="ProcessDonation" Text="Confirm" HorizontalOptions="Start"  />
                           
                        <Button FontSize="Medium" x:Name="donateCancel" BackgroundColor="Transparent" TextColor="Navy" Clicked="CloseWindow" Text="Cancel" HorizontalOptions="Start"  />
                         
                        
                    </StackLayout>
                </StackLayout>
                </Frame>
*/
  