using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSPractice.Models;
using SSPractice.Models.ViewModels;

namespace SSPractice.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowWords(string urlAddress)
        {
            string html = Search.DownloadHtml(urlAddress);
            if (html != null)
            {
                string text = Search.HtmlToText(html);
                return View(new WordsInfoViewModel
                {
                    //чуточку нечитаемо
                    words = Search.WordCount(Search.SplitText(text)).OrderByDescending(p => p.Value).ToDictionary(pair => pair.Key, pair => pair.Value)
                });
            }
            return View("Index");
        }
    }
}
