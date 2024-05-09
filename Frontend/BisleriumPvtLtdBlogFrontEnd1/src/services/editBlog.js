import api_urls from "./api_urls";

export async function edit_blog( title, Body, coverImage, blogId) {

    const api_url = api_urls.editBlog(blogId);
    const token = "Bearer "+localStorage.getItem("token");

    const formData = new FormData();

    formData.append('Title',title )
    formData.append('Body', Body),
    
    coverImage instanceof File && formData.append('CoverImage', coverImage)

    try {
      const res = await fetch(api_url, 
        {
        method:"PUT",
        headers:{
            Authorization: token
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