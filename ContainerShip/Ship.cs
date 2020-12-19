using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;

namespace ContainerShip
{
    public class Ship
    {
        public int Width { get; private set; }
        public int Length { get; private set; }
        public List<List<ContainerStack>> Containers { get; private set; }
        public List<Container> StartContainers { get; private set; }


        /* diagram for me to remember how I assigned the axeses
         O--->x width
         |
         |
         y
         length

         */

        public Ship(List<Container> containers)
        {
            StartContainers = containers;
            UpdateDimensions();
        }

        public Ship()
        {
        }

        public Ship(List<List<ContainerStack>> containers)
        {
            this.Containers = containers;
            UpdateDimensions();
        }

        private void UpdateDimensions()
        {
            Width = Containers.Count;
            Length = Containers[0].Count;
        }

        public void Sort(List<Container> containers)
        {
            List<Container> orderedList = OrderList(containers);


        }

        private int[,,] FindEmptySpot(Container container)
        {
            int[,,] coords = new int[0,0,0];
            int startCornerX;
            int endCornerX;
            int y = Length;

            //assign the search area according to the balance of the ship
            if (Balance())
            {
                startCornerX = 0;
                endCornerX = Width / 2;
            }
            else
            {
                startCornerX = Width - (Width/2);//weird calculation to account for uneven widths
                endCornerX = Width;
            }

            if (container.Cooled)//if cooling is needed only check the first row
            {
                y = 0;
            }

            return coords;
        }

        public bool Balance()//true is lower half Width lighter, false is upper half Width lighter
        {
            bool side = true;


            int bottomHalf = WeightByArea(0,Width/2,0, Length);
            int upperHalf = WeightByArea(Width-(Width/2),Width,0, Length);

            if ((bottomHalf-upperHalf) < 0) side = true;
            else if ((bottomHalf - upperHalf) > 0) side = false;

            return side;
        }



        public int WeightByArea(int widthStart, int widthEnd, int lengthStart, int lengthEnd) //gets the total weight of the specified range
        {
            int total = 0;

            for (int i = widthStart; i < widthEnd; i++)
            {
                for (int j = lengthStart; j < lengthEnd; j++)
                {
                    total += Containers[i][j].GetWeight();
                }
            }
            return total;
        }


        public List<Container> OrderList(List<Container> list)
        {
            return list.OrderByDescending(x => x.Cooled).ThenBy(x => x.Valuable).ToList();
        }


        public override string ToString() //todo make tostring output a correctly formatted url for the unity visualiser
        {
            return base.ToString();
        }
    }
}
