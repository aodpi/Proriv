using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
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
            const int n = 11, m = 14;
            WordP wp = new WordP(_words);
            //wp.Test(new int[n, m]
            //{
            //    {1,1,1,1,1,1,1,0,0 },
            //    {1,0,0,0,0,1,0,0,0 },
            //    {1,0,0,0,1,1,1,1,1 },
            //    {1,1,1,0,0,0,1,0,0 },
            //    {0,0,1,0,1,1,1,1,1 },
            //    {0,1,1,1,0,0,1,0,0 },
            //    {0,1,0,0,1,1,1,0,1 },
            //    {1,1,1,1,0,0,0,0,1 },
            //    {0,1,0,1,1,1,0,0,0 },
            //    {0,0,0,0,0,0,0,0,0 },
            //}, n, m);


            wp.Test(new int[n, m]
            {
                { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
                { 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
                { 0, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 1 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            }, n, m);

            //wp.Test(new int[10, 10]
            //{
            //    {0,0,0,0,0,0,1,0,1,0 },
            //    {0,0,0,1,1,1,1,1,1,1 },
            //    {0,0,0,0,0,0,1,0,1,0 },
            //    {0,0,0,0,0,0,1,0,1,0 },
            //    {1,1,1,1,1,0,1,0,1,0 },
            //    {0,0,0,0,1,0,1,0,1,0 },
            //    {0,0,0,0,1,0,1,0,0,0 },
            //    {0,1,1,1,1,1,1,1,1,0 },
            //    {0,0,0,0,0,0,1,0,0,0 },
            //    {0,0,0,0,0,0,1,0,0,0 },
            //}, 10, 10);
            char[,] xtest = wp.FirstTest(n, m);
            RebusParent.HorizontalAlignment = HorizontalAlignment.Center;
            RebusParent.VerticalAlignment = VerticalAlignment.Center;
            for (int i = 0; i < n; i++)
            {
                RebusParent.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                for (int j = 0; j < m; j++)
                {
                    if (xtest[i,j]!='\0')
                    {
                        RebusParent.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                        TextBlock xx = new TextBlock();
                        xx.Text = xtest[i, j].ToString();
                        Border brd = new Border();
                        brd.BorderThickness = new Thickness(2, 2, 2, 2);
                        brd.BorderBrush = new SolidColorBrush(Colors.Black);
                        brd.Child = xx;
                        Grid.SetRow(brd, i);
                        Grid.SetColumn(brd, j);
                        xx.HorizontalAlignment = HorizontalAlignment.Center;
                        xx.VerticalAlignment = VerticalAlignment.Center;
                        xx.Margin = new Thickness(10);
                        Grid.SetRow(xx, i);
                        Grid.SetColumn(xx, j);
                        RebusParent.Children.Add(brd);
                    }
                }
            }
        }
       
    }
}
