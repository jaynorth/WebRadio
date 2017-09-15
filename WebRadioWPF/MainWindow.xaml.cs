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
using System.Windows.Threading;
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

        DispatcherTimer dt = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 1);
           
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            BufferProgress.Value = Player.BufferingProgress * 100;
            BufferProgress.UpdateLayout();
        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
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

         
            Player.Source = new Uri(src);
           
            txtBuffer.Text = Player.BufferingProgress.ToString();

            Player.BufferingStarted += Player_BufferingStarted;
      
            Player.BufferingEnded += Player_BufferingEnded;
            
            txtBuffer.Text = Player.BufferingProgress.ToString();


            txtResult.Text = sb.ToString();

        }

        private void Player_BufferingEnded(object sender, RoutedEventArgs e)
        {
            
            string s = "buffer ended";
            BufferProgress.Value = Player.BufferingProgress * 100;
            double v = Player.BufferingProgress * 100;
            txtProgress.Text = s + " " + v.ToString() ;
            dt.Stop();
            
        }

        private void Player_BufferingStarted(object sender, RoutedEventArgs e)
        {
            dt.Start();
            string s = "buffer started";
            txtProgress.Text = s;
        }

        private void BufferProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        
        }
    }
}
