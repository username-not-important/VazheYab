using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using VazheYab.Controls.GameControls;
using VazheYab.Logic;

namespace VazheYab
{
    public partial class FlipBoardView : UserControl
    {
        public string SelectedWord
        {
            get
            {
                if (EnglishBoard.Opacity == 1.0)
                    return EnglishBoard.SelectedWord;
                else
                    return PersianBoard.SelectedWord;
            }
        }

        private Storyboard goEngStoryboard;
        private Storyboard goPerStoryboard;

        public FlipBoardView()
        {
            InitializeComponent();

            PersianBoard.SelectedWordChanged += _SelectedWordChanged;
            PersianBoard.WordSubmitted += _WordSubmitted;

            EnglishBoard.SelectedWordChanged += _SelectedWordChanged;
            EnglishBoard.WordSubmitted += _WordSubmitted;

            goEngStoryboard = Resources["GoEngStoryboard"] as Storyboard;
            goPerStoryboard = Resources["GoPerStoryboard"] as Storyboard;
        }

        public void Populate(string[,] persianLetters,string[,] englishLetters)
        {
            EnglishBoard.Populate(englishLetters, WordLanguage.English);
            PersianBoard.Populate(persianLetters, WordLanguage.Persian);
        }

        public void Flip(WordLanguage newLanguage)
        {
            if (newLanguage == WordLanguage.English)
                goEngStoryboard.Begin();
            else
                goPerStoryboard.Begin();
        }

        void _WordSubmitted(object sender, string word)
        {
            OnWordSubmitted(word);
        }

        void _SelectedWordChanged(object sender, EventArgs e)
        {
            OnSelectedWordChanged(e);
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
