﻿// 2019022210:24 AM

using System.Windows.Input;

namespace TestMVVM {
  public class MainViewModel : ViewModelBase
  {
      private string _imagePath;

      public string ImagePath
      {
          get
          {
              return _imagePath;
          }
          set
          {
              if (_imagePath != value)
              {
                  _imagePath = value;
                  OnPropertyChanged("ImagePath");
              }
          }
      }

      private double _zoom = 1.0;

      public double Zoom
      {
          get
          {
              return _zoom;
          }
          set
          {
              if (_zoom != value)
              {
                  _zoom = value;
                  OnPropertyChanged("Zoom");
              }
          }
      }

      private ICommand _openFileCommand;

      public ICommand OpenFileCommand
      {
          get { return _openFileCommand; }
      }

      private ZoomCommand _zoomCommand;

      public ZoomCommand ZoomCommand
      {
          get { return _zoomCommand; }
      }

      public MainViewModel()
      {
          _openFileCommand = new OpenFileCommand(this);
          _zoomCommand = new ZoomCommand(this);
      }
  }
}
