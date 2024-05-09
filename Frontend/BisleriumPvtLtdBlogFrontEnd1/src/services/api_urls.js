const BASE_URL = "https://localhost:7055"

const api_urls= {
    login: ()=> `${BASE_URL}/login`,
    register: ()=> `${BASE_URL}/api/CustomUser/register`,
    registerAdmin: ()=> `${BASE_URL}/api/CustomUser/register/admin`,
    verifyEmail : ()=>`${BASE_URL}/api/CustomUser/verify-email`,
    forgotPassword: ()=> `${BASE_URL}/api/CustomUser/forgotPassword`,
    changeForgotPassword: ()=>`${BASE_URL}/api/CustomUser/reset-password`,
    getUserData: ()=> `${BASE_URL}/api/CustomUser/user-details`,
    getFilteredBlogs: (filter)=> `${BASE_URL}/getBlogsFilter/${filter}`,
    getBlogDetails: (id)=> `${BASE_URL}/${id}`,
    addBlogReaction: ()=> `${BASE_URL}/addReaction`,
    deleteBlogReaction: (reactionId)=> `${BASE_URL}/deleteReaction/${reactionId}`,
    updateBlogReaction: (reactionId)=> `${BASE_URL}/updateReaction/${reactionId}`,
    addCommentReaction: ()=> `${BASE_URL}/addCommentReaction`,
    updateCommentReaction: (reactionId)=> `${BASE_URL}/updateCommentReaction/${reactionId}`,
    deleteCommentReaction: (reactionId)=> `${BASE_URL}/deleteCommentReaction/${reactionId}`,
    addComment: ()=> `${BASE_URL}/addComment`,
    editComment: ()=> `${BASE_URL}/editComment`,
    addBlog: ()=> `${BASE_URL}/api/blogs`,
    editBlog: (id)=>`${BASE_URL}/${id}`,
    updateUserProfile: ()=>  `${BASE_URL}/api/CustomUser/updateUserDetails`,
    deleteUserProfile: (userId)=>  `${BASE_URL}/api/CustomUser/deleteUser/${userId}`,
    getAnalysisDetails: ()=>  `${BASE_URL}/api/CustomUser/getAnalysis`,
}

export default api_urls