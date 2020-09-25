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

        internal int _frameCount;
        public int FrameCount
        {
            get => _frameCount;
            set => SetField(ref _frameCount, value, "FrameCount", "Calculated", "TimeSpan", "FormattedString");
        }

        public int Calculated
            => (_frameCount >= 20000 ? _frameCount - 20000 : _frameCount) / 30;

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