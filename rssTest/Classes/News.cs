using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rssTest.Classes
{
    /// <summary>
    ///    The News for the Day
    /// </summary>
    /// <remarks>
    /// Author:   Stephen McCutcheon
    /// Date:     10/08/2016
    /// </remarks>
    public class News
    {
        #region public properties

        /// <summary>
        ///     Title
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public string title { get; set; }

        /// <summary>
        ///     Link 
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public string link { get; set; }

        /// <summary>
        ///     Description
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public string description { get; set; }

        /// <summary>
        ///     A collection of New Items for the Document
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public List<NewsItems> items { get; set; }

        #endregion public properties
    }
}
