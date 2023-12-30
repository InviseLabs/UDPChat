using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UDPChatAppWpfNet47
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // A property to store the user name
        public string UserName { get; set; }

        // A property to store the input text
        public string InputText { get; set; }


        // Create a new ChatViewModel instance
        ChatViewModel chatViewModel = new ChatViewModel();



        public MainWindow()
        {
            InitializeComponent();

            InitApp();
        }

        private void InitApp()
        {
            // Set the DataContext of the window to the chatViewModel
            this.DataContext = chatViewModel;


            //UsernameTxt.Text = $"{Binding UserName}";

            // Find the UsernameTxt XAML object, create new binding, define the binding, and lastly set the binding.
            Label Usernametxt = FindName("UsernameTxt") as Label;
            Binding binding = new Binding { Source = UserName, Mode = BindingMode.OneWay };
            Usernametxt.SetBinding(Label.ContentProperty, binding);

            // Set the data context of the window to this instance
            DataContext = this;

            // Set the initial user name
            UserName = "User" + DateTime.Now.Millisecond;

            // Set the initial input text
            InputText = "";

            SimulateResponse();

        }

        // A method to handle the minimize button click
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            // Minimize the window
            WindowState = WindowState.Minimized;
        }

        // A method to handle the close button click
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the window
            Close();
        }

        // A method to handle the menu button click
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the menu popup
            MenuPopup.IsOpen = !MenuPopup.IsOpen;
        }

        // A method to handle the edit name menu item click
        private void EditNameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Prompt the user to enter a new name
            string newName = Microsoft.VisualBasic.Interaction.InputBox("Enter a new name:", "Edit Name", UserName);

            // If the user entered a valid name, update the user name property
            if (!string.IsNullOrEmpty(newName))
            {
                UserName = newName;
            }
        }

        // A method to handle the more settings menu item click
        private void MoreSettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Show a message box with some dummy settings
            MessageBox.Show("Here are some more settings:\n- Theme: Dark\n- Font Size: 14\n- Notifications: On", "More Settings");
        }

        // A method to handle the send button click
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            // If the input text is not empty, send the message
            if (!string.IsNullOrEmpty(InputText))
            {
                // Create a new paragraph with the input text
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run(InputText));

                // Set the alignment and margin of the paragraph
                paragraph.TextAlignment = TextAlignment.Right;
                paragraph.Margin = new Thickness(10, 5, 10, 5);

                // Set the background and foreground of the paragraph
                paragraph.Background = new SolidColorBrush(Color.FromRgb(33, 150, 243));
                paragraph.Foreground = Brushes.White;

                // Add the paragraph to the chat box document
                ChatBox.Document.Blocks.Add(paragraph);

                // Clear the input text
                InputText = "";

                // Scroll the chat box to the end
                ChatBox.ScrollToEnd();

                // Simulate a response from the other person
                SimulateResponse();
            }
        }

        private void AddMessage(int type)
        {

        }

        // A method to simulate a response from the other person
        private void SimulateResponse()
        {
            // Set the typing status to true
            chatViewModel.IsTyping = true;
            chatViewModel.TypingPerson = "Alice";

            // Use a timer to delay the response
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 2000; // 2 seconds
            timer.AutoReset = true;
            int type = 1;
            int iterations = 4;
            timer.Elapsed += (s, e) =>
            {
                iterations--;
                if (iterations == 0) { timer.Stop(); }

                if(type==1)
                {
                    type = 2;
                    // Invoke the dispatcher to update the UI
                    Dispatcher.Invoke(() => { MessageSimulator(1); });
                }
                else
                {
                    type = 1;
                    // Invoke the dispatcher to update the UI
                    Dispatcher.Invoke(() => { MessageSimulator(2); });
                }

            };
            timer.Start();


        }

        private void MessageSimulator(int type)
        {
            chatViewModel.IsTyping = false;

            if (type == 1)
            {
                AddMsgFromOthers("I just found this really cool thing.");
            }
            else if (type == 2)
            {
                AddMsgFromUser("Wow, that's really neato burrito, bro!");
            }
        }

        private void AddMsgFromOthers(string message)
        {
            // Create a new BlockUIContainer
            BlockUIContainer container = new BlockUIContainer();

            // Create a new Border
            Border border = new Border();

            // Set the border properties
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = Brushes.Gray;
            border.CornerRadius = new CornerRadius(5);
            border.Margin = new Thickness(10, 0, 300, 20);

            // Set the horizontal alignment of the border to left
            border.HorizontalAlignment = HorizontalAlignment.Left;

            // Create a new RichTextBox
            RichTextBox richTextBox = new RichTextBox();

            // Create a new Paragraph
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(message));
           

            // Add the paragraph to the rich text box document
            richTextBox.Document.Blocks.Add(paragraph);

            // Set the rich text box properties
            richTextBox.IsReadOnly = true;
            richTextBox.BorderThickness = new Thickness(0);
            richTextBox.Padding = new Thickness(0);
            richTextBox.VerticalAlignment = VerticalAlignment.Center;

            // Add the rich text box to the border
            border.Child = richTextBox;

            // Add the border to the container
            container.Child = border;

            // Add the container to the chat box document
            ChatBox.Document.Blocks.Add(container);

            // Scroll the chat box to the end
            ChatBox.ScrollToEnd();

        }

        private void AddMsgFromUser(string message)
        {
            // Create a new BlockUIContainer
            BlockUIContainer container = new BlockUIContainer();

            // Create a new Border
            Border border = new Border();

            // Set the border properties
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = Brushes.Gray;
            border.CornerRadius = new CornerRadius(5);
            border.Margin = new Thickness(300, 0, 10, 20);

            // Set the horizontal alignment of the border to left
            border.HorizontalAlignment = HorizontalAlignment.Right;

            // Create a new RichTextBox
            RichTextBox richTextBox = new RichTextBox();

            // Create a new Paragraph
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(message));

            // Add the paragraph to the rich text box document
            richTextBox.Document.Blocks.Add(paragraph);

            // Set the rich text box properties
            richTextBox.IsReadOnly = true;
            richTextBox.BorderThickness = new Thickness(0);
            richTextBox.Padding = new Thickness(0);
            richTextBox.VerticalAlignment = VerticalAlignment.Center;

            // Add the rich text box to the border
            border.Child = richTextBox;

            // Add the border to the container
            container.Child = border;

            // Add the container to the chat box document
            ChatBox.Document.Blocks.Add(container);

            // Scroll the chat box to the end
            ChatBox.ScrollToEnd();
        }

        private void SizeButton_Click(object sender, EventArgs e)
        {

        }

        // A method to handle the title bar mouse left button down
        private void TitleBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                // Drag the window
                DragMove();
            }
            catch { }
        }
    }
}
