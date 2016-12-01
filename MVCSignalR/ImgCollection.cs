using MVCSignalR.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSignalR
{
    public class ImgCollection : IFileCollection
    {

        private Dictionary<string, string> imgs;
        public ImgCollection()
        {
            imgs = new Dictionary<string, string>();
        }
        public string GetFile(string key)
        {
            foreach(var item in imgs)
            {
                if (item.Key == key)
                    return item.Value;
            }
            return "";
        }
        public void SetFile(string key, string value)
        {
            if (!imgs.ContainsKey(key))
                imgs.Add(key, value);
        }

        public string GetFileByNick(string nick)
        {
            var img = "";
            if (!string.IsNullOrEmpty(nick))
            {
                img = GetFile(nick);
                if (string.IsNullOrEmpty(img))
                {
                    img = GetRandom();
                    SetFile(nick, img);
                }
            }
            return img;
        }

        private string GetRandom()
        {
            return new Random().Next(1, 6).ToString();
        }
    }
}