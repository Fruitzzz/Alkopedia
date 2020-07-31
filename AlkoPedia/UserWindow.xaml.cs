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
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        Style p_title;
        Style p_element;
        Style p_image;
        ControlTemplate p_btn;
        int count;
        string name;
        public UserWindow()
        {
            InitializeComponent();
            name = string.Empty;
            count = 0;
            p_btn = paste_btn.Template;
            p_title = paste_title.Style;
            p_element = paste_el.Style;
            p_image = paste_img1.Style;
           // DeleteCopies();
        }
        public UserWindow(string name)
        {
            InitializeComponent();
            count = 0;
            this.name = name;
            user_entry_text.Text += name;
            user_nentry.Visibility = Visibility.Hidden;
            ConfBtn.Visibility = Visibility.Hidden;
            user_entry.Visibility = Visibility.Visible;
            p_btn = paste_btn.Template;
            p_title = paste_title.Style;
            p_element = paste_el.Style;
            p_image = paste_img1.Style;
            //DeleteCopies();
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

        /// <summary>
        /// /////////////
        /// </summary>
        private void ClearPanel()
        {
            for (int i = 0; i != count; ++i)
            {
                UnregisterName("id_" + i);
            }
            count = 0;
            drink_list.Children.RemoveRange(0, drink_list.Children.Count);
        }
        private void Button_Click_Favorites(object sender, RoutedEventArgs e)
        {
            ClearPanel();
            string[] favorites;
            try
            {
                favorites = GetFavorites();
                if (favorites != null)
                {
                    using (DrinkContext db = new DrinkContext())
                    {

                        List<Drink> drink = db.Drinks.ToList();
                        foreach (string el in favorites)
                        {
                            StackPanel main = new StackPanel();
                            StackPanel child1 = new StackPanel();
                            StackPanel child2 = new StackPanel();

                            Button button = new Button();
                            Image image1 = new Image();
                            Image image2 = new Image();
                            List<TextBlock> info = new List<TextBlock>() { new TextBlock(), new TextBlock(), new TextBlock() };
                            Drink cur_drink = drink.Find(book => book.Id == Convert.ToInt32(el.ToString(), fromBase: 10));
                            List<string> temp = new List<string>() { cur_drink.Title, cur_drink.Ingredients, cur_drink.Id.ToString() };

                            SetPropeties(main, child1, child2, info[2], image1, image2, info[0], info[1], button);
                            button.Name = "btn_" + count;
                            info[2].Name = "id_" + count;

                            for (int i = 0; i != info.Count; ++i)
                            {
                                info[i].Text = temp[i];
                                child1.Children.Add(info[i]);
                            }
                            if (string.IsNullOrEmpty(cur_drink.User))
                            {
                                image1.Source = new ImageSourceConverter().ConvertFrom("../../Images/Cocktails/" + cur_drink.Id.ToString() + "_1.jpg") as ImageSource;
                                image2.Source = new ImageSourceConverter().ConvertFrom("../../Images/Cocktails/" + cur_drink.Id.ToString() + "_2.jpg") as ImageSource;
                            }
                            button.Name = "btn_" + count;

                            RegisterName("id_" + count, child1.Children[2]);
                            child1.Children.Add(button);
                            child2.Children.Add(image1);
                            child2.Children.Add(image2);
                            main.Children.Add(child1);
                            main.Children.Add(child2);
                            drink_list.Children.Add(main);
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            user_scroll.Visibility = Visibility.Hidden;
            drink_scroll.Visibility = Visibility.Visible;
            SearchToBackSwap();
        }
        private void SetPropeties(StackPanel main, StackPanel child1, StackPanel child2, TextBlock id, Image image1, Image image2, TextBlock title, TextBlock el, Button button)
        {
            main.Orientation = Orientation.Horizontal;
            main.Margin = new Thickness(0, 20, 0, 0);

            child1.Width = 500;

            child2.Margin = new Thickness(30, -40, 0, 0);
            child2.Orientation = Orientation.Horizontal;

            title.Style = p_title;
            el.Style = p_element;

            id.Height = 0;
            id.Opacity = 0;

            button.Click += Button_Click_ShowDrink;
            button.Content = "Больше..."; 
            button.Template = p_btn;

            image1.Margin = new Thickness(0, 30, 0, 0);
            image2.Margin = new Thickness(50, 30, 0, 0);
            image1.Style = p_image;
            image2.Style = p_image;
        }
        private string[] GetFavorites()
        {
            string[] favorites;
            using (UserContext db = new UserContext())
            {
                List<User> users = db.Users.ToList();
                if (!string.IsNullOrEmpty(users.Find(el => el.Name == name).Drinks))
                    favorites = users.Find(el => el.Name == name).Drinks.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                else
                    favorites = null;
            }
            return favorites;
        }
        private void SearchToBackSwap()
        {
            Search.Margin = new Thickness(160, 0, 0, 0);
            back_btn.Visibility = Visibility.Visible;
        }
        private void Button_Click_ShowDrink(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                TextBlock a;
                if (button.Name[button.Name.Length - 2] == '_')
                    a = button.FindName("id_" + button.Name[button.Name.Length - 1]) as TextBlock;
                else
                    a = button.FindName("id_" + button.Name[button.Name.Length - 2] + button.Name[button.Name.Length - 1]) as TextBlock;
                int id = Convert.ToInt32(a.Text, fromBase: 10);
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
                drink_scroll.Visibility = Visibility.Hidden;
                Search.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
        }
        private void CheckFavorites(int id)
        {
            using (UserContext db = new UserContext())
            {
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(db.Users.ToList().Find(el => el.Name == name).Drinks))
                {

                    string[] favorites = db.Users.ToList().Find(el => el.Name == name).Drinks.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (favorites.Contains(id.ToString()))
                    {
                        in_fav.Visibility = Visibility.Visible;
                        not_fav_btn.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    in_fav.Visibility = Visibility.Hidden;
                    not_fav_btn.Visibility = Visibility.Visible;
                }
            }
        }
        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            if (drink_scroll.Visibility == Visibility.Visible)
            {
                drink_scroll.Visibility = Visibility.Hidden;
                user_scroll.Visibility = Visibility.Visible;
                Search.Margin = new Thickness(0, 0, 0, 0);
                back_btn.Visibility = Visibility.Hidden;
                user.Children.RemoveRange(0, user.Children.Count);
                ClearPanel();
                ShowUsers();
            }
            if (selected_cocktail.Visibility == Visibility.Visible)
            {
                selected_cocktail.Visibility = Visibility.Hidden;
                drink_scroll.Visibility = Visibility.Visible;
                Search.Visibility = Visibility.Visible;
            }
        }
        private void Button_Click_In_Favorite(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    using (UserContext db = new UserContext())
                    {
                        db.Users.First(el => el.Name == name).Drinks += select_id.Text + ";";
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowUsers();
        }
        private void ShowUsers ()
        {
            try
            {
                using (UserContext db = new UserContext())
                {
                    List<User> users = db.Users.ToList();
                    foreach (User el in users)
                    {
                        StackPanel panel = new StackPanel();
                        List<TextBlock> blocks = new List<TextBlock>() { new TextBlock(), new TextBlock(), new TextBlock() };
                        List<string> temp = CheckNull(el);
                        Button button = new Button();
                        SetPropeties(panel, blocks[0], blocks[1], blocks[2], button);

                        button.Name = "btn_" + count;
                        blocks[2].Name = "id_" + count;

                        for (int i = 0; i != blocks.Count; ++i)
                        {
                            if (i != 1)
                                blocks[i].Text = temp[i];
                            else
                                blocks[i].Text += "Количество коктейлей: " + temp[i];
                            panel.Children.Add(blocks[i]);
                        }
                        RegisterName("id_" + count, panel.Children[2]);
                        panel.Children.Add(button);
                        user.Children.Add(panel);
                        ++count;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
        }
        private List<string> CheckNull(User user)
        {
            if (string.IsNullOrEmpty(user.Drinks))
                return new List<string>() { user.Name, 0.ToString(), user.Id.ToString() };
            string[] drinks = user.Drinks.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            return new List<string>() { user.Name, drinks.Length.ToString(), user.Id.ToString() };
        }
        private void SetPropeties(StackPanel panel, TextBlock name, TextBlock elements, TextBlock id, Button button)
        {
            panel.HorizontalAlignment = HorizontalAlignment.Left;
            panel.Width = 500;
            panel.Margin = new Thickness(0, 10, 0, 0);

            name.Style = p_title;
            name.FontSize = 64;

            elements.Style = p_element;
            elements.FontSize = 50;
            elements.Width = 300;
            elements.Margin = new Thickness(10, -15, 0, 0);

            id.Height = 0;
            id.Opacity = 0;

            button.Template = exit_btn.Template;
            button.FontSize = 24;
            button.Height = 32;
            button.Width = 150;
            button.Content = "Увидеть избранное";
            button.Margin = new Thickness(-80, 0, 0, 0);
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.Click += Button_Click_FavoriteList;
        }
        private void Button_Click_FavoriteList(object sender, RoutedEventArgs e)
        {
            drink_list.Children.RemoveRange(0, drink_list.Children.Count);
            try
            {
                Button send_button = sender as Button;
                TextBlock a = send_button.FindName("id_" + send_button.Name[send_button.Name.Length - 1]) as TextBlock;
                int id = Convert.ToInt32(a.Text, fromBase: 10);
                string[] favorites;
                using (UserContext db = new UserContext())
                {
                    if (!string.IsNullOrEmpty(db.Users.First(el => el.Id == id).Drinks))
                        favorites = db.Users.First(el => el.Id == id).Drinks.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    else
                        favorites = null;
                }
                if (favorites != null)
                {
                    using (DrinkContext db = new DrinkContext())
                    {
                        List<Drink> drinks = db.Drinks.ToList();
                        foreach (string el in favorites)
                        {
                            StackPanel main = new StackPanel();
                            StackPanel child1 = new StackPanel();
                            StackPanel child2 = new StackPanel();

                            Button button = new Button();
                            Image image1 = new Image();
                            Image image2 = new Image();
                            List<TextBlock> info = new List<TextBlock>() { new TextBlock(), new TextBlock(), new TextBlock() };
                            Drink cur_drink = drinks.Find(drink => drink.Id == Convert.ToInt32(el.ToString(), fromBase: 10));
                            List<string> temp = new List<string>() { cur_drink.Title, cur_drink.Ingredients, cur_drink.Id.ToString() };

                            SetPropeties(main, child1, child2, info[2], image1, image2, info[0], info[1], button);
                            button.Name = "btn_" + count;
                            info[2].Name = "id_" + count;

                            for (int i = 0; i != info.Count; ++i)
                            {
                                info[i].Text = temp[i];
                                child1.Children.Add(info[i]);
                            }
                            if (string.IsNullOrEmpty(cur_drink.User))
                            {
                                image1.Source = new ImageSourceConverter().ConvertFrom("../../Images/Cocktails/" + cur_drink.Id.ToString() + "_1.jpg") as ImageSource;
                                image2.Source = new ImageSourceConverter().ConvertFrom("../../Images/Cocktails/" + cur_drink.Id.ToString() + "_2.jpg") as ImageSource;
                            }
                            button.Name = "btn_" + count;

                            RegisterName("id_" + count, child1.Children[2]);
                            child1.Children.Add(button);
                            child2.Children.Add(image1);
                            child2.Children.Add(image2);
                            main.Children.Add(child1);
                            main.Children.Add(child2);
                            drink_list.Children.Add(main);
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
            user_scroll.Visibility = Visibility.Hidden;
            drink_scroll.Visibility = Visibility.Visible;
            SearchToBackSwap();
        }
        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            string order = search_text.Text.ToLower();
            if (!string.IsNullOrEmpty(order))
            {
                using (DrinkContext db = new DrinkContext())
                {
                    List<Drink> drinks = db.Drinks.ToList();
                    if (drinks.Exists(el => el.Title.ToLower() == order))
                    {
                        Drink drink = drinks.Find(el => el.Title.ToLower() == order);
                        select_name.Text = drink.Title;
                        select_elem.Text = drink.Ingredients;
                        select_recipe.Text = drink.Cooking;
                        select_id.Text = drink.Id.ToString();
                        if (string.IsNullOrEmpty(drink.User))
                        {
                            select_img1.Source = new ImageSourceConverter().ConvertFrom("../../Images/Cocktails/" + drink.Id.ToString() + "_1.jpg") as ImageSource;
                            select_img2.Source = new ImageSourceConverter().ConvertFrom("../../Images/Cocktails/" + drink.Id.ToString() + "_2.jpg") as ImageSource;
                        }
                        CheckFavorites(drink.Id);
                        selected_cocktail.Visibility = Visibility.Visible;
                        drink_scroll.Visibility = Visibility.Hidden;
                        Search.Visibility = Visibility.Hidden;
                    }
                    else
                        MessageBox.Show("Nothing found");
                }

            }
            else
                MessageBox.Show("Row is empty");
        }
    }
}
