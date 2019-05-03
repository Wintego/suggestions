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
        public SuggestionController()
        {
            suggestionCount = 3;
            symbolCount = 1;
        }
        static List<Suggestion> _Suggestions { get; set; } = new List<Suggestion>
        {
                new Suggestion {Id=0, FirstName="авдокия"},
                new Suggestion {Id=1, FirstName="авдокам"},
                new Suggestion {Id=2, FirstName="авдокбм"},
                new Suggestion {Id=3, FirstName="ион"}
        };
        public static int suggestionCount { get; set; } = 3;
        public static int symbolCount { get; set; } = 1;
        public IActionResult Index(string value)
        {
            if (value != null)
            {
                _Suggestions.Add(new Suggestion { FirstName = value });
            }
            return View(_Suggestions);
        }
        public IActionResult Settings(int suggestionCount, int symbolCount)
       {
            SuggestionController.suggestionCount = suggestionCount;
            SuggestionController.symbolCount = symbolCount;
            return View();
        }
        [HttpPost]
        public JsonResult Create(string prefix)
        {
            if (prefix.Length >= symbolCount)
            {
                var getSuggestions = from s in _Suggestions
                                     where s.FirstName.StartsWith(prefix)
                                     orderby s.FirstName ascending
                                     select s.FirstName;
                return Json(getSuggestions.Take(suggestionCount));
            }
            else return Json(null);
        }
    }
}