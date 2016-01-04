using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Globalization;
using System.IO;

namespace NASA_IOTD_WALLPAPER_TEST
{
    class CustomXmlReader : XmlTextReader
    {
        private bool readingDate = false;
        const string CustomUtcDateTimeFormat = "ddd, dd MMM yyyy HH:mm z"; // Mon, 04 Jan 2016 10:57 EST

        public CustomXmlReader(Stream s) : base(s) { }

        public CustomXmlReader(string inputUri) : base(inputUri) { }

        public override void ReadStartElement()
        {
            if (string.Equals(base.NamespaceURI, string.Empty, StringComparison.InvariantCultureIgnoreCase) &&
                (string.Equals(base.LocalName, "lastBuildDate", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(base.LocalName, "pubDate", StringComparison.InvariantCultureIgnoreCase)))
            {
                readingDate = true;
            }
            base.ReadStartElement();
        }

        public override void ReadEndElement()
        {
            if (readingDate)
            {
                readingDate = false;
            }
            base.ReadEndElement();
        }

        public override string ReadString()
        {
            if (readingDate)
            {
                string dateString = base.ReadString();

                //catch for that one time they put '(All Day)' in the date field
                int index = dateString.IndexOf('(');
                if ( index != -1 )
                {
                    //default the time to midnight EST
                    dateString = dateString.Remove(index);
                    dateString = dateString + " 12:00 EST";
                }

                return dateString;
            }
            else
            {
                return base.ReadString();
            }
        }
    }
}
