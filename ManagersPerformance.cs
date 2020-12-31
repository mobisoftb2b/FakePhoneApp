using Newtonsoft.Json;
using NLog;
using RabbitMQManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FakePhoneApp
{
    public class StartParams {
        public int testNumber { get; set; }
        public string ManagerId { get; set; }

        public bool started { get; set; }
    }
    class ManagersPerformance
    {
        private static Barrier barrier;
        RabbitMQManagersApprove m;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public void BasicTest()
        {
            int numManagers = int.Parse(ConfigurationManager.AppSettings["numberOfManagers"]);
            m = new RabbitMQManagersApprove();

            List<StartParams> spList = new List<StartParams>();

            List<string> allManagers = m.GetAllManagers(numManagers);
            numManagers = allManagers.Count;
            barrier = new Barrier(numManagers + 1);
            try
            {
                // SNIP configure logging

                // Create the specified number of clients, to carry out test operations, each on their own threads
                Thread[] threads = new Thread[numManagers];
                for(int count = 0; count< numManagers; count++)
                {
                    var index = count;
                    ParameterizedThreadStart ts = new ParameterizedThreadStart(this.RunClient);

                    threads[count] = new Thread(ts);
                    threads[count].Name = $"Client {count}"; // for debugging
                    StartParams sp = new StartParams() { testNumber = count, ManagerId = allManagers[count] };
                    spList.Add(sp);
                    threads[count].Start(sp);
                }

                //for (int count = 0; count < numManagers; ++count)
                //{
                //    threads[count].Sleep(1000);
                //}
                // We loose the convenience of awaiting all tasks,
                // but use a thread barrier to block this thread until all the others are done.
                //System.Threading.Thread.Sleep(5000); //10 SEC
                barrier.SignalAndWait();
                logger.Info(JsonConvert.SerializeObject(spList));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
        private readonly object balanceLock = new object();
        private void RunClient(object startParams)
        {
            try
            {
                lock (balanceLock)
                {
                    System.Diagnostics.Debug.WriteLine($"test number {((StartParams)startParams).testNumber}");
                    SingleManager sm = new SingleManager(null, ((StartParams)startParams).ManagerId, ((StartParams)startParams).testNumber);
                    sm.login();
                    sm.history();
                    //System.Threading.Thread.Sleep(5000); //10 SEC
                    if (sm.isBound)
                        ((StartParams)startParams).started = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            finally
            {
                barrier.SignalAndWait();
            }
        }
    }
}
