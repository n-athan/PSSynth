using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Media;
using System.IO;

namespace PSSynth
{
    [Cmdlet(VerbsCommon.New, "PSSynthSample")]
    [OutputType(typeof(Wave))]
    [Alias("New-Sample")]
    public class NewSample : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public Wave[] waves { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 1,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public SwitchParameter Loop { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 2,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public float weigth { get; set; }

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("Begin!");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            Sample sample = new Sample(waves, Loop, weigth);
            WriteObject(sample);
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        // protected override void EndProcessing() { WriteVerbose("End!"); }
    }
}