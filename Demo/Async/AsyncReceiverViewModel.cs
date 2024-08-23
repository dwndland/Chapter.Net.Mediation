// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncReceiverViewModel.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using Chapter.Net;
using Chapter.Net.Mediation;

namespace Demo.Async;

public class AsyncReceiverViewModel : ObservableObject
{
    public AsyncReceiverViewModel(IMessageBus messageBus)
    {
        One = new AsyncSubscriptionHelper<NotificationOne>(messageBus);
        Two = new AsyncSubscriptionHelper<NotificationTwo>(messageBus);
        Three = new AsyncSubscriptionHelper<NotificationThree>(messageBus);
    }

    public AsyncSubscriptionHelper<NotificationOne> One { get; }
    public AsyncSubscriptionHelper<NotificationTwo> Two { get; }
    public AsyncSubscriptionHelper<NotificationThree> Three { get; }
}