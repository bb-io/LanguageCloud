using Apps.LanguageCloud.Models;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace Apps.LanguageCloud;

public class LanguageCloudRequest : RestRequest
{
    public LanguageCloudRequest(string endpoint, Method method) :
        base(endpoint, method)
    {
    }


}