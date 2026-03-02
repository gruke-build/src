// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

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
        ObjectDisposedException.ThrowIf(Disposed, this);

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
                _get.WaitOne();
            }
            catch (ThreadInterruptedException)
            {
                DidError = true;
            }

            try
            {
                _read = Console.ReadKey(intercept: !ConsumeInput);
            }
            catch (InvalidOperationException)
            {
                DidError = true;
            }
            finally
            {
                _got.Set();
            }

            if (DidError)
                return;
        }
    }

    public bool DidError { get; private set; }

    public ConsoleKeyInfo? Read(TimeSpan timeout)
    {
        ObjectDisposedException.ThrowIf(Disposed, this);

        _get.Set();
        return _got.WaitOne(timeout)
            ? DidError
                ? null
                : _read
            : null;
    }

    public void Reset()
    {
        _get.Reset();
        _got.Reset();
        _read = null;
        if (_thread.IsAlive)
            _thread.Interrupt();
        _thread = null;
        DidError = false;
        Init();
    }

    public void Dispose()
    {
        if (!Disposed)
        {
            _get.Reset();
            _got.Reset();
            _read = null;
            if (_thread.IsAlive)
                _thread.Interrupt();
            _thread = null;
            DidError = false;

            Disposed = true;
        }
    }
}
