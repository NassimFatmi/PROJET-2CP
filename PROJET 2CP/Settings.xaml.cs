using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Windows.Media.Animation;
using MaterialDesignThemes.Wpf;
using System.Windows.Diagnostics;
using System.Text.RegularExpressions;

namespace PROJET_2CP
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
            if (LogIN.LoggedUser.Theme == true)
                toggleTheme.IsChecked = true;
            else
                toggleTheme.IsChecked = false;

            toggleTheme.Checked += new RoutedEventHandler(this.toggleTheme_Checked);
            toggleTheme.Unchecked += new RoutedEventHandler(this.toggleTheme_Unchecked);

            refreshGrid.Visibility = Visibility.Collapsed;
            initialiserLangue();
            saveBtn.IsEnabled = false;
            saveBtn.Command = MaterialDesignThemes.Wpf.DialogHost.OpenDialogCommand;
            inisialiserInfoUser();
        }

        private void initialiserLangue()
        {
            if (MainWindow.langue == 0)
            {
                //la langue français
                title.Content = "Paramètres";
                back.Text = "Retour";
                userid.Text = "Utilisateur ID";
                nom.Text = "Nom";
                prenom.Text = "Prénom";
                mdpss.Text = "ajouter/changer mot de passe";
                save.Text = "Sauvegarder";
                dhconfirmer.Content = "Confirmer";
                dhretoure.Content = "Retoure";
                MaterialDesignThemes.Wpf.HintAssist.SetHint(passwordtxt, "Nouveau mot de passe");
                MaterialDesignThemes.Wpf.HintAssist.SetHint(confirmepasswordtxt, "Confirmer le Nouveau mot de passe");
                MaterialDesignThemes.Wpf.HintAssist.SetHint(oldPasswordtxt, "Ancien le mot de passe");
                addimage.ToolTip = "Changer l'image";
                refresh.ToolTip = "تحديث";
                refreshlbl.Content = "حدث التطبيق لتغيير اللغة";
                themebtn.Text = "theme";
                deleteAcc.Text = "Supprimer le compte";
                if (toggleTheme.IsChecked == true)
                    toggleTheme.ToolTip = "Theme blue";
                else
                    toggleTheme.ToolTip = "Theme Rouge";
            }
            else
            {
                //la langue arabe
                title.Content = "الاعدادت";
                back.Text = "عودة";
                userid.Text = "اسم ID";
                nom.Text = "اللقب";
                prenom.Text = "الاسم";
                mdpss.Text = "اضف/غير كلمة المرور";
                save.Text = "حفظ";
                dhconfirmer.Content = "تأكيد";
                dhretoure.Content = "عودة";
                MaterialDesignThemes.Wpf.HintAssist.SetHint(passwordtxt, "كلمة المرور الجديدة");
                MaterialDesignThemes.Wpf.HintAssist.SetHint(confirmepasswordtxt, "تأكيد كلمة المرور الجديدة");
                MaterialDesignThemes.Wpf.HintAssist.SetHint(oldPasswordtxt, "كلمة المرور القديمة");
                addimage.ToolTip = "غير الصورة";
                refresh.ToolTip = "Actualiser la page";
                refreshlbl.Content = "Actualiser l'application pour changer la langue";
                themebtn.Text = "التصميم";
                deleteAcc.Text = "حدف الحساب";
                if (toggleTheme.IsChecked == true)
                    toggleTheme.ToolTip = "التصميم الأزرق";
                else                        
                    toggleTheme.ToolTip = "التصميم الأحمر";
            }
        }

        private void inisialiserInfoUser()
        {
            if (MainWindow.langue == 0)
                langueCmb.SelectedIndex = 0;
            else
                langueCmb.SelectedIndex = 1;

            userIDtxt.Text = LogIN.LoggedUser.UtilisateurID;
            nomTxt.Text = LogIN.LoggedUser.Nom;
            prenomtxt.Text = LogIN.LoggedUser.Prenom;
            emailtxt.Text = LogIN.LoggedUser.Email;

            try
            {
                if (LogIN.LoggedUser.Image.Equals(""))
                    avatar.Fill = new ImageBrush(new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\Default.png")));
                else
                    avatar.Fill = new ImageBrush(new BitmapImage(new Uri(LogIN.LoggedUser.Image)));
            }
            catch (Exception)
            {

            }
            refresh.IsEnabled = false;
            refresh.Visibility = Visibility.Collapsed;
            refreshGrid.Visibility = Visibility.Collapsed;
        }

        private void infoHasChanged(object sender, TextChangedEventArgs e)
        {
            //Enable the save button to give the possibility to save
            //The changes of user infos ! 
            if (!nomTxt.Text.Equals(LogIN.LoggedUser.Nom) || !prenomtxt.Text.Equals(LogIN.LoggedUser.Prenom) || !userIDtxt.Text.Equals(LogIN.LoggedUser.UtilisateurID) || !emailtxt.Text.Equals(LogIN.LoggedUser.Email))
                saveBtn.IsEnabled = true;
            else
            if (nomTxt.Text.Equals(LogIN.LoggedUser.Nom) && prenomtxt.Text.Equals(LogIN.LoggedUser.Prenom) && userIDtxt.Text.Equals(LogIN.LoggedUser.UtilisateurID) && emailtxt.Text.Equals(LogIN.LoggedUser.Email))
            {
                saveBtn.IsEnabled = false;
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {

            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(emailtxt.Text, pattern))
            {

            }
            else
            {
                if (MainWindow.langue == 0)
                {
                    MessageBox.Show("Vérifiez votre email !");
                }
                else
                {
                    MessageBox.Show("تأكد من البريد الألكتروني");
                }
                return;
            }


            //Update Save DB to the current userID
            if (!userIDtxt.Text.Equals(LogIN.LoggedUser.UtilisateurID))
                updateSaveDB();

            //Save the changes
            string userDbconnString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\UtilisateurBDD.mdf;Integrated Security=True";
            SqlConnection userDbConnection = new SqlConnection(userDbconnString);
            SqlCommand cmdUpdate;
            try
            {
                if (userDbConnection.State == ConnectionState.Closed)
                    userDbConnection.Open();

                string queryUpdate = "UPDATE Utilisateur SET UtilisateurID = '" + userIDtxt.Text + "'," +
                                     "Nom = '" + nomTxt.Text + "'," +
                                     "Prenom = '" + prenomtxt.Text + "'," +
                                     "Email = '" + emailtxt.Text+ "'" +
                                     "WHERE UtilisateurID = '" + LogIN.LoggedUser.UtilisateurID + "'";

                cmdUpdate = new SqlCommand(queryUpdate, userDbConnection);
                cmdUpdate.CommandType = CommandType.Text;
                cmdUpdate.ExecuteNonQuery();

                if (userDbConnection.State == ConnectionState.Open)
                    userDbConnection.Close();

            }
            catch (Exception)
            {
                if (userDbConnection.State == ConnectionState.Open)
                    userDbConnection.Close();

                MessageBox.Show("Error has accured in settings !");
            }

            LogIN.LoggedUser.UtilisateurID = userIDtxt.Text;
            LogIN.LoggedUser.Nom = nomTxt.Text;
            LogIN.LoggedUser.Prenom = prenomtxt.Text;
            LogIN.LoggedUser.Email = emailtxt.Text;
            MainWindow.quizFrame.Content = new Home();
            Home.mainFrame.Content = new Settings();
        }
        private void updateSaveDB()
        {
            string saveDBconnString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\Trace\Save.mdf;Integrated Security=True";
            string renameTraceTableQuery = "EXEC sp_rename '" + LogIN.LoggedUser.UtilisateurID + "Trace', '" + userIDtxt.Text + "Trace'";

            string renameNoteTestTableQuery = "EXEC sp_rename '" + LogIN.LoggedUser.UtilisateurID + "NoteTest', '" + userIDtxt.Text + "NoteTest'";
            SqlConnection saveConnection = new SqlConnection(saveDBconnString);
            SqlCommand renameTraceCMD;
            SqlCommand renameNoteTestCMD;

            using (saveConnection)
            {
                try
                {
                    if (saveConnection.State == ConnectionState.Closed)
                        saveConnection.Open();

                    renameTraceCMD = new SqlCommand(renameTraceTableQuery, saveConnection);
                    renameTraceCMD.CommandType = CommandType.Text;
                    renameTraceCMD.ExecuteNonQuery();

                    renameNoteTestCMD = new SqlCommand(renameNoteTestTableQuery, saveConnection);
                    renameNoteTestCMD.CommandType = CommandType.Text;
                    renameNoteTestCMD.ExecuteNonQuery();

                    if (saveConnection.State == ConnectionState.Open)
                        saveConnection.Close();

                }
                catch (Exception)
                {
                    if (saveConnection.State == ConnectionState.Open)
                        saveConnection.Close();
                    MessageBox.Show("Error has accured in upadate DB save ");
                }
            }
        }
        private void areyousureTOUpdate()
        {
            //if the user is 100% want to change his infos or not !
            dialogueHost.Children.Clear();
            dialogueHost.Height = 150;
            dialogueHost.Width = 300;

            StackPanel yesorno = new StackPanel();
            StackPanel message = new StackPanel();

            Button yes = new Button();
            yes.Background = Brushes.Red;
            yes.BorderBrush = null;
            yes.Click += new RoutedEventHandler(this.saveBtn_Click);
            //yes.Command = MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand;

            Button no = new Button();
            no.Foreground = Brushes.LightBlue;
            no.Background = null;
            no.BorderBrush = null;
            no.Command = MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand;
            no.Click += new RoutedEventHandler(this.noRefresh);

            TextBlock msg = new TextBlock();
            msg.Margin = new Thickness(15, 0, 0, 0);
            msg.Foreground = Brushes.Gray;
            msg.FontSize = 13;
            msg.VerticalAlignment = VerticalAlignment.Center;

            MaterialDesignThemes.Wpf.PackIcon icon = new MaterialDesignThemes.Wpf.PackIcon();
            MaterialDesignThemes.Wpf.PackIconKind packIconKind = new MaterialDesignThemes.Wpf.PackIconKind();
            packIconKind = MaterialDesignThemes.Wpf.PackIconKind.Dangerous;
            icon.Kind = packIconKind;
            icon.Foreground = Brushes.Red;
            icon.Height = 30;
            icon.Width = 30;

            if (MainWindow.langue == 0)
            {
                yes.Content = "Oui";
                no.Content = "Non";
                msg.Text = "Etes-vous sur de changer votre info ?";
            }
            else
            {
                yes.Content = "نعم";
                no.Content = "لا";
                msg.Text = "هل انت متأكد من تغيير معلوماتك ؟";
            }

            message.Orientation = Orientation.Horizontal;
            message.Children.Add(icon);
            message.Children.Add(msg);
            message.VerticalAlignment = VerticalAlignment.Top;
            message.HorizontalAlignment = HorizontalAlignment.Center;
            message.Margin = new Thickness(10);

            yesorno.Orientation = Orientation.Horizontal;
            yesorno.Height = 40;
            yesorno.VerticalAlignment = VerticalAlignment.Bottom;
            yesorno.HorizontalAlignment = HorizontalAlignment.Right;
            yesorno.Margin = new Thickness(10);
            yesorno.Children.Add(no);
            yesorno.Children.Add(yes);

            dialogueHost.Children.Add(message);
            dialogueHost.Children.Add(yesorno);
        }

        private void saveBtnOpenDialogue(object sender, RoutedEventArgs e)
        {
            areyousureTOUpdate();
        }

        private void changeImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog parcourire = new OpenFileDialog();
            parcourire.Filter = "Image Files(*.jpg;*.jpeg)|*.jpg;*.jpeg|PNG Files(*png)|*.png";
            parcourire.Multiselect = false;
            parcourire.DefaultExt = ".jpg";

            if (parcourire.ShowDialog() == true)
            {
                imagepathtxt.Text = parcourire.FileName.ToString();

                avatar.Fill = new ImageBrush(new BitmapImage(new Uri(imagepathtxt.Text)));

                //save image in the database
                string userDbconnString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\UtilisateurBDD.mdf;Integrated Security=True";
                SqlConnection userDbConnection = new SqlConnection(userDbconnString);
                SqlCommand cmdUpdate;
                try
                {
                    if (userDbConnection.State == ConnectionState.Closed)
                        userDbConnection.Open();

                    string queryUpdate = "UPDATE Utilisateur SET Image = '" + imagepathtxt.Text + "'" +
                                         "WHERE UtilisateurID = '" + LogIN.LoggedUser.UtilisateurID + "'";

                    cmdUpdate = new SqlCommand(queryUpdate, userDbConnection);
                    cmdUpdate.CommandType = CommandType.Text;
                    cmdUpdate.ExecuteNonQuery();

                    if (userDbConnection.State == ConnectionState.Open)
                        userDbConnection.Close();

                }
                catch (Exception)
                {
                    if (userDbConnection.State == ConnectionState.Open)
                        userDbConnection.Close();

                    MessageBox.Show("Error has accured in settings save image !");
                }

                LogIN.LoggedUser.Image = imagepathtxt.Text;
                try
                {
                    Home.getProfilepic().Fill = new ImageBrush(new BitmapImage(new Uri(LogIN.LoggedUser.Image)));
                }
                catch (Exception) { }
             
            }
        }

        private void langueCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (langueCmb.SelectedIndex == 0)
                MainWindow.langue = 0;
            else
                MainWindow.langue = 1;

            refresh.IsEnabled = true;
            refresh.Visibility = Visibility.Visible;
            refreshGrid.Visibility = Visibility.Visible;
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.quizFrame.Content = new Home();
        }

        private void dhconfirmer_Click(object sender, RoutedEventArgs e)
        {
            if (LogIN.LoggedUser.Password.Equals(""))
            {
                if (!passwordtxt.Password.Equals(confirmepasswordtxt.Password))
                {
                    if (MainWindow.langue == 0)
                    {
                        errLbl.Content = "Vérifier votre mot de passe !";
                    }
                    else
                    {
                        errLbl.Content = "تأكد من كلمة المرور";
                    }
                    return;
                }
                else
                {
                    savePassword();
                    Home.mainFrame.Content = new Settings();
                }
            }
            else
            {
                // !LogIN.LoggedUser.Password.Equals("")
                if (!oldPasswordtxt.Password.Equals(LogIN.LoggedUser.Password))
                {
                    if (MainWindow.langue == 0)
                    {
                        errLbl.Content = "Vérifier votre ancient mot de passe !";
                    }
                    else
                    {
                        errLbl.Content = "تأكد من كلمة المرور السابقة";
                    }
                    return;
                }
                else
                {
                    if (!passwordtxt.Password.Equals(confirmepasswordtxt.Password))
                    {
                        if (MainWindow.langue == 0)
                        {
                            errLbl.Content = "Vérifier votre mot de passe !";
                        }
                        else
                        {
                            errLbl.Content = "تأكد من كلمة المرور";
                        }
                        return;
                    }
                    else
                    {
                        savePassword();
                        Home.mainFrame.Content = new Settings();
                    }
                }
            }
        }

        private void changePassClick(object sender, RoutedEventArgs e)
        {
            if (LogIN.LoggedUser.Password.Equals(""))
                oldstack.Visibility = Visibility.Collapsed;
            else
                oldstack.Visibility = Visibility.Visible;
        }

        private void savePassword()
        {
            //save the new password in the database
            string userDbconnString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\UtilisateurBDD.mdf;Integrated Security=True";
            SqlConnection userDbConnection = new SqlConnection(userDbconnString);
            SqlCommand cmdUpdate;
            try
            {
                if (userDbConnection.State == ConnectionState.Closed)
                    userDbConnection.Open();

                string queryUpdate = "UPDATE Utilisateur SET Password = '" + passwordtxt.Password + "'" +
                                     "WHERE UtilisateurID = '" + LogIN.LoggedUser.UtilisateurID + "'";

                cmdUpdate = new SqlCommand(queryUpdate, userDbConnection);
                cmdUpdate.CommandType = CommandType.Text;
                cmdUpdate.ExecuteNonQuery();

                if (userDbConnection.State == ConnectionState.Open)
                    userDbConnection.Close();

            }
            catch (Exception)
            {
                if (userDbConnection.State == ConnectionState.Open)
                    userDbConnection.Close();

                MessageBox.Show("Error has accured in settings save Password !");
            }
        }

        private void DeleteDialogueOpenClick(object sender, RoutedEventArgs e)
        {
            dialogueHost.Children.Clear();
            dialogueHost.Height = 170;
            StackPanel deletemsg = new StackPanel();
            deletemsg.Orientation = Orientation.Horizontal;
            deletemsg.VerticalAlignment = VerticalAlignment.Top;
            deletemsg.HorizontalAlignment = HorizontalAlignment.Center;
            deletemsg.Margin = new Thickness(0, 40, 0, 0);

            MaterialDesignThemes.Wpf.PackIcon icon = new MaterialDesignThemes.Wpf.PackIcon();
            icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Dangerous;
            icon.Foreground = Brushes.Red;
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Height = 30;
            icon.Width = 30;

            TextBlock deleteMessage = new TextBlock();
            deleteMessage.FontSize = 14;
            deleteMessage.VerticalAlignment = VerticalAlignment.Center;
            deleteMessage.Margin = new Thickness(20, 0, 0, 0);

            Button yes = new Button();
            yes.Background = Brushes.Red;
            yes.BorderBrush = null;
            yes.Click += new RoutedEventHandler(this.DeleteClick);

            Button no = new Button();
            no.Foreground = Brushes.LightBlue;
            no.Background = null;
            no.BorderBrush = null;
            no.Command = MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand;
            no.Click += new RoutedEventHandler(this.noRefresh);

            StackPanel yesorno = new StackPanel();
            yesorno.Orientation = Orientation.Horizontal;
            yesorno.Height = 40;
            yesorno.VerticalAlignment = VerticalAlignment.Bottom;
            yesorno.HorizontalAlignment = HorizontalAlignment.Right;
            yesorno.Margin = new Thickness(10);


            if (MainWindow.langue == 0)
            {
                deleteMessage.Text = "Est-ce-que vous etes\nsur de supprimer votre compte ?";
                yes.Content = "Oui";
                no.Content = "Non";
            }
            else
            {
                deleteMessage.Text = "هل انت متأكد من حذف حسابك ؟";
                yes.Content = "نعم";
                no.Content = "لا";
            }

            yesorno.Children.Add(no);
            yesorno.Children.Add(yes);
            deletemsg.Children.Add(icon);
            deletemsg.Children.Add(deleteMessage);
            dialogueHost.Children.Add(deletemsg);
            dialogueHost.Children.Add(yesorno);
        }
        /// <summary>
        /// Delete the user from Utilisateur database
        /// </summary>
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            string connectionToUser = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\UtilisateurBDD.mdf;Integrated Security=True";
            SqlConnection userConn = new SqlConnection(connectionToUser);
            SqlCommand deleteCMD;
            string deleteQuery = "DELETE FROM Utilisateur WHERE UtilisateurID = '" + LogIN.LoggedUser.UtilisateurID + "'";

            if (userConn.State == ConnectionState.Closed)
                userConn.Open();

            using (userConn)
            {
                try
                {
                    deleteCMD = new SqlCommand(deleteQuery, userConn);
                    deleteCMD.CommandType = CommandType.Text;
                    deleteCMD.ExecuteNonQuery();

                    if (userConn.State == ConnectionState.Open)
                        userConn.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error in delete account !");
                    if (userConn.State == ConnectionState.Open)
                        userConn.Close();
                }
            }
            //To delete saved data of the user ! 
            deleteUserSave();
            //Go back to login
            if (MainWindow.langue == 0)
            {
                MessageBox.Show("L'utilisateur " + LogIN.LoggedUser.UtilisateurID + " a été supprimer !");
            }
            else
            {
                MessageBox.Show(" لقد تم حفد حساب" + LogIN.LoggedUser.UtilisateurID + "");
            }

            MainWindow.quizFrame.Content = new LogIN();
        }
        /// <summary>
        /// Delete all saved data from the user
        /// </summary>
        private void deleteUserSave()
        {
            string connStringToSave = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\Trace\Save.mdf;Integrated Security=True";
            SqlConnection connectionToSave = new SqlConnection(connStringToSave);
            string queryNote = "DROP TABLE " + LogIN.LoggedUser.UtilisateurID + "Trace";
            string queryTrace = "DROP TABLE " + LogIN.LoggedUser.UtilisateurID + "NoteTest";

            SqlCommand deleteNote;
            SqlCommand deleteTrace;

            if (connectionToSave.State == ConnectionState.Closed)
                connectionToSave.Open();
            using (connectionToSave)
            {
                try
                {
                    deleteNote = new SqlCommand(queryNote, connectionToSave);
                    deleteNote.CommandType = CommandType.Text;
                    deleteNote.ExecuteNonQuery();

                    deleteTrace = new SqlCommand(queryTrace, connectionToSave);
                    deleteTrace.CommandType = CommandType.Text;
                    deleteTrace.ExecuteNonQuery();

                    if (connectionToSave.State == ConnectionState.Open)
                        connectionToSave.Close();
                }
                catch (Exception)
                {
                    if (connectionToSave.State == ConnectionState.Open)
                        connectionToSave.Close();
                    MessageBox.Show("Error in delete saved table");
                }
            }
        }
        private void noRefresh(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Settings();
        }
        
        private void toggleTheme_Checked(object sender, RoutedEventArgs e)
        {
            setTheme(true);
            LogIN.LoggedUser.Theme = true;

            GradientStop color1 = new GradientStop();
            GradientStop color2 = new GradientStop();

            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            linearGradientBrush.StartPoint = new Point(0, 0);
            linearGradientBrush.EndPoint = new Point(1, 1);

            color1.Offset = 0;
            color1.Color = Color.FromRgb(14, 115, 134);
            linearGradientBrush.GradientStops.Add(color1);

            color2.Offset = 1;
            color2.Color = Color.FromRgb(0, 200, 238);
            linearGradientBrush.GradientStops.Add(color2);

            MainWindow.getInstance().Background = linearGradientBrush;
        }

        private void toggleTheme_Unchecked(object sender, RoutedEventArgs e)
        {
            setTheme(false);
            LogIN.LoggedUser.Theme = false;
            GradientStop color1 = new GradientStop();
            GradientStop color2 = new GradientStop();

            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            linearGradientBrush.StartPoint = new Point(0, 0);
            linearGradientBrush.EndPoint = new Point(1, 1);

            color1.Offset = 0;
            color1.Color = Color.FromRgb(134, 14, 14);
            linearGradientBrush.GradientStops.Add(color1);

            color2.Offset = 1;
            color2.Color = Color.FromRgb(223, 119, 119);
            linearGradientBrush.GradientStops.Add(color2);

            MainWindow.getInstance().Background = linearGradientBrush;          
        }

        private void setTheme(bool themeChanged)
        {
            string connectionToUser = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\UtilisateurBDD.mdf;Integrated Security=True";
            SqlConnection userConn = new SqlConnection(connectionToUser);
            SqlCommand deleteCMD;
            string themeQuery = "UPDATE Utilisateur SET theme = '" + themeChanged.ToString() + "' WHERE UtilisateurID = '" + LogIN.LoggedUser.UtilisateurID + "'";

            if (userConn.State == ConnectionState.Closed)
                userConn.Open();

            using (userConn)
            {
                try
                {
                    deleteCMD = new SqlCommand(themeQuery, userConn);
                    deleteCMD.CommandType = CommandType.Text;
                    deleteCMD.ExecuteNonQuery();

                    if (userConn.State == ConnectionState.Open)
                        userConn.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error set theme !");
                    if (userConn.State == ConnectionState.Open)
                        userConn.Close();
                }
            }
        }

        private void backclick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new PagePrincipale();
        }
    }
}
