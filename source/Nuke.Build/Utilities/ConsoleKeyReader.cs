// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/GreemDev/NUKE/blob/master/LICENSE

using System;
using System.Threading;

namespace Nuke.Build.Utilities;

public class ConsoleKeyReader : IDisposable
{
    private Thread _thread;
    private AutoResetEvent _get, _got;

    private ConsoleKeyInfo? _read;
    private bool Disposed { get; set; }

    public bool ConsumeInput { get; init; }

    public ConsoleKeyReader()
    {
        Init();
    }

    private void Init()
    {
        _get = new AutoResetEvent(false);
        _got = new AutoResetEvent(false);
        _thread = new Thread(Reader) { IsBackground = true };
        _thread.Start();
    }

    private void Reader()
    {
        while (!Disposed)
        {
            try
            {
                Thread.Sleep(1);
            }
            catch (ThreadInterruptedException)
            {
                return;
            }

            _get.WaitOne();
            _read = Console.ReadKey(intercept: !ConsumeInput);
            _got.Set();
        }
    }

    public ConsoleKeyInfo? Read(TimeSpan timeout)
    {
        _get.Set();
        return _got.WaitOne(timeout)
            ? _read
            : null;
    }

    public void Reset()
    {
        _get.Reset();
        _got.Reset();
        _read = null;
        _thread.Interrupt();
        _thread = null;
        Init();
    }

    public void Dispose()
    {
        if (!Disposed)
        {
            _thread.Interrupt();
        }
    }
}
