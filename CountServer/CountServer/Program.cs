using System.Xml.Linq;

namespace CountServer{
    
    internal class Program
    {
        private static void Main(string[] args)
        {

            ///Результат работы методов с доп параметрами

            //string writerName = "writer0";
            //Thread writerthread = new(lambda => CountService.AddToCount(new Writer(writerName), 7));
            //writerthread.Start();
            //Thread.Sleep(1000);
            //for (int i = 1; i < 12; i++)
            //{
            //    string name = "name" + i.ToString();
            //    Thread myThread = new(lambda => CountService.GetCount(new Customer(name)));
            //    myThread.Start();
            //    if (i % 3 == 0)
            //    {
            //        writerName = "writer" + i.ToString();
            //        Thread writerThread1 = new(lambda => CountService.AddToCount(new Writer(writerName), i));
            //        writerThread1.Start();
            //    }
            //}

            ///Результат работы с программным ожиданием читателей, пока запишется Count

            string writerName = "writer0";
            Thread writerthread = new(lambda => CountService.AddToCount(new Writer(writerName), 7));
            writerthread.Start();
            Thread.Sleep(1500);
            for (int i = 1; i < 12; i++)
            {
                string name = "name" + i.ToString();
                Thread myThread = new(lambda => CountService.GetCount(new Customer(name), new object()));
                myThread.Start();
                if (i % 3 == 0)
                {
                    writerName = "writer" + i.ToString();
                    Thread writerThread1 = new(lambda => CountService.AddToCount(new Writer(writerName), i));
                    writerThread1.Start();
                }
            }


            ///Результат работы методов без доп параметров как в задании

            //Thread writerthread = new(lambda => CountService.AddToCount(7));
            //writerthread.Start();
            //Thread.Sleep(1000);
            //for (int i = 1; i < 12; i++)
            //{
            //    Thread myThread = new(lambda => CountService.GetCount());
            //    myThread.Start();
            //    if (i % 3 == 0)
            //    {
            //        Thread writerThread1 = new(lambda => CountService.AddToCount(i));
            //        writerThread1.Start();
            //    }
            //}

            Console.ReadLine();
        }
    }
}