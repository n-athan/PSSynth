using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Media;
using System.IO;

namespace PSSynth
{
    public class Sample
    {

        public int length;
        public short[] sample;
        public byte[] binarySample;
        public float weigth;
        public bool loop;
        private int SAMPLE_RATE;
        private short BITS_PER_SAMPLE;
        private SoundPlayer Player;

        public Sample(Wave[] waves, bool Loop = true, float Weight = 1)
        {
            loop = Loop;
            weigth = Weight;
            SAMPLE_RATE = PSSynthOption.SAMPLE_RATE;
            BITS_PER_SAMPLE = PSSynthOption.BITS_PER_SAMPLE;
            Player = new SoundPlayer();

            length = 0;
            int Samples_per_beat = (60 * this.SAMPLE_RATE) / PSSynthOption.TEMPO;
            foreach (Wave w in waves)
            {
                this.length += w.length * Samples_per_beat;
            }

            sample = new short[length];
            int offSet = 0;
            foreach (Wave w in waves)
            {
                NewSample(w, this.SAMPLE_RATE);
                Buffer.BlockCopy(w.sample, 0, this.sample, offSet, w.sample.Length);
                offSet += w.length * Samples_per_beat;
            }

            binarySample = new byte[this.length * sizeof(short)];
            Buffer.BlockCopy(this.sample, 0, this.binarySample, 0, this.length * sizeof(short));
        }

        //New-PSSynthSample
        static public void NewSample(Wave wave, int sampleRate)
        {
            int Samples_per_beat = (60 * sampleRate) / PSSynthOption.TEMPO;
            int numSamples = wave.length * Samples_per_beat;
            wave.sample = new short[numSamples];
            switch (wave.waveForm)
            {
                case "Sine":
                    for (int i = 0; i < numSamples; i++)
                    {
                        wave.sample[i] = Convert.ToInt16(short.MaxValue * Math.Sin(((Math.PI * 2 * wave.frequency) / sampleRate) * i));
                    };
                    break;
                case "Rest": //silence
                    // for (int i = 0; i < numSamples; i++)
                    // {
                    //     wave.sample[i] = 0;
                    // };
                    break;
                case "Square":
                    for (int i = 0; i < numSamples - 1; i++)
                    {
                        wave.sample[i] = Convert.ToInt16(short.MaxValue * Math.Sign(Math.Sin(((Math.PI * 2 * wave.frequency) / sampleRate) * i)));
                    }
                    break;
                case "Saw":
                    // Determine the number of samples per wavelength
                    int samplesPerWavelength = Convert.ToInt32(sampleRate / wave.frequency);

                    // Determine the amplitude step for consecutive samples
                    short ampStep = Convert.ToInt16((short.MaxValue * 2) / samplesPerWavelength);

                    // Temporary sample value, added to as we go through the loop
                    short tempSample = (short)-short.MaxValue;

                    // Total number of samples written so we know when to stop
                    int totalSamplesWritten = 0;

                    while (totalSamplesWritten < numSamples)
                    {
                        tempSample = (short)-short.MaxValue;

                        for (uint i = 0; i < samplesPerWavelength && totalSamplesWritten < numSamples; i++)
                        {
                            tempSample += ampStep;
                            wave.sample[totalSamplesWritten] = tempSample;

                            totalSamplesWritten++;
                        }
                    }
                    break;
                case "Triangle":
                    // Determine the number of samples per wavelength
                    int samplesPerWavelength2 = Convert.ToInt32(sampleRate / wave.frequency);

                    // Determine the amplitude step for consecutive samples
                    short ampStep2 = Convert.ToInt16((short.MaxValue * 2) / samplesPerWavelength2);

                    // Temporary sample value, added to as we go through the loop
                    short tempSample2 = (short)-short.MaxValue;

                    for (int i = 0; i < numSamples - 1; i++)
                    {
                        // Negate ampstep whenever it hits the amplitude boundary
                        if (Math.Abs(tempSample2) > short.MaxValue)
                            ampStep = (short)-ampStep2;

                        tempSample2 += ampStep2;
                        wave.sample[i] = tempSample2;
                    }
                    break;
                default:
                    // code block
                    break;
            }

            // wave.binarySample = new byte[sampleRate * sizeof(short)];
            // Buffer.BlockCopy(wave.sample, 0, wave.binarySample, 0, wave.sample.Length * sizeof(short));
        }

        //Play-PSSynthSample
        static public void PlaySample(Sample sample)
        {
            //TODO check if samplerate/options still like PSSynthOptions. Otherwise rebuild sample.
            using (MemoryStream memoryStream = new MemoryStream())
            using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
            { // build WAVE chunk: http://soundfile.sapp.org/doc/WaveFormat/
                int blockAlign = sample.BITS_PER_SAMPLE / 8;
                int subChunk2Size = sample.length/2 * blockAlign;

                // block 1 'RIFF' subchunk
                binaryWriter.Write(new[] { 'R', 'I', 'F', 'F' });
                binaryWriter.Write(36 + subChunk2Size);
                binaryWriter.Write(new[] { 'W', 'A', 'V', 'E' });

                //block 2 'fmt ' subchunk
                binaryWriter.Write(new[] { 'f', 'm', 't', ' ' });
                binaryWriter.Write(16);
                binaryWriter.Write((short)1);
                binaryWriter.Write((short)1);
                binaryWriter.Write(sample.SAMPLE_RATE);
                binaryWriter.Write(sample.SAMPLE_RATE * blockAlign); //byterate 
                binaryWriter.Write((short)blockAlign);
                binaryWriter.Write(sample.BITS_PER_SAMPLE);

                // block 3 data subchunk
                binaryWriter.Write(new[] { 'd', 'a', 't', 'a' });
                binaryWriter.Write(subChunk2Size); //== NumSamples * NumChannels * BitsPerSample/8
                binaryWriter.Write(sample.binarySample);
                memoryStream.Position = 0;
                sample.Player.Stream = memoryStream;
                if (sample.loop)
                {
                    sample.Player.PlayLooping();
                }
                else
                {
                    sample.Player.Play();
                }
                memoryStream.Close();
                memoryStream.Dispose();
            }
        }
        
        static public void StopSample(Sample sample) {
            sample.Player.Stop();
        }
    }
}