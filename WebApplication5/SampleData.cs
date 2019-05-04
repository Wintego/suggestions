using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5
{
    public static class SampleData
    {
        public static void InitData()
        {
            string[] fnames = File.ReadAllLines("wwwroot/name_rus.txt", System.Text.Encoding.UTF8);
            using (Context db = new Context())
            {
                foreach (var item in fnames)
                {
                    db.Suggestions.Add(new Suggestion { FirstName = item });
                }
                db.SaveChanges();
            }
        }
    }
}
