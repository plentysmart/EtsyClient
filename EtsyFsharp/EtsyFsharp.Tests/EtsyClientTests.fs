namespace EtsyFsharp.Tests
open Xunit
open System
module WebTests =
    open EtsyFsharp.EtsyAPI
    open ListingsApi
    [<Fact>]
    let ``getListing should return listing``() = 
        let basicRequest =
             withSortOrder Created Down None
//                |> withIncludeList [Images]
                |> withField "listing_id"
                |> withField "creation_tsz"
                |> withField "last_modified_tsz"
        let executeRequest request=
                System.Diagnostics.Debug.WriteLine(sprintf "Starting page %d" request.Paging.Value.Page.Value)
                let response = client.GetActive request    
                System.Diagnostics.Debug.WriteLine(sprintf "Page %d Finished" request.Paging.Value.Page.Value)
                response
        let pageSize = 100;
                
        let requests =
            [1..2]
            |> List.map (fun i -> basicRequest |> withPage i pageSize)
            |> List.map (fun i -> i.Value)
        let responses = 
            requests
            |> List.map executeRequest
        Assert.NotEmpty(responses)  

