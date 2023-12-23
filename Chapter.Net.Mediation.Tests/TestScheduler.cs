// -----------------------------------------------------------------------------------------------------------------
// <copyright file="TestScheduler.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Chapter.Net.Mediation.Tests;

public class TestScheduler : IScheduler, IAsyncScheduler
{
    public bool Called { get; private set; }

    public Task Invoke(Func<Task> action)
    {
        Called = true;
        return action();
    }

    public void Invoke(Action action)
    {
        Called = true;
        action();
    }
}