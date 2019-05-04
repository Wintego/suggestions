using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class SuggestionController : Controller
    {
        static List<Suggestion> _Suggestions { get; set; } = new List<Suggestion>
        {
                new Suggestion {Id=0, FirstName="авдокия"},
                new Suggestion {Id=1, FirstName="авдокам"},
                new Suggestion {Id=2, FirstName="авдокбм"},
                new Suggestion {Id=3, FirstName="ион"}
        };
        
        public IActionResult Index(string value)
        {
            //не заносим в базу пустые значения
            if (value != null)
            {
                _Suggestions.Add(new Suggestion { FirstName = value });
                ViewBag.Last = value; //оставляем введенное значение в инпуте
            }
            return View(_Suggestions);
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
                var getSuggestions = from s in _Suggestions
                                     where s.FirstName.StartsWith(prefix)
                                     orderby s.FirstName ascending
                                     select s.FirstName;
                return Json(getSuggestions.Take(SuggestionCount));
            }
            else return Json(null);
        }
    }
}