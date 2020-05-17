using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
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

namespace PROJET_2CP
{
    /// <summary>
    /// Logique d'interaction pour contactUs.xaml
    /// </summary>
    public partial class contactUs : Page
    {
       private TextBox sender = new TextBox();
       private TextBox subject = new TextBox();
       private TextBox message = new TextBox();
       private PasswordBox pass = new PasswordBox();

        public contactUs()
        {
            InitializeComponent();
            langue();
        }

        private void send_Click(object sender, RoutedEventArgs e)
        {
            sendMail();
        }
        private void sendMail()
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");

            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential(sender.Text, pass.Password);
            smtp.EnableSsl = true;
            try
            {
                mail.From = new MailAddress(sender.Text);
                mail.To.Add(new MailAddress("edcodedelaroute@gmail.com"));
                mail.Subject = subject.Text;
                mail.Body = message.Text;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
           
            mail.BodyEncoding = Encoding.UTF8; 
            mail.Priority = MailPriority.Normal;
            mail.IsBodyHtml = true;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            smtp.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            string userstate;
            if (MainWindow.langue == 0)
            {
                userstate = "En cours d'envoie ...";
            }
            else
            {
                userstate = "يتم الإرسال";
            }
            smtp.SendAsync(mail, userstate);
           /* try { 
                smtp.Send(mail);
                MessageBox.Show("Email sent", "Email sent", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception){
                MessageBox.Show("Une erreur s'est produit , SVP vérifiez que l'option << autoriser les applications moins sécurisées >> est activé dans vos paramètres google account ");
            }
           */
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            string m;
            string note;
            string terminer;
            if(MainWindow.langue==0)
            {
                m = "Message";
                note = "envoie annulé";
                terminer = "Message envoyé avec succès";
            }
            else
            {
                m = "رسالة";
                note = "إلغاء الإرسال";
                terminer = "لقد تم إرسال الرسالة بنجاح";
            }

          if(e.Cancelled)
            {
                MessageBox.Show(string.Format("{0}"+note,e.UserState),m,MessageBoxButton.OK,MessageBoxImage.Information );
            }
          if(e.Error !=null)
            {
                MessageBox.Show(string.Format("{0} {1}", e.UserState, e.Error), m, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(terminer, m, MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        private void langue()
        {
            TextBlock from = new TextBlock();
            TextBlock password = new TextBlock();
            TextBlock sujet = new TextBlock();
            TextBlock msg = new TextBlock();
            
            //Label settings
            from.Foreground = Brushes.White;
            from.FontSize = 20;
            from.Width = 200;
            from.Margin = new Thickness(10, 0, 10, 0);
            password.Foreground = Brushes.White;
            password.FontSize = 20;
            password.Width = 200;
            password.Margin = new Thickness(10, 0, 10, 0);
            sujet.Foreground = Brushes.White;
            sujet.FontSize = 20;
            sujet.Width = 200;
            sujet.Margin = new Thickness(10, 0, 10, 0);
            msg.Foreground = Brushes.White;
            msg.FontSize = 20;
            msg.Width = 200;
            msg.Margin = new Thickness(10, 0, 10, 0);
            //textBoxSetting
            sender.Height = 40;
            sender.Width = 500;
            sender.FontSize = 20;
            sender.Foreground = Brushes.White;
            subject.Height = 40;
            subject.Width = 500;
            subject.FontSize = 20;
            subject.Foreground = Brushes.White;
            message.Height = 300;
            message.Width = 500;
            message.FontSize = 20;
            message.TextWrapping = TextWrapping.Wrap;
            message.Foreground = Brushes.White;
            //passwordbox settings
            pass.Height = 40;
            pass.Width = 500;
            pass.FontSize = 20;
            pass.Foreground = Brushes.White;
            pass.PasswordChar = '*';
            if (MainWindow.langue==0)
            {   
                 from.Text = "votre compte gmail :";
                password.Text = "votre mot de passe :";
                sujet.Text = "Sujet :";
                msg.Text = "Message :";
                send.Content = "Envoyer";

                from.TextAlignment = TextAlignment.Left;
                password.TextAlignment = TextAlignment.Left;
                sujet.TextAlignment = TextAlignment.Left;
                msg.TextAlignment = TextAlignment.Left;

                sender.TextAlignment = TextAlignment.Left;
                pass.HorizontalContentAlignment = HorizontalAlignment.Left;
                subject.TextAlignment = TextAlignment.Left;
                message.TextAlignment = TextAlignment.Left;

                forUser.Children.Add(from);
                forUser.Children.Add(sender);

                forPassword.Children.Add(password);
                forPassword.Children.Add(pass);

                forSubject.Children.Add(sujet);
                forSubject.Children.Add(subject);

                forMessage.Children.Add(msg);
                forMessage.Children.Add(message);
            }
            else
            {
                from.Text =  ":"+ " حسابك في الجمايل";
                password.Text = ":" +  "كلمة المرور";
                sujet.Text = ":" + "الموضوع";
                msg.Text = ":" + "الرسالة";
                send.Content = "إرسال";

                from.TextAlignment = TextAlignment.Right;
                password.TextAlignment = TextAlignment.Right;
                sujet.TextAlignment = TextAlignment.Right;
                msg.TextAlignment = TextAlignment.Right;

                sender.TextAlignment = TextAlignment.Right;
                pass.HorizontalContentAlignment = HorizontalAlignment.Right;
                subject.TextAlignment = TextAlignment.Right;
                message.TextAlignment = TextAlignment.Right;

                forUser.Children.Add(sender);
                forUser.Children.Add(from);
               
                forPassword.Children.Add(pass);
                forPassword.Children.Add(password);
               
                forSubject.Children.Add(subject);
                forSubject.Children.Add(sujet);
               
                forMessage.Children.Add(message);
                forMessage.Children.Add(msg);
            }
        }
    }
}
