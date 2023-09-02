using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CountServer
{
    /// <summary>
    /// Сервер со счетчиком
    /// </summary>
    internal static class CountService
    {
        static int Count = 0;
        static object locker = new();
        static bool lockedForRead = false;

        //Добавил один параметр типа Customer для наглядности работы
        public static void GetCount(Customer customer) {
            customer.Read();
            if (lockedForRead)
            {
                Console.WriteLine("{0} хотел получить доступ к Count, но Count записывается", customer.Name);
            }
            else
            {
                Console.WriteLine("{0} читает Count, Count равен: {1}", customer.Name, Count);
            }
        }

        //Если нам требуется, чтобы пользователь ждал программно, когда закончится запись Count
        public static void GetCount(Customer customer, object nothing)
        {
            customer.Read();

            while (lockedForRead)
            {
                Thread.Sleep(100);
            }
            Console.WriteLine("{0} читает Count, Count равен: {1}", customer.Name, Count);
        }

        //Добавил один параметр типа Writer для наглядности работы
        public static void AddToCount(Writer writer, int value)
        {
            //Не даем выстроить очередь писателей, которые не дадут читать читатетлям
            Thread.Sleep(1000);
            bool locked = false;
            writer.Write();
            try
            {
                Monitor.Enter(locker, ref locked);
                lockedForRead = locked;
                Console.WriteLine("{0} записывает Count, Count теперь равен {1} + {2} = {3}", writer.Name, Count, value, Count + value);
                Count += value;
                //Симулируем долгую запись
                Thread.Sleep(1000);
            }
            finally
            {
                if (locked) Monitor.Exit(locker);
                lockedForRead = false;      
            }
        }

        //Без параметров как в задании
        public static void GetCount()
        {
            if (lockedForRead)
            {
                Console.WriteLine("Кто-то хотел получить доступ к Count, но Count записывается");
            }
            else
            {
                Console.WriteLine("Кто-то читает Count, Count равен: {0}", Count);
            }
        }

        //Без доп. параметров как в задании
        public static void AddToCount (int value)
        {
            //Не даем выстроить очередь писателей, которые не дадут читать читатетлям
            Thread.Sleep(400);
            bool locked = false;
            try
            {
                Monitor.Enter(locker, ref locked);
                lockedForRead = locked;
                Console.WriteLine("Кто-то записывает Count, Count теперь равен {0} + {1} = {2}",Count, value, Count + value);
                Count += value;
                //Симулируем долгую запись
                Thread.Sleep(300);
            }
            finally
            {
                if (locked) Monitor.Exit(locker);
                lockedForRead = false;
            }
        }

    }
}
