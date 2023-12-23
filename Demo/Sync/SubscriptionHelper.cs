// -----------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriptionHelper.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using Chapter.Net;
using Chapter.Net.Mediation;

namespace Demo.Sync;

public class SubscriptionHelper<T> : ObservableObject
{
    private readonly IMessageBus _messageBus;
    private bool _isSubscribed;
    private int _receivedCount;
    private ISubscriber _token;

    public SubscriptionHelper(IMessageBus messageBus)
    {
        _messageBus = messageBus;
        _receivedCount = 0;
    }

    public int ReceivedCount
    {
        get => _receivedCount;
        set => NotifyAndSetIfChanged(ref _receivedCount, value);
    }

    public bool IsSubscribed
    {
        get => _isSubscribed;
        set
        {
            NotifyAndSetIfChanged(ref _isSubscribed, value);

            if (value)
                _token = _messageBus.Subscribe<T>(Callback);
            else
                _token.Dispose();
        }
    }

    private void Callback(T _)
    {
        ReceivedCount += 1;
    }
}