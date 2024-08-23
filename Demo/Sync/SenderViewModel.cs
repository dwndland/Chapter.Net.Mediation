// -----------------------------------------------------------------------------------------------------------------
// <copyright file="SenderViewModel.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using Chapter.Net;
using Chapter.Net.Mediation;

namespace Demo.Sync;

public class SenderViewModel : ObservableObject
{
    private readonly IMessageBus _messageBus;

    public SenderViewModel(IMessageBus messageBus)
    {
        _messageBus = messageBus;

        PublishOneCommand = new DelegateCommand(PublishOne);
        PublishTwoCommand = new DelegateCommand(PublishTwo);
        PublishThreeCommand = new DelegateCommand(PublishThree);
    }

    public IDelegateCommand PublishOneCommand { get; }
    public IDelegateCommand PublishTwoCommand { get; }
    public IDelegateCommand PublishThreeCommand { get; }

    private void PublishOne()
    {
        _messageBus.Publish(new NotificationOne());
    }

    private void PublishTwo()
    {
        _messageBus.Publish(new NotificationTwo());
    }

    private void PublishThree()
    {
        _messageBus.Publish(new NotificationThree());
    }
}