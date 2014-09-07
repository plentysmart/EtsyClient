module EtsyFsharp.Model
open System
type EtsyResponse<'T> ={
    count:int64;
    results:'T array;
    ``type``:string;
    }
type FeedBackInfo = {
    count:int64;
    score:int64 option;
    }
type User = {
    user_id:int64;
    login_name:string;
    creation_tsz:DateTime;
    referred_by_user_id: int64 option;
    feedback_info: FeedBackInfo;
    awaiting_feedback_count: int64 option;
    }

type Category ={
    category_id:int64;
    name:string;
    meta_title:string option;
    meta_keywords:string option;
    meta_description: string option;
    page_description: string option;
    page_title: string option;
    category_name: string;
    short_name:string;
    long_name:string;
    num_children:int64;
    }
type MainImage = {
    listing_image_id: int64;
    listing_id: int64;
    }
type ListingImage = {
    listing_image_id: int64;
    hex_code: string;
    red:  int64 option;
    green: int64 option;
    blue: int64 option;
    hue: int64 option;
    saturation: int64 option;
    brightness: int64 option;
    is_black_and_white: bool option;
    creation_tsz: DateTime;
    listing_id: int64;
    rank: int64 option;
    url_75x75: string;
    url_170x135: string; 
    url_570xN:string; 
    url_fullxfull: string;
    full_height: int64 option;
    full_width: int64 option;
}
type ShippingInfo = {
    shipping_info_id: int64;
    origin_country_id: int64;
    destination_country_id:int64 option;
    currency_code: string option;
    primary_cost: string option;
    secondary_cost:string option;
    listing_id: int64;
    region_id: int64 option;
    origin_country_name: string option;
    destination_country_name: string option;
    }
    
type Shop ={
    shop_id: int64;
    shop_name: string;
    user_id: int64;
    creation_tsz: DateTime;
    title: string option;
    announcement: string option;
    currency_code: string;
    is_vacation: bool;
    vacation_message: string option;
    sale_message: string option;
    digital_sale_message: string option;
    last_updated_tsz: DateTime;
    listing_active_count: int64;
    login_name: string;
    accepts_custom_requests: bool;
    policy_welcome: string option;
    policy_payment: string option;
    policy_shipping: string option;
    policy_refunds: string option;
    policy_additional: string option;
    policy_seller_info: string option;
    policy_updated_tsz: DateTime;
    vacation_autoreply: string option;
    url: string;
    image_url_760x100: string option;
    num_favorers: int64;
    languages: string list
    }
type Listing = { 
    listing_id:int64;
    state:string;
    user_id:int64;
    category_id:int64;
    title:string;
    description:string;
    creation_tsz:DateTime;
    ending_tsz:DateTime;
    original_creation_tsz:DateTime;
    last_modified_tsz:DateTime;
    price:string
    currency_code:string;
    quantity:int64;
    tags: string array;
    category_path: string array;
    category_path_ids: int64 array;
    materials: string array;
    shop_section_id :int64 option;
    featured_rank:string option;
    state_tsz:DateTime;
    url:string;
    views:int64;
    num_favorers:int64;
    shipping_template_id:int64 option;
    shipping_profile_id:int64 option;
    processing_min:int64 option;
    processing_max:int64 option;
    who_made:string option;
    is_supply: bool option;
    when_made:string option;
    is_private:bool;
    recipient:string;
    occasion:string;
    style:string array;
    non_taxable:bool;
    is_customizable:bool;
    is_digital:bool;
    file_data:string;
    has_variations:bool;
    language:string;
    User: User option;
    Images: ListingImage list;
    MainImage: MainImage option;
    ShippingInfo: ShippingInfo list;
    Shop: Shop option;
    }