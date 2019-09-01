using System;
using System.Diagnostics;
namespace DataStructureTests.StatisticalTools
{

    /// <summary>
    /// Log data into the class and it will keep track of the SD 
    /// and the average, 
    /// it's keeping track of them at runtime. 
    /// </summary>
    public class DataLogger
    {
        public double LastDataEntry;
        long DataEntryCount;
        double DataSquaredSum;
        double DataSum;
        public DataLogger()
        {

        }

        /// <summary>
        /// Get the Average for all the data you entered.
        /// </summary>
        /// <returns></returns>
        public double GetAverage()
        {
            if (DataEntryCount == 0)
            {
                throw new Exception("No data entried. ");
            }
            return DataSum / DataEntryCount;
        }

        public double GetStanderedDeviation()
        {
            if (DataEntryCount == 0)
                throw new Exception("No data has been entered. ");
            double SquaredSumAverage = DataSquaredSum / DataEntryCount;
            double SumAverage = GetAverage();
            return Math.Sqrt(SquaredSumAverage - SumAverage * SumAverage);
        }

        public void Register(double data)
        {
            if (Double.IsNaN(data) || Double.IsInfinity(data))
                throw new Exception("Don't Pollute The stats.");
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

            Register((double)data);
        }
    }

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

        public void Reset()
        {
            SysStopwatch.Reset();
        }

        /// <summary>
        /// It will just called the System.Stopwatch start()
        /// </summary>
        public void Tick()
        {
            SysStopwatch.Start();
        }
        /// <summary>
        /// Get the time interval since last time the tick() method is triggered. 
        /// </summary>
        /// <returns></returns>
        public long Tock()
        {
            SysStopwatch.Stop();
            long thetime = SysStopwatch.ElapsedMilliseconds;
            TimeElapseDataLog.Register(thetime);
            SysStopwatch.Reset();
            return thetime;
        }
    }
}
