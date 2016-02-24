using System;
using System.Collections.Generic;
using System.Text;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Popups;

namespace ApogeeApp1
{
    class schedulednotif
    {
        public void schedulenotif(double time, string content)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");

            toastTextElements[0].AppendChild(toastXml.CreateTextNode(content + " begins in 15 minutes"));
            double dueTimeInSec = time;

            DateTime dueTime = DateTime.Now.AddSeconds(dueTimeInSec);
            ScheduledToastNotification scheduledToast = new ScheduledToastNotification(toastXml, dueTime);
            string content1 = content;
            if (content.Length > 16)
                content1 = content.Substring(0, 16);
            scheduledToast.Id = content1;


            ToastNotificationManager.CreateToastNotifier().AddToSchedule(scheduledToast);

        }
        public void schedulenotifrem(string content)
        {
            //ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
            //XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            //XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            //toastTextElements[0].AppendChild(toastXml.CreateTextNode("Reminder- " + content + " begins in 15 minutes"));
            //double dueTimeInSec = time;

            //DateTime dueTime = DateTime.Now.AddSeconds(dueTimeInSec);
            //ScheduledToastNotification scheduledToast = new ScheduledToastNotification(toastXml, dueTime);
            //scheduledToast.Id = content;
            ScheduledToastNotification scheduledtoa;
            //ToastNotificationManager.CreateToastNotifier().RemoveFromSchedule(scheduledToast);
            IReadOnlyList<ScheduledToastNotification> his = ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications();
            string content1 = content;
            if (content.Length > 16)
                content1 = content.Substring(0, 16);

            foreach (var ii in his)
            {
                if (ii.Id.Contains(content1) == true)
                {
                    scheduledtoa = ii;
                    ToastNotificationManager.CreateToastNotifier().RemoveFromSchedule(scheduledtoa);
                    //MessageDialog msgbox3 = new MessageDialog("removed");
                    //await msgbox3.ShowAsync();
                    break;
                }
            }
        }
    }
}

