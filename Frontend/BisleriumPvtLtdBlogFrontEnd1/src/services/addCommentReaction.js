import api_urls from "./api_urls";

export async function add_comment_reaction(
    reactionData
) {
    const api_url = api_urls.addCommentReaction();
    const token = "Bearer "+localStorage.getItem("token");
    
    try {
      const res = await fetch(api_url, 
        {
        method:"POST",
          headers:{
              Authorization: token,
          },
          body:new Blob([JSON.stringify(reactionData)], {type: 'application/json'})
      }
      );
  
      return res;
    } catch (error) {
      console.error("Error fetching user data:", error);
      throw error; // Rethrow the error to propagate it to the caller
    }
  }