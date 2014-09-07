module ListingsApi

type Paging = {
    Limit:int option;
    Offset: int option;
    Page: int option;
    }

type ListingSortOrder = 
    |Created
    |Price
    |Score
type SortDirection = 
    |Up
    |Down
type ListingSortingOptions = {
    Order: ListingSortOrder;
    Direction: SortDirection;
    }
type IncludeOption =
    |User
    |Shop 
    |Images
    |MainImage
    |ShippingInfo
    |PaymentInfo
type ActiveListingsRequest = {
    Paging: Paging option;
    Keywords: string option;
    Sort: ListingSortingOptions option;
    MinPrice: float option;
    MaxPrice: float option;
    Tags: string list;
    Latitude: decimal option;
    Longtitude: decimal option;
    IncudeOptions: IncludeOption list;
    Fields: string list;
    }

let private emptyRequest = {Paging=None; Keywords=None; Sort=None; MinPrice=None;MaxPrice=None;Tags=[];Latitude=None;Longtitude=None; IncudeOptions= []; Fields=[]}
let private requestOrEmpty  (request:ActiveListingsRequest option) =
    match request with 
    |Some r -> r
    |None -> emptyRequest
let withPage (page:int) (limit:int) (request:ActiveListingsRequest option) =
    Some {(requestOrEmpty request) with Paging = Some {Limit = Some limit;Page=Some page;Offset=None}}

let withKeywords (keywords:string) (request:ActiveListingsRequest option) =
    Some {(requestOrEmpty request) with Keywords = Some keywords}
    
let withMinPrice (minPrice:float) (request:ActiveListingsRequest option) =
    Some {(requestOrEmpty request) with MinPrice = Some minPrice}

let withMaxPrice (maxPrice:float) (request:ActiveListingsRequest option) =
    Some {(requestOrEmpty request) with MaxPrice = Some maxPrice}

let withSortOrder (order:ListingSortOrder) (direction:SortDirection) (request:ActiveListingsRequest option) =
    Some {(requestOrEmpty request) with Sort = Some {Order= order; Direction = direction}}
let withIncludeList (includes: IncludeOption list) (request:ActiveListingsRequest option) =
    let req = (requestOrEmpty request)
    Some {req with IncudeOptions = includes @ req.IncudeOptions}
    
let withInclude (includeObject:IncludeOption)(request:ActiveListingsRequest option) = withIncludeList [includeObject]

let withField (name:string) (request: ActiveListingsRequest option) =
    let req = (requestOrEmpty request)
    Some {req with Fields = name:: req.Fields}