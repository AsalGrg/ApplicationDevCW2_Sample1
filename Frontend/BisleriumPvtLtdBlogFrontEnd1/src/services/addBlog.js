import api_urls from "./api_urls";

export async function add_blog(
    title, Body, coverImage
) {
    const api_url = api_urls.addBlog();
    const token = "Bearer "+localStorage.getItem("token");
    
    const formData = new FormData();

    formData.append('Title',title )
    formData.append('Body', Body),
    formData.append('CoverImage', coverImage)

    try {
      const res = await fetch(api_url, 
        {
        method:"POST",
          headers:{
              Authorization: token,
          },
          body: formData
      }
      );
  
      return res;
    } catch (error) {
      console.error("Error fetching user data:", error);
      throw error; // Rethrow the error to propagate it to the caller
    }
  }