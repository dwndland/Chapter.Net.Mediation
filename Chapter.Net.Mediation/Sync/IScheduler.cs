// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IScheduler.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.Mediation;

/// <summary>
///     The scheduler where the <see cref="MessageBus" /> messages can be send over.
/// </summary>
public interface IScheduler
{
    /// <summary>
    ///     Invokes the action.
    /// </summary>
    /// <param name="action">The action to invoke.</param>
    void Invoke(Action action);
}