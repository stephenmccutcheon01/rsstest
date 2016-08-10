using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rssTest.Classes;
using System.ServiceModel.Syndication;
using System.Xml;

namespace rssTest.Implementation
{
    /// <summary>
    ///      Takes from the feed and reads the data into teh news object to be manuplated
    /// </summary>
    /// <remarks>
    /// Author:   Stephen McCutcheon
    /// Date:     10/08/2016
    /// </remarks>
    public class rssDocument
    {
        #region internal variables

        /// <summary>
        ///     URI for the RSS Feed
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        private string _rssUrl;

        #endregion internal vraiables

        #region constructor

        /// <summary>
        ///     Creates a instance of the rssDocument
        /// </summary>
        /// <param name="rssUrl"></param>
        public rssDocument(string rssUrl)
        {
            _rssUrl = rssUrl;
        }

        #endregion constructor

        #region public method

        /// <summary>
        ///      Filters the feed to only return the Items for a specific date
        /// </summary>
        /// <param name="dtToday"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public News GetNews()
        {
            XmlReader reader = XmlReader.Create(_rssUrl);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            if (feed != null)
            {
                News newsObj = new News();
                newsObj.title = feed.Title.Text;


                //Work out link
                newsObj.link = getLink(feed.Links);

                //work out description
                newsObj.description = feed.Description.Text;

                //load Today's news items....
                newsObj.items = generateNewsItems(feed);

                return newsObj;
            }
            else
            {
                return null;
            }

        }


        #endregion public method

        #region private methods


        /// <summary>
        ///      Generate the News Items
        /// </summary>
        /// <param name="dtToday"></param>
        /// <param name="feed"></param>
        /// <param name="newsObj"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        private List<NewsItems> generateNewsItems( SyndicationFeed feed)
        {
            List<NewsItems> newsItemsCollection = new List<NewsItems>();

            if ((feed != null) &&
                (feed.Items != null) 
               )
            {

          

                foreach (SyndicationItem item in feed.Items)
                {
                    var newsItem = new NewsItems();
                    newsItem.title = item.Title.Text;
                    newsItem.description = item.Summary.Text;
                    newsItem.link = getLink(item.Links);

                    if (item.PublishDate != null)
                    {
                        newsItem.pubDate = item.PublishDate.ToLocalTime().ToString("ddd, dd MMM yyyy HH:mm:ss");
                    }
                    newsItemsCollection.Add(newsItem);
                }
            }
            return newsItemsCollection;
        }

        /// <summary>
        ///     Converts the links into a string
        /// </summary>
        /// <param name="links"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        private string getLink(Collection<SyndicationLink> links)
        {
            var strLinkBuilder = new StringBuilder();

            links.ToList().ForEach(y =>
            {
                strLinkBuilder.Append(y.Uri);
                strLinkBuilder.Append(";");
            });

            strLinkBuilder.Remove(strLinkBuilder.Length - 1, 1);

            string strLink = strLinkBuilder.ToString();

            return strLink;
        }


        #endregion private methods
    }
}
