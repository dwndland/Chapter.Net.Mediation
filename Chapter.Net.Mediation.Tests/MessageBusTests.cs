// -----------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBusTests.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using NUnit.Framework;

namespace Chapter.Net.Mediation.Tests;

public class MessageBusTests
{
    private TestScheduler _scheduler;
    private MessageBus _target;

    [SetUp]
    public void SetUp()
    {
        _target = new MessageBus();
        _scheduler = new TestScheduler();
    }

    [Test]
    public void SubscribeSync_AnotherObjectGotSendSync_GotNotInformed()
    {
        void Callback(NotificationTwo two)
        {
            Assert.Fail();
        }

        var token = _target.Subscribe<NotificationTwo>(Callback);

        _target.Publish(new NotificationOne());
        token.Dispose();
    }

    [Test]
    public async Task SubscribeSync_AnotherObjectGotSendAsync_GotNotInformed()
    {
        void Callback(NotificationTwo two)
        {
            Assert.Fail();
        }

        var token = _target.Subscribe<NotificationTwo>(Callback);

        await _target.PublishAsync(new NotificationOne());
        token.Dispose();
    }

    [Test]
    public void SubscribeAsync_AnotherObjectGotSendSync_GotNotInformed()
    {
        Task Callback(NotificationTwo two)
        {
            Assert.Fail();
            return Task.CompletedTask;
        }

        var token = _target.Subscribe<NotificationTwo>(Callback);

        _target.Publish(new NotificationOne());
        token.Dispose();
    }

    [Test]
    public async Task SubscribeAsync_AnotherObjectGotSendAsync_GotNotInformed()
    {
        Task Callback(NotificationTwo two)
        {
            Assert.Fail();
            return Task.CompletedTask;
        }

        var token = _target.Subscribe<NotificationTwo>(Callback);

        await _target.PublishAsync(new NotificationOne());
        token.Dispose();
    }

    [Test]
    public void SubscribeSync_RequestedObjectGotSendSync_GotInformed()
    {
        var called = false;

        void Callback(NotificationOne two)
        {
            called = true;
        }

        var token = _target.Subscribe<NotificationOne>(Callback);

        _target.Publish(new NotificationOne());
        token.Dispose();

        Assert.That(called, Is.True);
    }

    [Test]
    public async Task SubscribeSync_RequestedObjectGotSendAsync_GotInformed()
    {
        var called = false;

        void Callback(NotificationOne two)
        {
            called = true;
        }

        var token = _target.Subscribe<NotificationOne>(Callback);

        await _target.PublishAsync(new NotificationOne());
        token.Dispose();

        Assert.That(called, Is.True);
    }

    [Test]
    public void SubscribeAsync_RequestedObjectGotSendSync_GotInformed()
    {
        var called = false;

        Task Callback(NotificationOne two)
        {
            called = true;
            return Task.CompletedTask;
        }

        var token = _target.Subscribe<NotificationOne>(Callback);

        _target.Publish(new NotificationOne());
        token.Dispose();

        Assert.That(called, Is.True);
    }

    [Test]
    public async Task SubscribeAsync_RequestedObjectGotSendAsync_GotInformed()
    {
        var called = false;

        Task Callback(NotificationOne two)
        {
            called = true;
            return Task.CompletedTask;
        }

        var token = _target.Subscribe<NotificationOne>(Callback);

        await _target.PublishAsync(new NotificationOne());
        token.Dispose();

        Assert.That(called, Is.True);
    }

    [Test]
    public void Unsubscribe_AnotherObjectGotSendSync_GotNotInformed()
    {
        void Callback(NotificationTwo two)
        {
            Assert.Fail();
        }

        var token = _target.Subscribe<NotificationTwo>(Callback);
        token.Dispose();

        _target.Publish(new NotificationOne());
    }

    [Test]
    public async Task Unsubscribe_AnotherObjectGotSendAsync_GotNotInformed()
    {
        void Callback(NotificationTwo two)
        {
            Assert.Fail();
        }

        var token = _target.Subscribe<NotificationTwo>(Callback);
        token.Dispose();

        await _target.PublishAsync(new NotificationOne());
    }

    [Test]
    public void Unsubscribe_RequestedObjectGotSendSync_GotNotInformed()
    {
        void Callback(NotificationOne two)
        {
            Assert.Fail();
        }

        var token = _target.Subscribe<NotificationOne>(Callback);
        token.Dispose();

        _target.Publish(new NotificationOne());
    }

    [Test]
    public async Task Unsubscribe_RequestedObjectGotSendAsync_GotNotInformed()
    {
        void Callback(NotificationOne two)
        {
            Assert.Fail();
        }

        var token = _target.Subscribe<NotificationOne>(Callback);
        token.Dispose();

        await _target.PublishAsync(new NotificationOne());
    }

    [Test]
    public void SubscribeSyncWithScheduler_AnotherObjectGotSendSync_GotNotInformedNeitherScheduler()
    {
        void Callback(NotificationTwo two)
        {
            Assert.Fail();
        }

        var token = _target.Subscribe<NotificationTwo>(Callback).On(_scheduler);

        _target.Publish(new NotificationOne());
        token.Dispose();

        Assert.That(_scheduler.Called, Is.False);
    }

    [Test]
    public async Task SubscribeSyncWithScheduler_AnotherObjectGotSendAsync_GotNotInformedNeitherScheduler()
    {
        void Callback(NotificationTwo two)
        {
            Assert.Fail();
        }

        var token = _target.Subscribe<NotificationTwo>(Callback).On(_scheduler);

        await _target.PublishAsync(new NotificationOne());
        token.Dispose();

        Assert.That(_scheduler.Called, Is.False);
    }

    [Test]
    public void SubscribeAsyncWithScheduler_AnotherObjectGotSendSync_GotNotInformedNeitherScheduler()
    {
        Task Callback(NotificationTwo two)
        {
            Assert.Fail();
            return Task.CompletedTask;
        }

        var token = _target.Subscribe<NotificationTwo>(Callback).On(_scheduler);

        _target.Publish(new NotificationOne());
        token.Dispose();

        Assert.That(_scheduler.Called, Is.False);
    }

    [Test]
    public async Task SubscribeAsyncWithScheduler_AnotherObjectGotSendAsync_GotNotInformedNeitherScheduler()
    {
        Task Callback(NotificationTwo two)
        {
            Assert.Fail();
            return Task.CompletedTask;
        }

        var token = _target.Subscribe<NotificationTwo>(Callback).On(_scheduler);

        await _target.PublishAsync(new NotificationOne());
        token.Dispose();

        Assert.That(_scheduler.Called, Is.False);
    }

    [Test]
    public void SubscribeSyncWithScheduler_RequestedObjectGotSendSync_GotInformedOverScheduler()
    {
        var called = false;

        void Callback(NotificationOne two)
        {
            called = true;
        }

        var token = _target.Subscribe<NotificationOne>(Callback).On(_scheduler);

        _target.Publish(new NotificationOne());
        token.Dispose();

        Assert.That(called, Is.True);
        Assert.That(_scheduler.Called, Is.True);
    }

    [Test]
    public async Task SubscribeSyncWithScheduler_RequestedObjectGotSendAsync_GotInformedOverScheduler()
    {
        var called = false;

        void Callback(NotificationOne two)
        {
            called = true;
        }

        var token = _target.Subscribe<NotificationOne>(Callback).On(_scheduler);

        await _target.PublishAsync(new NotificationOne());
        token.Dispose();

        Assert.That(called, Is.True);
        Assert.That(_scheduler.Called, Is.True);
    }

    [Test]
    public void SubscribeAsyncWithScheduler_RequestedObjectGotSendSync_GotInformedOverScheduler()
    {
        var called = false;

        Task Callback(NotificationOne two)
        {
            called = true;
            return Task.CompletedTask;
        }

        var token = _target.Subscribe<NotificationOne>(Callback).On(_scheduler);

        _target.Publish(new NotificationOne());
        token.Dispose();

        Assert.That(called, Is.True);
        Assert.That(_scheduler.Called, Is.True);
    }

    [Test]
    public async Task SubscribeAsyncWithScheduler_RequestedObjectGotSendAsync_GotInformedOverScheduler()
    {
        var called = false;

        Task Callback(NotificationOne two)
        {
            called = true;
            return Task.CompletedTask;
        }

        var token = _target.Subscribe<NotificationOne>(Callback).On(_scheduler);

        await _target.PublishAsync(new NotificationOne());
        token.Dispose();

        Assert.That(called, Is.True);
        Assert.That(_scheduler.Called, Is.True);
    }

    [Test]
    public void Unsubscribe_AnotherObjectGotSendSync_GotNotInformedNeitherScheduler()
    {
        void Callback(NotificationTwo two)
        {
            Assert.Fail();
        }

        var token = _target.Subscribe<NotificationTwo>(Callback).On(_scheduler);
        token.Dispose();

        _target.Publish(new NotificationOne());

        Assert.That(_scheduler.Called, Is.False);
    }

    [Test]
    public async Task Unsubscribe_AnotherObjectGotSendAsync_GotNotInformedNeitherScheduler()
    {
        void Callback(NotificationTwo two)
        {
            Assert.Fail();
        }

        var token = _target.Subscribe<NotificationTwo>(Callback).On(_scheduler);
        token.Dispose();

        await _target.PublishAsync(new NotificationOne());

        Assert.That(_scheduler.Called, Is.False);
    }

    [Test]
    public void Unsubscribe_RequestedObjectGotSendSync_GotNotInformedNeitherScheduler()
    {
        void Callback(NotificationOne two)
        {
            Assert.Fail();
        }

        var token = _target.Subscribe<NotificationOne>(Callback).On(_scheduler);
        token.Dispose();

        _target.Publish(new NotificationOne());

        Assert.That(_scheduler.Called, Is.False);
    }

    [Test]
    public async Task Unsubscribe_RequestedObjectGotSendAsync_GotNotInformedNeitherScheduler()
    {
        void Callback(NotificationOne two)
        {
            Assert.Fail();
        }

        var token = _target.Subscribe<NotificationOne>(Callback).On(_scheduler);
        token.Dispose();

        await _target.PublishAsync(new NotificationOne());

        Assert.That(_scheduler.Called, Is.False);
    }
}