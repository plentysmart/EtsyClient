namespace EtsyFsharp
open System
module Web = 
    type HttpMethod = 
        | GET
        | POST
        | DELETE
        | PUT

    type Request = { 
        Path:string;
        QueryStringParameters: ((string*string) list)
        Method:HttpMethod;
        Content:string;
        }
    type Header = {Name:string; Value:string}
    type  Response = {
        Request:Request;
        Headers: Header list;
        HttpCode: int;
        TextContent: string; 
    }
    let  defaultRequest = {Path="";Method=HttpMethod.GET;Content="";QueryStringParameters=[]}

    let  withPath (path:string) (request: Request ) =  {request with Path = path}
    let  withQueryStringParameter (key:string) (value:string) (request: Request) ={ request with QueryStringParameters = (key,value)::request.QueryStringParameters}
    let  withMethod (httpMethod:HttpMethod)(request: Request) = { request with Method = httpMethod}
    let ExecuteRequest (request:Request) :Response =
            raise (NotImplementedException())