// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IMessageBus.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Chapter.Net.Mediation
{
    /// <summary>
    ///     Brings possibility to communicate between modules not knowing each other.
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        ///     Subscribes on a specific object type.
        /// </summary>
        /// <typeparam name="T">The type of the object to subscribe on.</typeparam>
        /// <param name="callback">The callback.</param>
        /// <returns>The subscriber to dispose.</returns>
        ISubscriber Subscribe<T>(Action<T> callback);

        /// <summary>
        ///     Subscribes on a specific object type.
        /// </summary>
        /// <typeparam name="T">The type of the object to subscribe on.</typeparam>
        /// <param name="callback">The callback.</param>
        /// <returns>The subscriber to dispose.</returns>
        IAsyncSubscriber Subscribe<T>(Func<T, Task> callback);

        /// <summary>
        ///     Publishes the object all subscribers shall receive.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="item">The object instance.</param>
        void Publish<T>(T item);

        /// <summary>
        ///     Publishes the object all subscribers shall receive.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="item">The object instance.</param>
        Task PublishAsync<T>(T item);
    }
}