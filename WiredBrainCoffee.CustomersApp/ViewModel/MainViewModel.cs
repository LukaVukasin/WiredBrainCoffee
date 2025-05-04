using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class MainViewModel
    {
        public readonly ICustomerDataProvider _customerDataProvider;
        public ObservableCollection<Customer> Customers { get; } = new();

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
                    Customers.Add(customer);
                }
            }
        }
    }
}
