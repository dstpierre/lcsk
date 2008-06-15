using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using LiveChatStarterKit.VisitorChat.OperatorService;
using LiveChatStarterKit.VisitorChat.ChatService;
using System.Collections.ObjectModel;

namespace LiveChatStarterKit.VisitorChat
{
    public partial class Chat : UserControl
    {
        ChatServiceClient chatSvc;

        private ObservableCollection<DepartmentEntity> departmentList = new ObservableCollection<DepartmentEntity>();
        private string channelId = "";
        private long lastReadId = 0;

        public Chat()
        {
            InitializeComponent();

            // Registering control events
            chatNow.Click += new RoutedEventHandler(chatNow_Click);

            // Show online departments
            OperatorClient svc = new OperatorClient();
            svc.GetOnlineDepartmentCompleted += new EventHandler<GetOnlineDepartmentCompletedEventArgs>(GetOnlineDepartmentCompleted);
            svc.GetOnlineDepartmentAsync();
        }

        void GetOnlineDepartmentCompleted(object sender, GetOnlineDepartmentCompletedEventArgs e)
        {
            if (e.Error == null && !e.Cancelled)
            {
                departmentList = e.Result;
                RadioButton rdoDep = null;
                foreach (var dep in departmentList)
                {
                    rdoDep = new RadioButton();
                    rdoDep.Content = dep.DepartmentName;

                    departments.Children.Add(rdoDep);
                }

                if (departments.Children.Count() > 0)
                    return;
                
            }
            TextBlock txtOffline = new TextBlock();
            txtOffline.Text = "No operator are availaible.";
            departments.Children.Add(txtOffline);

            chatNow.Content = "Send e-mail";
        }

        void chatNow_Click(object sender, RoutedEventArgs e)
        {

            chatSvc = new ChatServiceClient();
            chatSvc.RequestChatCompleted += new EventHandler<RequestChatCompletedEventArgs>(RequestChatCompleted);
            
            ChatRequestEntity requestEntity = new ChatRequestEntity();
            requestEntity.DepartmentId = GetCheckedDepartment();
            requestEntity.RequestedDate = DateTime.Now;
            requestEntity.SendCopyOfChat = false; //TODO: Implement the feature
            requestEntity.VisitorEmail = email.Text;
            requestEntity.VisitorIp = "";
            requestEntity.VisitorName = name.Text;
            chatSvc.RequestChatAsync(requestEntity);

            chatMsg.Text += "Please wait for an operator...";

            requestCanvas.Visibility = Visibility.Collapsed;
            chatCanvas.Visibility = Visibility.Visible;
        }

        private int GetCheckedDepartment()
        {
            int departmentId = 0;
            if (departments.Children.Count() > 0)
            {
                foreach (UIElement elm in departments.Children)
                {
                    if (elm is RadioButton)
                    {
                        RadioButton rdo = elm as RadioButton;
                        if ((bool)rdo.IsChecked)
                        {
                            var dep = departmentList.SingleOrDefault(d => d.DepartmentName == rdo.Content.ToString());
                            if( dep != null )
                            {
                                departmentId = dep.EntityId;
                                break;
                            }
                        }
                    }
                }
            }
            return departmentId;
        }

        void RequestChatCompleted(object sender, RequestChatCompletedEventArgs e)
        {
            if (e.Error == null && !e.Cancelled)
            {
                channelId = e.Result;
                if (channelId.Length > 0)
                {
                    // Start the timer for polling messages
                    chatTimer.Duration = new TimeSpan(0, 0, 2);
                    chatTimer.Begin();

                    // Register channel events
                    chatSvc.HasNewMessageCompleted += new EventHandler<HasNewMessageCompletedEventArgs>(HasNewMessageCompleted);
                    chatSvc.GetMessagesCompleted += new EventHandler<GetMessagesCompletedEventArgs>(GetMessageCompleted);

                    WriteMessage(question.Text);
                }
            }
        }

        private void WriteMessage(string message)
        {
            MessageEntity msg = new MessageEntity();
            msg.ChannelId = channelId;
            msg.FromName = name.Text;
            msg.Message = message;
            msg.SendDate = DateTime.Now;
            
            chatSvc.WriteMessageAsync(msg);

            inputMsg.Text = "";
        }

        void GetMessageCompleted(object sender, GetMessagesCompletedEventArgs e)
        {
            if (e.Error == null && !e.Cancelled)
            {
                foreach (var msg in e.Result)
                {
                    chatMsg.Text += string.Format("{0} : {1}", msg.FromName, msg.Message);
                    lastReadId = msg.EntityId;
                }
            }
        }

        void HasNewMessageCompleted(object sender, HasNewMessageCompletedEventArgs e)
        {
            if (e.Error == null && !e.Cancelled)
            {
                if (e.Result)
                    chatSvc.GetMessagesAsync(channelId, lastReadId);

                chatTimer.Duration = new TimeSpan(0, 0, 2);
                chatTimer.Begin();
            }
        }

        private void TimerCompleted(object sender, EventArgs e)
        {
            chatSvc.HasNewMessageAsync(channelId, lastReadId);
        }
    }
}
