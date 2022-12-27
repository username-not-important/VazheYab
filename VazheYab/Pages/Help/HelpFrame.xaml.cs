using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace VazheYab.Pages.Help
{
    public partial class HelpFrame : PhoneApplicationPage
    {
        public HelpFrame()
        {
            InitializeComponent();

            Storyboard startupStoryboard = Resources["StartupStoryboard"] as Storyboard;
            startupStoryboard.Begin();

        }

        private void Buttons_Click(object sender, RoutedEventArgs e)
        {
            Storyboard goHelp = Resources["GoHelp"] as Storyboard;
            goHelp.Begin();

            int id = Int32.Parse((sender as Button).Name.Substring(1));

            var resource = Application.GetResourceStream(new Uri(@"VazheYab;component/Pages/Help/help" + id + ".txt", UriKind.Relative));
            var reader = new StreamReader(resource.Stream, Encoding.Unicode);
            string str = reader.ReadToEnd();

            string[] helpContent = str.Replace("\r","").Split('#');
            string title = helpContent[0].Replace("\n","");
            string content = helpContent[1].Trim('\n',' ');

            _TextTitle.Text = title;
            _TextContent.Text = content;
        }

        private void _CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard closeHelp = Resources["CloseHelp"] as Storyboard;
            closeHelp.Begin();
        }

    }
}