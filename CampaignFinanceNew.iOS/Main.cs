using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace CampaignFinanceNew.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            Console.WriteLine("test 123");
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
