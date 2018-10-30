using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone;
using System.Threading;

namespace Capstone
{
    public class VendingMenus
    {
        #region variables
        
        private const string _title = @"████████╗ █████╗ ███████╗████████╗██╗   ██╗    ███████╗███╗   ██╗ █████╗  ██████╗██╗  ██╗███████╗██╗
╚══██╔══╝██╔══██╗██╔════╝╚══██╔══╝╚██╗ ██╔╝    ██╔════╝████╗  ██║██╔══██╗██╔════╝██║ ██╔╝██╔════╝██║
   ██║   ███████║███████╗   ██║    ╚████╔╝     ███████╗██╔██╗ ██║███████║██║     █████╔╝ ███████╗██║
   ██║   ██╔══██║╚════██║   ██║     ╚██╔╝      ╚════██║██║╚██╗██║██╔══██║██║     ██╔═██╗ ╚════██║╚═╝
   ██║   ██║  ██║███████║   ██║      ██║       ███████║██║ ╚████║██║  ██║╚██████╗██║  ██╗███████║██╗
   ╚═╝   ╚═╝  ╚═╝╚══════╝   ╚═╝      ╚═╝       ╚══════╝╚═╝  ╚═══╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝";

        private string _pressR = "Press (r) to return to the previous menu";
        private List<Item> _purchaseList = new List<Item>();
        public VendingMachine _vm = null;

        #endregion

        #region constructor

        /// <summary>
        /// Constructs a VendingMenus object
        /// </summary>
        /// <param name="vm"></param>
        public VendingMenus(VendingMachine vm)
        {
            _vm = vm;
        }

        #endregion

        #region methods

        /// <summary>
        /// Displays the main menu
        /// </summary>
        public void MainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine($"\n{_title}\n");
                    Console.WriteLine($"{{  For Your Health  }}\n".PadLeft(57, ' '));
                    Console.WriteLine("(1) Display Items\n");
                    Console.WriteLine("(2) Purchase Items\n");
                    Console.WriteLine("(3) Walk Away From Vending Machine\n");

                    char input = Console.ReadKey().KeyChar;

                    if (input == '1')
                    {
                        DisplayMenu();
                    }
                    else if (input == '2')
                    {
                        PurchaseMenu();
                    }
                    else if (input == '3')
                    {
                        exit = true;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid selection!");
                        Console.ReadKey();
                    }
                }
                catch(Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Displays the Display Menu
        /// </summary>
        public void DisplayMenu()
        {
            

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\n " + _title + "\n");

                DisplayItems();

                Console.WriteLine($"\n{_pressR}");

                bool isLetterR = (('r' == Console.ReadKey().KeyChar) || ('R' == Console.ReadKey().KeyChar));

                if (isLetterR)
                {
                    exit = true;
                }
            }
        }

        /// <summary>
        /// Displays the Purchase Menu
        /// </summary>
        public void PurchaseMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\n" + _title + "\n");
                Console.WriteLine($"{{  Current Money In Machine: {_vm.MachineBalance.ToString("C")}  }}\n".PadLeft(66, ' '));
                Console.WriteLine("(1) Feed Money\n");
                Console.WriteLine("(2) Select Product\n");
                Console.WriteLine("(3) Finish Transaction\n\n");
                Console.WriteLine($"{_pressR}");

                char purchaseMenuInput = Console.ReadKey().KeyChar;

                if (purchaseMenuInput == '1')
                {
                    FeedMoneyMenu();
                }
                else if (purchaseMenuInput == '2')
                {
                    SelectProductMenu();
                }
                else if (purchaseMenuInput == '3')
                {
                    FinishTransactionMenu();
                }
                else if (purchaseMenuInput == 'r' || purchaseMenuInput == 'R')
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Not a valid selection!");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Displays the Feed Money Menu
        /// </summary>
        public void FeedMoneyMenu()
        {
            bool exit = false;
            while (!exit)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("\n" + _title);
                    Console.WriteLine("Select Amount of Money to Feed:\n");
                    Console.WriteLine("(1) $1\n(2) $2\n(3) $5\n(4) $10\n(5) $20");
                    Console.WriteLine($"\n{_pressR} when you are finished.\n");
                    Console.Write($"Current Money Provided: {_vm.MachineBalance.ToString("C")}");

                    char selection = Console.ReadKey().KeyChar;

                    if (selection.ToString().ToLower() == "r")
                    {
                        exit = true;
                    }
                    else
                    {
                        _vm.FeedMoney(selection);
                    }
                }
                catch (InvalidSelectionException)
                {
                    Console.WriteLine("\nInvalid Selection, please try again...");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Displays the Select Product Menu
        /// </summary>
        public void SelectProductMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\n" + _title + "\n");

                DisplayItems();

                Console.WriteLine("\nPress (r) to return to the previous menu");
                Console.Write("\nEnter Slot ID for the item you would like to purchase: ");
                
                string selection = Console.ReadLine();

                Item itemPurchased = _vm.Dispense(selection);

                bool enoughMoney = true;
                bool productExists = _vm.ProductExists(selection);
                bool soldOut = _vm.SoldOut(selection);

                if (!productExists && selection.ToLower() != "r")
                {
                    Console.WriteLine("Not a valid selection!");
                    Console.ReadKey();
                }
                else if (selection.ToLower() != "r")
                {
                    enoughMoney = _vm.MachineBalance >= itemPurchased.Price;
                }
                
                if (productExists && soldOut)
                {
                    Console.WriteLine("Selection is SOLD OUT.");
                    Console.WriteLine("\nPress any key to return to the Purchase Menu.");
                    Console.ReadKey();
                }
                else if (productExists && enoughMoney)
                {
                    _purchaseList.Add(itemPurchased);
                    _vm.UpdateBalance(selection);
                    itemPurchased.QtyUpdate();

                    Console.Clear();
                    Console.WriteLine($"\n              You purchased: {itemPurchased.Name}!!!\n");
                    Console.WriteLine(itemPurchased.ItemArt());
                    Console.WriteLine("\n\nPress any key to return to continue.");
                    Console.ReadKey();
                }
                else if (productExists && !enoughMoney)
                {
                    Console.WriteLine("Add more money to machine to purchase this item!");
                    Console.ReadKey();
                }
                else if (selection.ToLower() == "r")
                {
                    exit = true;
                }
            }
        }

        /// <summary>
        /// Displays Finish Transaction Menu
        /// </summary>
        public void FinishTransactionMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine($"\n [{_vm.FinishTransaction()}]\n");

                foreach (Item item in _purchaseList)
                {
                    Console.WriteLine($"     {item.MakeSound()}");
                }

                _purchaseList.RemoveRange(0, _purchaseList.Count);

                Console.WriteLine($"\n{_pressR}");
                Console.WriteLine("\nPress (w) to walk away from the vending machine");

                char selection = Console.ReadKey().KeyChar;

                if (selection == 'r' || selection == 'R')
                {
                    exit = true;
                }
                else if (selection == 'w' || selection == 'W')
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Not a valid selection!");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Displays Vending Machine inventory
        /// </summary>
        private void DisplayItems()
        {
            Console.WriteLine("Slot ID".PadRight(10, ' ') +
                              "Item Name".PadRight(21, ' ') +
                              "Price".PadRight(10, ' ') +
                              "Quantity".PadRight(10, ' '));

            Console.WriteLine("".PadRight(49, '-'));

            var itemList = _vm.GetItemList();
            foreach (Item item in itemList)
            {
                Console.WriteLine($"{item.Slot.PadRight(9, ' ')} " +
                                  $"{item.Name.PadRight(20, ' ')} " +
                                  $"{item.Price.ToString("C").PadRight(10, ' ')}" +
                                  $"{item.DisplayQty}");
            }
        }

        #endregion
    }
}
