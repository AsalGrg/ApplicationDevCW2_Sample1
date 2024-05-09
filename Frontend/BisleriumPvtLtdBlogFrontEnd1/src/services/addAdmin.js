import api_urls from "./api_urls";

export default async function add_admin(
    userRegisterData
){
    const api_url= api_urls.registerAdmin();

    // console.log(userLoginData)
    
    const res = await fetch(api_url,
    {   
        method: 'POST',
        body:new Blob([JSON.stringify(userRegisterData)], {type: 'application/json'})

    })
    return res;
}