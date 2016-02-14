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

        IList<string> _words;
        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            StorageFile sf = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///wordlist.txt", UriKind.Absolute));
            _words = await FileIO.ReadLinesAsync(sf);
            WordP wp = new WordP(_words);
            //wp.Test(new int[11, 14]
            //{
            //    {   0,  0, -1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
            //    {   0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
            //    {   0,  0, -1,  0,  0,  0,  0,  0, -1,  0,  0,  0,  0, -1 },
            //    {  -1, -1, -1, -1,  0,  0,  0, -1, -1, -1, -1,  0,  0, -1 },
            //    {   0,  0, -1,  0,  0,  0,  0,  0, -1,  0,  0,  0,  0, -1 },
            //    {   0,  0, -1,  0,  0,  0,  0,  0, -1,  0,  0,  0,  0, -1 },
            //    {   0,  0, -1,  0,  0,  0,  0,  0, -1,  0,  0,  0,  0, -1 },
            //    {   0,  0, -1,  0,  0,  0,  0,  0, -1,  0,  0,  0,  0, -1 },
            //    {   0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, -1 },
            //    {   0,  0, -1,  0,  0,  0,  0,  0, -1,  0,  0,  0,  0, -1 },
            //    {  -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
            //}
            //, 11, 14);
            wp.Test(new int[9, 9]
            {
                {  -1, 0, -1, -1, 0, 0, 0, 0, 0 },
                {  -1, 0, -1,  0, 0, 0, 0, 0, 0 },
                {  -1, 0, -1, -1, 0, 0, 0, 0, 0 },
                {  -1, 0, -1,  0, 0, 0, 0, 0, 0 },
                {  -1, 0, -1, -1, 0, 0, 0, 0, 0 },
                {  -1, 0, -1,  0, 0, 0, 0, 0, 0 },
                {  -1, 0, -1, -1, 0, 0, 0, 0, 0 },
                {  -1, 0, -1,  0, 0, 0, 0, 0, 0 },
                {  -1, 0, -1, -1, 0, 0, 0, 0, 0 },
            }
            , 9, 9);
            char[,] xtest = wp.FirstTest(9, 9);
            string buffer = string.Empty;
            RebusParent.HorizontalAlignment = HorizontalAlignment.Center;
            RebusParent.VerticalAlignment = VerticalAlignment.Center;
            for (int i = 0; i < 9; i++)
            {
                RebusParent.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                for (int j = 0; j < 9; j++)
                {
                    RebusParent.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                    if (xtest[i,j]=='\0')
                    {
                        buffer += "* ";
                    }
                    TextBlock xx = new TextBlock();
                    if (xtest[i,j]=='\0')
                    {
                        xx.Text = "*";
                    }
                    else
                    {
                        xx.Text = xtest[i, j].ToString();
                    }
                    xx.HorizontalAlignment = HorizontalAlignment.Center;
                    xx.VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetRow(xx, i);
                    Grid.SetColumn(xx, j);
                    RebusParent.Children.Add(xx);
                }
                buffer += Environment.NewLine;
            }
        }
       
    }
}
