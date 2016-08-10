using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using rssTest.Classes;
using System.IO;



namespace rssTest.Implementation
{
    /// <summary>
    ///     Controls all the interaction with the Hard Drive, by
    ///     Reading and saving the Data
    /// </summary>
    /// <remarks>
    /// Author:   Stephen McCutcheon
    /// Date:     10/08/2016
    /// </remarks>
    public class FileManagement
    {
        #region internal fields

        /// <summary>
        ///     The seprator used in the file names
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        private const string _separator = "-";

        /// <summary>
        ///    The name of the directory where to put the JSON Files
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        private string _feeddir;

        /// <summary>
        ///    The Extension of the json files to use
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        private string _fileExtension;

        /// <summary>
        ///    The currently generated filename
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        private string _filename;

        /// <summary>
        ///     The full filename 
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        private string _fullfilename;


        /// <summary>
        ///    The Current Date
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        private DateTimeOffset _now;
        
        #endregion internal fields

        #region properties

        /// <summary>
        ///     The full file path for the filename
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public string FullFileName
        {
            get
            {
                return _fullfilename;
            }
        }

        /// <summary>
        ///     The new file which will get created
        /// </summary>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public string FileName
        {
            get
            {
                return _filename;
            }
        }



        #endregion properties

        #region constructor

        /// <summary>
        ///     Constructor, which generates the file name
        /// </summary>
        /// <param name="feeddir"></param>
        /// <param name="fileExtension"></param>
        /// <param name="now"></param>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public FileManagement(string feeddir, string fileExtension, DateTimeOffset now)
        {
            _feeddir = feeddir;
            _fileExtension = fileExtension;
            _now = now;
            _filename = generateFileName();
            _fullfilename = generateFullFileName(_filename);
            
        }

        #endregion constructor

        #region private methods

        /// <summary>
        ///     Generates a full filename including path
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        private string generateFullFileName(string filename)
        {
            StringBuilder strFileName = new StringBuilder();
            strFileName.Append(_feeddir);
            strFileName.Append("\\");
            strFileName.Append(filename);

            string fullFileName = strFileName.ToString();
            return fullFileName;
        }


        /// <summary>
        ///      Generates a file name
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        private string generateFileName()
        {
            StringBuilder strbuilder = new StringBuilder();

            strbuilder.Append(_now.Year);
            strbuilder.Append(_separator);

            if (_now.Month < 10)
            {
                strbuilder.Append("0");
            }
            strbuilder.Append(_now.Month);
            strbuilder.Append(_separator);

            if (_now.Day < 10)
            {
                strbuilder.Append("0");
            }

            strbuilder.Append(_now.Day);
            strbuilder.Append(_separator);

            if (_now.Hour < 10)
            {
                strbuilder.Append("0");
            }
            strbuilder.Append(_now.Hour);
            strbuilder.Append(_fileExtension);


            return strbuilder.ToString(); 
        }

        #endregion private methods

        #region public methods

        /// <summary>
        ///     Get list json files generated today, full file path
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public string[] GetTodaysFileList()
        {
            
            //check file does not already exist if delete, cause you 
            //are going to re-create it again
            if (File.Exists(@_fullfilename))
            {
                File.Delete(@_fullfilename);
            }

            //Build up a search path to find based on current date
            var searchPathBuilder = new StringBuilder();

            searchPathBuilder.Append(_now.Year);
            searchPathBuilder.Append(_separator);

            if (_now.Month < 10)
            {
                searchPathBuilder.Append("0");
            }
            searchPathBuilder.Append(_now.Month);
            searchPathBuilder.Append(_separator);

            if (_now.Day < 10)
            {
                searchPathBuilder.Append("0");
            }

            searchPathBuilder.Append(_now.Day);
            searchPathBuilder.Append("-*");
            searchPathBuilder.Append(_fileExtension);


            var searchpath = searchPathBuilder.ToString();

            if (Directory.Exists(@_feeddir) == false)
            {
                Directory.CreateDirectory(@_feeddir);
            }

            //Get all the files which reside in the Feed directory
            //which match the search path
            return Directory.GetFiles(@_feeddir, searchpath);

        }


        /// <summary>
        ///     Loads data from a specific json file into a news object
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public News LoadFile(string filename)
        {
            if (File.Exists(filename))
            {
                // deserialize JSON directly from a file
                using (StreamReader file = File.OpenText(@filename))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    var news = (News)serializer.Deserialize(file, typeof(News));
                    return news;
                }
            }

            return null;
        }

        /// <summary>
        ///     Saves file from a specfic news object into a json file
        /// </summary>
        /// <param name="CurrentNews"></param>
        /// <returns></returns>
        /// <remarks>
        /// Author:   Stephen McCutcheon
        /// Date:     10/08/2016
        /// </remarks>
        public bool SaveFile(News CurrentNews )
        {
            bool savedSuccessfully = false;

            if (Directory.Exists(@_feeddir) == false)
            {
                Directory.CreateDirectory(@_feeddir);
            }

            //check file does not already exist if it does Create new file name
            if (File.Exists(@_fullfilename))
            {
                File.Delete(@_fullfilename);
            }


            using (FileStream fs = File.Open(@_fullfilename, FileMode.CreateNew))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;
                jw.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;

                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, CurrentNews);
                savedSuccessfully = true;
            }

            return savedSuccessfully;
        }

        
        #endregion public methods

    }
}
