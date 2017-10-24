﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfAppCounter
{
    public class MyCommand : ICommand
    {
        private Action<object> _action;
        private Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {

                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (_canExecute != null)
                {

                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public MyCommand(Action<object> action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public MyCommand(Action<object> action) : this(action, null)
        {

        }

        public void OnCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute();
            }

            return true;
        }

        public void Execute(object parameter)
        {
            _action?.Invoke(parameter);
        }
    }
}
