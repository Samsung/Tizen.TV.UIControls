using System;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms.Impl
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class ExportMediaSourceHandlerAttribute : HandlerAttribute
    {
        public ExportMediaSourceHandlerAttribute(Type handler, Type target) : base(handler, target)
        {
        }
    }
}