﻿using System;
using System.Linq;
using System.Data.Entity;
using System.Collections;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        using (var ctx = new DatabaseContext())
        {
            InitializeDB(ctx);

            Console.WriteLine("Clientes nacidos antes del año 2000 y sus compras ordenadas por fecha");
            Console.WriteLine("=====================================================================");

            var customersData = ctx.Customers
                .Where(c => c.DateOfBirth.Year < 2000)
                .Select(customer => new
                {
                    Customer = customer,
                    Purchases = ctx.Purchases
                        .Where(p => p.CustomerId == customer.CustomerId)
                        .OrderByDescending(p => p.PurchaseDateUTC)
                        .ThenByDescending(p => p.Total)
                        .ToList()
                })
                .ToList();

            foreach (var data in customersData)
            {
                var customer = data.Customer;
                int age = GetAge(customer.DateOfBirth);
                Console.WriteLine($"{customer.FullName} (Edad: {age} años)");
                Console.WriteLine("========================================");

                decimal totalAmount = 0;

                foreach (var purchase in data.Purchases)
                {
                    var localDate = purchase.PurchaseDateUTC.AddHours(-3);
                    string datePurchase = localDate.ToString("dd/MM/yyyy");

                    
                    Console.WriteLine("{0,-12} ---- {1,3} {2,10:N0}", datePurchase, "$", purchase.Total);

                    totalAmount += purchase.Total;
                }

                Console.WriteLine("---------------------------------------");
                Console.WriteLine("TOTAL: {0,10:C0}", totalAmount);
                Console.WriteLine("========================================\n");
            }

            // Opción para salir
            Console.WriteLine("Escriba 'salir' para cerrar el script.");
            while (Console.ReadLine().ToLower() != "salir")
            {
                Console.WriteLine("Escriba 'salir' para cerrar el script.");
            }
        }
    }



    public static int GetAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;
        if (today.Month < dateOfBirth.Month || (today.Month == dateOfBirth.Month && today.Day < dateOfBirth.Day))
        {
            age--;
        }
        return age;
    }


    public static void InitializeDB(DatabaseContext ctx)
    {
        if (ctx.Customers.Count() == 0)
        {
            ctx.Customers.Add(new Customer() { CustomerId = 1, FullName = "Sanchez Mario", DateOfBirth = new DateTime(1985, 10, 18) });
            ctx.Customers.Add(new Customer() { CustomerId = 2, FullName = "Gimenez Pedro", DateOfBirth = new DateTime(2010, 01, 10) });
            ctx.Customers.Add(new Customer() { CustomerId = 3, FullName = "Gomez Ricardo", DateOfBirth = new DateTime(1993, 11, 25) });
            ctx.Customers.Add(new Customer() { CustomerId = 4, FullName = "Araujo María", DateOfBirth = new DateTime(2009, 12, 2) });
    
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1001, PurchaseDateUTC = new DateTime(2021, 2, 2, 15, 22, 35), Total = 255m, CustomerId = 1 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1002, PurchaseDateUTC = new DateTime(2021, 2, 7, 12, 07, 45), Total = 888m, CustomerId = 3 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1003, PurchaseDateUTC = new DateTime(2021, 2, 9, 9, 00, 10), Total = 672m, CustomerId = 1 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1004, PurchaseDateUTC = new DateTime(2021, 1, 2, 10, 12, 32), Total = 1000m, CustomerId = 1 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1005, PurchaseDateUTC = new DateTime(2021, 1, 4, 2, 25, 55), Total = 56m, CustomerId = 2 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1006, PurchaseDateUTC = new DateTime(2021, 1, 7, 3, 12, 57), Total = 75m, CustomerId = 2 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1007, PurchaseDateUTC = new DateTime(2021, 1, 12, 1, 17, 12), Total = 987m, CustomerId = 3 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1008, PurchaseDateUTC = new DateTime(2021, 1, 15, 8, 55, 00), Total = 12000m, CustomerId = 3 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1009, PurchaseDateUTC = new DateTime(2021, 1, 25, 10, 43, 10), Total = 1m, CustomerId = 3 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1010, PurchaseDateUTC = new DateTime(2021, 2, 2, 17, 32, 22), Total = 100m, CustomerId = 4 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1011, PurchaseDateUTC = new DateTime(2021, 2, 2, 15, 22, 35), Total = 256m, CustomerId = 1 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1012, PurchaseDateUTC = new DateTime(2021, 2, 7, 12, 07, 45), Total = 887m, CustomerId = 3 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1013, PurchaseDateUTC = new DateTime(2021, 2, 9, 9, 00, 10), Total = 673m, CustomerId = 1 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1014, PurchaseDateUTC = new DateTime(2021, 1, 12, 1, 17, 12), Total = 987m, CustomerId = 3 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1015, PurchaseDateUTC = new DateTime(2021, 1, 15, 8, 55, 00), Total = 12000m, CustomerId = 3 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1016, PurchaseDateUTC = new DateTime(2021, 1, 25, 10, 43, 10), Total = 1m, CustomerId = 3 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1017, PurchaseDateUTC = new DateTime(2021, 1, 25, 12, 43, 10), Total = 111m, CustomerId = 3 });
            ctx.Purchases.Add(new Purchase() { PurchaseId = 1018, PurchaseDateUTC = new DateTime(2021, 1, 25, 16, 43, 10), Total = 10m, CustomerId = 3 });
            ctx.SaveChanges();
        }

    }
}

public class Purchase
{
    public int PurchaseId { get; set; }
    public DateTime PurchaseDateUTC { get; set; }
    public Decimal Total { get; set; }
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}

public class Customer
{
    public int CustomerId { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class DatabaseContext : DbContext
{
    public DatabaseContext() : base()
    {

    }

    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Customer> Customers { get; set; }
}
