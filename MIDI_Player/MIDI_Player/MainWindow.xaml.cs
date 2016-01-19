using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Media;
using System.Windows.Media;
using System.Timers;
using System.Windows.Threading;

namespace MIDI_Player
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string filename;
        private bool isPlaying;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            
            filename = null;
            isPlaying = false;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0); // Every seconds 
            timer.Tick += new EventHandler(timer_Elapsed);
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
        
        private void MenuItem_Click_Open(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            //Stream myStream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set filter for file extension and default file extension
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "midi files (*.mid)|*.mid";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDialog.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                filename = openFileDialog.FileName;
                labelFile.Content = filename;

                InitMediaElm();
            }
            else
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: ");
            }
        }

        private void InitMediaElm()
        {
            mediaElement.Source = new Uri(filename);

            mediaElement.MediaOpened += new System.Windows.RoutedEventHandler(media_MediaOpened);
            mediaElement.LoadedBehavior = System.Windows.Controls.MediaState.Manual;
            mediaElement.UnloadedBehavior = System.Windows.Controls.MediaState.Manual;
        }

        private void media_MediaOpened(object sender, System.Windows.RoutedEventArgs e)
        {
            TimeSpan tsDuration = mediaElement.NaturalDuration.TimeSpan;
            slider.Maximum = (int)tsDuration.TotalSeconds;
            string str = tsDuration.ToString(@"hh\:mm\:ss");
            labelTimeStop.Content = str;
        }

        private void timer_Elapsed(object sender, EventArgs e)
        {
            Debug.WriteLine(mediaElement.Position.TotalSeconds);
            slider.Value = mediaElement.Position.TotalSeconds;

            TimeSpan tsTimeActu = TimeSpan.FromSeconds(slider.Value);
            string str = tsTimeActu.ToString(@"hh\:mm\:ss");
            labelTimeActual.Content = str;
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            if (filename != null)
            {
                if(isPlaying)
                {
                    mediaElement.Pause();
                    timer.Stop();
                }
                else
                {
                   /*byte[] MIDI = File.ReadAllBytes(filename);
                    for (int i = 0; i < MIDI.Length; i++)
                    {
                        if (MIDI[i] == 144)
                        {
                            Debug.WriteLine(MIDI[i + 1]);  //This is the note value
                        }
                    }*/

                    mediaElement.Play();
                    timer.Start();
                }

                isPlaying = !isPlaying;
            }
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            if (filename != null)
            {
                mediaElement.Stop();
                timer.Stop();
                slider.Value = 0;

                TimeSpan tsTimeActu = TimeSpan.FromSeconds(slider.Value);
                string str = tsTimeActu.ToString(@"hh\:mm\:ss");
                labelTimeActual.Content = str;

                isPlaying = false;
            }
        }
    }
}
