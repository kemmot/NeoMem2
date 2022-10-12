// <copyright file="SettingHive.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;

using NeoMem2.Utils.ComponentModel;

namespace NeoMem2.Core.Settings
{
    public class SettingHive : NotifyPropertyChangedBase
    {
        private static SettingHive m_Instance;


        public static SettingHive Instance
        {
            get { return m_Instance ?? (m_Instance = new SettingHive()); }
        }
        

        private readonly Dictionary<string, string> m_Settings = new Dictionary<string, string>();


        public string this[string key]
        {
            get { return GetSetting(key); }
            set { SetSetting(key, value); }
        }


        public T GetSetting<T>(string key, T defaultValue = default(T))
        {
            string defaultValueString = ReferenceEquals(defaultValue, null) ? string.Empty : defaultValue.ToString();
            object stringValue = GetSetting(key, defaultValueString);

            T result;
            if (typeof(T) == typeof(string))
            {
                result = (T)stringValue;
            }
            else
            {
                var parseMethod = typeof(T).GetMethod("Parse");
                object resultObject = parseMethod.Invoke(null, new [] { stringValue });
                result = (T)resultObject;
            }

            return result;
        }

        public string GetSetting(string key, string defaultValue = "")
        {
            string result;
            if (m_Settings.ContainsKey(key))
            {
                result = m_Settings[key];
            }
            else
            {
                result = defaultValue;
                SetSetting(key, defaultValue);
            }

            return result;
        }

        public void SetSetting(string key, DateTime value)
        {
            SetSetting(key, value.ToString());
        }

        public void SetSetting(string key, long value)
        {
            SetSetting(key, value.ToString());
        }

        public void SetSetting(string key, int value)
        {
            SetSetting(key, value.ToString());
        }

        public void SetSetting(string key, string value)
        {
            m_Settings[key] = value;
            OnPropertyChanged(new PropertyChangedEventArgs(key));
        }
    }
}
