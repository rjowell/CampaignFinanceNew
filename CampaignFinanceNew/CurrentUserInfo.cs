using System;
namespace CampaignFinanceNew
{
    public class CurrentUserInfo
    {
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String systemID { get; set; }


        public CurrentUserInfo(string userFirstName, string userLastName, string userSystemId)
        {
            firstName = userFirstName;
            lastName = userLastName;
            systemID = userSystemId;
        }
    }
}
