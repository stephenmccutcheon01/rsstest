using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rssTest.Classes
{
    /// <summary>
    ///    A News Item for the Day
    /// </summary>
    /// <remarks>
    /// Author:   Stephen McCutcheon
    /// Date:     10/08/2016
    /// </remarks>
    public class NewsItems
    {
        #region public properties

        /// <summary>
        ///     title
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public string title { get; set; }

        /// <summary>
        ///     description
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public string description { get; set; }

        /// <summary>
        ///     link
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public string link { get; set; }

        /// <summary>
        ///    Publication date
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public string pubDate { get; set; }

        #endregion public properties
    }
}
