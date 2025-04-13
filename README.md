![Chapter](https://raw.githubusercontent.com/dwndland/Chapter.Net.Mediation/master/Icon.png)

# Chapter.Net.Mediation Library

## Overview
Chapter.Net.Mediation is a lightweight library that implements the mediator pattern to enable decoupled communication between components in .NET applications.

## Features
- **Send messages anonymously synchronously:** The IMessageBus can send and receive messages around synchronously.
- **Send messages anonymously asynchronously:** The IMessageBus can send and receive messages around asynchronously.

## Getting Started

1. **Installation:**
    - Install the Chapter.Net.Mediation library via NuGet Package Manager:
    ```bash
    dotnet add package Chapter.Net.Mediation
    ```

2. **Synchronously:**
    - Send
    ```csharp
    _messageBus.Publish(new NotificationOne());
    ```
    - Receive
    ```csharp
    _token = _messageBus.Subscribe<NotificationOne>(Callback);

    _token.Dispose();
    ```

3. **Asynchronously:**
    - Send
    ```csharp
    await _messageBus.PublishAsync(new NotificationOne());
    ```
    - Receive
    ```csharp
    _token = _messageBus.Subscribe<NotificationOne>(Callback);

    _token.Dispose();
    ```

## Links
* [NuGet](https://www.nuget.org/packages/Chapter.Net.Mediation)
* [GitHub](https://github.com/dwndland/Chapter.Net.Mediation)

## License
Copyright (c) David Wendland. All rights reserved.
Licensed under the MIT License. See LICENSE file in the project root for full license information.
