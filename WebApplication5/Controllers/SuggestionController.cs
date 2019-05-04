using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class SuggestionController : Controller
    {
        
        public IActionResult Index(string value)
        {
            //не заносим в базу пустые значения
            if (value != null)
            {
                ViewBag.Last = value; //оставляем введенное значение в инпуте
                using (Context db = new Context())
                {
                    db.Suggestions.Add(new Suggestion { FirstName = value });
                    db.SaveChanges();
                    return View(db.Suggestions);
                }
            }
            return View();
        }
        public static int SuggestionCount { get; set; } = 3; //количество подсказок
        public static int SymbolCount { get; set; } = 1; //количество символов после которых появляется подсказка
        public IActionResult Settings(int suggestionCount, int symbolCount)
        {
            //чтобы значения не перезаписывались нулями после перехода в настройки
            if (symbolCount != 0)
            {
                SuggestionController.SuggestionCount = suggestionCount;
                SuggestionController.SymbolCount = symbolCount;
            }            
            return View();
        }
        /// <summary>
        /// отправляем заданное количество подсказок
        /// </summary>
        /// <param name="prefix">префикс подсказки</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Create(string prefix)
        {
            if (prefix.Length >= SymbolCount)
            {
                List<string> names = new List<string>(); //список с подсказками
                using (Context db = new Context())
                {
                    var getSuggestions = from s in db.Suggestions
                                         where s.FirstName.StartsWith(prefix)
                                         orderby s.FirstName ascending
                                         select s.FirstName;
                    foreach (var item in getSuggestions.Take(SuggestionCount))
                        names.Add(item);
                }
                return Json(names);
            }
            else return Json(null);
        }
    }
}