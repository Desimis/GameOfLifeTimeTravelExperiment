using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CAK
{
    public class Universe
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public List<int[]> TimeSpace { get; set; } = new List<int[]>();


        public Universe(int width, int height)
        {
            Width = width;
            Height = height;


            TimeSpace.Add(new int[width * height]);
        }


        public int StepContinuous(int index)
        {
            return TimeSpace.Count - 1;
        }

        public int StepBranchContinuous(int index)
        {
            return TimeSpace.Count - 1;
        }


        private int StepCounter = 0;

        public int StepSingleInstanceEngine(int off)
        {
            /*StepCounter++;
            if (StepCounter > 50)
            {
                MessageBox.Show("Stuck in a Time Loop, exiting");
                return off;
            }*/

            if (off + 1 >= TimeSpace.Count)
            {
                StepCounter = 0;
                TimeSpace.Add(new int[Width * Height]);
            }

            int ret = off + 1;


            for (int x = 1; x < Width - 1; x++)
            {
                for (int y = 1; y < Width - 1; y++)
                {
                    var popDensity = 0;

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            popDensity += TimeSpace[off][(y + j) * Width + (x + i)];
                        }
                    }

                    var val = TimeSpace[off][y * Width + x];
                    popDensity -= val;


                    if (val == 1) //alive
                    {
                        if (popDensity == 2 || popDensity == 3)
                        {
                            TimeSpace[off + 1][y * Width + x] = 1;
                        }
                        else if (popDensity == 6 && MainForm.EnableTimeTravel && off > 4)
                        {
                            var backstep = 4;
                            /*  if (off - backstep < 0)
                             {
                                 off += backstep;
                                 // ret += backstep;
 
 
                                 for (int i = 0; i < backstep; i++)
                                 {
                                     TimeSpace.Insert(0, new int[Width * Height]);
                                 }
                             }*/

                            TimeSpace[off + 1][y * Width + x] = 0;
                            // return off - backstep;

                            if (TimeSpace[off - backstep][y * Width + x] != 1) //this prevents time loops
                            {
                                TimeSpace[off - backstep][y * Width + x] = 1;

                                var p = TimeSpace.Count - off;

                                for (int i = off - backstep; i <= off; i++)
                                {
                                    ret += (StepSingleInstanceEngine(i) - ret) + 2;
                                }

                                ret = (TimeSpace.Count - p) + 1;

                                return ret;
                                // ret = off - backstep;}
                            }
                        }
                        else
                        {
                            TimeSpace[off + 1][y * Width + x] = 0;
                        }
                    }
                    else
                    {
                        if (popDensity == 3)
                        {
                            TimeSpace[off + 1][y * Width + x] = 1;
                        }
                        else
                        {
                            TimeSpace[off + 1][y * Width + x] = 0;
                        }
                    }
                }
            }

            return ret;
        }
    }
}