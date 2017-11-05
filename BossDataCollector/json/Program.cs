using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BossDataCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            Bosses bosses = JsonConvert.DeserializeObject<Bosses>(JsonTextReader(".\\boss.json"));

            RaidBoss tmp = new RaidBoss();
            
            RaidBoss[] rb = new RaidBoss[bosses.RaidBosses.Length];
            for (int x = 0; x < bosses.RaidBosses.Length; x++)
            {
                rb[x] = bosses.RaidBosses[x];
            }

            int i, j;
            var count = rb.Length;
            for (i = 1; i < count; i++)
            {
                var t = rb[i];
                for (j = i - 1; j >= 0 && rb[j].Level > t.Level; j--)
                { rb[j + 1] = rb[j]; }
                rb[j + 1] = t;
            }
            for (int x = 0; x < bosses.RaidBosses.Length; x++)
            {
                Console.WriteLine(rb[x].Name);
            }
            Console.ReadKey();


        }

        public static void InsertSort(double[] data)
        {
            int i, j;
            var count = data.Length;
            for (i = 1; i < count; i++)
            {
                var t = data[i];
                for (j = i - 1; j >= 0 && data[j] > t; j--)
                    data[j + 1] = data[j];
                data[j + 1] = t;

            }
        }

        static void BubbleSort(int[] intArray)
        {
            int temp = 0;
            bool swapped;
            for (int i = 0; i < intArray.Length; i++)
            {
                swapped = false;
                for (int j = 0; j < intArray.Length - 1 - i; j++)
                {
                    if (intArray[j] > intArray[j + 1])
                    {
                        temp = intArray[j];
                        intArray[j] = intArray[j + 1];
                        intArray[j + 1] = temp;
                        if (!swapped)
                            swapped = true;
                    }
                    if (!swapped)
                        return;
                }
            }
        }



        static string JsonTextReader(string file)
        {
            string jsonText;
            using (System.IO.StreamReader sr = new System.IO.StreamReader(file))
            {
                jsonText = sr.ReadToEnd();
                sr.Close();
            }
            return jsonText;
        }
    }
}
