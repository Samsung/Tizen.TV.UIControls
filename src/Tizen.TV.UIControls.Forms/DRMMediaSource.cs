using System;
using System.Collections.Generic;
using System.Text;

namespace Tizen.TV.UIControls.Forms
{
    public class ExtradataValue
    {
        Object _extradataValue;
        public ExtradataValue(int value)
        {
            _extradataValue = value;
        }
        public ExtradataValue(bool value)
        {
            _extradataValue = value;
        }
        public ExtradataValue(string value)
        {
            _extradataValue = value;
        }
        public static implicit operator ExtradataValue(int value)
        {
            return new ExtradataValue(value);
        }
        public static implicit operator ExtradataValue(bool value)
        {
            return new ExtradataValue(value);
        }
        public static implicit operator ExtradataValue(string value)
        {
            return new ExtradataValue(value);
        }
    }
    public class DRMMediaSource : UriMediaSource
    {
        public Dictionary<string, ExtradataValue> Extradata { get; } = new Dictionary<string, ExtradataValue>();
        public string LicenseUrl { get; set; }

        public string Url { get; set; }

        public void AddProperty(string propertyName, int propertyValue)
        {
            Extradata.Add(propertyName, propertyValue);
        }
        public void AddProperty(string propertyName, bool propertyValue)
        {
            Extradata.Add(propertyName, propertyValue);
        }
        public void AddProperty(string propertyName, string propertyValue)
        {
            Extradata.Add(propertyName, propertyValue);
        }
    }
}
