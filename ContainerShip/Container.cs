using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http.Headers;
using System.Text;

namespace ContainerShip
{
    public class Container
    {
        public int Weight { get; private set; } //4000-30000 max 120000 on top
        public bool Valuable { get; private set; } //only valuables allowed on top of valuables 
        public bool Cooled { get; private set; } //only in front row

        private static readonly Random _random = new Random();

        public Container(int weight,bool valuable,bool cooled)
        {
            Weight = weight;
            Valuable = valuable;
            Cooled = cooled;
        }


        public List<Container> GenerateContainersList(int amount)
        {
            List<Container> tempList = new List<Container>();

            for (int i = 0; i < amount; i++)
            {
                tempList.Add(GenerateContainer());
            }

            return tempList;
        }

        public static Container GenerateContainer()
        {
            int randweight = _random.Next(4, 30); //random weight in the allowed range
            bool randvalue = !Convert.ToBoolean(_random.Next(0, 5)); //1 in 5 containers will be valuable
            bool randcool = !Convert.ToBoolean(_random.Next(0, 10)); //1 in 10 containers will be cooled

            return new Container(randweight, randvalue, randcool);
        }


        public override string ToString()
        {
            string temp = $"Weight: {Weight}, Valuable: {Valuable}, Cooled: {Cooled}";
            return temp;
        }
    }
}
