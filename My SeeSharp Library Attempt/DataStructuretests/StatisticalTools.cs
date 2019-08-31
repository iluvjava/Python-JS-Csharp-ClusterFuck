using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace DataStructureTests.StatisticalTools
{

    /// <summary>
    /// A Stopwatch with a statistiscal sense. 
    /// </summary>
    public class MyStopwatch
    {
        public Stopwatch SysStopwatch;
        DataLogger TimeElapseDataLog;
        public MyStopwatch()
        {
            SysStopwatch = new Stopwatch();
            TimeElapseDataLog = new DataLogger();
        }

        /// <summary>
        /// It will just called the System.Stopwatch start()
        /// </summary>
        public void Start()
        {
            SysStopwatch.Start();
        }


        public void Reset()
        {
            SysStopwatch.Reset();
        }


        /// <summary>
        /// Stops the stopwatch and it will register the data too. 
        /// </summary>
        /// <returns></returns>
        public long Stop()
        {
            SysStopwatch.Stop();
            long thetime = SysStopwatch.ElapsedMilliseconds;
            TimeElapseDataLog.Register(thetime);
            return thetime;
        }

        /// <summary>
        /// Get the everage elapsed time from all the stopping and starting.
        /// </summary>
        /// <returns></returns>
        public double GetAverageTimeElapse()
        {
            return TimeElapseDataLog.GetAverage();
        }
        /// <summary>
        /// Geting the standard deviation for all the data point. 
        /// If there is only 1, or 2 events, it will just return 0.
        /// </summary>
        /// <returns></returns>
        public double GetStandardDeviation()
        {
            return TimeElapseDataLog.GetStanderedDeviation();
        }
    }


    /// <summary>
    /// Log data into the class and it will keep track of the SD 
    /// and the average, 
    /// it's keeping track of them at runtime. 
    /// </summary>
    public class DataLogger 
    {
        public double LastDataEntry;
        long DataEntryCount;
        double DataSum;
        double DataSquaredSum;

        public DataLogger()
        {

        }

        public void Register(double data)
        {
            LastDataEntry = data;
            DataEntryCount++;
            DataSum += data;
            DataSquaredSum += data * data;
        }

        /// <summary>
        /// You cannot register weird stuff to pollute the stats, 
        /// Nan, inf and shit like that. 
        /// </summary>
        /// <param name="data"></param>
        public void Register(long data)
        {
            if (Double.IsNaN(data) || Double.IsInfinity(data))
                throw new Exception("Don't Pollute The stats.");
            Register((double)data);
        }


        /// <summary>
        /// Get the Average for all the data you entered.
        /// </summary>
        /// <returns></returns>
        public double GetAverage()
        {
            return DataSum / DataEntryCount;
        }

        public double GetStanderedDeviation()
        {
            double SquaredSumAverage = DataSquaredSum / DataEntryCount;
            double SumAverage = GetAverage();
            return Math.Sqrt(SquaredSumAverage - SumAverage);
        }

    }
    
}
