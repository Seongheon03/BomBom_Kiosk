using BomBom_Kiosk.Model;
using BomBom_Kiosk.Service;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace BomBom_Kiosk.ViewModel
{
    public class OrderViewModel : BindableBase
    {
        private DBManager dbManager = new DBManager();

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

                if (_selectedDrink != null)
                {
                    AddToOrderList();
                }
            }
        }

        private ObservableCollection<OrderedDrink> _orderList = new ObservableCollection<OrderedDrink>();
        public ObservableCollection<OrderedDrink> OrderList
        {
            get => _orderList;
            set => SetProperty(ref _orderList, value);
        }

        private int _totalPrice = 0;
        public int TotalPrice
        {
            get => _totalPrice;
            set => SetProperty(ref _totalPrice, value);
        }
        #endregion

        public ICommand IncreaseCommand { get; set; }
        public ICommand DecreaseCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand RemoveAllCommand { get; set; }

        public OrderViewModel()
        {
            InitCommand();
            SetCategories();
            SetDrinks();
            SetDisplayDrinks();
        }

        private void InitCommand()
        {
            IncreaseCommand = new DelegateCommand<int?>(IncreaseCount);
            DecreaseCommand = new DelegateCommand<int?>(DecreaseCount);
            RemoveCommand = new DelegateCommand<int?>(RemoveDrink);
            RemoveAllCommand = new DelegateCommand(RemoveAllDrink);
        }

        private void IncreaseCount(int? drinkIdx)
        {
            OrderedDrink orderedDrink = OrderList.Where(x => x.MenuIdx == drinkIdx).FirstOrDefault();

            orderedDrink.Count++;
            orderedDrink.TotalPrice = orderedDrink.Price * orderedDrink.Count;

            TotalPrice += orderedDrink.Price;
        }

        private void DecreaseCount(int? drinkIdx)
        {
            OrderedDrink orderedDrink = OrderList.Where(x => x.MenuIdx == drinkIdx).FirstOrDefault();

            if (orderedDrink.Count == 1)
            {
                OrderList.Remove(orderedDrink);
            }
            else
            {
                orderedDrink.Count--;
                orderedDrink.TotalPrice = orderedDrink.Price * orderedDrink.Count;
            }

            TotalPrice -= orderedDrink.Price;
        }

        private void RemoveDrink(int? drinkIdx)
        {
            OrderedDrink removeDrink = OrderList.Where(x => x.MenuIdx == drinkIdx).FirstOrDefault();

            OrderList.Remove(removeDrink);
            TotalPrice -= removeDrink.TotalPrice;
        }

        private void RemoveAllDrink()
        {
            OrderList.Clear();
            TotalPrice = 0;
        }

        private void SetCategories()
        {
            Categories.Add(new Category { Type = ECategory.COFFEE, Name = "커피" });
            Categories.Add(new Category { Type = ECategory.SMOOTHIE, Name = "스무디" });
            Categories.Add(new Category { Type = ECategory.ADE, Name = "에이드" });
        }

        private void SetDrinks()
        {
            Drinks = dbManager.GetDrinks();
            //Drinks.Add(new Drink { Idx = 1, Name = "a", Price = 1000, DiscountPrice = 100, Category = ECategory.ADE });
            //Drinks.Add(new Drink { Idx = 2, Name = "a", Price = 2000, DiscountPrice = 100, Category = ECategory.ADE });
            //Drinks.Add(new Drink { Idx = 3, Name = "a", Price = 3000, DiscountPrice = 100, Category = ECategory.ADE });
            //Drinks.Add(new Drink { Idx = 4, Name = "a", Price = 4000, DiscountPrice = 100, Category = ECategory.ADE });
            //Drinks.Add(new Drink { Idx = 5, Name = "a", Price = 5000, DiscountPrice = 100, Category = ECategory.ADE });
            //Drinks.Add(new Drink { Idx = 6, Name = "a", Price = 6000, DiscountPrice = 100, Category = ECategory.ADE });
        }

        private void SetDisplayDrinks()
        {
            ECategory selectedCategory = (ECategory)SelectedCategory;

            DisplayDrinks = Drinks.Where(x => x.Category == selectedCategory).ToList();
        }

        private void AddToOrderList()
        {
            if (OrderList.Where(x => x.MenuIdx == SelectedDrink.Idx).Count() == 0)
            {
                OrderList.Add(new OrderedDrink
                {
                    MenuIdx = SelectedDrink.Idx,
                    Name = SelectedDrink.Name,
                    Price = SelectedDrink.Price,
                    TotalPrice = SelectedDrink.Price
                });
            }

            IncreaseCount(SelectedDrink.Idx);
        }
    }
}
