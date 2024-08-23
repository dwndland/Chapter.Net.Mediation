// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncSubscriber.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Mediation;

/// <summary>
///     Represents a single subscription on an object type.
/// </summary>
/// <typeparam name="T">The type of the object this subscriber is listen for.</typeparam>
public class AsyncSubscriber<T> : IAsyncSubscriber
{
    private readonly Guid _instance;
    private Func<T, Task> _callback;
    private IAsyncScheduler _scheduler;

    internal AsyncSubscriber(Func<T, Task> callback)
    {
        _callback = callback;
        _instance = Guid.NewGuid();
    }

    /// <inheritdoc />
    public event EventHandler Disposed;

    /// <inheritdoc />
    public IAsyncSubscriber On(IAsyncScheduler scheduler)
    {
        _scheduler = scheduler;
        return this;
    }

    /// <summary>
    ///     Disposes this subscription.
    /// </summary>
    public void Dispose()
    {
        _callback = null;
        Disposed?.Invoke(this, EventArgs.Empty);
    }

    internal async Task Invoke(T obj)
    {
        if (_scheduler != null)
            await _scheduler.Invoke(() => _callback.Invoke(obj));
        else
            await _callback.Invoke(obj);
    }

    /// <summary>
    ///     Compares this subscriber to another object.
    /// </summary>
    /// <param name="obj">The other object.</param>
    /// <returns>True if the object is the same; otherwise false.</returns>
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return _instance.Equals(((AsyncSubscriber<T>)obj)._instance);
    }

    /// <summary>
    ///     Returns the hashcode representing this instance.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return _instance.GetHashCode();
    }
}