// -----------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBus.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter.Net.Mediation;

/// <inheritdoc />
public class MessageBus : IMessageBus
{
    private readonly Dictionary<Type, List<IAsyncSubscriber>> _asyncSubscribers;
    private readonly Dictionary<Type, List<ISubscriber>> _subscribers;

    /// <summary>
    ///     Creates a new instance of <see cref="MessageBus" />.
    /// </summary>
    public MessageBus()
    {
        _subscribers = new Dictionary<Type, List<ISubscriber>>();
        _asyncSubscribers = new Dictionary<Type, List<IAsyncSubscriber>>();
    }

    #region Sync

    /// <inheritdoc />
    public ISubscriber Subscribe<T>(Action<T> callback)
    {
        if (callback == null)
            throw new ArgumentNullException(nameof(callback));

        var type = typeof(T);
        if (!_subscribers.ContainsKey(type))
            _subscribers[type] = new List<ISubscriber>();

        var subscriber = new Subscriber<T>(callback);
        subscriber.Disposed += OnDisposed;
        _subscribers[type].Add(subscriber);

        return subscriber;
    }

    /// <inheritdoc />
    public void Publish<T>(T item)
    {
        var type = typeof(T);
        if (_subscribers.TryGetValue(type, out var subscribers))
            foreach (var subscriber in subscribers.Cast<Subscriber<T>>().ToList())
                subscriber.Invoke(item);

        if (_asyncSubscribers.TryGetValue(type, out var asyncSubscribers))
            foreach (var asyncSubscriber in asyncSubscribers.Cast<AsyncSubscriber<T>>().ToList())
            {
#pragma warning disable CS4014
                asyncSubscriber.Invoke(item);
#pragma warning restore CS4014
            }
    }

    private void OnDisposed(object sender, EventArgs e)
    {
        var subscriber = (ISubscriber)sender;
        subscriber.Disposed -= OnDisposed;

        foreach (var (_, value) in _subscribers)
        {
            if (!value.Contains(sender))
                continue;

            value.Remove(subscriber);
            break;
        }

        ClearEmpty();
    }

    private void ClearEmpty()
    {
        var keys = _subscribers.Keys.ToList();
        foreach (var key in keys)
            if (!_subscribers[key].Any())
                _subscribers.Remove(key);
    }

    #endregion

    #region Async

    /// <inheritdoc />
    public IAsyncSubscriber Subscribe<T>(Func<T, Task> callback)
    {
        if (callback == null)
            throw new ArgumentNullException(nameof(callback));

        var type = typeof(T);
        if (!_asyncSubscribers.ContainsKey(type))
            _asyncSubscribers[type] = new List<IAsyncSubscriber>();

        var subscriber = new AsyncSubscriber<T>(callback);
        subscriber.Disposed += OnAsyncDisposed;
        _asyncSubscribers[type].Add(subscriber);

        return subscriber;
    }

    /// <inheritdoc />
    public async Task PublishAsync<T>(T item)
    {
        var type = typeof(T);
        if (_asyncSubscribers.TryGetValue(type, out var asyncSubscribers))
            foreach (var asyncSubscriber in asyncSubscribers.Cast<AsyncSubscriber<T>>().ToList())
                await asyncSubscriber.Invoke(item);

        if (_subscribers.TryGetValue(type, out var subscribers))
            foreach (var subscriber in subscribers.Cast<Subscriber<T>>().ToList())
                subscriber.Invoke(item);
    }

    private void OnAsyncDisposed(object sender, EventArgs e)
    {
        var subscriber = (IAsyncSubscriber)sender;
        subscriber.Disposed -= OnDisposed;

        foreach (var (_, value) in _asyncSubscribers)
        {
            if (!value.Contains(sender))
                continue;

            value.Remove(subscriber);
            break;
        }

        ClearEmptyAsync();
    }

    private void ClearEmptyAsync()
    {
        var keys = _asyncSubscribers.Keys.ToList();
        foreach (var key in keys)
            if (!_asyncSubscribers[key].Any())
                _asyncSubscribers.Remove(key);
    }

    #endregion
}