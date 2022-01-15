﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

using AsyncDemoLabExercise.Model.Contracts;

namespace AsyncDemoLabExercise.Model
{
    public class Chronometer : IChronometer
    {
        private Stopwatch stopWatch;

        private List<string> laps;

        public Chronometer()
        {
            this.stopWatch = new Stopwatch();
            this.laps = new List<string>();
        }

        public string GetTime => this.stopWatch.Elapsed.ToString(@"mm\:ss\.ffff");

        public List<string> Laps => this.laps;

        public void Start()
        {
            this.stopWatch.Start();
        }

        public void Stop()
        {
            this.stopWatch.Stop();
        }

        public string Lap()
        {
            string result = GetTime;
            this.laps.Add(result);

            return result;
        }

        public void Reset()
        {
            this.stopWatch.Reset();
            this.laps.Clear();
        }
    }
}