using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NzbDrone.Core.MetadataSource.AniList.Resource
{
    public class AniListMedia<T>
    {
        public List<T> Media { get; set; }
    }
}
