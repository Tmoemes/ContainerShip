using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace ContainerShip
{
    public class Ship
    {
        public int Width { get; private set; }
        public int Length { get; private set; }
        public List<List<ContainerStack>> Containers { get; private set; }

        public Ship(int width, int length)
        {
            this.Width = width;
            this.Length = length;
            Containers = new List<List<ContainerStack>>();
            InitialiseShip(width,length);
            
        }

        public Ship(List<List<ContainerStack>> containers,int width, int length)
        {
            this.Containers = containers;
            this.Width = width;
            this.Length = length;
        }


        private void InitialiseShip(int width, int length)
        {

            for (var i = 0; i < width; i++)
            {
                Containers.Add(new List<ContainerStack>());

                for (var j = 0; j < length; j++)
                {
                    Containers[i].Add(new ContainerStack());
                }
            }
        }


        public void SortShip(List<Container> containers)
        {
            var orderedList = OrderList(containers);

            foreach (var cont in orderedList)
            {
                var coords = FindEmptySpot(cont);
                if (coords != null)
                {
                    
                    Containers[coords[0]][coords[1]].AddToStack(cont);
                }
                
            }
        }

        private int[] FindEmptySpot(Container container)
        {
            var startCornerX = Width / 2 ;
            var endCornerX = Width;
            var depth = Length;

            //assign the search area according to the balance of the ship
            if (IsLowerHalfLighter())
            {
                startCornerX = 0;
                endCornerX = Width / 2;
            }
            
            if (container.Cooled)//if cooling is needed only check the first row
            {
                depth = 1;
            }

            for (var i = startCornerX; i < endCornerX; i++)
            {
                for (var j = 0; j < depth; j++)
                {
                    if (Containers[i][j].CheckStackingWeigth(container))
                    {
                        return new[] { i, j };
                    };

                }
            }

            return null;
        }

        public bool IsLowerHalfLighter()
        {

            var bottomHalf = GetWeightByArea(0,Width/2,0, Length);
            var upperHalf = GetWeightByArea(Width-(Width/2),Width,0, Length);

            return bottomHalf - upperHalf < 0;
        }


        public int GetWeightByArea(int widthStart, int widthEnd, int lengthStart, int lengthEnd) //gets the total weight of the specified area
        {
            int total = 0;
            for (int i = widthStart; i < widthEnd; i++)
            {
                
                for (int j = lengthStart; j < lengthEnd; j++)
                {
                    
                    total += Containers[i][j].GetTotalWeight();
                }
            }
            return total;
        }


        private List<Container> OrderList(List<Container> list)
        {
            return list.OrderByDescending(x => x.Cooled).ThenBy(x => x.Valuable).ToList();
        }


        public override string ToString()
        {
            const string baseUrl = "https://i872272core.venus.fhict.nl/ContainerVisualizer/index.html?";
            var dimenString = $"length={Length}&width={Width}";
            var stackString = "&stacks=";
            var weightString = "&weights=";

            foreach (var stackList in Containers)
            {
                foreach (var stack in stackList)
                {
                    if (stack.GetHeight() != 0)
                    {
                        stackString += stack.ToString();
                        stackString += ",";

                        weightString += stack.GetWeightString();
                        weightString += ",";
                    }
                    else
                    {
                        stackString += "*";
                        weightString += "*";
                    }
                }
                //remove the last extra added , 
                stackString = stackString.Remove(stackString.Length - 1);
                stackString += "/";

                weightString = weightString.Remove(weightString.Length - 1);
                weightString += "/";

            }

            //remove the last extra added /
            stackString = stackString.Remove(stackString.Length - 1);
            weightString = weightString.Remove(weightString.Length - 1);
            //remove all the placeholder *
            stackString = stackString.Replace("*", "");
            weightString = weightString.Replace("*", "");

            return baseUrl+dimenString+stackString+weightString;
        }
    }
}
