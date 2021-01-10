using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NLog;
using NzbDrone.Common.Cloud;
using NzbDrone.Common.Extensions;
using NzbDrone.Common.GQL;
using NzbDrone.Common.Http;
using NzbDrone.Common.Serializer;
using NzbDrone.Core.Configuration;
using NzbDrone.Core.Exceptions;
using NzbDrone.Core.Languages;
using NzbDrone.Core.MediaCover;
using NzbDrone.Core.MetadataSource.AniList.Resource;
using NzbDrone.Core.MetadataSource.SkyHook.Resource;
using NzbDrone.Core.Movies;
using NzbDrone.Core.Movies.AlternativeTitles;
using NzbDrone.Core.Movies.Credits;
using NzbDrone.Core.Movies.Translations;
using NzbDrone.Core.Parser;

namespace NzbDrone.Core.MetadataSource.AniList
{
    public class AniListProxy : ISearchForNewAnime
    {
        private readonly IGraphQLClient _graphQLClient;
        private readonly Logger _logger;

        private readonly IConfigService _configService;
        private readonly IMovieService _movieService;
        private readonly IMovieTranslationService _movieTranslationService;

        public AniListProxy(IGraphQLClient graphQLClient,
            IConfigService configService,
            IMovieService movieService,
            IMovieTranslationService movieTranslationService,
            Logger logger)
        {
            _graphQLClient = graphQLClient;
            _configService = configService;
            _movieService = movieService;
            _movieTranslationService = movieTranslationService;

            _logger = logger;
        }

        public List<Movie> SearchForNewAnime(string title)
        {
            var query = @"
                query ($searchString: String!) {
                    Page {
                        media(search: $searchString, type: ANIME) {
                            id,
                            title {
                                romaji,
                                english,
                                native
                            }
                        }
                    }
                }";
            var variables = new { searchString = title };

            var graphQlRequest = new GraphQLRequestBuilder("https://graphql.anilist.co")
                .WithQuery(query)
                .WithVariables(variables)
                .Build();

            var response = _graphQLClient.Execute<AniListPage<AniListMediaAnime>>(graphQlRequest).Result;

            Console.WriteLine(response);

            throw new NotImplementedException();
        }
    }
}
