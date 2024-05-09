import api_urls from "./api_urls";

export default async function register_user(
    userRegisterData
){
    const api_url= api_urls.register();

    // console.log(userLoginData)
    
    const res = await fetch(api_url,
    {   
        method: 'POST',
        body:new Blob([JSON.stringify(userRegisterData)], {type: 'application/json'})

    })
    return res;
}