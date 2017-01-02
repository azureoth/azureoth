using Azureoth.Datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azureoth.Utility
{
    public class ParamValidators
    {
        public static void ValidateAppId(string AppId)
        {
            Guid.Parse(AppId);
        }

        public static void ValidateAppTitle(string AppName)
        {
            if (string.IsNullOrEmpty(AppName))
            {
                throw new ArgumentException("Invalid AppId");
            }
        }
        public static void ValidateApp(UserApplication App)
        {
            if (App == null)
                throw new ArgumentException("Invalid App");

            //ValidateAppId(App.Id);
            ValidateAppTitle(App.Title);
        }
    }
}
