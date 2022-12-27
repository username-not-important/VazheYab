using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework.Input.Touch;
using VazheYab.Logic;

namespace VazheYab.Controls.GameControls
{
    public delegate void WordSubmittedEventHandler(object sender, string word);
    public partial class BoardView : UserControl
    {
        protected string[,] _Letters;

        
        public string SelectedWord
        {
            get
            {
                string word = "";
                foreach (LetterView view in ViewsSelected)
                {
                    word += view.Text;
                }

                return word;
            }
        }
        public BoardView()
        {
            InitializeComponent();
        }

        public void Populate(string[,] letters, WordLanguage language)
        {
            MainGrid.Children.Clear();
            _Letters = letters;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    LetterView l = new LetterView((i + j)*50, language);
                    
                    l.Text = letters[i, j];
                    l.Name = i + "," + j;

                    l.HorizontalAlignment = HorizontalAlignment.Left;
                    l.VerticalAlignment = VerticalAlignment.Top;

                    l.MouseLeftButtonDown += View_MouseLeftButtonDown;
                    l.MouseEnter += l_MouseEnter;

                    MainGrid.Children.Add(l);
                    l.SetValue(Grid.RowProperty, i);
                    l.SetValue(Grid.ColumnProperty, j);
                }
            }
        }

        List<LetterView> ViewsSelected = new List<LetterView>();
        private void l_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LetterView view = sender as LetterView;

            if (!view.IsOnHold)
            {
                view.IsOnHold = true;

                ViewsSelected.Add(view);
                OnSelectedWordChanged(new EventArgs());
            }
            else
            {
                int index = ViewsSelected.FindIndex(v => v == view);

                if (index < ViewsSelected.Count - 3)
                    return;

                try
                {
                    for (int i = index+1; i < ViewsSelected.Count; )
                    {
                        ViewsSelected[i].IsOnHold = false;
                        ViewsSelected.RemoveAt(i);
                    }

                    OnSelectedWordChanged(new EventArgs());
                }
                catch
                {

                }
            }
        }

        private void View_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LetterView view = sender as LetterView;

            view.IsOnHold = true;
        }

        private void Board_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            foreach (LetterView view in MainGrid.Children)
            {
                view.IsOnHold = false;
            }

            string word = SelectedWord;

            ViewsSelected.Clear();

            if (word.Length < 3)
                OnSelectedWordChanged(new EventArgs());
            else
                OnWordSubmitted(word);
        }

        public event EventHandler SelectedWordChanged;
        public event WordSubmittedEventHandler WordSubmitted;

        protected virtual void OnSelectedWordChanged(EventArgs e)
        {
            if (SelectedWordChanged != null)
                SelectedWordChanged(this, e);
        }
        protected virtual void OnWordSubmitted(string word)
        {
            if (WordSubmitted != null)
                WordSubmitted(this, word);
        }

    }
}
