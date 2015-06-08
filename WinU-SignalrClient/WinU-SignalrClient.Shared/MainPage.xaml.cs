using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WinU_SignalrClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.

            SetupWidgetHub();
        }

        private async void SetupWidgetHub()
        {
            //create a proxy to the widgetHub
            var hubConnection = new HubConnection("http://localhost:49919/");
            var hubProxy = hubConnection.CreateHubProxy("widgetHub");

            //handle the server messages
            hubProxy.On<int>("updateWidgetCount", count => Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                WidgetCountTextBlock.Text = count.ToString();
            }));

            await hubConnection.Start(new LongPollingTransport());
        }
    }
}
