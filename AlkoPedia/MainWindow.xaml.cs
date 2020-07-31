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
using AlkoPedia.Alkopediadb;
namespace AlkoPedia
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string name;
        bool light;
        public MainWindow()
        {
            InitializeComponent();
            light = true;
        }
        public MainWindow(string name)
        {
            InitializeComponent();
            this.name = name;
            user_entry_text.Text += name;
            user_nentry.Visibility = Visibility.Hidden;
            ConfBtn.Visibility = Visibility.Hidden;
            user_entry.Visibility = Visibility.Visible;
            light = true;
        }
        private void Button_Click_Theme_Dark(object sender, RoutedEventArgs e)
        {
            try
            {
                theme_btn_dark.Visibility = Visibility.Visible;
                theme_btn_light.Visibility = Visibility.Hidden;
                var uri = new Uri("Themes/Dark.xaml", UriKind.Relative);
                ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
                light = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Click_Theme_Light(object sender, RoutedEventArgs e)
        {
            try
            {
                theme_btn_dark.Visibility = Visibility.Hidden;
                theme_btn_light.Visibility = Visibility.Visible;
                var uri = new Uri("Themes/Light.xaml", UriKind.Relative);
                ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
                light = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                        if (selected_cocktail.Visibility == Visibility.Visible)
                            CheckFavorites(Convert.ToInt32(select_id.Text, fromBase: 10));
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
            in_fav.Visibility = Visibility.Hidden;
            not_fav_btn.Visibility = Visibility.Visible;
        }

        private void Button_Click_Registation(object sender, RoutedEventArgs e)
        {
            using (UserContext db = new UserContext())
            {
                List<User> users = db.Users.ToList();
                if (reg_name.Text == string.Empty || reg_pword.Password == string.Empty)
                    MessageBox.Show("Name or password are empty");
                else if (!users.Exists(user => user.Name == reg_name.Text))
                {
                    db.Users.Add(new User { Name = reg_name.Text, Password = reg_pword.Password });
                    db.SaveChanges();
                    reg_name.Clear();
                    reg_pword.Clear();
                }
                else if (users.Exists(user => user.Name == reg_name.Text))
                {
                    reg_name.Clear();
                    reg_pword.Clear();
                    MessageBox.Show("This user already registered");
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DrinkContext db = new DrinkContext())
                {
                    List<Drink> drinks = db.Drinks.ToList();
                    int rand = new Random().Next(1, drinks.Count);
                    Drink drink = drinks.Find(el => el.Id == rand);
                    main_title.Text = drink.Title;
                    main_el.Text = drink.Ingredients;
                    main_id.Text = drink.Id.ToString();
                    if (string.IsNullOrEmpty(drink.User))
                    {
                        main_img1.Source = new ImageSourceConverter().ConvertFrom("../../Images/Cocktails/" + drink.Id.ToString() + "_1.jpg") as ImageSource;
                        main_img2.Source = new ImageSourceConverter().ConvertFrom("../../Images/Cocktails/" + drink.Id.ToString() + "_2.jpg") as ImageSource;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void Button_Click_Select_Cocktail(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(main_id.Text, fromBase: 10);
                using (DrinkContext db = new DrinkContext())
                {
                    List<Drink> drinks = db.Drinks.ToList();
                    Drink drink = drinks.Find(el => el.Id == id);
                    select_name.Text = drink.Title;
                    select_elem.Text = drink.Ingredients;
                    select_recipe.Text = drink.Cooking;
                    select_id.Text = drink.Id.ToString();
                    if (string.IsNullOrEmpty(drink.User))
                    {
                        select_img1.Source = new ImageSourceConverter().ConvertFrom("../../Images/Cocktails/" + drink.Id.ToString() + "_1.jpg") as ImageSource;
                        select_img2.Source = new ImageSourceConverter().ConvertFrom("../../Images/Cocktails/" + drink.Id.ToString() + "_2.jpg") as ImageSource;
                    }
                }
                CheckFavorites(id);
                selected_cocktail.Visibility = Visibility.Visible;
                main.Visibility = Visibility.Hidden;
                back_btn.Visibility = Visibility.Visible;
                theme_btn_light.Visibility = Visibility.Hidden;
                theme_btn_dark.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void CheckFavorites(int id)
        {
            if (!string.IsNullOrEmpty(name))
            {
                using (UserContext db = new UserContext())
                {
                    string favorites = GetFavorites();
                    if (favorites.Contains(id.ToString()))
                    {
                        in_fav.Visibility = Visibility.Visible;
                        not_fav_btn.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        in_fav.Visibility = Visibility.Hidden;
                        not_fav_btn.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                in_fav.Visibility = Visibility.Hidden;
                not_fav_btn.Visibility = Visibility.Visible;
            }
        }
        private string GetFavorites()
        {
            string favorites = string.Empty;
            using (UserContext db = new UserContext())
            {
                List<User> users = db.Users.ToList();
                favorites = users.Find(el => el.Name == name).Drinks;
            }
            return favorites;
        }
        private void Button_Click_In_Favorite(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    using (UserContext db = new UserContext())
                    {
                        db.Users.First(el => el.Name == name).Drinks += select_id.Text;
                        db.SaveChanges();
                    }
                    in_fav.Visibility = Visibility.Visible;
                    not_fav_btn.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Authorisation Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            selected_cocktail.Visibility = Visibility.Hidden;
            main.Visibility = Visibility.Visible;
            back_btn.Visibility = Visibility.Hidden;
            if (light)
                theme_btn_light.Visibility = Visibility.Visible;
            else
                theme_btn_dark.Visibility = Visibility.Visible;
        }
    }
}
