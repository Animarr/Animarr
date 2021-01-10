using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NzbDrone.Core.MetadataSource.AniList.Resource
{
    public class AniListAnime
    {
        public long Id { get; set; }
        public AniListAnimeTitle Title { get; set; }
    }
}
