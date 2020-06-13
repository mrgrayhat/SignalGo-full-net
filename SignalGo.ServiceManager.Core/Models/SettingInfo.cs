﻿using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace SignalGo.ServiceManager.Core.Models
{
    public class SettingInfo
    {
        private static SettingInfo _Current = null;

        private readonly static string ServerDbName = "Data.json";

        public static SettingInfo Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = LoadSettingInfo();
                    SaveSettingInfo();
                }
                return _Current;
            }
        }
        //[JsonIgnore]
        //public Guid ServerKey { get; set; }
        public ObservableCollection<ServerInfo> ServerInfo { get; set; } = new ObservableCollection<ServerInfo>();

        public static SettingInfo LoadSettingInfo()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ServerDbName);
                if (!File.Exists(path) || File.ReadAllLinesAsync(path).Result.Length <= 0)
                {
                    File.Delete(path);
                    return new SettingInfo()
                    {
                        ServerInfo = new ObservableCollection<ServerInfo>()
                    };
                }
                return JsonConvert.DeserializeObject<SettingInfo>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ServerDbName), Encoding.UTF8));
            }
            catch
            {
                return new SettingInfo()
                {
                    ServerInfo = new ObservableCollection<ServerInfo>()
                };
            }
        }

        public static void SaveSettingInfo()
        {
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ServerDbName), JsonConvert.SerializeObject(Current,Formatting.Indented), Encoding.UTF8);
        }
    }
}
