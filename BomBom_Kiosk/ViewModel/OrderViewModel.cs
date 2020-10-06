using BomBom_Kiosk.Model;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;

namespace BomBom_Kiosk.ViewModel
{
    public class OrderViewModel : BindableBase
    {
        #region Property
        private List<Category> _categories = new List<Category>();
        public List<Category> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        private int _selectedCategory = 0;
        public int SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                SetProperty(ref _selectedCategory, value);
                SetDisplayDrinks();
            }
        }

        private List<Drink> _drinks = new List<Drink>();
        public List<Drink> Drinks
        {
            get => _drinks;
            set => SetProperty(ref _drinks, value);
        }

        private List<Drink> _displayDrinks = new List<Drink>();
        public List<Drink> DisplayDrinks
        {
            get => _displayDrinks;
            set => SetProperty(ref _displayDrinks, value);
        }

        private Drink _selectedDrink;
        public Drink SelectedDrink
        {
            get => _selectedDrink;
            set
            {
                _selectedDrink = value;
                AddToOrderList();
            }
        }

        private List<Drink> _orderList = new List<Drink>();
        public List<Drink> OrderList
        {
            get => _orderList;
            set => SetProperty(ref _orderList, value);
        }
        #endregion

        public OrderViewModel()
        {
            SetCategories();
            SetDrinks();
        }

        private void SetCategories()
        {
            Categories.Add(new Category { Type = ECategory.COFFEE, Name = "커피" });
            Categories.Add(new Category { Type = ECategory.SMOOTHIE, Name = "스무디" });
            Categories.Add(new Category { Type = ECategory.ADE, Name = "에이드" });
        }

        private void SetDrinks()
        {
            //MySqlConnection conn = new MySqlConnection(App.connStr);
            //MySqlCommand cmd = conn.CreateCommand();

            //string query = "SELECT * FROM menu";
            //cmd.CommandText = query;

            //try
            //{
            //    conn.Open();
            //}
            //catch
            //{
            //    MessageBox.Show("DB 연결이 되지 않았습니다.");
            //    return;
            //}

            //MySqlDataReader reader = cmd.ExecuteReader();

            //while(reader.Read())
            //{
            //    Drink drink = new Drink();
            //    drink.Name = reader["name"].ToString();
            //    drink.Price = int.Parse(reader["price"].ToString());
            //    drink.Image = reader["image"].ToString();
            //    drink.Category = (ECategory)int.Parse(reader["type"].ToString());

            //    Drinks.Add(drink);
            //}

            Drinks.Add(new Drink { Image = "https://cafebombom.co.kr/images/sub/menu08_img_200730_1.png", Name = "옛날빙수", Price = 3000, Category = ECategory.COFFEE });
            Drinks.Add(new Drink { Name = "b", Price = 4500, Category = ECategory.COFFEE });
            Drinks.Add(new Drink { Name = "c", Price = 100, Category = ECategory.COFFEE });
            Drinks.Add(new Drink { Name = "d", Price = 30000, Category = ECategory.ADE });
            Drinks.Add(new Drink { Name = "d", Price = 30000, Category = ECategory.SMOOTHIE });
            Drinks.Add(new Drink { Name = "d", Price = 30000, Category = ECategory.SMOOTHIE });
            Drinks.Add(new Drink { Name = "d", Price = 30000, Category = ECategory.SMOOTHIE });
            Drinks.Add(new Drink { Name = "d", Price = 30000, Category = ECategory.SMOOTHIE });
            Drinks.Add(new Drink { Name = "d", Price = 30000 });
        }

        private void SetDisplayDrinks()
        {
            ECategory selectedCategory = (ECategory)SelectedCategory;

            DisplayDrinks = Drinks.Where(x => x.Category == selectedCategory).ToList();
        }

        private void AddToOrderList()
        {

        }
    }
}
