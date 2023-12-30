using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UDPChatAppWpfNet47
{
    public class Message
    {
        public string Text { get; set; }
        public string Type { get; set; }
        public string Background { get; set; }
        public TextAlignment TextAlignment { get; set; }
    }

    public class ChatViewModel : INotifyPropertyChanged
    {
        private bool isTyping;
        public bool IsTyping
        {
            get { return isTyping; }
            set
            {
                isTyping = value;
                OnPropertyChanged("IsTyping");
            }
        }

        private string typingPerson;
        public string TypingPerson
        {
            get { return typingPerson; }
            set
            {
                typingPerson = value;
                OnPropertyChanged("TypingPerson");
            }
        }

        private ObservableCollection<Message> messages;
        public ObservableCollection<Message> Messages
        {
            get { return messages; }
            set
            {
                messages = value;
                OnPropertyChanged("Messages");
            }
        }

        public ChatViewModel()
        {
            Messages = new ObservableCollection<Message>();
        }

        public void AddMessageFromUser(string text)
        {
            Message message = new Message();
            message.Text = text;
            message.Type = "User";
            message.Background = "LightGreen";
            message.TextAlignment = TextAlignment.Right;
            Messages.Add(message);
        }

        public void AddMessageFromOtherUser(string text)
        {
            Message message = new Message();
            message.Text = text;
            message.Type = "Other";
            message.Background = "White";
            message.TextAlignment = TextAlignment.Left;
            Messages.Add(message);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
