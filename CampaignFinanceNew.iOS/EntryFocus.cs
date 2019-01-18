using System;
using Xamarin.Forms;
using CampaignFinanceNew.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(EntryFocus))]
namespace CampaignFinanceNew.iOS
{
    public class EntryFocus: IEntryFocus
    {
        public EntryFocus()
        {
        }



        public void SetFormFocus(Entry currentEntry)
        {

        }
    }
}
