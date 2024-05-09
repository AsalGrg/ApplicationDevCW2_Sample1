import api_urls from "./api_urls";

export async function get_all_blogs(filter) {
    const api_url = api_urls.getFilteredBlogs(filter);
    // const token = "Bearer "+localStorage.getItem("token");
    try {
      const res = await fetch(api_url, 
        {
        method:"GET",
      }
      );
  
      return res;
    } catch (error) {
      console.error("Error fetching user data:", error);
      throw error; // Rethrow the error to propagate it to the caller
    }
  }