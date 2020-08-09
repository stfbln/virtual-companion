using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Virtualcompanion.Core.Contexts.Features.Audios
{
    public class AudioInputFeature
    {
        public CultureInfo Culture { get; set; }

        public byte[] Buffer { get; set; }
    }
}
