using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Media;
using System.IO;

namespace PSSynth
{
    public static class PSSynthOption
    {
        public static int SAMPLE_RATE = 44100; // TODO change to setting.
        public static short BITS_PER_SAMPLE = 16; // TODO change to setting.
        public static int TEMPO = 120; // TODO change to setting.
        public static double TUNING = 440; // TODO change to setting.


        static public void GetOption()
        {

        }

        static public void SetOption(int sampleRate = 0, short bitsPerSample = 0, int tempo = 0)
        {
            if (sampleRate != 0)
            {
                SAMPLE_RATE = sampleRate;
            }
            if (bitsPerSample != 0)
            {
                BITS_PER_SAMPLE = bitsPerSample;
            }
            if (tempo != 0)
            {
                TEMPO = tempo;
            }
        }
    }


    [Cmdlet(VerbsCommon.Set, "PSSynthOption")]
    [OutputType(typeof(PSSynthOption))]
    [Alias("Set-Option")]
    public class SetOption : PSCmdlet
    {
        [Parameter(
            Mandatory = false,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public int sampleRate { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 1,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public short bitsPerSample { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 2,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public int tempo { get; set; }

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("Begin!");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            PSSynthOption.SetOption(sampleRate, bitsPerSample, tempo);
            // WriteVerbose($"Samplerate: {PSSynthOption.SAMPLE_RATE}, bits per sample:  {PSSynthOption.BITS_PER_SAMPLE}");
            PSObject options = new PSObject();
            options.Members.Add(new PSNoteProperty("Sample Rate", PSSynthOption.SAMPLE_RATE));
            options.Members.Add(new PSNoteProperty("Bits per Sample", PSSynthOption.BITS_PER_SAMPLE));
            options.Members.Add(new PSNoteProperty("Tempo (bpm)", PSSynthOption.TEMPO));
            this.WriteObject(options);
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        // protected override void EndProcessing() { WriteVerbose("End!"); }
    }

    [Cmdlet(VerbsCommon.Get, "PSSynthOption")]
    [OutputType(typeof(PSSynthOption))]
    [Alias("Get-Option")]
    public class GetOption : PSCmdlet
    {
        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("Begin!");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            PSObject options = new PSObject();
            options.Members.Add(new PSNoteProperty("Sample Rate", PSSynthOption.SAMPLE_RATE));
            options.Members.Add(new PSNoteProperty("Bits per Sample", PSSynthOption.BITS_PER_SAMPLE));            
            options.Members.Add(new PSNoteProperty("Tempo (bpm)", PSSynthOption.TEMPO));
            this.WriteObject(options);
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        // protected override void EndProcessing() { WriteVerbose("End!"); }
    }

}