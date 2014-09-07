namespace EtsyFsharp
open System
module EtsyAPI =
    open Newtonsoft
    open Newtonsoft.Json
    open HttpClient
    open EtsyFsharp.Model
    open System.Collections.Generic
    open Microsoft.FSharp.Reflection
    open Newtonsoft.Json
    open Newtonsoft.Json.Converters
    let  formatToArray (ids: 'a list) =
            String.Join(",",ids)
    type OptionConverter() =
        inherit JsonConverter()
    
        override x.CanConvert(t) = 
            t.IsGenericType && t.GetGenericTypeDefinition() = typedefof<option<_>>

        override x.WriteJson(writer, value, serializer) =
            let value = 
                if value = null then null
                else 
                    let _,fields = FSharpValue.GetUnionFields(value, value.GetType())
                    fields.[0]  
            serializer.Serialize(writer, value)

        override x.ReadJson(reader, t, existingValue, serializer) =        
            let innerType = t.GetGenericArguments().[0]
            let innerType = 
                if innerType.IsValueType then typedefof<Nullable<_>>.MakeGenericType([|innerType|])
                else innerType        
            let value = serializer.Deserialize(reader, innerType)
            let cases = FSharpType.GetUnionCases(t)
            if value = null then FSharpValue.MakeUnion(cases.[0], [||])
            else FSharpValue.MakeUnion(cases.[1], [|value|])
    type EpochDateConverter() =
        inherit Newtonsoft.Json.JsonConverter()
        override x.CanConvert(objectType:Type):bool =
            objectType = typedefof<DateTime>
        override x.ReadJson( reader:JsonReader, objectType:Type, existingValue:Object ,serializer: JsonSerializer ):Object =
            let t = Int64.Parse((string)reader.Value);
            DateTime(1970, 1, 1).AddSeconds(float t) :> Object
        override x.WriteJson(writer:JsonWriter, value:Object,  serializer: JsonSerializer) =
            raise (NotImplementedException())
    module StandardParameters =
        let  withApiKey apikey= withQueryStringItem  {name="api_key";value=apikey}
        let  withfields (fields:string list) = withQueryStringItem {name="fields"; value=(formatToArray fields)}
        let  withLimit (limit: int) = withQueryStringItem {name="limit";value= (limit.ToString())}
        let  withOffset (offset: int) = withQueryStringItem {name="offset"; value=(offset.ToString())}
    module ModelMapper =
        let deserializeTo<'T> (json:string) = Json.JsonConvert.DeserializeObject<'T>(json, EpochDateConverter(), OptionConverter())
            
    type Client(api_key:string) =
        
        let baseUrl = "https://openapi.etsy.com/v2"
        let createUrl (path:string):string =
            if path.StartsWith("/") then
                baseUrl + path
            else
                baseUrl+"/"+path
        member x.GetListing (ids:int list): EtsyResponse<Listing> = 
            let request = 
                createUrl ("/listings/"+(formatToArray ids))
                |> createRequest Get
                |> withResponseCharacterEncoding "utf-8"
                |> StandardParameters.withApiKey api_key
            System.Diagnostics.Debug.WriteLine( sprintf "%A" request)
            let response = request |> getResponse
            let result = ModelMapper.deserializeTo<EtsyResponse<Listing>>(response.EntityBody.Value)
            result
      