﻿namespace CJEngine.Models
{
    public class ExperimentParameters
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool ShowTitle { get; set; }
        public bool ShowTimer { get; set; }
        public bool AddComment { get; set; }
        public int Timer { get; set; }
        public bool TimeLine { get; set; }
        public int NumberOfPairings { get; set; }
        //TODO: change numpairings to maxJudgejudgmnets and then work on disabling an experiment after the judge has finished

    }
}
