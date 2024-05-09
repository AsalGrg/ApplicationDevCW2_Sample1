import api_urls from "./api_urls";

export async function delete_blog(
    blogId
) {
    const api_url = api_urls.editBlog(blogId);
    const token = "Bearer "+localStorage.getItem("token");
  
    try {
      const res = await fetch(api_url, 
        {
        method:"DELETE",
          headers:{
              Authorization: token,
          }
      }
      );
  
      return res;
    } catch (error) {
      console.error("Error fetching user data:", error);
      throw error; // Rethrow the error to propagate it to the caller
    }
  }