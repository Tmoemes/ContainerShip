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

            foreach (var cont in orderedList)
            {
                int[] coords = FindEmptySpot(cont);
                if (cont.Valuable)
                {

                }
            }
        }

        private int[] FindEmptySpot(Container container)
        {
            int[] coords = new int[0];
            int startCornerX;
            int endCornerX;
            int depth = Length;

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
                depth = 0;
            }


            for (int i = startCornerX; i <= endCornerX; i++)
            {
                for (int j = 0; j <= depth; j++)
                {
                    if (Containers[i][j].CheckPossible(container))
                    {
                        if (CheckAccess(i, j,container.Valuable))
                        {
                            return new[] {i, j};
                        }
                        else if (!container.Valuable && CheckAccess(i,j))
                        {
                            return new[] { i, j };
                        }
                    }
                }
            }

            return null;
        }

        public bool Balance()//true is lower half Width lighter, false is upper half Width lighter
        {

            int bottomHalf = WeightByArea(0,Width/2,0, Length);
            int upperHalf = WeightByArea(Width-(Width/2),Width,0, Length);

            if ((bottomHalf-upperHalf) < 0) return true;
            else if ((bottomHalf - upperHalf) > 0) return false;

            return true;
        }

        

        bool CheckAccess(int x , int z, bool value)//check if the container would not block any accessibility if placed in specified stack //todo make this better and shorter
        {

            int y = Containers[x][z].GetHeight();
            if(Containers[x][z+1].GetContainerAt(y) == null && Containers[x][z - 1].GetContainerAt(y) == null) //if there is both no container in front and behind
            {
                return true;
            }
            else if (Containers[x][z + 1].GetContainerAt(y) != null && Containers[x][z - 1].GetContainerAt(y) == null)//if there is only a container in front
            {
                if (Containers[x][z + 1].GetContainerAt(y).Valuable && Containers[x][z + 2].GetContainerAt(y) == null)//if container in front is valuable and accessible from the other side
                {
                    return true;
                }
                else if (!Containers[x][z + 1].GetContainerAt(y).Valuable) //if container in front is not valuable
                {
                    return true;
                }
            }
            else if (Containers[x][z + 1].GetContainerAt(y) == null && Containers[x][z - 1].GetContainerAt(y) != null)//if there is only a container in front
            {
                if (Containers[x][z - 1].GetContainerAt(y).Valuable && Containers[x][z - 2].GetContainerAt(y) == null)//if container behind is valuable and accessible from the other side
                {
                    return true;
                }
                else if (!Containers[x][z - 1].GetContainerAt(y).Valuable) //if container behind is not valuable
                {
                    return true;
                }
            }
            else if (Containers[x][z + 1].GetContainerAt(y) != null && Containers[x][z - 1].GetContainerAt(y) != null)//if there is a container in front and behind
            {
                if (value) return false;


                if (!value)
                {
                    if ((Containers[x][z + 1].GetContainerAt(y).Valuable &&
                         Containers[x][z + 2].GetContainerAt(y) == null) ||
                        (Containers[x][z - 1].GetContainerAt(y).Valuable &&
                         Containers[x][z - 2].GetContainerAt(y) == null)
                    ) //if either container is valuable but accessible from other side
                    {
                        return true;
                    }
                    else if (!Containers[x][z + 1].GetContainerAt(y).Valuable && !Containers[x][z - 1].GetContainerAt(y).Valuable) return true; //if neither container is valuable
                    else return false;
                }
            }

            return false;
        }  


        public int WeightByArea(int widthStart, int widthEnd, int lengthStart, int lengthEnd) //gets the total weight of the specified area
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
