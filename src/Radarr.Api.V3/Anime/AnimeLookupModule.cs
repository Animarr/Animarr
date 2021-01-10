using System.Collections.Generic;
using System.Linq;
using Nancy;
using NzbDrone.Core.Configuration;
using NzbDrone.Core.Languages;
using NzbDrone.Core.MediaCover;
using NzbDrone.Core.MetadataSource;
using NzbDrone.Core.Movies;
using NzbDrone.Core.Organizer;
using Radarr.Http;
using Radarr.Http.REST;

namespace Radarr.Api.V3.Movies
{
    public class AnimeLookupModule : RadarrRestModule<MovieResource>
    {
        private readonly ISearchForNewAnime _searchProxy;
        private readonly IProvideMovieInfo _movieInfo;
        private readonly IBuildFileNames _fileNameBuilder;
        private readonly IMapCoversToLocal _coverMapper;
        private readonly IConfigService _configService;

        public AnimeLookupModule(ISearchForNewAnime searchProxy,
                                 IProvideMovieInfo movieInfo,
                                 IBuildFileNames fileNameBuilder,
                                 IMapCoversToLocal coverMapper,
                                 IConfigService configService)
            : base("/anime/lookup")
        {
            _movieInfo = movieInfo;
            _searchProxy = searchProxy;
            _fileNameBuilder = fileNameBuilder;
            _coverMapper = coverMapper;
            _configService = configService;
            Get("/", x => Search());
        }

        private object Search()
        {
            var searchResults = _searchProxy.SearchForNewAnime((string)Request.Query.term);
            return MapToResource(searchResults);
        }

        private IEnumerable<MovieResource> MapToResource(IEnumerable<Movie> movies)
        {
            foreach (var currentMovie in movies)
            {
                var availDelay = _configService.AvailabilityDelay;
                var translation = currentMovie.Translations.FirstOrDefault(t => t.Language == (Language)_configService.MovieInfoLanguage);
                var resource = currentMovie.ToResource(availDelay, translation);

                _coverMapper.ConvertToLocalUrls(resource.Id, resource.Images);

                var poster = currentMovie.Images.FirstOrDefault(c => c.CoverType == MediaCoverTypes.Poster);
                if (poster != null)
                {
                    resource.RemotePoster = poster.RemoteUrl;
                }

                resource.Folder = _fileNameBuilder.GetMovieFolder(currentMovie);

                yield return resource;
            }
        }
    }
}
