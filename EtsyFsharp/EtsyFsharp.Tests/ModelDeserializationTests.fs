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
        Assert.Equal(1L, response.count)
        Assert.Equal(1, response.results.Length)

    [<Fact>]
    let ``Category response should be deserialized to Category type``() =
        let response = 
            readFile "Categories.json"
            |> EtsyFsharp.EtsyAPI.ModelMapper.deserializeTo<EtsyResponse<Category>>
        Assert.NotNull response
        Assert.Equal(31L, response.count)
        Assert.Equal(31, response.results.Length)
        let first = response.results.[0]
        Assert.NotNull(first)
        Assert.Equal(first.category_id,69150467L)
        Assert.Equal<string>(first.name ,"accessories")
        Assert.Equal<string>(first.meta_title.Value,"Handmade Accessories on Etsy - Belts, hats, pins, scarves")
        Assert.Equal<string>(first.meta_keywords.Value, "handmade accessories, handmade belt, handmade hat, handmade wallet, handmade scarf, handmade keychain, handmade necktie, handmade accessory")
        Assert.Equal<string>(first.meta_description.Value, "Shop for unique, handmade accessories for men and women on Etsy, a global handmade marketplace. Browse belts, hats, pins, scarves & more from independent artisans.")
        Assert.Equal<string>(first.page_description.Value, "Shop for unique, handmade accessories from our artisan community")
        Assert.Equal<string>(first.page_title.Value, "Handmade accessories")
        Assert.Equal<string>(first.category_name, "accessories")
        Assert.Equal<string>(first.short_name, "Accessories")
        Assert.Equal<string>(first.long_name, "Accessories")
        Assert.Equal(first.num_children, 27L)
    [<Fact>]
    let ``Users response should be deserialized to User type``() =
        let response = 
            readFile "User.json"
            |> EtsyFsharp.EtsyAPI.ModelMapper.deserializeTo<EtsyResponse<User>>
        Assert.NotNull response
        Assert.Equal(38554L, response.count)
        Assert.Equal(100, response.results.Length)
        let first = response.results.[0]
        Assert.Equal(first.user_id,51798028L)
        Assert.Equal<string>(first.login_name,"siamfinecraft")
        Assert.Equal(first.creation_tsz, new DateTime(2014,8,8,08,26,13))
        Assert.True(first.referred_by_user_id.IsNone)
        Assert.Equal(first.feedback_info.count,0L)
        Assert.True(first.feedback_info.score.IsNone)
    [<Fact>]
    let ``Shops response should be deserialized to Shop type``() =
        let response = 
            readFile "Shop.json"
            |> EtsyFsharp.EtsyAPI.ModelMapper.deserializeTo<EtsyResponse<Shop>>
        Assert.NotNull response
        Assert.Equal(50100L, response.count)
        Assert.Equal(100, response.results.Length)
        let first = response.results.[0]
        Assert.Equal(first.shop_id,9976779L)
        Assert.Equal(first.user_id,53050038L)
        Assert.Equal<string>(first.shop_name,"Custardtub")
        Assert.Equal(first.creation_tsz, new DateTime(2014,9,7,13,34,50))
        Assert.True(first.title.IsNone)
        Assert.True(first.announcement.IsNone)
        Assert.Equal<string>(first.currency_code,"GBP")
        Assert.False(first.is_vacation)
        Assert.True(first.vacation_message.IsNone)
        Assert.True(first.sale_message.IsNone)
        Assert.True(first.digital_sale_message.IsNone)
        Assert.Equal(first.last_updated_tsz, new DateTime(2014,9,7,13,34,50))
        Assert.Equal(first.listing_active_count,1L)
        Assert.Equal<string>(first.url,"https://www.etsy.com/shop/Custardtub?utm_source=fapiclient&utm_medium=api&utm_campaign=api")
   