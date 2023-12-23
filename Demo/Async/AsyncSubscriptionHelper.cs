// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncSubscriptionHelper.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Chapter.Net;
using Chapter.Net.Mediation;

namespace Demo.Async;

public class AsyncSubscriptionHelper<T> : ObservableObject
{
    private readonly IMessageBus _messageBus;
    private bool _isSubscribed;
    private int _receivedCount;
    private IAsyncSubscriber _token;

    public AsyncSubscriptionHelper(IMessageBus messageBus)
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

    private async Task Callback(T _)
    {
        await Task.Delay(1000);
        ReceivedCount += 1;
    }
}