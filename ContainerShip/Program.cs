﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerShip
{
    class Program
    {
        static void Main(string[] args)
        {

            var begincontainers = new List<Container>();

            for (int i = 0; i < 100; i++)
            {
                begincontainers.Add(Container.GenerateContainer());
            }

            var containership = new Ship(4,4);

            containership.SortShip(begincontainers);

            Console.WriteLine(containership.ToString());

        }
    }
}
