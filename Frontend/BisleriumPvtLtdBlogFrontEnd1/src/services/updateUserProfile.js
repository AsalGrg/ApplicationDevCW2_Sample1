import api_urls from "./api_urls";

export async function update_user_profile(updateProfileDetails) {
  const api_url = api_urls.updateUserProfile();
  const token = "Bearer " + localStorage.getItem("token");

  try {
    const res = await fetch(api_url, {
      method: "PUT",
      headers: {
        Authorization: token,
      },
      body: new Blob([JSON.stringify(updateProfileDetails)], {
        type: "application/json",
      }),
    });

    return res;
  } catch (error) {
    console.error("Error fetching user data:", error);
    throw error; // Rethrow the error to propagate it to the caller
  }
}
