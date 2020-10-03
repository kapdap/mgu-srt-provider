using SRTPluginBase;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace SRTPluginProviderMGU
{
    public class SRTPluginProviderMGU : IPluginProvider
    {
        private GameMemoryMGUScanner _memoryScanner;
        private Stopwatch _stopwatch;
        private IPluginHostDelegates _hostDelegates;
        public IPluginInfo Info => new PluginInfo();

        public bool GameRunning
        {
            get
            {
                if (_memoryScanner != null && !_memoryScanner.ProcessRunning)
                    _memoryScanner.Initialize(GetProcess());

                return _memoryScanner != null && _memoryScanner.ProcessRunning;
            }
        }

        public int Startup(IPluginHostDelegates hostDelegates)
        {
            _hostDelegates = hostDelegates;
            _memoryScanner = new GameMemoryMGUScanner(GetProcess());
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            return 0;
        }

        public int Shutdown()
        {
            _memoryScanner?.Dispose();
            _memoryScanner = null;
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
                    _memoryScanner.Update();
                    _stopwatch.Restart();
                }

                return _memoryScanner.Refresh();
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