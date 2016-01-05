using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Drawing;



namespace NASA_IOTD_WALLPAPER_TEST
{
    class RSSImageCacher
    {

        public string cacheFolder { get; set; }


        public RSSImageCacher( string cacheFolderIn )
        {
            cacheFolder = cacheFolderIn;
        }

        ~RSSImageCacher()
        {

        }

        //function to get only the first item in an IEnum
        static T First<T>(IEnumerable<T> items)
        {
            using (IEnumerator<T> iter = items.GetEnumerator())
            {
                iter.MoveNext();
                return iter.Current;
            }
        }

        public string CacheFirstImageFromRSS(string URL)
        {
            string result = "";
            XmlReader rssReader = new CustomXmlReader(URL);

            SyndicationFeed feed = SyndicationFeed.Load(rssReader);

            //read item 0 (should be the most recent item)
            SyndicationItem item = First<SyndicationItem>(feed.Items);
            foreach (SyndicationLink link in item.Links)
            {
                if (link.RelationshipType == "enclosure")
                {
                    Uri imageURL = link.GetAbsoluteUri();
                    result = CacheImageFromWeb(imageURL, true);
                    return result;
                }
              
            }
            return result;
        }

        private string CacheImageFromWeb(Uri imageURL, bool AsBitmap)
        {
            //get the output filename
            string outputFileName = Path.GetFileName(imageURL.AbsolutePath);
            //construct the output filepath
            string cacheFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + cacheFolder;  
           
            if (!Directory.Exists(cacheFolderPath))
            {
                Directory.CreateDirectory(cacheFolderPath);
            }

            string outputFilePath = cacheFolderPath + "\\" + outputFileName;

            //check to see if we already have this file or not
            if (!File.Exists(outputFilePath))
            {
                WebClient client = new WebClient();
                //download the file to the specified cache folder
                client.DownloadFile(imageURL, outputFilePath);
            }

            if (Path.GetExtension(outputFilePath) != ".bmp")
            {
                //if our source image is not a bitmap, load our file, convert it, then save it out
                Image srcImage = Image.FromFile(outputFilePath);

                Bitmap bmpImage = new Bitmap(srcImage);
                //save the file, note that the input variable is converted here to have a .bmp extension
                string bmpFilePath = Path.ChangeExtension(outputFilePath, ".bmp");
                bmpImage.Save(bmpFilePath);

                srcImage.Dispose();
                File.Delete(outputFilePath);

                return bmpFilePath;
            }
            else
            {
                return outputFilePath;
            }
        }       
    }
}
