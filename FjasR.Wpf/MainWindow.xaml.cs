using System;
using System.Threading.Tasks;
using System.Windows;
using FjasR.PushNotifactions;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace FjasR.Wpf
{
    public partial class MainWindow
    {
        private readonly HubConnection connection;

        public MainWindow()
        {
            InitializeComponent();
            connection = new HubConnection("http://localhost:9081/");
            var messageOverviewHub = connection.CreateHubProxy("MessageOverviewHub");
            connection.StateChanged += change =>
            {
                Console.WriteLine(change.OldState + " => " + change.NewState);

                var connectionState = change.NewState;
                switch (connectionState)
                {
                    case ConnectionState.Connecting:
                        break;
                    case ConnectionState.Connected:
                        break;
                    case ConnectionState.Reconnecting:
                        break;
                    case ConnectionState.Disconnected:
                        Connect();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };

            

            messageOverviewHub.On<MessageUpdated>("invoke", i => label1.Content = string.Format("Message was updated at {0} ", i.Time));

            this.Loaded +=OnLoaded;

            
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Connect();
        }

        private void Connect()
        {
            connection.Start().ContinueWith(task => Console.WriteLine(task.Exception.ToString()), TaskContinuationOptions.OnlyOnFaulted);

        }

    }
}