﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Threading;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Model;
using Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);

            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (Debugger.IsAttached)
            {
                //x this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            var rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += this.OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();

            DispatcherHelper.Initialize();

            HistoryProvider.Instance.KnownTargets.Add("SingleFontIconViewModel", typeof(SingleFontIconViewModel));
            await this.LoadHistoryAsync();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            //TODO: Save application state and stop any background activity
            await this.SaveHistoryAsync();

            deferral.Complete();
        }

        private async Task LoadHistoryAsync()
        {
            var historyFile = await ApplicationData.Current.RoamingFolder.TryGetItemAsync("history.xml") as IStorageFile;
            if (historyFile == null)
            {
                return;
            }
            Debug.WriteLine($"Hisory file loading: {historyFile.Path}");
            await HistoryProvider.Instance.LoadAsync(historyFile);
            Debug.WriteLine("Hisory file loaded");
        }

        private async Task SaveHistoryAsync()
        {
            var historyFile = await ApplicationData.Current.RoamingFolder.CreateFileAsync("history.xml", CreationCollisionOption.ReplaceExisting);
            Debug.WriteLine($"Hisory file was saved: {historyFile.Path}");
            await HistoryProvider.Instance.SaveAsync(historyFile);
        }
    }
}
