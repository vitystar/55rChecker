using System;

namespace _55rCheckerWF
{
    [Serializable]
    public class CookieMsg
    {
        public string email { get; set; }
        public string key { get; set; }
        public string uid { get; set; }
        public string expire_in { get; set; }
    }
}
