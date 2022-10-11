using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio08
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();
        static void Main(string[] args)
        {
            Joining();
            Console.Read();
        }

        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery = from num in numbers
                           where (num % 2) == 0
                           select num;

            foreach (int num in numQuery)
            {
                Console.Write("{0,1}", num);
            }
        }

        static void LamdaIntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };


            var numQuery = numbers.Where(c => c % 2 == 0).Select(c => c);


            foreach (int num in numQuery)
            {
                Console.Write("{0,1}", num);
            }

        }

        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void LamdaDataSource()
        {
            var queryAllCustomers = context.clientes.Select(x => x);

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;
            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }


        static void LamdaFiltering()
        {
            var queryLondonCustomers = context.clientes.Where(x => x.Ciudad == "Londres")
                        .Select(x => x);

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        static void Ordering()
        {
            var queryLondonCUstomers = from cust in context.clientes
                                       where cust.Ciudad == "London"
                                       orderby cust.NombreContacto ascending
                                       select cust;

            foreach (var item in queryLondonCUstomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }

        static void LamdaOrdering()
        {
            var queryLondonCUstomers = context.clientes.Where(x => x.Ciudad == "London")
                                       .OrderBy(x => x.NombreContacto)
                                       .Select(x => x);

            foreach (var item in queryLondonCUstomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }

        static void Grouping()
        {
            var queryCustomerByCity = from cust in context.clientes
                                      group cust by cust.Ciudad;

            foreach (var customerGroup in queryCustomerByCity)
            {
                Console.WriteLine(customerGroup);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("   {0}", customer.NombreCompañia);
                }
            }
        }

        static void LamdaGrouping()
        {
            var queryCustomerByCity = context.clientes.GroupBy(x => x.Ciudad);


            foreach (var customerGroup in queryCustomerByCity)
            {
                Console.WriteLine(customerGroup);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("   {0}", customer.NombreCompañia);
                }
            }
        }

        static void Grouping2()
        {
            var custQuery = from cust in context.clientes
                            group cust by cust.Ciudad into custGroup
                            where custGroup.Count() > 2
                            orderby custGroup.Key
                            select custGroup;
            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void LamdaGrouping2()
        {
            var custQuery = context.clientes.GroupBy(x => x.Ciudad)
                            .Where(x => x.Count() > 2)
                            .OrderBy(x => x.Key)
                            .Select(x => x);

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void Joining()
        {
            var innetJoinQuery =
                from cust in context.clientes
                join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };

            foreach (var item in innetJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }

        }

        static void LamdaJoining()
        {
            var clients = context.clientes;
            var pedidos = context.Pedidos;

            var innetJoinQuery =
                clients.Join(pedidos, c =>
                c.idCliente, p => p.IdCliente,
                (c, p)
                => new { CustomerName = c.NombreCompañia, DistributorName = p.PaisDestinatario });


            foreach (var item in innetJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }

        }

    }
}
