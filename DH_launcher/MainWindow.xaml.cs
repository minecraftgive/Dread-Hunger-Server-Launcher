using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace DH_launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string dhs_dir = "";
        string map = "";
        string server_port = "";
        private void Window_Initialized(object sender, EventArgs e)
        {
            Init();

        }

        private void Init()
        {

            //Map List
            Map_Selector.Items.Add("峰顶 The summit (Departure_Persistent)");
            Map_Selector.Items.Add("代价(广阔天地) The Expanse (Expanse_Persistent)");
            Map_Selector.Items.Add("入口 The Approach (Approach_Persistent)");


            //Scan Server

        }

        private void Launch_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process dhs = new System.Diagnostics.Process();
                {
                    dhs.StartInfo.FileName = dhs_dir_tb.Text;
                    dhs.StartInfo.UseShellExecute = false;
                    dhs.StartInfo.RedirectStandardInput = false;
                    dhs.StartInfo.RedirectStandardOutput = false;
                    dhs.StartInfo.RedirectStandardError = false;
                    dhs.StartInfo.CreateNoWindow = false;
                    dhs.StartInfo.Arguments = map + " " + "-port" + server_port_tb.Text + " " + "-log";
                }
                dhs.Start();
                //MessageBox.Show(dhs.ProcessName);
                //MessageBox.Show("ID:" + dhs.Id);
            }

            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.Message + "\r" + dhs_dir_tb.Text);
            }

            //p.StartInfo.FileName = AppConst.EngineFilePath;   //exe程序文件地址
            //p.StartInfo.UseShellExecute = false;
            //p.StartInfo.RedirectStandardInput = false;
            //p.StartInfo.RedirectStandardOutput = false;
            //p.StartInfo.RedirectStandardError = false;
            //p.StartInfo.CreateNoWindow = true;                  //不弹出窗口，改为后台运行
            //p.StartInfo.Arguments = sb.ToString();

        }

        private void get_path_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Dir_dhs = new OpenFileDialog();
            {
                Dir_dhs.Multiselect = false;
                Dir_dhs.CheckFileExists = true;
                Dir_dhs.CheckPathExists = true;
                //Dir_dhs.DefaultExt = ".exe";
                Dir_dhs.Filter = "Dread Huner Server|*.exe";
            }
            Dir_dhs.ShowDialog();
            if (Dir_dhs.FileName != "")
            {
                dhs_dir_tb.Text = Dir_dhs.FileName;
                dhs_dir = dhs_dir_tb.Text;
            }
        }

        private void Map_Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Map_Selector.SelectedIndex)
            {
                case 0:
                    map = "Departure_Persistent";
                    break;
                case 1:
                    map = "Expanse_Persistent";
                    break;
                case 2:
                    map = "Approach_Persistent";
                    break;
            }
            //MessageBox.Show(map);
        }

        private void server_port_TextChanged(object sender, TextChangedEventArgs e)
        {
            server_port = server_port_tb.Text;
        }

        private void kill_Click(object sender, RoutedEventArgs e)
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.ProcessName.Contains("DreadHungerServer-Win64-Shipping"))
                {
                    //Console.WriteLine($"ProcessName = ({process.ProcessName}), Id = {process.Id}");
                    //MessageBox.Show(($"ProcessName = ({process.ProcessName}), Id = {process.Id}"));
                    process.Kill();
                }
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
