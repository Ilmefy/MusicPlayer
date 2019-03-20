using MusicPlayer.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MusicPlayer.UI.Elements
{
    /// <summary>
    /// Logika interakcji dla klasy RadioTab.xaml
    /// </summary>
    public partial class RadioTab : UserControl
    {
        private bool SearchBarActive = false;
        public RadioTab()
        {
            InitializeComponent();
        }


        public void AddElement(Station station)
        {
                RadioElement radioElement = new RadioElement()
                {
                    StationName = station.Name,
                    StationGenre = RadioElement.Genre.Westfall | RadioElement.Genre.Pop,
                    StationDescription = station.Description[0],
                    Index = (UInt16)StationList.Children.Count,
                    Station = station,
                };
                StationList.Children.Add(radioElement);
            

        }
        public void RemoveElement(UInt16 index)
        {
            List<RadioElement> r = StationList.Children.OfType<RadioElement>().ToList();
           RadioElement re= r.Where(c => c.Index == index).First();
            int controlIndex = re.Index;
            StationList.Children.Remove(re);
            r = StationList.Children.OfType<RadioElement>().Where(c => c.Index > controlIndex).ToList();
            foreach(RadioElement _re in r)
            {
                ChangeElementIndex(_re);
            }
        }
        private void ChangeElementIndex(RadioElement element)
        { 
            element.Index -=1 ;
        }

        private void SearchButtonMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AnimateOnSearchBarActive();

        }
        private void AnimateOnSearchBarActive()
        {
            #region AnimateButton
            //ThicknessAnimation ta = new ThicknessAnimation();
            //ta.From = SearchButton.Margin;
            //double x = Grid_SearchBar.ActualWidth-SearchButton.ActualWidth;
            //System.Windows.Thickness thickness = new System.Windows.Thickness(x, 0, 0, 0);
            //ta.To = thickness;
            //ta.Duration = TimeSpan.FromSeconds(0.5f);
            //SearchButton.BeginAnimation(MarginProperty, ta);
            #endregion

            #region AnimateSearchBar
            if (SearchBar.ActualWidth == Grid_SearchBar.ActualWidth)
                return;
            DoubleAnimation da = new DoubleAnimation();
            da.From = SearchBar.ActualWidth;
            da.To = Grid_SearchBar.ActualWidth;
            da.Completed += Da_Completed;
            da.Duration = TimeSpan.FromSeconds(0.3f);
            SearchBar.BeginAnimation(WidthProperty, da);
          
            #endregion

        }

        private void Da_Completed(object sender, EventArgs e)
        {
            SearchTextBox.Visibility = System.Windows.Visibility.Visible;
            SearchTextBox.Focus();
        }

        private void SearchTextBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBox tbox = sender as TextBox;
            tbox.Text = "";
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = (sender as TextBox).Text;
            if (Source.Global.AppInitialized && text.Length>0)
            {
                ShowElementsByPhrase(text);
            }
            if(Source.Global.AppInitialized && text.Length==0)
            {
                ShowAllElements();
            }
        }
        private void ShowElementsByPhrase(string text)
        {
            Stopwatch sw = new Stopwatch();
            foreach (RadioElement re in StationList.Children.OfType<RadioElement>().Where(c => !c.NameContains(text) && !c.NameStartsWith(text)))
            {
                re.Visibility = System.Windows.Visibility.Collapsed;
            }
            foreach (RadioElement re in StationList.Children.OfType<RadioElement>().Where(c => c.NameContains(text) && c.NameStartsWith(text)))
            {
                re.Visibility = System.Windows.Visibility.Visible;
            }
        }
        private void ShowAllElements()
        {
            foreach(RadioElement re in StationList.Children.OfType<RadioElement>())
            {
                re.Visibility = System.Windows.Visibility.Visible;
            }

        }

    }
}
