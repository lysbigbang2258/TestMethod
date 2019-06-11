using System;
using System.Windows.Input;

namespace SimpleMVVM {
  public class ButtonCommand : ICommand
  {
    CustomerViewModel viewModel;

    public ButtonCommand(CustomerViewModel viewModel) {
      this.viewModel = viewModel;
    }
    public bool CanExecute(object parameter)
    {
      // When to execute
      // Validation logic goes here
      return true;
    }

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter) {
      viewModel.Caculate();
    }
  }
}