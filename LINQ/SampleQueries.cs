// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();
		
		[Category("Lesson")]
		[Title("Where - Task 1")]
		[Description("Выдайте список всех клиентов, чей суммарный оборот (сумма всех заказов) превосходит некоторую величину X.")]
		public void Linq1()
		{
			var rand = new Random();
			int sum = rand.Next(1000,3000);

			var customer = dataSource.Customers
				.Where(x => x.Orders.Sum(o => o.Total) > sum)
				.ToList();

			ObjectDumper.Write(sum);
			foreach (var c in customer)
			{
				ObjectDumper.Write($"Id-{c.CustomerID} , Name-{c.CompanyName} ,Sum-{c.Orders.Sum(o=>o.Total)}");
			}

		}
		
		[Category("Lesson")]
		[Title("Where - Task 2")]
		[Description("Для каждого клиента составьте список поставщиков, находящихся в той же стране и том же городе")]
		
		public void Linq2()
		{
			var customers = dataSource.Customers
				.Join(dataSource.Suppliers,
					c => c.Country,
					s => s.Country,
					(c, s) => new {cus = c, sup = s})
				.Where(x => x.cus.City == x.sup.City)
				.Select(x => new
				{
					Id = x.cus.CustomerID,
					CompanyName = x.cus.CompanyName,
					County = x.cus.Country,
					City = x.cus.City,
					SupplierName = x.sup.SupplierName
				})
				.ToList();

			foreach (var cus in customers)
			{
				ObjectDumper.Write(cus);
			}

		}
		
		[Category("Lesson")]
		[Title("Where - Task 3")]
		[Description(". Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X")]
		
		public void Linq3()
		{
			var rand = new Random();
			int sum = rand.Next(5000, 15000);

			var customer = dataSource.Customers
				.Where(x => x.Orders.Where(o => o.Total > sum).Any())
				.ToList();

			foreach (var c in customer)
			{
				ObjectDumper.Write($"Id-{c.CustomerID} , Name-{c.CompanyName} ,Sum-{c.Orders.Sum(o=>o.Total)}");
			}

		}

		[Category("Lesson")]
		[Title("Where - Task 4")]
		[Description("Выдайте список клиентов с указанием, начиная с какого месяца какого года они стали клиентами")]

		public void Linq4()
		{
			var customers = dataSource.Customers
				.Where(x=>x.Orders.Length > 0)
				.Select(x => new
				{
					Id = x.CustomerID,
					Name = x.CompanyName,
					Date = x.Orders.Select(o => o.OrderDate).Min()
				})
				.ToList();

			foreach (var cus in customers)
			{
				ObjectDumper.Write($"Id = {cus.Id} Name = {cus.Name}, Year = {cus.Date.Year}, Month = {cus.Date.Month}");
			}
		}
		
		[Category("Lesson")]
		[Title("Where - Task 6")]
		[Description("Укажите всех клиентов, у которых указан нецифровой почтовый код или не заполнен регион или в телефоне не указан код оператора")]

		public void Linq6()
		{
			var customers = dataSource.Customers
				.Where(x => string.IsNullOrEmpty(x.Region) 
				            || !(x.PostalCode?.All(a=> char.IsDigit(a)) ?? false)
				            || (x.Phone.IndexOf('(') < 0 && x.Phone.IndexOf(')') < 0))
				.Select(x=> new
				{
					Id = x.CustomerID,
					Name = x.CompanyName,
					Region = x.Region != null ? x.Region : "Не найден регион",
					Phone = x.Phone,
					PostalCode = x.PostalCode
				})
				.ToList();

			foreach (var cus in customers)
			{
				ObjectDumper.Write(cus);
			}
		}
		
		[Category("Lesson")]
		[Title("Where - Task 7")]
		[Description("Сгруппируйте все продукты по категориям, внутри – по наличию на складе, внутри последней группы отсортируйте по стоимости")]

		public void Linq7()
		{
			var product = dataSource.Products
				.GroupBy(x => new
				{
					x.Category,
					x.UnitsInStock,
					x.UnitPrice
				})
				.Select(g=> new
				{
					Category = g.Key.Category,
					UnitsInStock = g.Key.UnitsInStock,
					Price = g.Key.UnitPrice
				})
				.ToList();

			foreach (var prod in product)
			{
				ObjectDumper.Write(prod);
			}
		}
		
		[Category("Lesson")]
		[Title("Where - Task 8")]
		[Description("Сгруппируйте товары по группам «дешевые», «средняя цена», «дорогие». Границы каждой группы задайте сами;")]

		public void Linq8()
		{
			var product = dataSource.Products
				.GroupBy(x => x.UnitPrice < 10 ? "Дешевая"
					: x.UnitPrice < 40 ? "Средняя" : "Дорогая");

			foreach (var prod in product)
			{
				ObjectDumper.Write(prod.Key);
				foreach (var c in prod)
				{
					ObjectDumper.Write($"ProductName = {c.ProductName}, Price = {c.UnitPrice}");
				}
			}
		}
		
		[Category("Lesson")]
		[Title("Where - Task 9")]
		[Description("Рассчитайте среднюю прибыльность каждого города (среднюю сумму заказа по всем клиентам из данного города) и среднюю интенсивность (среднее количество заказов, приходящееся на клиента из каждого города)")]

		public void Linq9()
		{
			var customers = dataSource.Customers.GroupBy(x => x.City)
				.Select(z => new
				{
					City = z.Key,
					Profit = z.Average(x => x.Orders.Sum(y => y.Total)).ToString("F2"),
					Intensiveness = z.Average(x => x.Orders.Length).ToString(("F2"))
				})
				.ToList();

			foreach (var cus in customers)
			{
				ObjectDumper.Write(cus);
			}
		}
	}
}
