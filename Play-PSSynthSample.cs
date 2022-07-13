using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Media;
using System.IO;

namespace PSSynth
{
    [Cmdlet(VerbsLifecycle.Start, "PSSynthSample")]
    [Alias("Play-Sample", "Play-PSSynthSample")]
    public class PlaySample : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public Sample sample { get; set; }

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose($"Starting Sample {sample}");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            Sample.PlaySample(sample);
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        // protected override void EndProcessing() { WriteVerbose("End!"); }
    }

    [Cmdlet(VerbsLifecycle.Stop, "PSSynthSample")]
    [Alias("Stop-Sample")]
    public class StopSample : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public Sample sample { get; set; }

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose($"Stopping playback of Sample {sample}");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            Sample.StopSample(sample);
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        // protected override void EndProcessing() { WriteVerbose("End!"); }
    }

}