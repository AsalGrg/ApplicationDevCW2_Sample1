import api_urls from "./api_urls";

export default async function loginUser(
    userLoginData
){
    const api_url= api_urls.login();

    console.log(api_url)
    
    const res = await fetch(api_url,
    {   
        method: 'POST',
        headers:{
            "Access-Control-Allow-Origin": "*"
        },
        body:userLoginData
    })
    return res;
}