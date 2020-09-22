using System;
using System.Diagnostics;
using System.Globalization;

namespace SRTPluginProviderMGU.Models
{
    [DebuggerDisplay("{_DebuggerDisplay,nq}")]
    public class TimeEntry : BaseNotifyModel
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string _DebuggerDisplay => 
            FormattedString;

        private const string TIMESPAN_STRING_FORMAT = @"hh\:mm\:ss";

        internal int _frameCounter;
        public int FrameCounter
        {
            get => _frameCounter;
            set => SetField(ref _frameCounter, value, "FrameCounter", "Calculated", "TimeSpan", "FormattedString");
        }

        public int Calculated
            => (_frameCounter >= 20000 ? _frameCounter - 20000 : _frameCounter) / 30;

        public TimeSpan TimeSpan
        {
            get
            {
                if (Calculated <= TimeSpan.MaxValue.Ticks)
                    return new TimeSpan(0, 0, Calculated);
                else
                    return new TimeSpan();
            }
        }

        public string FormattedString
            => TimeSpan.ToString(TIMESPAN_STRING_FORMAT, CultureInfo.InvariantCulture);
    }
}