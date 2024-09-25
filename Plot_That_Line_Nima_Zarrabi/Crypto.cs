using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plot_That_Line_Nima_Zarrabi
{
    internal class Crypto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DateTime> Date { get; set; }
        public List<float> Open { get; set; }
        public List<float> High { get; set; }
        public List<float> Low { get; set; }
        public List<float> Close { get; set; }
        public List<float> Volume { get; set; }
        public string Currency { get; set; }



        internal Crypto(int givenId, string givenName, List<DateTime> givenDateTime, List<float> givenOpen, List<float> givenHigh, List<float> givenLow, List<float> givenClose, List<float> givenVolume, string givenCurrency)
        {
            this.Id = givenId;
            this.Name = givenName;
            this.Date = givenDateTime;
            this.Open = givenOpen;
            this.High = givenHigh;
            this.Low = givenLow;
            this.Close = givenClose;
            this.Volume = givenVolume;
            this.Currency = givenCurrency;
        }
        internal void AddValuesToCrypto(int id)
        {

        }
    }
}
