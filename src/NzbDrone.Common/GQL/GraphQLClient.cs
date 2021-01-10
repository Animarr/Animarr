using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace NzbDrone.Common.GQL
{
    public interface IGraphQLClient
    {
        Task<T> Execute<T>(GraphQLRequest graphQLRequest);
    }

    public class GraphQLClient : IGraphQLClient
    {
        public async Task<T> Execute<T>(GraphQLRequest graphQLRequest)
        {
            var request = new GraphQL.GraphQLRequest
            {
                Query = graphQLRequest.Query,
                Variables = graphQLRequest.Variables
            };

            var graphQLClient = new GraphQLHttpClient("https://graphql.anilist.co", new NewtonsoftJsonSerializer());
            var response = await graphQLClient.SendQueryAsync<T>(request).ConfigureAwait(true);
            return response.Data;
        }
    }
}
