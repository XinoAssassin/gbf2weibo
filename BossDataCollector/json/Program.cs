using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace BossDataCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            var boss = JsonConvert.DeserializeObject<Bosses>(GetJsonString("boss.json"));
            RaidBoss[] rb = new RaidBoss[boss.RaidBosses.Length];
            for (int i = 0; i < boss.RaidBosses.Length; i++)
            {
                rb[i] = boss.RaidBosses[i];
                Console.WriteLine(rb[i].Name);
            }

            OrderByLv(rb); //排序
            Console.Clear();

            model.MyBoss[] mb = new model.MyBoss[rb.Length];
            for (int i = 0; i < rb.Length; i++)
            {
                mb[i] = new model.MyBoss();

                if (rb[i].TranslatedName != null)
                {
                    if (rb[i].Language == "ENGLISH")
                    {
                        if (rb[i].Name != null)
                        {
                            mb[i].Name_EN = rb[i].Name;
                        }
                        if (rb[i].TranslatedName.Value != null)
                        {
                            mb[i].Name_JP = rb[i].TranslatedName.Value;
                        }
                    }
                    else
                    {
                        if (rb[i].Name != null)
                        {
                            mb[i].Name_JP = rb[i].Name;
                        }
                        if (rb[i].TranslatedName.Value != null)
                        {
                            mb[i].Name_EN = rb[i].TranslatedName.Value;
                        }
                    }
                }
                else
                {
                    if (rb[i].Language == "ENGLISH")
                    {
                        mb[i].Name_EN = rb[i].Name;
                    }
                    else
                    {
                        mb[i].Name_JP = rb[i].Name;
                    }
                }

                mb[i].Lv = rb[i].Level;
                mb[i].Pic_URL = rb[i].Image.Value;
                Console.WriteLine("{0}, {1}", mb[i].Name_EN, mb[i].Name_JP);
            }
            WriteJsonFile(JsonConvert.SerializeObject(mb),"test.json");


            List<model.MyBoss> list = new List<model.MyBoss>();
            foreach (var _mb in mb)
            {
                if (list.Exists(x=>x.Name_EN==_mb.Name_EN) == false)
                {
                    list.Add(_mb);
                }
            }
            WriteJsonFile(JsonConvert.SerializeObject(list), "lastBossList.json");
            Console.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i].Name_EN +", "+list[i].Name_JP);
            }
            foreach (var l in list )
            {
                if (l.Name_EN != null)
                {
                    ImageDownloader(l.Pic_URL, l.Name_EN);
                }
                else
                {
                    ImageDownloader(l.Pic_URL, l.Name_JP);
                }
            }
            Console.WriteLine("done");
            Console.ReadKey();
            

        }

        static void OrderByLv(RaidBoss[] _rb)
        {
            int i, j;
            var count = _rb.Length;
            for (i = 1; i < _rb.Length; i++)
            {
                var t = _rb[i];
                for (j= i-1; j >=0&&_rb[j].Level>t.Level; j--)
                {
                    _rb[j + 1] = _rb[j];
                }
                _rb[j + 1] = t;
            }
            
        }

        static void WriteJsonFile(string jsonText,string newFileName)
        {
            if (jsonText != null)
            {
                using (StreamWriter sw = new StreamWriter(newFileName, false))
                {

                    sw.WriteLine(jsonText);
                    sw.Close();
                }
            }
        }

        static string  GetJsonString(string file)
        {
            string jsonText = null;
            using (StreamReader sr = new StreamReader(file))
            {
                jsonText = sr.ReadToEnd();
                sr.Close();
            }
            return jsonText;
        }

        static void ImageDownloader(string url, string filename)
        {
            string foldName = Path.Combine(Environment.CurrentDirectory, "pics");
            if (!Directory.Exists(foldName))
            {
                Directory.CreateDirectory(foldName);
            }
            if (File.Exists(Path.Combine(foldName,filename)))
            {

            }
            else
            {
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)hwr.GetResponse();
                Stream stream = response.GetResponseStream();

                FileStream fs = new FileStream(Environment.CurrentDirectory + "\\pics\\" + filename + ".jpg", FileMode.OpenOrCreate, FileAccess.Write);
                byte[] buff = new byte[response.ContentLength];
                int i = 0;
                while ((i = stream.Read(buff, 0, buff.Length)) > 0)
                {
                    fs.Write(buff, 0, i);
                }
                fs.Close();
                stream.Close();
                fs.Dispose();
                stream.Dispose();
            }
        }
    }
}
