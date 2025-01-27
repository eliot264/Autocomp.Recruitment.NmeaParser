﻿using Autocomp.Nmea.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Autocomp.Nmea.WPF
{
    internal class ParseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly INmeaMessageParseService _parseService;

        private string message;
        private NmeaMessageContent? messageContent;
        private Dictionary<string, string>? messageDictionary;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        public NmeaMessageContent? MessageContent
        {
            set
            {
                messageContent = value;
                OnPropertyChanged(nameof(MessageName));
                MessageDictionary = messageContent?.ToDictionary();
            }
        }
        public string MessageName => messageContent?.Name ?? string.Empty;
        public Dictionary<string, string>? MessageDictionary
        {
            get { return messageDictionary; }
            set
            {
                messageDictionary = value;
                OnPropertyChanged(nameof(MessageDictionary));
            }
        }
        public ICommand ParseCommand { get; set; }
        public ParseViewModel(INmeaMessageParseService parseService)
        {
            _parseService = parseService;
            message = string.Empty;
            messageContent = null;
            messageDictionary = null;

            ParseCommand = new ParseCommand(this, _parseService);
        }

        private void ParseCommand_CanExecuteChanged(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
