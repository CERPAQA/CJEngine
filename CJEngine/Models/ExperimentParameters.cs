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
        //TODO: need to add in new parameter for next and hidden buttons, perform migrations and other necessary steps
    }
}
