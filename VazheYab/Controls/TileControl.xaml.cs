using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace VazheYab
{
    public partial class TileControl : ButtonBase
    {
        public static readonly DependencyProperty TitleProperty;
        public static readonly DependencyProperty DescriptionProperty;

        static TileControl()
        {
            TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(TileControl), new PropertyMetadata(""));
            DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(TileControl), new PropertyMetadata(""));
        }


        public TileControl()
        {
            InitializeComponent();

            Storyboard animation = Resources["TileAnimation"] as Storyboard;
            animation.Begin();
        }

        public string Title
        {
            get { return GetValue(TitleProperty).ToString(); }
            set { SetValue(TitleProperty, value); }
        }

        public string Desciption
        {
            get { return GetValue(DescriptionProperty).ToString(); }
            set { SetValue(DescriptionProperty, value); }
        }
    }
}
