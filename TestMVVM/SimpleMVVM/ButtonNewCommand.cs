using System;
using System.Windows.Input;

namespace SimpleMVVM {
  public class ButtonNewCommand : ICommand
  {
    Action WhattoExecute;
    Func<bool> WhentoExecute;
    /// <inheritdoc />
    public event EventHandler CanExecuteChanged;
    public ButtonNewCommand(Action what, Func<bool> when) // Point 1
    {
      WhattoExecute = what;
      WhentoExecute = when;
    }
    public bool CanExecute(object parameter)
    {
      return WhentoExecute(); // Point 2
    }
    public void Execute(object parameter)
    {
      WhattoExecute(); // Point 3
    }

        
  }
}