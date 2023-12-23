// -----------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using Chapter.Net;
using Chapter.Net.Mediation;
using Demo.Async;
using Demo.Sync;

namespace Demo;

public partial class MainWindow
{
    private readonly IMessageBus _messageBus;

    public MainWindow()
    {
        InitializeComponent();

        _messageBus = new MessageBus();

        Sender = new ObservableList<SenderViewModel>();
        Receiver = new ObservableList<ReceiverViewModel>();
        AddSenderCommand = new DelegateCommand(AddSender);
        RemoveSenderCommand = new DelegateCommand<SenderViewModel>(RemoveSender);
        AddReceiverCommand = new DelegateCommand(AddReceiver);
        RemoveReceiverCommand = new DelegateCommand<ReceiverViewModel>(RemoveReceiver);

        AsyncSender = new ObservableList<AsyncSenderViewModel>();
        AsyncReceiver = new ObservableList<AsyncReceiverViewModel>();
        AddAsyncSenderCommand = new DelegateCommand(AddAsyncSender);
        RemoveAsyncSenderCommand = new DelegateCommand<AsyncSenderViewModel>(RemoveAsyncSender);
        AddAsyncReceiverCommand = new DelegateCommand(AddAsyncReceiver);
        RemoveAsyncReceiverCommand = new DelegateCommand<AsyncReceiverViewModel>(RemoveAsyncReceiver);

        DataContext = this;
    }

    public IDelegateCommand AddSenderCommand { get; }
    public IDelegateCommand RemoveSenderCommand { get; }
    public IDelegateCommand AddReceiverCommand { get; }
    public IDelegateCommand RemoveReceiverCommand { get; }

    public IDelegateCommand AddAsyncSenderCommand { get; }
    public IDelegateCommand RemoveAsyncSenderCommand { get; }
    public IDelegateCommand AddAsyncReceiverCommand { get; }
    public IDelegateCommand RemoveAsyncReceiverCommand { get; }

    public ObservableList<SenderViewModel> Sender { get; }
    public ObservableList<ReceiverViewModel> Receiver { get; }

    public ObservableList<AsyncSenderViewModel> AsyncSender { get; }
    public ObservableList<AsyncReceiverViewModel> AsyncReceiver { get; }

    private void AddSender()
    {
        Sender.Add(new SenderViewModel(_messageBus));
    }

    private void RemoveSender(SenderViewModel obj)
    {
        Sender.Remove(obj);
    }

    private void AddReceiver()
    {
        Receiver.Add(new ReceiverViewModel(_messageBus));
    }

    private void RemoveReceiver(ReceiverViewModel obj)
    {
        obj.One.IsSubscribed = false;
        obj.Two.IsSubscribed = false;
        obj.Three.IsSubscribed = false;
        Receiver.Remove(obj);
    }

    private void AddAsyncSender()
    {
        AsyncSender.Add(new AsyncSenderViewModel(_messageBus));
    }

    private void RemoveAsyncSender(AsyncSenderViewModel obj)
    {
        AsyncSender.Remove(obj);
    }

    private void AddAsyncReceiver()
    {
        AsyncReceiver.Add(new AsyncReceiverViewModel(_messageBus));
    }

    private void RemoveAsyncReceiver(AsyncReceiverViewModel obj)
    {
        obj.One.IsSubscribed = false;
        obj.Two.IsSubscribed = false;
        obj.Three.IsSubscribed = false;
        AsyncReceiver.Remove(obj);
    }
}