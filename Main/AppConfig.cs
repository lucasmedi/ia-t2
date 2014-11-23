using System;
using System.Configuration;

namespace Main
{
    public class AppConfig
    {
        public static int FeatureRanking
        {
            get
            {
                return Int32.Parse(ConfigurationManager.AppSettings["feature.ranking"]);
            }
        }
    }
}