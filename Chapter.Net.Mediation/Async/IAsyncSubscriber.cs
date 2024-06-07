// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IAsyncSubscriber.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Mediation;

/// <summary>
///     Represents a single subscription on an object type.
/// </summary>
public interface IAsyncSubscriber : IDisposable
{
    /// <summary>
    ///     Gets raised if this subscriber got disposed.
    /// </summary>
    event EventHandler Disposed;

    /// <summary>
    ///     Enables the dispatcher the callback gets invoked on.
    /// </summary>
    /// <param name="scheduler">The scheduler the callback gets invoked on.</param>
    /// <returns>The subscriber.</returns>
    IAsyncSubscriber On(IAsyncScheduler scheduler);
}