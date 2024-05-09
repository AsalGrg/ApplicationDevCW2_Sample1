import api_urls from "./api_urls";

export default async function loginUser(
    userLoginData
){
    const api_url= api_urls.login();

    console.log(userLoginData)
    
    const res = await fetch(api_url,
    {   
        method: 'POST',
        body:new Blob([JSON.stringify(userLoginData)], {type: 'application/json'})

    })
    return res;
}