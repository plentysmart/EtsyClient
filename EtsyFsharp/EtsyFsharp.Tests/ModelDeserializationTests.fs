module ModelDeserializationTests
open System
open System.IO
open Xunit
let readFile filename = File.ReadAllText(filename) 
    
module ModelDeserialization =
    open EtsyFsharp.Model
    [<Fact>]
    let ``Listing response should be deserialized to Listing type``() = 
        let response = 
            readFile "ListingsResponse.json"
            |> EtsyFsharp.EtsyAPI.ModelMapper.deserializeTo<EtsyResponse<Listing>>
        Assert.NotNull response
        Assert.Equal(1, response.count)
        Assert.Equal(1, response.results.Length)

    [<Fact>]
    let ``Category response should be deserialized to Category type``() =
        let response = 
            readFile "Categories.json"
            |> EtsyFsharp.EtsyAPI.ModelMapper.deserializeTo<EtsyResponse<Category>>
        Assert.NotNull response
        Assert.Equal(31, response.count)
        Assert.Equal(31, response.results.Length)
        let first = response.results.[0]
        Assert.NotNull(first)
        Assert.Equal(first.category_id,69150467)
        Assert.Equal<string>(first.name ,"accessories")
        Assert.Equal<string>(first.meta_title.Value,"Handmade Accessories on Etsy - Belts, hats, pins, scarves")
        Assert.Equal<string>(first.meta_keywords.Value, "handmade accessories, handmade belt, handmade hat, handmade wallet, handmade scarf, handmade keychain, handmade necktie, handmade accessory")
        Assert.Equal<string>(first.meta_description.Value, "Shop for unique, handmade accessories for men and women on Etsy, a global handmade marketplace. Browse belts, hats, pins, scarves & more from independent artisans.")
        Assert.Equal<string>(first.page_description.Value, "Shop for unique, handmade accessories from our artisan community")
        Assert.Equal<string>(first.page_title.Value, "Handmade accessories")
        Assert.Equal<string>(first.category_name, "accessories")
        Assert.Equal<string>(first.short_name, "Accessories")
        Assert.Equal<string>(first.long_name, "Accessories")
        Assert.Equal(first.num_children, 27)