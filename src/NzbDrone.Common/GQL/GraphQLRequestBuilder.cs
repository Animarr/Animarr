using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NzbDrone.Common.GQL
{
    public class GraphQLRequestBuilder
    {
        private readonly Uri _Url;
        private string _Query;
        private object _Variables;

        public GraphQLRequestBuilder(string url)
            : this(new Uri(url))
        {
        }

        public GraphQLRequestBuilder(Uri url)
        {
            _Url = url;
        }

        public GraphQLRequestBuilder WithQuery(string query)
        {
            _Query = query;
            return this;
        }

        public GraphQLRequestBuilder WithVariables(object variables)
        {
            _Variables = variables;
            return this;
        }

        public GraphQLRequest Build()
        {
            var graphQLRequest = new GraphQLRequest
            {
                Url = _Url,
                Query = _Query,
                Variables = _Variables
            };
            return graphQLRequest;
        }
    }
}
