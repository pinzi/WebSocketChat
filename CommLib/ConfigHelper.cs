using System;
using System.Configuration;
using System.Web.Configuration;

namespace CommLib
{
    /// <summary>  
    /// WebConfig读写辅助类  
    /// </summary>  
    public class ConfigHelper : IDisposable
    {
        private Configuration _config;

        /// <summary>  
        /// WebConfig读写辅助类  
        /// </summary>  
        /// <param name="path"></param>  
        public ConfigHelper()
        {
            _config = WebConfigurationManager.OpenWebConfiguration("~");
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (_config != null)
            {
                _config.Save();
            }
        }

        /// <summary>   
        /// 保存所作的修改   
        /// </summary>   
        public void Save()
        {
            _config.Save();
            _config = null;
        }

        /// <summary>
        /// 获取应用程序配置节点
        /// </summary>
        /// <param name="Key">节点名称</param>
        /// <param name="Section">配置节点路径名称</param>
        /// <returns>节点值</returns>
        public string GetValue(string Key, string Section = "appSettings")
        {
            string Value = string.Empty;
            try
            {
                switch (Section)
                {
                    case "appSettings":
                        var appSetting = (AppSettingsSection)_config.AppSettings;
                        if (appSetting.Settings[Key] != null)
                        {
                            Value = appSetting.Settings[Key].Value;
                        }
                        break;
                    case "connectionStrings":
                        var connectionSetting = (ConnectionStringsSection)_config.GetSection(Section);
                        if (connectionSetting.ConnectionStrings[Key] != null)
                        {
                            Value = connectionSetting.ConnectionStrings[Key].ConnectionString;
                        }
                        break;
                    default:
                        goto case "appSettings";
                }
            }
            catch
            {
            }
            //LogHelper.Info("Key=" + Key + "：" + Value);
            return Value;
        }

        /// <summary>   
        /// 设置应用程序配置节点，如果已经存在此节点，则会修改该节点的值，否则添加此节点  
        /// </summary>
        /// <param name="Key">节点名称</param>
        /// <param name="Value">节点值</param>
        /// <param name="Section">配置节点路径名称</param>
        public void SetValue(string Key, string Value, string Section = "appSettings")
        {
            switch (Section)
            {
                case "appSettings":
                    var appSetting = (AppSettingsSection)_config.AppSettings;
                    if (appSetting.Settings[Key] == null) //如果不存在此节点，则添加   
                    {
                        appSetting.Settings.Add(Key, Value);
                    }
                    else //如果存在此节点，则修改   
                    {
                        appSetting.Settings[Key].Value = Value;
                    }
                    break;
                case "connectionStrings":
                    var connectionSetting = (ConnectionStringsSection)_config.GetSection(Section);
                    if (connectionSetting.ConnectionStrings[Key] == null) //如果不存在此节点，则添加   
                    {
                        var connectionStringSettings = new ConnectionStringSettings(Key, Value);
                        connectionSetting.ConnectionStrings.Add(connectionStringSettings);
                    }
                    else //如果存在此节点，则修改   
                    {
                        connectionSetting.ConnectionStrings[Key].ConnectionString = Value;
                    }
                    break;
                default:
                    goto case "appSettings";
            }
            Save();
        }
    }
}
