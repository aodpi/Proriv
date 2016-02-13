using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace WordProcessor
{
    public class WordP
    {
        IList<string> _words;

        public enum Orientation
        {
            Horizontal,
            Vertical
        }
        public WordP()
        {

        }
        public struct Rebus
        {
            public int xorigin, yorigin;
            public Orientation orient;
            public int length;
            public int Id;
            public List<Intersect> intersects;
        }
        public struct Intersect
        {
            public int xIntersect, yIntersect;
            public int IntersectId;
        }
        public WordP(IList<string> Words)
        {
            this._words = Words;
            char[,] x = new char[3, 3];
            x[0, 0] = '1';
        }

        public void Test(int[,] matrix, int xmax, int ymax)
        {
            List<Rebus> arr = new List<Rebus>();
            int i = 1, x, y;
            Rebus temp;
            //horizontal

            for (x = 0; x < xmax; x++)
            {
                y = 0;
                while (y < ymax - 1)
                {
                    if (matrix[x, y] != 0)
                    {
                        if (matrix[x, y + 1] != 0)
                        {
                            temp = new Rebus();
                            temp.length = 0;
                            temp.xorigin = x;
                            temp.yorigin = y;
                            temp.Id = i;
                            temp.orient = Orientation.Horizontal;
                            temp.intersects = new List<Intersect>();
                            while (y < ymax && matrix[x, y] != 0)
                            {
                                temp.length++;
                                matrix[x, y] = i;
                                y++;
                            }
                            arr.Add(temp);
                            i++;
                        }
                        else
                            y++;
                    }
                    else y++;
                }
            }

            //vertical
            for (y = 0; y < ymax; y++)
            {
                x = 0;
                while (x < xmax - 1)
                {
                    if (matrix[x, y] != 0)
                    {
                        if (matrix[x + 1, y] != 0)
                        {
                            temp = new Rebus();
                            temp.length = 0;
                            temp.xorigin = x;
                            temp.yorigin = y;
                            temp.Id = i;
                            temp.orient = Orientation.Vertical;
                            temp.intersects = new List<Intersect>();
                            while (x < xmax && matrix[x, y] != 0)
                            {
                                temp.length++;
                                if (matrix[x, y] != -1)
                                {
                                    arr[matrix[x, y] - 1].intersects.Add(new Intersect() { xIntersect = x, yIntersect = y, IntersectId = i });
                                    temp.intersects.Add(new Intersect() { xIntersect = x, yIntersect = y, IntersectId = matrix[x, y] });
                                }
                                matrix[x, y] = i;
                                x++;
                            }
                            arr.Add(temp);
                            i++;
                            continue;
                        }
                        else
                            x++;
                    }
                    else
                        x++;
                }
            }
        }


        /// <summary>
        /// Gaseste cuvantul potrivit pentru intersectarea cu cuvintul introdus. 
        /// </summary>
        /// <param name="firstword">Cuvintul deja prezent in rebus.</param>
        /// <param name="intrsctpos">Index zero-based, al caracterului unde cuvintele se intersecteaza.</param>
        /// <param name="requiredlength">Marimea cuvintului cautat care coincide cu spatiile in rebus.</param>
        /// <param name="lengthbeforeint">Lungimea cuvintului cautat pina la intersectarea lor.</param>
        /// <returns></returns>
        public IList<string> FindWordForIntersect(string firstword,int intrsctpos,int requiredlength,int lengthbeforeint)
        {
            IList<string> foundwords = new List<string>();
            foreach (string item in _words)
            {
                if (item.Length==requiredlength)
                {
                    char intrsctchar = firstword[intrsctpos];
                    if (item.Contains(intrsctchar))
                    {
                        if (item.IndexOf(intrsctchar)-0==lengthbeforeint)
                        {
                            foundwords.Add(item);   
                        }
                    }
                }
            }
            return foundwords;
        }
    }
}
