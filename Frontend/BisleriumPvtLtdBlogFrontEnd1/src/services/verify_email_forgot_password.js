import api_urls from "./api_urls";

export default async function change_forgot_password(
    forgotPasswordDetails
){
    const api_url= api_urls.changeForgotPassword();

    // console.log(userLoginData)
    
    const res = await fetch(api_url,
    {   
        method: 'POST',
        body:new Blob([JSON.stringify(forgotPasswordDetails)], {type: 'application/json'})

    })
    return res;
}