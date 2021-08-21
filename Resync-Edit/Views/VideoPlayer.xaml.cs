﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Resync_Edit.ViewModels;
using Unosquare.FFME.Common;

namespace Resync_Edit.Views
{
    /// <summary>
    /// Interaction logic for VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : UserControl
    {
        VideoPlayerViewModel vm = new VideoPlayerViewModel();
        public VideoPlayer()
        {
            InitializeComponent();
            this.DataContext = vm;
            vm.PlayRequested += MediaPlayer_PlayRequested;
            vm.PauseRequested += MediaPlayer_PauseRequested;
            vm.CloseRequested += MediaPlayer_CloseRequested;
            vm.VolumeChangeRequested += MediaPlayer_VolumeChangeRequested;
            vm.SeekChangeRequested += MediaPlayer_SeekChangeRequested;
        }

        private async void MediaPlayer_PlayRequested(object sender, EventArgs e) => await Media.Play();

        private async void MediaPlayer_PauseRequested(object sender, EventArgs e) => await Media.Pause();

        private async void MediaPlayer_CloseRequested(object sender, EventArgs e) => await Media.Close();

        private void MediaPlayer_VolumeChangeRequested(object sender, VolumeEventArgs e) => Media.Volume = e.Volume;

        private async void Media_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(vm.CurrentVideo is null))
            {
                Media.ScrubbingEnabled = true;
                await Media.Open(new Uri(vm.CurrentVideo));
                await Media.Play();
            }
            else
            {
                MessageBox.Show("Error: No Video");
            }
        }

        private async void MediaPlayer_SeekChangeRequested(object sender, SliderEventArgs e)
        {
            await Media.Seek(TimeSpan.FromSeconds(e.Position));
            //await Media.Play();
        }
    }
}
