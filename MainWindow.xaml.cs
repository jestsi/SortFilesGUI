using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Drawing;

namespace SortFileGUI
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private const int minInMilSec = 60000;
        private Thread thread = null;

        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = this.Height;
            this.MaxWidth = this.Width;

            SortAPI.BaseDirectory = "C:/Users/" + Environment.UserName + "/Downloads";


            DirectoryInSort.Text = "C:/Users/" + Environment.UserName + "/Downloads";

            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("Main.ico");
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object obj, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
                thread.Join();
            }
                
            this.Close();
        }

        private void CloseButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaimCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void SelectDirectory_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = false;

            string newPath = "";

            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var _char in folderBrowser.SelectedPath.ToCharArray())
                {
                    if (_char == '\\') newPath += '/';
                    else newPath += _char;
                }

                DirectoryInSort.Text = newPath;
                SortAPI.BaseDirectory = newPath;
            } else
            {
                DirectoryInSort.Text = "C:/Users/" + Environment.UserName + "/Downloads";
            }

        }

        private void DirectoryInSort_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DirectoryInSort.Text = "";
            DirectoryInSort.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(186, 99, 218));
        }

        private void DirectoryInSort_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DirectoryInSort.Text = "C:/Users/" + Environment.UserName + "/Downloads";
            DirectoryInSort.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(156, 69, 178));
        }

        private void DirectoryInSort_Copy_TextChanged(object sender, TextChangedEventArgs e){}

        private void StartUpdateOfTimer_Click(object sender, RoutedEventArgs e)
        {
            SortAPI.BaseDirectory = DirectoryInSort.Text;
            ParameterizedThreadStart threadStart = new ParameterizedThreadStart(EveryTimeSort);
            thread = new Thread(threadStart);
            thread.Start(int.Parse(MinsForUpdate.Text));

            StopUpdateOfTimer.Visibility = Visibility.Visible;
            StartUpdateOfTimer.Visibility = Visibility.Hidden;
        }

        private void StopUpdateOfTimer_Click(object sender, RoutedEventArgs e)
        {
            StartUpdateOfTimer.Visibility = Visibility.Visible;
            StopUpdateOfTimer.Visibility = Visibility.Hidden;
            thread.Abort();
            thread.Join();
        }

        private void EveryTimeSort(object time)
        {
            while (true)
            {
                if (SortAPI.BaseDirectory != null &&
                    SortAPI.BaseDirectory.Length > 5 &&
                    SortAPI.BaseDirectory != "Enter directory for sort")
                    SortAPI.ReadAllSorts();
                try
                {
                    Thread.Sleep((int)time * minInMilSec);
                }
                catch (Exception e)
                {
                    throw e;
                }
                
            }
            
        }

        private void StartSortButton_Click(object sender, RoutedEventArgs e)
        {
            SortAPI.BaseDirectory = DirectoryInSort.Text;
            SortAPI.ReadAllSorts();

        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

      
    }
}
