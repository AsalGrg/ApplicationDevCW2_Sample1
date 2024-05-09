import api_urls from "./api_urls";

export default async function forgot_password(email) {
  // console.log(verifyEmailDetails)
  const api_url = api_urls.forgotPassword();

  const res = await fetch(api_url, {
    method: "POST",
    body: new Blob([JSON.stringify({ email: email })], {
      type: "application/json",
    }),
  });
  return res;
}
