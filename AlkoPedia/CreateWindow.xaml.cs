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
    /// Логика взаимодействия для CreateWindow.xaml
    /// </summary>
    public partial class CreateWindow : Window
    {
        int count;
        string name;
        public CreateWindow()
        {
            InitializeComponent();
            name = string.Empty;
            count = 0;
        }
        public CreateWindow(string name)
        {
            InitializeComponent();
            count = 0;
            this.name = name;
            user_entry_text.Text += name;
            user_nentry.Visibility = Visibility.Hidden;
            ConfBtn.Visibility = Visibility.Hidden;
            user_entry.Visibility = Visibility.Visible;
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

        //////////////

        private void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(create_lvl.Text) || string.IsNullOrEmpty(create_recipe.Text) || string.IsNullOrEmpty(create_title.Text) || string.IsNullOrEmpty(create_elements.Text))
                MessageBox.Show("One of the lines is empty.");
            else if (create_lvl.Text.Length == 1 && (create_lvl.Text == "0" || create_lvl.Text == "1" || create_lvl.Text == "2" || create_lvl.Text == "3"))
            {
                using (DrinkContext db = new DrinkContext())
                {
                    if (!db.Drinks.ToList().Exists(el => el.Title.ToLower() == create_title.Text.ToLower()))
                    {
                        Drink drink = new Drink { Title = create_title.Text, Lvl = Convert.ToInt32(create_lvl.Text, fromBase: 10), Ingredients = create_elements.Text, Cooking = create_recipe.Text, User = "u" };
                        db.Drinks.Add(drink);
                        db.SaveChanges();
                        create_btn.Visibility = Visibility.Hidden;
                        created.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MessageBox.Show("A cocktail with the same name already exists.");
                    }
                }
            }
            else
                MessageBox.Show("Invalid LVL");
            ClearFields();
        }
        private void ClearFields ()
        {
            create_title.Clear();
            create_recipe.Clear();
            create_lvl.Clear();
            create_elements.Clear();
        }
    }
}
