using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WorkingWithConnectAnimation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        object _storedItem;
        public MainPage()
        {
            this.InitializeComponent();
            var items = new List<Model>();
            for (int i = 0; i < 30; i++)
            {
                items.Add(new Model { Title = i.ToString(), Content = i.ToString() + "index" });
            }

            collection.ItemsSource = items;
        }

        private void Conllection_ItemClick(object sender, ItemClickEventArgs e)
        {
            ConnectedAnimation animation = null;
            var container = collection.ContainerFromItem(e.ClickedItem) as GridViewItem;
            if(container != null)
            {
                _storedItem = container.Content;
                animation = collection.PrepareConnectedAnimation("forwardAnimation", _storedItem, "connectedElement");
            }
            SmokeGrid.Visibility = Visibility.Visible;
            SmokeGrid.DataContext = e.ClickedItem;
            animation.TryStart(destinationElement);
        }

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectedAnimation animation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backwardsAnimation", destinationElement);

            // Collapse the smoke when the animation completes.
            animation.Completed += Animation_Completed;

            // If the connected item appears outside the viewport, scroll it into view.
            collection.ScrollIntoView(_storedItem, ScrollIntoViewAlignment.Default);
            collection.UpdateLayout();

            // Use the Direct configuration to go back (if the API is available). 
            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7))
            {
                animation.Configuration = new DirectConnectedAnimationConfiguration();
            }

            // Play the second connected animation. 
            await collection.TryStartConnectedAnimationAsync(animation, _storedItem, "connectedElement");
        }

        private void Animation_Completed(ConnectedAnimation sender, object args)
        {
             SmokeGrid.Visibility = Visibility.Collapsed;
        }
    }
}
