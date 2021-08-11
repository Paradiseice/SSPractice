using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using HtmlAgilityPack;

namespace SSPractice.Models
{
    public static class Search
    {
        public static string DownloadHtml(string urlAddress)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (String.IsNullOrWhiteSpace(response.CharacterSet))
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                    string data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();
                    return data;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
        public static string HtmlToText(string data)
        {
            var document = new HtmlDocument();
            document.LoadHtml(data);
            string text = document.DocumentNode.InnerText;
            return text;
        }
        public static List<string> SplitText(string text)
        {
            string[] subs = text.Split(new Char[] {' ', ',', '.', '!', '?', '\"', ';', ':', '[', ']', '(', ')', '\r', '\n', '\t', '«', '»'});
            List<string> wordsList = new List<string>(subs);
            for (int i = 0; i < wordsList.Count; i++)
            {
                wordsList[i] = wordsList[i].Replace(" ", "");
                wordsList[i] = wordsList[i].ToLower();
                if (wordsList[i] == "" || wordsList[i] == "-" || wordsList[i] == "—")
                {
                    wordsList.RemoveAt(i);
                    i--;
                }
            }
            return wordsList;
        }
        public static Dictionary<string, int> WordCount(List<string> subs)
        {
            Dictionary<string, int> words = new Dictionary<string, int>();
            foreach(string word in subs)
            {
                if (!words.ContainsKey(word))
                {
                    words.Add(word, 1);
                }
                else
                {
                    words[word]++;
                }
            }
            return words;
        }
    }
}
