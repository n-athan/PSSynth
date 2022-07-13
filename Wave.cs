using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Media;
using System.IO;
using System.Text.RegularExpressions;

namespace PSSynth
{
    public class Wave
    {
        private static int SAMPLE_RATE = PSSynthOption.SAMPLE_RATE;
        private static short BITS_PER_SAMPLE = PSSynthOption.BITS_PER_SAMPLE;
        // private static double FREQ_INTERVAL = Math.Pow(2, (1 / 12)); //return 1... very strange.
        private static double FREQ_INTERVAL = 1.0594630943593;

        public double frequency { get; set; }
        public string waveForm { get; set; }
        public short[] sample { get; set; }
        public int length { get; set; }
        public string pitch { get; set; }
        public int octave { get; set; }


        //Constructors
        public Wave(double Frequency, string WaveForm, int Length)
        {
            frequency = Frequency;
            waveForm = WaveForm;
            length = Length;
        }

        public Wave(string Note, string WaveForm, int Length)
        {
            //note like As4, C2, Gb6 
            waveForm = WaveForm;
            length = Length;

            //calculate frequency
            frequency = PSSynthOption.TUNING;

            //multiply/divide by FREQ_INTERVAL #distance times for pitch difference to A. 
            pitch = Regex.Match(Note, @"[A-Gsf]+").Value;

            switch (pitch)
            {
                case "C":
                    frequency = frequency / Math.Pow(FREQ_INTERVAL, 9);
                    break;
                case "Cs":
                    frequency = frequency / Math.Pow(FREQ_INTERVAL, 8);
                    break;
                case "Df":
                    frequency = frequency / Math.Pow(FREQ_INTERVAL, 8);
                    break;
                case "D":
                    frequency = frequency / Math.Pow(FREQ_INTERVAL, 7);
                    break;
                case "Ds":
                    frequency = frequency / Math.Pow(FREQ_INTERVAL, 6);
                    break;
                case "Ef":
                    frequency = frequency / Math.Pow(FREQ_INTERVAL, 6);
                    break;
                case "E":
                    frequency = frequency / Math.Pow(FREQ_INTERVAL, 5);
                    break;
                case "F":
                    frequency = frequency / Math.Pow(FREQ_INTERVAL, 4);
                    break;
                case "Fs":
                    frequency = frequency / Math.Pow(FREQ_INTERVAL, 3);
                    break;
                case "Gf":
                    frequency = frequency / Math.Pow(FREQ_INTERVAL, 3);
                    break;
                case "G":
                    frequency = frequency / Math.Pow(FREQ_INTERVAL, 2);
                    break;
                case "Gs":
                    frequency = frequency / Math.Pow(FREQ_INTERVAL, 1);
                    break;
                case "Af":
                    frequency = frequency / Math.Pow(FREQ_INTERVAL, 1);
                    break;
                case "A":
                    //do nothing
                    break;
                case "As":
                    frequency = frequency * Math.Pow(FREQ_INTERVAL, 1);
                    break;
                case "Bf":
                    frequency = frequency * Math.Pow(FREQ_INTERVAL, 1);
                    break;
                case "B":
                    frequency = frequency * Math.Pow(FREQ_INTERVAL, 2);
                    break;
            }
            //multiply/divide by 2 for every whole octave difference to 4
            octave = Int32.Parse(Regex.Match(Note, @"\d+").Value);
            if (octave < 4)
            {
                frequency = frequency / Math.Pow(2, 4 - octave);
            }
            else if (octave > 4)
            {
                frequency = frequency * Math.Pow(2, octave - 4);
            }

        }
    }
}