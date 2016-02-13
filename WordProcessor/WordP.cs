using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace WordProcessor
{
    /// <summary>
    /// Converts int array to char array with words.
    /// </summary>
    public class WordP
    {
        #region Fields
        List<Rebus> arr = new List<Rebus>();
        IList<string> _words;

        public enum Orientation
        {
            Horizontal,
            Vertical
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
        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="Words">All the words found in the file.</param>
        public WordP(IList<string> Words)
        {
            this._words = Words;
            char[,] x = new char[3, 3];
            x[0, 0] = '1';
        }
        
        /// <summary>
        /// Grouped found word in int array.
        /// </summary>
        public IEnumerable<IGrouping<int,Rebus>> Groups
        {
            get
            {
                return arr.GroupBy(a => a.length);
            }
        }
        /// <summary>
        /// Convert a bidimensional int array into a <see cref="List{<see cref="Rebus"/>}"/>
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="xmax"></param>
        /// <param name="ymax"></param>
        public void Test(int[,] matrix, int xmax, int ymax)
        {
            
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
            var asd = Groups;
        }

        /// <summary>
        /// Lungimea maxima a cuvintelor posibile.
        /// </summary>
        public int Max
        {
            get
            {
                int max = arr[0].length;
                for (int i = 0; i < arr.Count; i++)
                {
                    if (arr[i].length > max)
                    {
                        max = arr[i].length;
                    }
                }
                return max;
            }
        }
        
        /// <summary>
        /// TestFunc
        /// </summary>
        /// <param name="xmax"></param>
        /// <param name="ymax"></param>
        /// <returns></returns>
        public char[,] FirstTest(int xmax,int ymax)
        {
            char[,] result = new char[xmax, ymax];
            string foundword = string.Empty;
            IList<string> PWords = GetPossibleWords(Max);
            for (int i = 0; i < arr.Count; i++)
            {
                switch (arr[i].orient)
                {
                    case Orientation.Horizontal:
                        foreach (string item in PWords)
                        {
                            if (item.Length == arr[i].length)
                            {
                                for (int j = 0; j < item.Length; j++)
                                {
                                    result[arr[i].xorigin, arr[i].yorigin + j] = item[j];
                                }
                            }
                        }
                        break;
                    case Orientation.Vertical:
                        foreach (string item in PWords)
                        {
                            if (item.Length == arr[i].length)
                            {
                                for (int j = 0; j < item.Length; j++)
                                {
                                    result[arr[i].xorigin+j, arr[i].yorigin] = item[j];
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// Returneaza cuvintele posibile de cuvinte cu numarul lungimii maximale a cuvintelor indicat.
        /// </summary>
        /// <param name="max">Lungimea maximala a cuvintelor.</param>
        /// <returns></returns>
        public IList<string> GetPossibleWords(int max)
        {
            return _words.Where(x => x.Length <= max).ToList();
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
