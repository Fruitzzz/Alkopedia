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
    /// Логика взаимодействия для HardDrinkWindow.xaml
    /// </summary>
    public partial class HardDrinkWindow : Window
    {
        Style p_title;
        Style p_element;
        Style p_image;
        int count;
        ControlTemplate p_btn;
        string name;
        public HardDrinkWindow()
        {
            InitializeComponent();
            count = 0;
            name = string.Empty;
            p_btn = paste_btn.Template;
            p_title = paste_title.Style;
            p_element = paste_el.Style;
            p_image = paste_img1.Style;
        }
        public HardDrinkWindow(string name)
        {
            InitializeComponent();
            count = 0;
            this.name = name;
            user_entry_text.Text += name;
            user_nentry.Visibility = Visibility.Hidden;
            ConfBtn.Visibility = Visibility.Hidden;
            user_entry.Visibility = Visibility.Visible;
            fav_btn.Visibility = Visibility.Visible;
            p_btn = paste_btn.Template;
            p_title = paste_title.Style;
            p_element = paste_el.Style;
            p_image = paste_img1.Style;
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
                        fav_btn.Visibility = Visibility.Visible;
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
            fav_btn.Visibility = Visibility.Hidden;
            in_fav.Visibility = Visibility.Hidden;
            not_fav_btn.Visibility = Visibility.Visible;

        }
        //////////////////////
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
            lvl_list.Visibility = Visibility.Hidden;
            drink_scroll.Visibility = Visibility.Visible;
            SearchToBackSwap();
        }
        private void ChosenLvl (int lvl)
        {
            ClearPanel();
            try
            {
                using(DrinkContext db = new DrinkContext())
                {
                    List<Drink> drinks = db.Drinks.ToList().FindAll(el => el.Lvl == lvl);
                    foreach(Drink el in drinks)
                    {
                        StackPanel main = new StackPanel();
                        StackPanel child1 = new StackPanel();
                        StackPanel child2 = new StackPanel();

                        Button button = new Button();
                        Image image1 = new Image();
                        Image image2 = new Image();
                        List<TextBlock> info = new List<TextBlock>() { new TextBlock(), new TextBlock(), new TextBlock() };
                        List<string> temp = new List<string>() { el.Title, el.Ingredients, el.Id.ToString() };

                        SetPropeties(main, child1, child2, info[2], image1, image2, info[0], info[1], button);
                        button.Name = "btn_" + count;
                        info[2].Name = "id_" + count;

                        for (int i = 0; i != info.Count; ++i)
                        {
                            info[i].Text = temp[i];
                            child1.Children.Add(info[i]);
                        }

                        if (string.IsNullOrEmpty(el.User))
                        {
                            image1.Source = new ImageSourceConverter().ConvertFrom("../../Images/Cocktails/" + el.Id.ToString() + "_1.jpg") as ImageSource;
                            image2.Source = new ImageSourceConverter().ConvertFrom("../../Images/Cocktails/" + el.Id.ToString() + "_2.jpg") as ImageSource;
                        }

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
            lvl_list.Visibility = Visibility.Hidden;
            drink_scroll.Visibility = Visibility.Visible;
        }
        private void SetPropeties (StackPanel main, StackPanel child1, StackPanel child2, TextBlock id, Image image1, Image image2, TextBlock title, TextBlock el, Button button)
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (name == string.Empty)
                fav_btn.Visibility = Visibility.Hidden;
        }
        private void ClearPanel()
        {
            for(int i = 0; i != count; ++i)
            {
                UnregisterName("id_" + i);
            }
            count = 0;
            drink_list.Children.RemoveRange(0, drink_list.Children.Count);
        }
        private void Button_Click_ShowDrink(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                TextBlock a = button.FindName("id_" + button.Name[button.Name.Length - 1]) as TextBlock;
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
            }
        }
        private void CheckFavorites (int id)
        {
            using (UserContext db = new UserContext())
            {
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(db.Users.ToList().Find(el => el.Name == name).Drinks))
                {

                    string[] favorites = GetFavorites();
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
                else
                {
                    in_fav.Visibility = Visibility.Hidden;
                    not_fav_btn.Visibility = Visibility.Visible;
                }
            }
            
        }
        private void Button_Click_ZeroLvl(object sender, RoutedEventArgs e)
        {
            ChosenLvl(0);
            SearchToBackSwap();
        }
        private void Button_Click_FirstLvl(object sender, RoutedEventArgs e)
        {
            ChosenLvl(1);
            SearchToBackSwap();
        }
        private void Button_Click_SecondLvl(object sender, RoutedEventArgs e)
        {
            ChosenLvl(2);
            SearchToBackSwap();
        }
        private void Button_Click_ThirdLvl(object sender, RoutedEventArgs e)
        {
            ChosenLvl(3);
            SearchToBackSwap();
        }
        private void SearchToBackSwap ()
        {
            Search.Margin = new Thickness(160, 0, 0, 0);
            back_btn.Visibility = Visibility.Visible;
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            if(drink_scroll.Visibility == Visibility.Visible)
            {
                drink_scroll.Visibility = Visibility.Hidden;
                lvl_list.Visibility = Visibility.Visible;
                Search.Margin = new Thickness(0, 0, 0, 0);
                back_btn.Visibility = Visibility.Hidden;
            }
            if(selected_cocktail.Visibility == Visibility.Visible)
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void create_btn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(name))
            {
                CreateWindow window = new CreateWindow();
                window.Show();
            }
            else
            {
                CreateWindow window = new CreateWindow(name);
                window.Show();
            }
            Close();
        }
    }
}
