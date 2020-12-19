using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerShip
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Container> tempList= new Container().GenerateContainersList(25);

            foreach (var cont in tempList)
            {
                Console.WriteLine(tempList.IndexOf(cont) + ": " + cont);
            }

            List<Container> tempOrderedList = new Ship().OrderList(tempList);

            foreach (var cont in tempOrderedList)
            {
                Console.WriteLine(tempList.IndexOf(cont) + ": " + cont);
            }


        }
    }
}
