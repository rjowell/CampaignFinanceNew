using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CampaignFinanceNew
{

    public class IssuePosition
    {
        public string issue { get; set; }
        public string position { get; set; }

        public IssuePosition(String inputIssue, String inputPosition)
        {
            issue = inputIssue;
            position = inputPosition;
        }


    }



    public partial class PositionsAndIssues : ContentPage
    {

        List<IssuePosition> listData;
        
        String issues="";

        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
       

        public PositionsAndIssues()
        {
            InitializeComponent();
           
            listData = new List<IssuePosition>();
            //App.newUser = new NewUserInfo();

            issueWarning.IsVisible = false;

            tapGestureRecognizer.Tapped += (s, e) => {

                Label currentLabel = (Label)s;

                listData.Remove(listData.Find((obj) => obj.issue == currentLabel.ClassId));
                

                positionDisplay.ItemsSource = null;
                positionDisplay.ItemsSource = listData;
            };




            //deleteLabel.GestureRecognizers.Add(tapGestureRecognizer);

            if (issues != "")
            {
                String[] issueItems = issues.Split(',');

                foreach (String things in issueItems)
                {
                    String[] thisItem = things.Split('=');
                    String supportString;
                    if (thisItem[1] == "1")
                    {
                        supportString = "Support";
                    }
                    else
                    {
                        supportString = "Oppose";
                    }
                    listData.Add(new IssuePosition(thisItem[0], supportString));
                }

                positionDisplay.ItemsSource = listData;
            }


        }

        private void ButtonControl(object sender, EventArgs e)
        {
            Button current = (Button)sender;

            if(current.Text=="Support")
            {
                supportButton.BackgroundColor = Color.FromHex("#aeccfc");
                supportButton.TextColor = Color.Black;
                opposeButton.BackgroundColor = Color.Transparent;
            }
            else
            {
                supportButton.BackgroundColor = Color.Transparent;
                opposeButton.BackgroundColor = Color.FromHex("#aeccfc");
                opposeButton.TextColor = Color.Black;
            }
        }

        private String SupportOppose()
        {
            if (supportButton.BackgroundColor==Color.Red)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        private void DeleteItem(object sender, EventArgs e)
        {

            Button current = (Button)sender;

            listData.Remove(listData.Find((obj) => obj.issue==current.ClassId));


            positionDisplay.ItemsSource = null;
            positionDisplay.ItemsSource = listData;
        }

       

        private void GoBack(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OfficeSelection());
        }


        private void AddToString(object sender, EventArgs e)
        {
            String currentInfo="";



                foreach (IssuePosition thisThing in listData)
                {
                    currentInfo += thisThing.issue + "=" + thisThing.position + ",";
                }

                App.newUser.issues = currentInfo;
            App.newUser.ideology = Math.Round(ideologySlider.Value, 1).ToString();
            Console.WriteLine(ideologySlider.Value.ToString());
            Console.WriteLine(currentInfo);

            Navigation.PushAsync(new ContactInfoPage());
        }

        private void AddPosition(object sender, EventArgs e)
        {


            if (supportButton.BackgroundColor == Color.FromHex("#aeccfc") || opposeButton.BackgroundColor == Color.FromHex("#aeccfc"))
            {
                String currentSupport;
                if (supportButton.BackgroundColor == Color.FromHex("#aeccfc"))
                {
                    currentSupport = "Support";
                }
                else
                {
                    currentSupport = "Oppose";
                }


                if(listData.Count==5)
                {
                    issueWarning.IsVisible = true;
                }
                else
                {
                    listData.Add(new IssuePosition(issueEntry.Text, currentSupport));
                }







                issueEntry.Text = "";

                supportButton.BackgroundColor = Color.Transparent;
                supportButton.TextColor = Color.FromHex("#c4daff");
                opposeButton.BackgroundColor = Color.Transparent;
                opposeButton.TextColor = Color.FromHex("#c4daff");
            }


            positionDisplay.ItemsSource = null;
            positionDisplay.ItemsSource = listData;
        }
    }
}
