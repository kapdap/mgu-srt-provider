using SRTPluginBase;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace SRTPluginProviderMGU
{
    public class SRTPluginProviderMGU : IPluginProvider
    {
        private Process _process;
        private GameMemoryMGUScanner _memory;
        private Stopwatch _stopwatch;
        private IPluginHostDelegates _hostDelegates;
        public IPluginInfo Info => new PluginInfo();
        public bool GameRunning
        {
            get
            {
                if (_memory != null && !_memory.ProcessRunning)
                {
                    _process = GetProcess();
                    if (_process != null)
                        _memory.Initialize(_process); // Re-initialize and attempt to continue.
                }

                return _memory != null && _memory.ProcessRunning;
            }
        }

        public int Startup(IPluginHostDelegates hostDelegates)
        {
            _hostDelegates = hostDelegates;
            _process = GetProcess();
            _memory = new GameMemoryMGUScanner(_process);
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            return 0;
        }

        public int Shutdown()
        {
            _memory?.Dispose();
            _memory = null;
            _stopwatch?.Stop();
            _stopwatch = null;
            return 0;
        }

        public object PullData()
        {
            try
            {
                if (!GameRunning) // Not running? Bail out!
                    return null;

                if (_stopwatch.ElapsedMilliseconds >= 2000L)
                {
                    _stopwatch.Restart();
                }

                return _memory.Refresh();
            }
            catch (Win32Exception ex)
            {
                if ((ProcessMemory.Win32Error)ex.NativeErrorCode != ProcessMemory.Win32Error.ERROR_PARTIAL_COPY)
                    _hostDelegates.ExceptionMessage(ex);// Only show the error if its not ERROR_PARTIAL_COPY. ERROR_PARTIAL_COPY is typically an issue with reading as the program exits or reading right as the pointers are changing (i.e. switching back to main menu).
            }
            catch (Exception ex)
            {
                _hostDelegates.ExceptionMessage(ex);
            }

            return null;
        }

        private Process GetProcess() => Process.GetProcessesByName("martian gothic")?.FirstOrDefault();
    }
}