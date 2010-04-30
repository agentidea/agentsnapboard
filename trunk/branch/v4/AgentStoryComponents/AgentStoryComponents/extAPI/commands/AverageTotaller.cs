using System;
using System.Collections.Generic;

namespace AgentStoryComponents.extAPI.commands
{
    /// <summary>
    /// running list values, to get Average from
    /// </summary>
    public class AverageTotaller
    {
        private List<double> _ints = new List<double>();
        public AverageTotaller(int initialValue)
        {
            this.add(initialValue);
        }
        public void add(int val)
        {
            _ints.Add((double)val);
        }

        public double Average
        {
            get
            {
                double sum = 0.0;
                double numItems = (double)_ints.Count;
                for (int i = 0; i < numItems; i++)
                    sum = sum + _ints[i];

                double result = sum / numItems;
                return Math.Round(result, 1);
            }
        }

        public double Min
        {

            get {
                double res = 100.0;

                double numItems = (double)_ints.Count;
                for (int i = 0; i < numItems; i++)
                {
                    if (_ints[i] <= res)
                    {
                        res = _ints[i];
                    }

                }

                return res;

            }
        }

        public double Max
        {

            get
            {
                double res = 0.0;

                double numItems = (double)_ints.Count;
                for (int i = 0; i < numItems; i++)
                {
                    if (_ints[i] >= res)
                    {
                        res = _ints[i];
                    }

                }

                return res;

            }
        }


    }
}
