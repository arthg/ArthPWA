using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nancy.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ArthPWA.Common;

namespace ArthPWA.Tests.Common.Helpers
{
    public static class TestBrowserExtensions
    {
        public static TModel BodyAsJson<TModel>(this BrowserResponse response)
        {
            if (!response.ContentType.Contains("application/json"))
            {
                throw new ArgumentException("Not json body.");
            }
            var json = response.Body.AsString();
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumJsonConverter());
            return JsonConvert.DeserializeObject<TModel>(json, settings);
        }

        public static dynamic BodyAsDynamic(this BrowserResponse response)
        {
            return response.BodyAsJson<dynamic>();
        }

        public static byte[] BodyAsBytes(this BrowserResponse response)
        {
            var responseStream = new MemoryStream();
            response.Context.Response.Contents(responseStream);
            return responseStream.ToArray();
        }

        public static BrowserContext QueryString(this BrowserContext context, string key, string value)
        {
            context.Query(key, value);
            return context;
        }

        public static BrowserContext Query(this BrowserContext context, IEnumerable<KeyValuePair<string, string>> queryValues)
        {
            foreach (var queryValue in queryValues)
            {
                context.Query(queryValue.Key, queryValue.Value);
            }
            return context;
        }

        public static BrowserContext HttpsBrowserContext(this BrowserContext context)
        {
            context.HttpsRequest();
            return context;
        }

        public static BrowserContext JsonBrowserContext(this BrowserContext context, object request)
        {
            context.Body(JsonConvert.SerializeObject(request));
            context.Header("Content-Type", "application/json");
            return context;
        }

        public static BrowserContext ApiAuthToken(this BrowserContext context, string token)
        {
            context.Header("Authorization", "Token " + token);
            return context;
        }

        private sealed class StringEnumJsonConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return null;
                }
                var nameProperty = JObject.Load(reader).Properties().Single(p => p.Name.Equals("name", StringComparison.InvariantCultureIgnoreCase));
                return StringEnumBase.Parse(objectType, nameProperty.Value.ToString());
            }

            public override bool CanConvert(Type objectType)
            {
                return typeof(StringEnumBase).IsAssignableFrom(objectType);
            }

            public override bool CanWrite
            {
                get { return false; }
            }
        }
    }
}
