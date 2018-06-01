﻿using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace WeatherLab
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
		private readonly Client client;
	    private AudioRecorder recorder;

	    public MainWindow()
        {
            InitializeComponent();
	        client = new Client("tcp://127.0.0.1:2228");
        }

	    private void StartRecording(object sender, MouseButtonEventArgs e)
	    {
			recorder = new AudioRecorder();
			recorder.WaveSource.StartRecording();
		    mic.Source = new BitmapImage(new Uri("Resources/mic_rec.png", UriKind.Relative));
		}

	    private void StopRecording(object sender, MouseButtonEventArgs e)
	    {
			recorder.WaveSource.StopRecording();
		    mic.Source = new BitmapImage(new Uri("Resources/mic.png", UriKind.Relative));
			PrintResult();
	    }

	    private void PrintResult()
	    {
		    var response = client.Recognize(recorder.Stream.ToArray());
		    phrase.Text = response.Item1 == "NoText" ? "" : "\"" + response.Item1 + "\"";
		    text.Text = response.Item2;
		    mic.Source = new BitmapImage(new Uri("Resources/mic_done.png", UriKind.Relative));
		}
	}
}