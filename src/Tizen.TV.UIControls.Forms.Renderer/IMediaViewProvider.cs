using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMView = Tizen.Multimedia.MediaView;

namespace Tizen.TV.UIControls.Forms
{
    public interface IMediaViewProvider
    {
        MMView GetMediaView();
    }
}
