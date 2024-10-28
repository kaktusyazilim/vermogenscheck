using CorporateWebProject.Application.Dto.Content.Items.Concrete;
using CorporateWebProject.Application.Utilities.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Content.Items.Abstract
{
    public abstract class IBaseItem
    {

        private class MyCustomConverter : JsonCreationConverter<IBaseItem>
        {
            protected override IBaseItem Create(Type objectType,
              Newtonsoft.Json.Linq.JObject jObject)
            {
                if (jObject.Value<string>("type") != null) return new CardDTO();
                else if (jObject.Value<string>("type") != null) return new ArticleDTO();
                else return new WebViewItemResponse();
            }
        }
    }
}
