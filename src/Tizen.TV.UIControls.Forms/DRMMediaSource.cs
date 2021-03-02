using System;
using System.Collections.Generic;
using CommonUriMediaSource = Tizen.Theme.Common.UriMediaSource;

namespace Tizen.TV.UIControls.Forms
{
    public class DRMPropertyValue
    {
        Object _extradataValue;
        public Object Value => _extradataValue;

        public DRMPropertyValue(bool value)
        {
            _extradataValue = value;
        }

        public DRMPropertyValue(string value)
        {
            _extradataValue = value;
        }

        public static implicit operator DRMPropertyValue(bool value)
        {
            return new DRMPropertyValue(value);
        }

        public static implicit operator DRMPropertyValue(string value)
        {
            return new DRMPropertyValue(value);
        }
    }

    public class DRMMediaSource : CommonUriMediaSource
    {
        public Dictionary<string, DRMPropertyValue> ExtraData { get; } = new Dictionary<string, DRMPropertyValue>();
        public string LicenseUrl { get; set; }
    }
}
