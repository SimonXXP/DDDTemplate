using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using {{namespace}}.Database.POCO.Identities;
using {{namespace}}.Common.Constants;
using static {{namespace}}.Common.Constants.CommonEnum;

namespace {{namespace}}.Database.POCO.QueryParameters
{
    public class {{query-list-parameter-name}} : PagingListParameter
    {
{{Parameters}}

        public {{query-list-parameter-name}}() { }

        public {{query-list-parameter-name}}({{query-list-parameter-name}} source) : base(source)
        {
{{Parameter-Properties-Convertor}}
        }
    }
}