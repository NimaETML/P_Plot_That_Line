﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plot_That_Line_Nima_Zarrabi
{
    internal class Crypto
    {
        public int Id;
        public string Name;
        public List<DateTime> Date;
        public List<float> Open;
        public List<float> High;
        public List<float> Low;
        public List<float> Close;
        public List<float> Volume;
        public string Currency;



        internal Crypto(int givenId, string givenName, List<DateTime> givenDateTime, List<float> givenOpen, List<float> givenHigh, List<float> givenLow, List<float> givenClose, List<float> givenVolume, string givenCurrency)
        {
            Id = givenId;
            Name = givenName;
            Date = givenDateTime.ToList();
            Open = givenOpen.ToList();
            High = givenHigh.ToList();
            Low = givenLow.ToList();
            Close = givenClose.ToList();
            Volume = givenVolume.ToList();
            Currency = givenCurrency;
        }
        internal void AddValuesToCrypto(int id)
        {

        }
    }
}