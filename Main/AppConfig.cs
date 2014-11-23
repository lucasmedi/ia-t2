using System;
using System.Configuration;

namespace Main
{
    public class AppConfig
    {
        #region File Paths

        public static string AppPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static string FilesPath
        {
            get
            {
                return AppPath + @"..\..\Arquivos";
            }
        }

        #endregion

        #region App.config

        public static int FeatureRanking
        {
            get
            {
                return Int32.Parse(ConfigurationManager.AppSettings["feature.ranking"]);
            }
        }

        #endregion
    }
}