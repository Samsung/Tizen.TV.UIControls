using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tizen.TV.UIControls.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TMDbLib.Client;
using Tizen.Applications;

namespace TMDb
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        TMDbLib.Objects.Movies.Movie _movie;
        MovieListModel _similars;
        public DetailPage(int id)
        {
            InitializeComponent();

            WaitingView.Opacity = 1.0;

            Task.Run(async () =>
            {
                var client = new TMDbClient("870755bb1ee864493829202f5afa20bd");
                var taskMovie = client.GetMovieAsync(id);
                var taskSimilar = client.GetMovieSimilarAsync(id);
                var taskCredit = client.GetMovieCreditsAsync(id);
                var taskVideo = client.GetMovieVideosAsync(id);
                var movie = await taskMovie;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    WaitingView.Opacity = 0.0;
                    _movie = movie;
                    BindingContext = movie;

                    var credit = await taskCredit;
                    var cast = new CastListModel
                    {
                        Items = credit.Cast
                    };
                    CastList.BindingContext = cast;

                    var similars = await taskSimilar;
                    _similars = new MovieListModel
                    {
                        Title = "Similar Movies",
                        Items = similars.Results,
                    };
                    SimilarList.BindingContext = _similars;

                    var videos = await taskVideo;
                    int i = 0;
                    foreach (var video in videos.Results)
                    {
                        if (video.Site == "YouTube")
                        {
                            i++;
                            var button = new Button
                            {
                                Text = $"Watch trailer #{i}",
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.CenterAndExpand
                            };
                            button.Clicked += (s, e) =>
                            {
#if USE_VIDEOPAGE
                                Navigation.PushAsync(new VideoPage(video.Key));
#else
                                AppControl appControl = new AppControl();
                                appControl.ApplicationId = "com.samsung.tv.cobalt-yt";
                                appControl.Operation = AppControlOperations.Default;
                                appControl.ExtraData.Add("PAYLOAD", $"#play?v={video.Key}");
                                AppControl.SendLaunchRequest(appControl);
#endif
                                Console.WriteLine($"ID : {video.Key}");
                            };
                            Console.WriteLine($"Video : {video.Key} {video.Name} {video.Site}");
                            ButtonArea.Children.Add(button);

                            InputEvents.GetEventHandlers(button).Add(
                                new RemoteKeyHandler((arg) => {
                                    if (arg.KeyName == RemoteControlKeyNames.Up)
                                    {
                                        ScrollView.ScrollToAsync(0, 0, true);
                                        arg.Handled = true;
                                    }
                                }, RemoteControlKeyTypes.KeyDown
                            ));

                        }
                        if (i > 2)
                            break;
                    }
                });
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            InputEvents.GetEventHandlers(CastList).Add(new RemoteKeyHandler(async (evt) =>
            {
                if (evt.KeyName == RemoteControlKeyNames.Up && ButtonArea.Children.Count > 0)
                {
                    var btn = ButtonArea.Children[0];
                    await ScrollView.ScrollToAsync(btn, ScrollToPosition.Center, true);
                    btn.Focus();
                }
                else if (evt.KeyName == RemoteControlKeyNames.Down)
                {
                    await ScrollView.ScrollToAsync(SimilarList, ScrollToPosition.Center, true);
                    SimilarList.Focus();
                }
            }, RemoteControlKeyTypes.KeyDown));

            InputEvents.GetEventHandlers(SimilarList).Add(new RemoteKeyHandler(async (evt) =>
            {
                if (evt.KeyName == RemoteControlKeyNames.Up)
                {
                    await ScrollView.ScrollToAsync(CastList, ScrollToPosition.Center, true);
                    CastList.Focus();
                }
            }, RemoteControlKeyTypes.KeyDown));
        }
    }
}