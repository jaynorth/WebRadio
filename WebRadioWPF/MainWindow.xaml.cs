using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace WebRadioWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();


        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            //Player.Source = new Uri("http://wgltradio.ilstu.edu:8000/wgltjazz.mp3", UriKind.RelativeOrAbsolute);
            Player.Play();
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            Player.Stop();
        }

        private void ReadXML_Click(object sender, RoutedEventArgs e)
        {
            SimpleReader();
        }

        private void SimpleReader()
        {
            StringBuilder sb = new StringBuilder(512);
            XDocument xd;
            XElement node;

            xd = XDocument.Load(@"D:/Visual Studio Projects/Projects/WebRadioWPF/WebRadioWPF/XML/RadioStations.xml");

            node = xd.XPathSelectElement("/stations/station");

            sb.AppendFormat("name={0}", node.Element("name").Value);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("URL = {0}", node.Element("url").Value);

            string src = node.Element("url").Value;
            sb.Append(Environment.NewLine);

            //Player.Source = new Uri("http://wgltradio.ilstu.edu:8000/wgltjazz.mp3", UriKind.RelativeOrAbsolute);
            Player.Source = new Uri(src, UriKind.RelativeOrAbsolute);
            txtResult.Text = sb.ToString();


        }
    }
}
