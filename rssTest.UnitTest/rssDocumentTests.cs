using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using rssTest.Classes;
using rssTest.Implementation;
using System.Linq;

namespace rssTest.UnitTest
{
    /// <summary>
    ///      Unit tests for testsing the class rssDocument
    /// </summary>
    /// <remarks>
    /// Author:   Stephen McCutcheon
    /// Date:     10/08/2016
    /// </remarks>
    [TestClass]
    public class rssDocumentTests
    {
        #region tested unit

        /// <summary>
        ///     Unit to be tested
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        private rssDocument _rssDocument;

        #endregion tested unit

        #region set up tests

        /// <summary>
        ///     Sets up the tests, it reads the data from a static 
        ///     xml file
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        [TestInitialize]
        public void setup()
        {
            _rssDocument = new rssDocument(@"Data\\TestFeed.xml");
        }

        #endregion set up tests

        #region tests

        /// <summary>
        ///    Check it genmerates the news objects successfully for the
        ///    Day
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        [TestMethod]
        [TestCategory("rssDocument")]
        public void GeneratesNewsObjectSuccessfully()
        {
            
            //Run test
            var newObj = _rssDocument.GetNews();

            //Evalulate Results
            Assert.IsNotNull(newObj);
            Assert.AreEqual("BBC News - UK", newObj.title);
            Assert.AreEqual("http://www.bbc.co.uk/news/", newObj.link);
            Assert.AreEqual("BBC News - UK", newObj.description);
           

            DateTime dtDate = new DateTime(2016, 8, 8,16,59,8);
            dtDate = DateTime.SpecifyKind(dtDate, DateTimeKind.Utc);
            DateTimeOffset dtoDate = dtDate;

            string strToDate = dtoDate.ToLocalTime().ToString("ddd, dd MMM yyyy HH:mm:ss");
            var selectedItem = (from newsItem in newObj.items
                                where newsItem.pubDate == strToDate
                                select newsItem).FirstOrDefault();

            Assert.IsNotNull(selectedItem);
            Assert.AreEqual("RAF helicopter fire on Snowdonia peak after technical issue", selectedItem.title);
            Assert.AreEqual("http://www.bbc.co.uk/news/uk-wales-north-west-wales-37023986", selectedItem.link);
            Assert.AreEqual("An RAF helicopter bursts into flames on a Snowdonia peak after being forced to land due to a technical issue.", selectedItem.description);
        }

        #endregion tests
    }
}
