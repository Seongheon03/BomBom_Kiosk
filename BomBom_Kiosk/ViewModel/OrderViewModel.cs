using BomBom_Kiosk.Model;
using BomBom_Kiosk.Service;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BomBom_Kiosk.ViewModel
{
    public class OrderViewModel : BindableBase
    {
        private DBManager dbManager = new DBManager();
        private readonly int MAX_DRINK_NUM = 9;

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
                CurrentDrinkPage = 1;
            }
        }

        private List<Drink> _drinks = new List<Drink>();
        public List<Drink> Drinks
        {
            get => _drinks;
            set => SetProperty(ref _drinks, value);
        }

        private ObservableCollection<Drink> _displayDrinks = new ObservableCollection<Drink>();
        public ObservableCollection<Drink> DisplayDrinks
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

        private int _currentDrinkPage = 1;
        public int CurrentDrinkPage
        {
            get => _currentDrinkPage;
            set
            {
                _currentDrinkPage = value;
                SetDisplayDrinks();
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

        public delegate void LoadingEventHandler(object sender, bool isLoading);
        public event LoadingEventHandler LoadingAction;
        #endregion

        public ICommand NextDrinkCommand { get; set; }
        public ICommand PrevDrinkCommand { get; set; }
        public ICommand IncreaseCommand { get; set; }
        public ICommand DecreaseCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand RemoveAllCommand { get; set; }

        public OrderViewModel()
        {
            InitCommand();
            //InitData();
        }

        private void InitCommand()
        {
            NextDrinkCommand = new DelegateCommand(() => CurrentDrinkPage++);
            PrevDrinkCommand = new DelegateCommand(() => CurrentDrinkPage--);
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
            if (MessageBox.Show("모두 삭제하시겠습니까?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                OrderList.Clear();
                TotalPrice = 0;
            }
        }

        public async void InitData()
        {
            LoadingAction?.Invoke(this, true);

            await SetDrinks();
            SetCategories();
            SetDisplayDrinks();
            //await Task.Run(async () =>
            //{
            //    await SetDrinks();
            //    SetCategories();
            //    SetDisplayDrinks();
            //});

            LoadingAction?.Invoke(this, false);
        }

        private async Task SetDrinks()
        {
            Drinks = await Task.Run(() => dbManager.GetDrinks());
            //Drinks.Add(new Drink { Idx = 1, Name = "a", Price = 1000, DiscountPrice = 100, Category = ECategory.COFFEE });
            //Drinks.Add(new Drink { Idx = 2, Name = "a", Price = 2000, DiscountPrice = 100, Category = ECategory.COFFEE });
            //Drinks.Add(new Drink { Idx = 3, Name = "a", Price = 3000, DiscountPrice = 100, Category = ECategory.COFFEE });
            //Drinks.Add(new Drink { Idx = 4, Name = "a", Price = 4000, DiscountPrice = 100, Category = ECategory.COFFEE });
            //Drinks.Add(new Drink { Idx = 5, Name = "a", Price = 5000, DiscountPrice = 100, Category = ECategory.COFFEE });
            //Drinks.Add(new Drink { Idx = 6, Name = "a", Price = 6000, DiscountPrice = 100, Category = ECategory.COFFEE });
            //Drinks.Add(new Drink { Idx = 7, Name = "a", Price = 6000, DiscountPrice = 100, Category = ECategory.COFFEE });
            //Drinks.Add(new Drink { Idx = 8, Name = "a", Price = 6000, DiscountPrice = 100, Category = ECategory.COFFEE });
            //Drinks.Add(new Drink { Idx = 9, Name = "a", Price = 6000, DiscountPrice = 100, Category = ECategory.COFFEE });
            //Drinks.Add(new Drink { Idx = 10, Name = "a", Price = 6000, DiscountPrice = 100, Category = ECategory.COFFEE });
        }

        private void SetCategories()
        {
            Categories.Add(new Category { Type = ECategory.COFFEE, Name = "커피", Page = GetCategoryPage(ECategory.COFFEE) });
            Categories.Add(new Category { Type = ECategory.SMOOTHIE, Name = "스무디", Page = GetCategoryPage(ECategory.SMOOTHIE) });
            Categories.Add(new Category { Type = ECategory.ADE, Name = "에이드", Page = GetCategoryPage(ECategory.ADE) });
        }

        private int GetCategoryPage(ECategory category)
        {
            int count = Drinks.Where(x => x.Category == category).Count();
            int page = (int)(count - 0.1) / MAX_DRINK_NUM + 1;

            return page;
        }

        private void SetDisplayDrinks()
        {
            ECategory selectedCategory = (ECategory)SelectedCategory;
            int page = Categories.Where(x => x.Type == selectedCategory).FirstOrDefault().Page;

            if (CurrentDrinkPage > page)
            {
                CurrentDrinkPage = 1;
            }
            else if (CurrentDrinkPage < 1)
            {
                CurrentDrinkPage = page;
            }

            int maxIndex = CurrentDrinkPage * MAX_DRINK_NUM;
            var drinks = Drinks.Where(x => x.Category == selectedCategory).ToList();

            if (DisplayDrinks.Count != 0)
            {
                DisplayDrinks.Clear();
            }

            for (int i = maxIndex - MAX_DRINK_NUM; i < maxIndex; i++)
            {
                try
                {
                    DisplayDrinks.Add(drinks[i]);
                }
                catch
                {
                    return;
                }
            }
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
