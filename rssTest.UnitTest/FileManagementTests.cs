using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using rssTest.Implementation;
using rssTest.Classes;
using System.Linq;
using System.IO;

namespace rssTest.UnitTest
{
    /// <summary>
    ///     Unit tests for testing the public functions in File Management
    /// </summary>
    /// <remarks>
    /// Author:   Stephen McCutcheon
    /// Date:     10/08/2016
    /// </remarks>
    [TestClass]
    public class FileManagementTests
    {
        #region tested unit

        /// <summary>
        ///     Unit to be tested
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        private FileManagement _fileManagement;

        #endregion tested unit

        #region set up tests

        /// <summary>
        ///     Set up tests ready to run
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        [TestInitialize]
        public void Setup()
        {
            DateTime dtNow = new DateTime(2016, 8, 10, 13, 29, 00);

            DateTimeOffset dtoNow = new DateTimeOffset(dtNow);

            _fileManagement = new FileManagement("DATA", ".json", dtoNow);
        }
        #endregion set up tests

        #region tests

        /// <summary>
        ///     Check it generates the new filename in the correct format
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        [TestMethod]
        [TestCategory("File Management")]
        public void generateNewFileName()
        {
           
            var newfilename = _fileManagement.FileName;

            Assert.AreEqual("2016-08-10-13.json", newfilename);
        }



        /// <summary>
        ///     Check it loads a data from a json file successfully into 
        ///     a news object
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        [TestMethod]
        [TestCategory("File Management")]
        public void LoadFileSuccess()
        {
            var newsObject = _fileManagement.LoadFile(@"Data\\LoadFeed.json");

            Assert.IsNotNull(newsObject);
            Assert.AreEqual("BBC News - UK", newsObject.title);
            Assert.AreEqual("http://www.bbc.co.uk/news/", newsObject.link);
            Assert.AreEqual("BBC News - UK", newsObject.description);
            Assert.AreEqual(3, newsObject.items.Count);


            var firstNewsItem = (from newItem in newsObject.items
                                 select newItem).FirstOrDefault();

            Assert.IsNotNull(firstNewsItem);
            Assert.AreEqual("Rio Olympics 2016: Chris Froome wins bronze in men's time trial", firstNewsItem.title);
            Assert.AreEqual("http://www.bbc.co.uk/sport/olympics/37037338", firstNewsItem.link);
            Assert.AreEqual("Great Britain's Chris Froome wins bronze in the Olympic men's individual time trial, won by Swiss Fabian Cancellara.", firstNewsItem.description);
        }


        /// <summary>
        ///     Check it saves successfully
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        [TestMethod]
        [TestCategory("File Management")]
        public void SaveFileSuccess()
        {

            if (File.Exists(_fileManagement.FullFileName))
            {
                File.Delete(_fileManagement.FullFileName);
            }

            var currentNews = new News()
            {
                title = "title test",
                link = "link test",
                description = "description test"
            };


            var savedFile = _fileManagement.SaveFile(currentNews);

            Assert.AreEqual(true, savedFile);

            var newObject = _fileManagement.LoadFile(_fileManagement.FullFileName);

            Assert.IsNotNull(newObject);
            Assert.AreEqual("title test", newObject.title);
            Assert.AreEqual("link test", newObject.link);
            Assert.AreEqual("description test", newObject.description);

        }


        /// <summary>
        ///     Check Get Today's File Directory
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        [TestMethod]
        [TestCategory("File Management")]
        public void CheckExistingFilesListSuccess()
        {
            var filesWhichAlreadyExist = _fileManagement.GetTodaysFileList();

            Assert.IsNotNull(filesWhichAlreadyExist);
            Assert.AreEqual(1, filesWhichAlreadyExist.Count());

            var firstfile = filesWhichAlreadyExist.FirstOrDefault();

            Assert.IsNotNull(firstfile);
            Assert.AreEqual("DATA\\2016-08-10-12.json", firstfile);
         
        }

        #endregion tests
    }
}
