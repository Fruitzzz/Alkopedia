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
using System.Windows.Shapes;
using System.Data.Entity;
using AlkoPedia.Alkopediadb;

namespace AlkoPedia
{
    /// <summary>
    /// Логика взаимодействия для WarningWindow.xaml
    /// </summary>
    public partial class WarningWindow : Window
    {
        string name;
        public WarningWindow()
        {
            InitializeComponent();
            name = string.Empty;
        }
        public WarningWindow(string name)
        {
            InitializeComponent();
            this.name = name;
            user_entry_text.Text += name;
            user_nentry.Visibility = Visibility.Hidden;
            ConfBtn.Visibility = Visibility.Hidden;
            user_entry.Visibility = Visibility.Visible;
        }
        private void Button_Click_Theme(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_Main(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(name))
            {
                MainWindow window = new MainWindow();
                window.Show();
            }
            else
            {
                MainWindow window = new MainWindow(name);
                window.Show();
            }
            Close();
        }

        private void Button_Click_Users(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(name))
            {
                UserWindow window = new UserWindow();
                window.Show();
            }
            else
            {
                UserWindow window = new UserWindow(name);
                window.Show();
            }
            Close();
        }

        private void Button_Click_Warning(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(name))
            {
                WarningWindow window = new WarningWindow();
                window.Show();
            }
            else
            {
                WarningWindow window = new WarningWindow(name);
                window.Show();
            }
            Close();
        }

        private void Button_Click_HardDrinks(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(name))
            {
                HardDrinkWindow window = new HardDrinkWindow();
                window.Show();
            }
            else
            {
                HardDrinkWindow window = new HardDrinkWindow(name);
                window.Show();
            }
            Close();
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    List<User> users = db.Users.ToList();
                    if (users.Exists(user => user.Name == cur_name.Text && user.Password == cur_pword.Password))
                    {
                        name = cur_name.Text;
                        user_entry_text.Text += cur_name.Text;
                        user_nentry.Visibility = Visibility.Hidden;
                        user_entry.Visibility = Visibility.Visible;
                        ConfBtn.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        MessageBox.Show("Invalid name or password");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            name = string.Empty;
            user_nentry.Visibility = Visibility.Visible;
            ConfBtn.Visibility = Visibility.Visible;
            user_entry.Visibility = Visibility.Hidden;
            user_entry_text.Text = "Welcome, ";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using(UserContext db = new UserContext())
            {
                var users = db.Users.ToList();
                u_amount.Text += users.Count.ToString();
            }
            using(DrinkContext db = new DrinkContext())
            {
                var drinks = db.Drinks.ToList();
                d_amount.Text += drinks.Count.ToString();
            }
        }
    }
}
