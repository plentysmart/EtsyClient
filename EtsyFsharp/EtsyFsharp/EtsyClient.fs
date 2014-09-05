namespace EtsyFsharp

open System

type EtsyClient(api_key) =
    let apikey = api_key 

    member x.GetListings() = 
        raise (NotImplementedException())

