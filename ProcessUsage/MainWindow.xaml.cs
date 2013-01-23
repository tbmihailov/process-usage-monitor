using ProcessUsage.Models;
using ProcessUsage.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessUsage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _processWatcher = new ProcessWatcher(1000, OnUserWorkingProcessChanged);
            dataGridProcessLog.ItemsSource = _processUsageLog;
        }

        private ProcessUsageInfo _currentProcessUsage;
        private ObservableCollection<ProcessUsageInfo> _processUsageLog = new ObservableCollection<ProcessUsageInfo>();

        ProcessWatcher _processWatcher;

        int _maxExecutionsCount = 5;
        int _executionsCount = 0;

        void OnUserWorkingProcessChanged(Process oldProcess, Process newProcess)
        {
            //_executionsCount++;
            //if (_executionsCount >= _maxExecutionsCount)
            //{
            //    _processWatcher.Stop();
            //}

            string newProcessName = newProcess.ProcessName;
            DateTime newProcessUsageFrom = DateTime.Now;

            if (_currentProcessUsage == null)
            {
                _currentProcessUsage = new ProcessUsageInfo()
                                {
                                    Name = newProcessName,
                                    From = newProcessUsageFrom
                                };
            }
            else
            {
                if (_currentProcessUsage.Name != newProcessName)
                {
                    //set last process usage end
                    _currentProcessUsage.To = newProcessUsageFrom;

                    //new process usage started
                    ProcessUsageInfo newProcessUsageInfo = new ProcessUsageInfo()
                    {
                        Name = newProcessName,
                        From = newProcessUsageFrom
                    };
                    _currentProcessUsage = newProcessUsageInfo;

                    Dispatcher.BeginInvoke(
                        new Action(
                            () =>
                            {
                                //var last    _processUsageLog.FirstOrDefault()
                                _processUsageLog.Insert(0, _currentProcessUsage);
                            })
                    );
                }
            }

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!_processWatcher.IsRunning)
            {
                _processWatcher.Start();
                btnStart.Content = "Stop";
            }
            else
            {
                _processWatcher.Stop();
                btnStart.Content = "Start";
            }
        }
    }
}
