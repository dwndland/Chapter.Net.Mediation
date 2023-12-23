// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncSenderViewModel.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Chapter.Net;
using Chapter.Net.Mediation;

namespace Demo.Async;

public class AsyncSenderViewModel : ObservableObject
{
    private readonly IMessageBus _messageBus;

    public AsyncSenderViewModel(IMessageBus messageBus)
    {
        _messageBus = messageBus;

        PublishOneCommand = new AsyncDelegateCommand(PublishOne);
        PublishTwoCommand = new AsyncDelegateCommand(PublishTwo);
        PublishThreeCommand = new AsyncDelegateCommand(PublishThree);
    }

    public IDelegateCommand PublishOneCommand { get; }
    public IDelegateCommand PublishTwoCommand { get; }
    public IDelegateCommand PublishThreeCommand { get; }

    private async Task PublishOne()
    {
        await _messageBus.PublishAsync(new NotificationOne());
    }

    private async Task PublishTwo()
    {
        await _messageBus.PublishAsync(new NotificationTwo());
    }

    private async Task PublishThree()
    {
        await _messageBus.PublishAsync(new NotificationThree());
    }
}