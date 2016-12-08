using HtmlAgilityPack;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TouristicWallet.utils;

namespace TouristicWallet.models
{
    //CURRENCY DATABASE MODEL
    [Table("Currency")]
    class Currency : INotifyPropertyChanged
    {

        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _initials;
        [NotNull, Unique, MaxLength(3)]
        public string Initials
        {
            get
            {
                return _initials;
            }
            set
            {
                this._initials = value;
                OnPropertyChanged(nameof(Initials));
            }
        }

        private string _name;
        [NotNull]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                this._name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private double _rateToEUR;

        public double RateToEUR
        {
            get
            {
                return _rateToEUR;
            }
            set
            {
                this._rateToEUR = value;
                OnPropertyChanged(nameof(RateToEUR));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}
