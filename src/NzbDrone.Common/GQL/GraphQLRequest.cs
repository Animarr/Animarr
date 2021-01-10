using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NzbDrone.Common.GQL
{
    public class GraphQLRequest
    {
        internal GraphQLRequest()
        {
        }

        public Uri Url { get; set; }

        public string Query { get; set; }

        public object Variables { get; set; }
    }
}
