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

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            VKRequest.Dispatch <VKList<VKAudio>>( new VKRequestParameters(
                "audio.search",
                "q", "OneRepublic"),
                (result)=>
                {
                    foreach (var item in result.Data.items)
                    {
                        audioView.Items.Add(item.title);
                    }
                }
                );
        }
    }
}
