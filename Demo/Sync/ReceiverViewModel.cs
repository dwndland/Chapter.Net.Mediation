// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ReceiverViewModel.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using Chapter.Net;
using Chapter.Net.Mediation;

namespace Demo.Sync;

public class ReceiverViewModel : ObservableObject
{
    public ReceiverViewModel(IMessageBus messageBus)
    {
        One = new SubscriptionHelper<NotificationOne>(messageBus);
        Two = new SubscriptionHelper<NotificationTwo>(messageBus);
        Three = new SubscriptionHelper<NotificationThree>(messageBus);
    }

    public SubscriptionHelper<NotificationOne> One { get; }
    public SubscriptionHelper<NotificationTwo> Two { get; }
    public SubscriptionHelper<NotificationThree> Three { get; }
}