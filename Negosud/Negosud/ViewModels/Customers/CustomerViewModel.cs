using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using NegosudModel.Dto;
using NegosudModel.Entities;
using System.Windows;

namespace Negosud.ViewModels.Customers
{
    public class CustomerViewModel : BaseViewModel
    {
        private readonly CustomerService _customerService;

        public CustomerDto Customer { get; }

        public Func<Task>? RefreshCustomersAction { get; set; }
        private IAsyncRelayCommand? _deleteCommand;

        public CustomerViewModel(CustomerDto customer, CustomerService customerService)
        {
            Customer = customer;
            _customerService = customerService;
        }

        public IAsyncRelayCommand DeleteCommand => _deleteCommand ??= new AsyncRelayCommand(async () =>
        {
            try
            {
                bool success = await _customerService.DeleteCustomerAsync(Customer.Id);
                if (success)
                {
                    Console.WriteLine($"Customer {Customer.Id} successfully deleted.");
                    RefreshCustomersAction?.Invoke();
                }
                else
                {
                    Console.WriteLine($"Customer {Customer.Id} not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while deleting customer {Customer.Id}: {ex.Message}");
            }
        });
    }
}
