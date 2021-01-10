using System.Collections.Generic;
using NzbDrone.Core.Movies;

namespace NzbDrone.Core.MetadataSource
{
    public interface ISearchForNewAnime
    {
        List<Movie> SearchForNewAnime(string title);
    }
}
