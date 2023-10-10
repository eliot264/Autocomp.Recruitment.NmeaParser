﻿using Autocomp.Nmea.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Autocomp.Nmea.WPF
{
    internal class ParseCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly ParseViewModel _parseViewModel;
        private readonly INmeaMessageParseService _parseService;

        public ParseCommand(ParseViewModel parseViewModel, INmeaMessageParseService parseService)
        {
            _parseViewModel = parseViewModel;
            _parseService = parseService;
        }

        public bool CanExecute(object? parameter)
        {
            return _parseViewModel.Message.Count() > 0;
        }

        public void Execute(object? parameter)
        {
            try
            {
                _parseViewModel.MessageContent = _parseService.Parse(_parseViewModel.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error", $"Message cannot be parsed due to exception: {e.Message}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}