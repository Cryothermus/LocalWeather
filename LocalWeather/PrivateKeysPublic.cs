using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalWeather
{
    public static partial class PrivateKeysPublic
    {
        public const string APIKey = "OpenWeatherAPIKey"; // replace with a valid API key

        // This class is not meant to be referenced "normally"- it's here to substitute PrivateKeys.cs, which contains the confidential
        // OpenWeatherAPI key used by this project.
        // To make this source code work "properly", you should:
        // - Replace the APIKey variable with an actual, valid OpenAPI key.
        // - Rename this class + file to PrivateKeys and PrivateKeys.cs, respectively.
    }
}
