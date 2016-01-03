using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Drawing;
using System.IO;
using System.Xml;
using System.ServiceModel.Syndication;


namespace NASA_ITOD_WALLPAPER_svc
{
    public partial class Service1 : ServiceBase
    {
        private string cacheFolder = "NASA_IOTD";
        private string NASA_RSS_URL = "https://www.nasa.gov/rss/dyn/lg_image_of_the_day.rss";
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            GetRSSFeed(NASA_RSS_URL);
        }

        protected override void OnStop()
        {
        }

        private void GetRSSFeed(string URL)
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
                        Uri imageHTTPURL = link.GetAbsoluteUri();
                        CacheImage(imageHTTPURL.AbsolutePath);
                    }
                }
            }
        }

        private void CacheImage( string imageHTTPURL )
        {
            //get the output filename
            Uri imageURI = new Uri(imageHTTPURL);
            string outputFileName = Path.GetFileName(imageURI.AbsolutePath);
            //construct the output filepath
            string cacheFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            cacheFilePath = cacheFilePath + cacheFolder + "\\" + outputFileName;

            WebClient client = new WebClient();
            
            //download the file to the calculated cache );
            client.DownloadFile(imageHTTPURL, cacheFilePath);
                           
        }

        private void SetImageAsWallPaper()
        {

        }
    }
}
