using System;o
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


namespace NASA_ITOD_WALLPAPER_svc
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }

        private void GetRSSFeed(string URL)
        {

        }

        private void CacheImage( string imageURL, string imageFileName, string cacheFolderName )
        {

            string cacheFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            cacheFilePath = cacheFilePath + cacheFolderName + "\\" + imageFileName;

            WebClient client = new WebClient();
            
            //download the file to the calculated cache );
            client.DownloadFile(imageURL, cacheFilePath);
                           
        }

        private void SetImageAsWallPaper()
        {

        }
    }
}
