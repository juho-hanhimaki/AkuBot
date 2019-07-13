using System;
using Newtonsoft.Json.Linq;

namespace AkuBot
{
    public class Hessu
    {
        public Hessu(string name, string json)
        {
            dynamic o = JObject.Parse(json);

            try
            {
                Name = name;
                Rating = Math.Round(o.rating.Value, 2);
                RWS = Math.Round(o.rws.Value / o.rounds.Value, 2);
                KD = Math.Round((double)o.kills.Value / o.deaths.Value, 2);
                ADR = Math.Round((double)o.damage.Value / o.rounds.Value, 1);
            }
            catch (Exception)
            {

            }
        }

        public string Name { get; set; }

        public double Rating { get; set; }

        public double RWS { get; set; }

        public double KD { get; set; }

        public double ADR { get; set; }

        public override string ToString()
        {
            return $"{Name.PadRight(8, ' ')}{Rating.ToString("0.00").PadLeft(6, ' ')}{RWS.ToString("0.00").PadLeft(7, ' ')}{KD.ToString("0.00").PadLeft(6, ' ')}{ADR.ToString("0.0").PadLeft(7, ' ')}";
        }
    }
}