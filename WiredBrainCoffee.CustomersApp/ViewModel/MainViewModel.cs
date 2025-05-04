using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public readonly ICustomerDataProvider _customerDataProvider;
        private CustomerItemViewModel? _selectedCustomer;
        public ObservableCollection<CustomerItemViewModel> Customers { get; } = new();

        //Caller name is here SelectedCustomer
        public CustomerItemViewModel? SelectedCustomer 
        {
            get { return _selectedCustomer; }
            set 
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    base.RaisePropertyChanged();
                    base.RaisePropertyChanged(nameof(IsCustomerSelected));
                }          
            }
        }

        public bool IsCustomerSelected => SelectedCustomer != null;

        public MainViewModel(ICustomerDataProvider customerDataProvider)
        {
            _customerDataProvider = customerDataProvider;
        }

        public async Task LoadAsync()
        {
            if (this.Customers.Any()) 
            {
                return;
            }

            var customers =  await _customerDataProvider.GetAllAsync();
            if (customers != null) 
            {
                foreach (var customer in customers)
                {
                    Customers.Add(new CustomerItemViewModel(customer));
                }
            }
        }
    }
}
