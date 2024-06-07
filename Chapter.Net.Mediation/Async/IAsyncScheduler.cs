// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IAsyncScheduler.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Mediation;

/// <summary>
///     The scheduler where the <see cref="MessageBus" /> messages can be send over.
/// </summary>
public interface IAsyncScheduler
{
    /// <summary>
    ///     Invokes the action.
    /// </summary>
    /// <param name="action">The action to invoke.</param>
    /// <returns>The task to await</returns>
    Task Invoke(Func<Task> action);
}