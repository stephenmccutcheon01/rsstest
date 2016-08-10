using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rssTest.Properties;

namespace rssTest.Classes
{
    /// <summary>
    ///     All the Global Settings
    /// </summary>
    /// <remarks>
    /// Author:   Stephen McCutcheon
    /// Date:     10/08/2016
    /// </remarks>
    public class rssSettings
    {
        #region properties

        /// <summary>
        ///   uri link
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public string rssLink
        {
            get
            {
                return Settings.Default.rssLink;
            }
        }

        /// <summary>
        ///    Directory for the json files
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public string feedDir
        {
            get
            {
                return Settings.Default.feedDir;
            }
        }

        /// <summary>
        ///     File extension for .json
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public string feedFileExtension
        {
            get
            {
                return Settings.Default.feedFileExtension;
            }
        }

        #endregion properties
    }
}
