using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IProductService productService= new ProductManager(new FakeBankService());
            productService.Sell(new Product { Id = 1, Name = "laptop", Price = 1000 },new Customer { Id= 1, IsStudent=true,Name="Can", IsOfficer=false});
        }
    }
    class Customer:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsStudent { get; set; }
        public bool IsOfficer { get; set; }
    } 

    interface IEntity
    {

    }

    class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }  

    }

    class ProductManager : IProductService
    {
        private IBankService _bankService;

        public ProductManager(IBankService bankService)
        {
            _bankService = bankService;
        }

        public void Sell()
        {
            
        }

        public void Sell(Product product, Customer customer)
        {
            decimal price = product.Price;
            if (customer.IsStudent)
            {
                price = product.Price *(decimal) 0.90;
            }

           price = _bankService.ConvertRate(new CurrencyRate { Currency = 1, Price = price });
            Console.WriteLine(price);
            Console.ReadLine(); 

        }
    }

    internal interface IProductService
    {
        void Sell(Product product, Customer customer);
    }

    class FakeBankService:IBankService
    {

        public decimal ConvertRate(CurrencyRate currencyRate)
        {
            return currencyRate.Price / (decimal) 5.30;

        }
    }

    internal interface IBankService
    {
        decimal ConvertRate(CurrencyRate currencyRate);
    }

    class CurrencyRate
    {
        public decimal Price { get; set; }
        public int Currency { get; set; }
    }
}