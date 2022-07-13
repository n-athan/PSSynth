using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Media;
using System.IO;

namespace PSSynth
{

    [Cmdlet(VerbsCommon.New, "PSSynthWave")]
    [OutputType(typeof(Wave))]
    [Alias("New-Wave")]
    public class NewWave : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = "Frequency")]
        public double Frequency { get; set; } = 440;

        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = "Note")]
        [ValidatePattern("[A-G](s|f)?[\\d]")]
        public string Note { get; set; } = "A4";

        [Parameter(
            Position = 1,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        [ValidateSet("Sine", "Triangle", "Saw", "Square", "Rest")]
        [Alias("Shape")]
        public string WaveForm { get; set; } = "Sine";

        [Parameter(
            Position = 2,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        [Alias("Count")]
        public int Length { get; set; } = 1;

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("Begin!");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            if (this.Note != null)
            {
                WriteObject(new Wave(Note, WaveForm, Length));
            }
            else
            {
                WriteObject(new Wave(Frequency, WaveForm, Length));
            }
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        // protected override void EndProcessing() { WriteVerbose("End!"); }
    }
}