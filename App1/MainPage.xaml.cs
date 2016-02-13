using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WordProcessor;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        IList<string> rz;
        IList<string> _words;
        int count = 0;
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            WordP wp = new WordP();
            wp.Test(new int[11, 14]
            {
                {   0,  0, -1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {   0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                {   0,  0, -1,  0,  0,  0,  0,  0, -1,  0,  0,  0,  0, -1 },
                {  -1, -1, -1, -1,  0,  0,  0, -1, -1, -1, -1,  0,  0, -1 },
                {   0,  0, -1,  0,  0,  0,  0,  0, -1,  0,  0,  0,  0, -1 },
                {   0,  0, -1,  0,  0,  0,  0,  0, -1,  0,  0,  0,  0, -1 },
                {   0,  0, -1,  0,  0,  0,  0,  0, -1,  0,  0,  0,  0, -1 },
                {   0,  0, -1,  0,  0,  0,  0,  0, -1,  0,  0,  0,  0, -1 },
                {   0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, -1 },
                {   0,  0, -1,  0,  0,  0,  0,  0, -1,  0,  0,  0,  0, -1 },
                {  -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
            }
            , 11, 14);
            //StorageFile sf = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///wordlist.txt", UriKind.Absolute));
            //_words = await FileIO.ReadLinesAsync(sf);
            //WordP wp = new WordP(_words);
            //string x = string.Empty;
            //rz = wp.FindWordForIntersect("aam", 1, 4, 1);
        }

        public void DrawWord(string word)
        {
            if (Rebus.Children.Count > 0)
            {
                for (int i = 0; i < Rebus.Children.Count; i++)
                {
                    if (Rebus.Children[i].GetType()==typeof(TextBlock))
                    {
                        Rebus.Children.RemoveAt(i);
                    }
                }
            }
            ClearAll();

            for (int i = 0; i < word.Length; i++)
            {
                TextBlock txt = new TextBlock();
                txt.Text = word[i].ToString();
                txt.HorizontalAlignment = HorizontalAlignment.Center;
                txt.VerticalAlignment = VerticalAlignment.Center;
                txt.Tag = "llt";
                Grid.SetRow(txt, 1);
                Grid.SetColumn(txt, i);
                Rebus.Children.Add(txt);
            }
            
            WordP wp = new WordP(_words);
            string x = wp.FindWordForIntersect(word, word.Length-1, 5, 1)[0];
            for (int i = 0; i < x.Length; i++)
            {
                TextBlock txt = new TextBlock();
                txt.Text = x[i].ToString();
                txt.HorizontalAlignment = HorizontalAlignment.Center;
                txt.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetRow(txt, i);
                txt.Tag = "llt";
                Grid.SetColumn(txt, 3);
                Rebus.Children.Add(txt);
            }
        }

        public void ClearAll()
        {
            Rebus.Children.Clear();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DrawWord(rz[count++]);
        }
    }
}
