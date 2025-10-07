using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab_linq
{
    class mycomparer : IComparable<string>
    {
        public string x { get; set; }
        public int CompareTo(string y) {
            return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
        }

    }
    internal class Program
    {

        static void Main(string[] args)
        {
            //ListGenerator is static ==no object created of it 

            var cls = ListGenerator.CustomerList;
            var pls = ListGenerator.ProductList;

            /*
            //1
            var res = pls.Where(p => p.UnitsInStock == 0);
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------");

            //2
            var res2 = pls.Where(p => p.UnitsInStock == 0 && p.UnitPrice > 3);
            foreach (var item in res2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------");

            //3
            int i = 0;
            string[] Arr = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var res3 = Arr.Where(num => num.Length < i++);
            foreach (var item in res3)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------");

            */


            /*
            //4
            var res4 = pls.First(p => p.UnitsInStock == 0);
            Console.WriteLine(res4);

            Console.WriteLine("-------------------------------------------------------");

            //5
            var res5 = pls.FirstOrDefault(p => p.UnitPrice > 100);
            Console.WriteLine(res5);

            Console.WriteLine("-------------------------------------------------------");


            //6 Arr implements the ienumrable 

            
            int[] Arr2 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var res6 = Arr2.Where(n=>n>5).Skip(1).Take(1);
            //.Take(1) says “give me a sequence containing one element”, not “give me one element”.
            foreach (var item in res6)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------------------------------");
            */


            /*
            var r1 = pls.Select(p => p.ProductName[0]).Distinct();
            var r2 = cls.Select(c => c.CompanyName[0]).Distinct();
            var res = r1.Concat(r2);
            foreach (var item in res)
            {
                Console.Write(item + " ");
            }


            var r1 = pls.Select(p => p.ProductName[0]);
            var r2 = cls.Select(c => c.CompanyName[0]);
            var res = r1.Except(r2);
            foreach (var item in res)
            {
                Console.Write(item + " ");
            }



            var r1 = pls.Select(p => p.ProductName.Substring(p.ProductName.Length - 4));
            var res = r1.Concat(cls.Select(c => c.CompanyName.Substring(c.CompanyName.Length - 4)));
            foreach (var item in res)
            {
                Console.Write(item + " ");
            }
            */


            /*
            int[] Arr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var res = Arr.Count(n => n % 2 == 1);
            Console.WriteLine(res);
            Console.WriteLine(Arr.Count());


            var res1 = cls.Select(c => c.CompanyName).Zip(cls.Select(c => c.Orders?.Length ?? 0)); //if has length return it  , else return 0 
            foreach (var item in res1)
            {
                Console.WriteLine(item);
            }

            string[] lines = File.ReadAllLines("dictionary_english.txt");
            //long sum = 0;
            //var tot = lines.Select(l => l.Length).Select(n=> sum = n+sum);
            //foreach (var items in tot)
            //{
            //    Console.WriteLine(items);
            //}

            var tot = lines.Sum(l => l.Length);
            Console.WriteLine(tot);

            var totu = pls.GroupBy(p => p.Category)  //g1    name 
                                                     //p1    name  unitinstock cate .... 
                       .Select(g =>  new {gk=  g.Key ,tot_in = g.Sum(p => p.UnitsInStock) } ); //must write new {} variables
            foreach (var items in totu)
            {
                Console.WriteLine(items);
            }


            string[] lines2 = File.ReadAllLines("dictionary_english.txt");
            var mn = lines2.Min(l => l.Length); //if longest ==> use Max 
            Console.WriteLine(mn);


            //9. Get the products with the cheapest price in each category (Use Let)
            // i do not use let 
            var t = pls.GroupBy(p => p.Category)
                    .Select(g => new { gk = g.Key, mn_price = g.Min(p => p.UnitPrice) });
            foreach (var items in t)
            {
                Console.WriteLine(items);
            }


            var mn_price = pls.Min(p =>p.UnitPrice);
            Console.WriteLine(mn_price);


            var mn_per_cat = pls.GroupBy(p => p.Category)
                             .Select(g => new { gk = g.Key, mn_per_cat = g.Max(p => p.UnitPrice) });
            foreach (var items in mn_per_cat)
            {
                Console.WriteLine(items);
            }

            var avg = pls.Average(p => p.UnitPrice); //per all products if per group will be like min , max :D
            Console.WriteLine(avg);

            */


            /*
            var sorted = pls.OrderBy(p => p.ProductName);
            foreach(var i in sorted)
            {
                Console.WriteLine(i);
            }

            //-------\\
            string[] Arr = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var sorted2 = Arr.OrderBy(a => a , StringComparer.OrdinalIgnoreCase); //new comparer 


            var sorted3 = pls.OrderBy(p => -p.UnitsInStock);
            foreach (var i in sorted3)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("-----------------------------------");


            string[] Arr2 = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var sorted4 = Arr2.OrderBy(n => n.Length)
                            .ThenBy(a => a, StringComparer.Ordinal);
            foreach (var i in sorted4)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("***********************************************-");


            var sorted5 = pls.OrderBy(p => p.Category)
                            .ThenBy(p => -p.UnitPrice);
            foreach (var i in sorted5)
            {
                Console.WriteLine(i);
            }


            string[] Arr3 = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var rev_res = Arr3.Where(n => n[1] == 'i')
                            .Reverse();
            foreach (var i in rev_res)
            {
                Console.WriteLine(i);
            }
            */


            /*
            //first 3 orders in wash  at the whole customers level (first mean order creation time ) 

            //WA 1
            //wrong as i can not loop on an order inside customer 
            .TakeWhile(c => {
                 if (sum < 3)
                 { foreach (var o in c.Orders)
                     { sum++;
                         if (sum < 3) return true;
                         else return false;
                     }
                 }
                 else { return false; }
             });

            

            //WA 2
            //3 orders objects along the whole customers but not sorted by order date  
            
            int sum = 0;
            var top_3_wash2 = cls
                .Where(c => c.City == "Washington")
                .Where(c => c.Orders.Length != 0)
                
                .TakeWhile(c => {
                    if (sum < 3)
                    {
                        sum += c.Orders.Length;
                        return true;       // include this customer
                    }
                    else
                    {
                        return false;      // stop taking more customers
                    }
                });

            int counter = 0;
            foreach (var cus in top_3_wash2)
            {
                foreach (var o in cus.Orders)
                {
                    if (counter < 3)
                    {
                        Console.WriteLine(o.ToString());
                        counter++;
                    }
                }
            }
            

            //Right sol , i need to use selectmany 
            var first3Orders = cls.Where(c => c.City == "Washington")   // customers in Washington
                                  .Where(c => c.Orders.Length != 0)     // only those with orders
                                  .SelectMany(c => c.Orders)            // selecct + flatten all orders into one sequence
 before flatten [
  [Order101, Order102],
  [Order201, Order202]
]
//after flatten [Order101, Order102, Order201, Order202]
                                  .OrderBy(o => o.OrderDate)            // order by creation date
                                  .Take(3);                             // take the first 3 orders

            //printing 
            foreach (var o in first3Orders)
            {
                Console.WriteLine(o);
            }


            var skip2first3Orders = cls.Where(c => c.City == "Washington")   // customers in Washington
                                 .Where(c => c.Orders.Length != 0)     // only those with orders
                                 .SelectMany(c => c.Orders)            // flatten all orders into one sequence
                                 .OrderBy(o => o.OrderDate)            // order by creation date
                                 .Skip(2).Take(3);                            

            foreach (var o in skip2first3Orders)
            {
                Console.WriteLine(o);
            }

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            int i = 0;
            var res = numbers.TakeWhile(n => n >= i++);

            foreach (var h in res)
            {
                Console.WriteLine(h);
            }


            int idx = 0;
            int[] numbers2 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var res2 = numbers2.SkipWhile(n => n >= idx++);
            foreach (var h in res2)
            {
                Console.WriteLine(h);
            }
            */


            /*
            var names = pls.Select(p => p.ProductName.ToUpper());
            foreach (var i in names)
            {
                Console.WriteLine(i);
            }

            var someprob = pls.Select(p => new { new_n = p.ProductName, p.UnitPrice, pn = p.ProductName });
            foreach (var i in someprob)
            {
                Console.WriteLine(i);
            }

            int[] Arr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var res = Arr.Select((n, idx) =>
            {
                if (n == idx) return $"{n}: true";
                else return $"{n}: false";
            });

            foreach (var i in res)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("---------------------------------------------");

            //all combination between a , b 
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var comb = numbersA.SelectMany(a => numbersB, (a, b) =>
            {
                if (a == b) return $"{a} = {b}";
                else if (a > b) return $"{a} > {b}";
                else return $"{a} <{b}";
            });    
            foreach (var i in comb)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("----------------------------------------------");

            var ord = cls.SelectMany(c => c.Orders )
                       .Where(o=>o.Total <500);

            foreach (var i in ord)
            {
                Console.WriteLine(i);
            }

            */


            /*
            string[] lines = File.ReadAllLines("dictionary_english.txt");
            var res = lines.Contains("ei");


            //2.Return a grouped a list of products only for categories
            //    that have at least one product that is out of stock.

            var result = pls.GroupBy(p => p.Category)
                           .Where(g => g.Any(p => p.UnitsInStock == 0))
                           .Select(g=>g.Select(p=> new { p.ProductName ,p.UnitPrice}));//or select many to flat all groups

            foreach (var i in result)
            {
                foreach (var j in i)
                    Console.WriteLine(j);
            }
            Console.WriteLine("****************************");

            //3.Return a grouped a list of products only for categories that have
            //all of their products in stock.
            var cat_all_p_instock = pls.GroupBy(p => p.Category)
                                       .Where(g => g.All(p => p.UnitsInStock > 0))
                                       .SelectMany(g => g.Select(p => new { p.ProductName, p.UnitPrice }));

            foreach (var i in cat_all_p_instock)
            {
                    Console.WriteLine(i);
            }
            */

            
            /*
            //sol1 
            int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            var res = arr.GroupBy(n => n % 5)//keys [0,1,2,3,4]
                        .OrderBy(g => g.Key);

            foreach (var i in res)
            {
                Console.WriteLine($"numbers with remainder = {i.Key} when divided by 5 are :");
                foreach (var j in i)
                {
                    Console.Write(j + " ");
                }
                Console.WriteLine();
            }

            //sol2 
            int[] arr2 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            var res2 = arr2.GroupBy(n => n % 5)//keys [0,1,2,3,4]
                        .OrderBy(g => g.Key)   //Key = 0 → 5,10
                                               //Key = 1 → 1,6    
                                               //Key = 2 → 2,7
                        .Select(g => new { gk = g.Key, nums = g.Select(n => n) });//data selected i store in res2


            foreach (var i in res2)
            {
                Console.WriteLine($"numbers with remainder = {i.gk} when divided by 5 are :");
                foreach (var j in i.nums)
                {
                    Console.Write(j + " ");
                }
                Console.WriteLine();
            }

            string[] lines = File.ReadAllLines("dictionary_english.txt");
            var res = lines.GroupBy(w => w[0])
                            .Select(g => new {gk= g.Key, nums = g.Select(w => w) });
            foreach(var item in res)
            {
                foreach( var i in item.nums)
                {
                    Console.WriteLine(item.gk+ "  "+ i);
                }
            }
            */




        }
    }
        
}
