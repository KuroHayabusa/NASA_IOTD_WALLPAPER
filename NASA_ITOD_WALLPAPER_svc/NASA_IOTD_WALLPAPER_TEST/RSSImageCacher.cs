using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.ServiceModel.Syndication;
using System.Xml;



namespace NASA_IOTD_WALLPAPER_TEST
{
    class RSSImageCacher
    {

        public string cacheFolder { get; set; };

        public RSSImageCacher()
        {

        }

        public ~RSSImageCacher()
        {

        }

        public void GetRSSFeed(string URL)
        {
            XmlReader rssReader = XmlReader.Create(URL);

            SyndicationFeed feed = SyndicationFeed.Load(rssReader);

            //read item 0 (should be the most recent item)
            foreach (SyndicationItem item in feed.Items)
            {
                foreach (SyndicationLink link in item.Links)
                {
                    if (link.RelationshipType == "enclosure")
                    {
                        Uri imageURL = link.GetAbsoluteUri();
                        CacheImageFromWeb(imageURL);
                    }
                }
            }
        }

        private void CacheImageFromWeb(Uri imageURL)
        {
            //get the output filename
            string outputFileName = Path.GetFileName(imageURL.AbsolutePath);
            //construct the output filepath
            string cacheFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            cacheFilePath = cacheFilePath + "\\" + cacheFolder;
            if (!Directory.Exists(cacheFilePath))
            {
                Directory.CreateDirectory(cacheFilePath);
            }

            cacheFilePath = cacheFilePath + "\\" + outputFileName;

            //check to see if we already have this file or not
            if (!File.Exists(cacheFilePath))
            {
                WebClient client = new WebClient();
                //download the file to the specified cache folder
                client.DownloadFile(imageURL, cacheFilePath);
            }
        }

        private void SetWallpaper(string wallpaperFilePath)
        {

            if (Path.GetExtension(wallpaperFilePath) != ".bmp")
            {
                //if our source image is not a bitmap, load our file, convert it, then save it out
                Image wallpaper = Image.FromFile(wallpaperFilePath);
                Bitmap wallpaperBMP = new Bitmap(wallpaper);
                //save the file, note that the input variable is converted here to have a .bmp extension
                wallpaperBMP.Save(Path.ChangeExtension(wallpaperFilePath, ".bmp"));
            }

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, wallpaperFilePath,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
    }
}
