namespace EtsyFsharp
open System
module EtsyAPI =
    let  formatToArray (ids: 'a list) =
            String.Join(",",ids)
    module StandardParameters =
        open EtsyFsharp.Web
        let  withApiKey = withQueryStringParameter "api_key"
        let  withfields (fields:string list) = withQueryStringParameter "fields" (formatToArray fields)
        let  withLimit (limit: int) = withQueryStringParameter "limit" (limit.ToString())
        let  withOffset (offset: int) = withQueryStringParameter "offset" (offset.ToString())

  