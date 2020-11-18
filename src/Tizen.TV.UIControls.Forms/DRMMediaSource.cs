using System;
using System.Collections.Generic;
using System.Text;

namespace Tizen.TV.UIControls.Forms
{
    public class ExtradataValue
    {
        Object _extradataValue;
        public Object Value => _extradataValue;
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
    }
}
