using System;
using System.Windows;
using FjasR.PushNotifactions;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace FjasR.Wpf
{
    public partial class MainWindow
    {
        private readonly HubConnection connection;

        public MainWindow()
        {
            InitializeComponent();
            connection = new HubConnection("http://localhost:8081/");
            var messageOverviewHub = connection.CreateHubProxy("MessageOverviewHub");
            connection.StateChanged += change =>
            {
                Console.WriteLine(change.OldState + " => " + change.NewState);
            };

            messageOverviewHub.On<MessageUpdated>("invoke", i => MessageBox.Show(string.Format("Message was updated at {0} ", i.Time)));
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            connection.Start().Wait();
        }
    }
}