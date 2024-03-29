﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessUsage.Models
{
    public class ProcessUsageInfo : INotifyPropertyChanged
    {
        private string _machineName;
        public string MachineName
        {
            get
            {
                return _machineName;
            }

            set
            {
                if (_machineName == value)
                {
                    return;
                }
                _machineName = value;
                RaisePropertyChanged("MachineName");
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name == value)
                {
                    return;
                }
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                if (_title == value)
                {
                    return;
                }
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        private DateTime _from;
        public DateTime From
        {
            get
            {
                return _from;
            }

            set
            {
                if (_from == value)
                {
                    return;
                }
                _from = value;
                RaisePropertyChanged("From");
            }
        }
        private DateTime? _to;
        public DateTime? To
        {
            get
            {
                return _to;
            }

            set
            {
                if (_to == value)
                {
                    return;
                }
                _to = value;
                RaisePropertyChanged("To");
                RaisePropertyChanged("Interval");
            }
        }

        public TimeSpan? Interval { 
            get 
            {
                if (To != null)
                {
                    return To.Value - From;
                }
                else
                {
                    return null;
                }

            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}
