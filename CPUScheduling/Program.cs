using System;
using System.Collections;
using System.Collections.Generic;

namespace CPUScheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            Process[] pArr = new Process[4] { new Process("p1", 4, 0), 
                new Process("p2", 13, 2), 
                new Process("p3", 3, 10), 
                new Process("p4", 17, 4) };

            Utilities util = new Utilities();
            util.RunFCFS(pArr);
        }      
    }

    class Utilities
    {
        public Utilities()
        {

        }

        public Process GetProcessByArrivalTime(Array pArr, int arrivalTime)
        {
            foreach (Process p in pArr)
                if (p.GetArrivalTime() == arrivalTime)
                    return p;
            return null;
        }

        public void RunFCFS(Process[] pArr)
        {
            double avgWaitTime = 0;
            int[] pWaitTimes = new int[pArr.Length];          
            double avgTemp = 0;

            //loop that examines arrival times and creates new array in correct processing order
            int min = 100000000;
            int counter = 0;
            List<int> arrivalTimeList = new List<int>();
            List<Process> processList = new List<Process>();
            
            //add arrival times to list and sort
            foreach (Process p in pArr)
            {
                arrivalTimeList.Add(p.GetArrivalTime());
            }
            arrivalTimeList.Sort();
            
            //create new list with processes in order they wil be executed
            foreach(int time in arrivalTimeList)
            {
                Utilities util = new Utilities();
                processList.Add(util.GetProcessByArrivalTime(pArr, time));
            }
            
            
            //loop that will calculate the wait time for each process and store it in array pWaitTimes
            for(int i = 0; i < processList.Count; i++)
            {
                if(i == 0)
                {
                    pWaitTimes[i] = 0;
                } else
                    pWaitTimes[i] = (processList[i - 1].GetBurstTime() + pWaitTimes[i-1] + processList[i-1].GetArrivalTime()) 
                        - processList[i].GetArrivalTime();
            }
            int j = 0;
            Console.WriteLine("Process Name \tProcess ID \tWait Time \tBurst Time \tArrival Time");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            foreach (int time in pWaitTimes)
            {
                Console.WriteLine(processList[j].GetProcessName() + "  \t\t" + processList[j].GetProcessID() + "  \t\t" + time 
                    + "  \t\t" + processList[j].GetBurstTime() + "  \t\t" + processList[j].GetArrivalTime());

                avgTemp += time;
                j++;
            }
            Console.WriteLine("");
            Console.WriteLine("The average wait time is " + avgTemp / pWaitTimes.Length);


        }
    }

    class Process
    {
        private string pName = string.Empty;
        private int arrivalTime = 0;
        private int pid = 0;
        int burstTime = 0;

        public Process(string pName, int burstTime, int arrivalTime)
        {
            Random r = new Random();
            this.arrivalTime = arrivalTime;
            this.pid = r.Next(0, 100) * 1000;
            this.pName = pName;
            this.burstTime = burstTime;
        }

        public int GetBurstTime()
        {
            return this.burstTime;
        }

        public string GetProcessName()
        {
            return this.pName;
        }

        public int GetProcessID()
        {
            return this.pid;
        }

        public int GetArrivalTime()
        {
            return this.arrivalTime;
        }

    }
}
