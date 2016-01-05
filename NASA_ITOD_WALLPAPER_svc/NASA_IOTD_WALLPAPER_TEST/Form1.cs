using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.ServiceModel.Syndication;
using System.Xml;
using System.IO;
using System.Runtime.InteropServices;




namespace NASA_IOTD_WALLPAPER_TEST
{

    public partial class Form1 : Form
    {
        private string NASA_RSS_URL = "https://www.nasa.gov/rss/dyn/lg_image_of_the_day.rss";

        

        public Form1()
        {
            InitializeComponent();
        }

        private void CacheImageButton_Click(object sender, EventArgs e)
        {
            RSSImageCacher cacher = new RSSImageCacher("NASA_IOTD");
            Wallpaper wallpaper = new Wallpaper();

            string filepath = cacher.CacheFirstImageFromRSS(NASA_RSS_URL);
            if (filepath.Length != 0)
            {
                wallpaper.Set(filepath);
            }
            
        }


    }
}
