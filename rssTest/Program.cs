using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rssTest.Classes;
using rssTest.Implementation;

namespace rssTest
{
    /// <summary>
    ///     This program pulls the data from a specific feed and saves it into the json file which
    ///     resides in a folder, under the app directory called feed
    ///     
    ///     when it generates a new file, it checks the news item does not already exists in any json
    ///     file already generated today, if so it removes it from the newly generated files
    ///     
    ///     if no news items have changed, the gernerated json file has no item elements
    /// </summary>
    /// <remarks>
    /// Author:   Stephen McCutcheon
    /// Date:     10/08/2016
    /// </remarks>
    class Program
    {
        /// <summary>
        ///     Starting Point of the Console App
        /// </summary>
        /// <param name="args"></param>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        static void Main(string[] args)
        {
            //Get all the Setting
            var settings = new rssSettings();

            //Get the Data from the current feed
            var rssDocument = new rssDocument(settings.rssLink);

            var CurrentNewsObject = rssDocument.GetNews();

            //Get Current Date
            DateTimeOffset dtNowOffset = getCurrentDate();

            //Initiate the File Manager
            var FileManager = new FileManagement(settings.feedDir, settings.feedFileExtension, dtNowOffset);

            //Remove any news items which exists in any other json files
            removeAnyNewsItemsAlreadyInFiles(CurrentNewsObject, FileManager);

            //Saved resultant file
            var savedOK = FileManager.SaveFile(CurrentNewsObject);

            if (savedOK)
            {
                Console.WriteLine("generated " + FileManager.FileName + " OK");
            }
            else
            {
                Console.WriteLine("error generating " + FileManager.FileName);
            }


        }

        /// <summary>
        ///      Gets the Current date 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        private static DateTimeOffset getCurrentDate()
        {
            DateTime dtNow = DateTime.Now;
            dtNow = DateTime.SpecifyKind(dtNow, DateTimeKind.Utc);
            DateTimeOffset dtNowOffset = dtNow;
            return dtNowOffset;
        }

        /// <summary>
        ///    Goes through the current news items and removes any which have been published in
        ///    Existing json files
        /// </summary>
        /// <param name="CurrentNewsObject"></param>
        /// <param name="FileManager"></param>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        private static void removeAnyNewsItemsAlreadyInFiles(News CurrentNewsObject, FileManagement FileManager)
        {
            //remove any news items which are in another file
            var alreadyGeneratedFiles = FileManager.GetTodaysFileList();

            foreach (var file in alreadyGeneratedFiles)
            {
                var newsObject = FileManager.LoadFile(file);

                List<NewsItems> notAlreadyInFiles = (from x in CurrentNewsObject.items
                                                     where !(newsObject.items.Any(p2 => p2.pubDate == x.pubDate))
                                                     select x).ToList();

                CurrentNewsObject.items = notAlreadyInFiles;

            }
        }
    }
}
