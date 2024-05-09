import api_urls from "./api_urls";

export default async function verify_email(
    verifyEmailDetails
){
    console.log(verifyEmailDetails)
    const api_url= api_urls.verifyEmail();

    // console.log(userLoginData)
    
    const res = await fetch(api_url,
    {   
        method: 'POST',
        body:new Blob([JSON.stringify(verifyEmailDetails)], {type: 'application/json'})

    })
    return res;
}