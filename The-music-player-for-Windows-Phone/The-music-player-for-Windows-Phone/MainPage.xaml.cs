using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VK.WindowsPhone.SDK;
using VK.WindowsPhone.SDK.API;
using VK.WindowsPhone.SDK.API.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=391641

namespace The_music_player_for_Windows_Phone
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<string> _scope = new List<string> { VKScope.AUDIO };
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            VKSDK.Initialize("5528332");
            //VKSDK.WakeUpSession();
            VKSDK.Authorize(_scope, false, false);

        }


        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Подготовьте здесь страницу для отображения.

            // TODO: Если приложение содержит несколько страниц, обеспечьте
            // обработку нажатия аппаратной кнопки "Назад", выполнив регистрацию на
            // событие Windows.Phone.UI.Input.HardwareButtons.BackPressed.
            // Если вы используете NavigationHelper, предоставляемый некоторыми шаблонами,
            // данное событие обрабатывается для вас.
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PlayTrack((sender as TextBlock).Tag.ToString());
        }
        //Так как весь адрес url песни очень стращный и большой и несет много лишнего,
        //напишем метод который будет выводить поджстроку до ? это и будет истинный utl адрес
        public string GetTrueUrl(string InputString)
            => InputString.Substring(0, InputString.IndexOf('?')); //верси C# 6.0 новый синтаксис)

        private void textRequest_TextChanged(object sender, TextChangedEventArgs e)
        {
            VKRequest.Dispatch<VKList<VKAudio>>(new VKRequestParameters(
                "audio.search",
                "q", textRequest.Text),
                (result) =>
                {
                    try
                    {
                        audioView.ItemsSource = result.Data.items;
                    }
                    catch
                    { }

                }
                );
        }


        private void PlayTrack(string tempUrl)
        {
            Player.Source = new Uri(GetTrueUrl(tempUrl));
            Player.Play();
        }
        private void imageBack_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //передаем значение и одновременно делаем декремент значения в свойстве объекта
            //Вместо трех строченк ниже можно записать одну

            /* int n = --audioView.SelectedIndex;
             string tempUrl = (audioView.Items[n] as VKAudio).url;
             PlayTrack(tempUrl);*/
            
            PlayTrack((audioView.Items[--audioView.SelectedIndex] as VKAudio).url);
        }
        private void imageNext_Tapped(object sender, TappedRoutedEventArgs e)
        {
           
            //передаем значение и одновременно делаем инкремент значения в свойстве объекта
            
            //Вместо трех строченк ниже можно записать одну

            /* int n = ++audioView.SelectedIndex;
             string tempUrl = (audioView.Items[n] as VKAudio).url;
             PlayTrack(tempUrl);*/

            PlayTrack((audioView.Items[++audioView.SelectedIndex] as VKAudio).url);
        }

        private void imagePlay_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(Player.CurrentState==MediaElementState.Playing)
            {
                Player.Pause();
                Image_Loaded(@"Resources/ic_play_circle_outline_black_24dp.png");
            }
            else
            {
                Player.Play();
                Image_Loaded(@"Resources/ic_pause_circle_outline_black_24dp.png");
            }
        }
        //Метод который меняет картинку при ворспроизведении
        void Image_Loaded(string Icon)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.UriSource = new Uri(imagePlay.BaseUri, Icon);
            imagePlay.Source = bitmapImage;
        }
    }
}
