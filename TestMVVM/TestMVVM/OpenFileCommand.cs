// 2019022210:23 AM

using System;
using System.Windows.Input;
using Microsoft.Win32;

namespace TestMVVM {
    public class OpenFileCommand : ICommand {
        private MainViewModel _data;

        public OpenFileCommand(MainViewModel data) {
            _data = data;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            OpenFileDialog dialog = new OpenFileDialog() {Filter = "Image Files|*.jpg;*.png;*.bmp;*.gif"};

            if (dialog.ShowDialog().GetValueOrDefault()) {
                _data.ImagePath = dialog.FileName;
            }
        }
    }
}
