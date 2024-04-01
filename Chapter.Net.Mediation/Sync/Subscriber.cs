// -----------------------------------------------------------------------------------------------------------------
// <copyright file="Subscriber.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Mediation
{
    /// <summary>
    ///     Represents a single subscription on an object type.
    /// </summary>
    /// <typeparam name="T">The type of the object this subscriber is listen for.</typeparam>
    public class Subscriber<T> : ISubscriber
    {
        private readonly Guid _instance;
        private Action<T> _callback;
        private IScheduler _scheduler;

        internal Subscriber(Action<T> callback)
        {
            _callback = callback;
            _instance = Guid.NewGuid();
        }

        /// <inheritdoc />
        public event EventHandler Disposed;

        /// <inheritdoc />
        public ISubscriber On(IScheduler scheduler)
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

        internal void Invoke(T obj)
        {
            if (_scheduler != null)
                _scheduler.Invoke(() => _callback.Invoke(obj));
            else
                _callback.Invoke(obj);
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

            return _instance.Equals(((Subscriber<T>)obj)._instance);
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
}