// -----------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBusW.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

namespace Chapter.Net.Mediation;

/// <summary>
///     Provides a static access to the IMessageBus if its not expected to be injected.
/// </summary>
public static class MessageBusW
{
    /// <summary>
    ///     Access to the message bus instance.
    /// </summary>
    public static IMessageBus Instance { get; set; }
}