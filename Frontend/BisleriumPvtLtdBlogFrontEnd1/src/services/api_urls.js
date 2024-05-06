const BASE_URL = "http://localhost:7055"

const api_urls= {
    login: ()=> `${BASE_URL}/login`,
    register: ()=> `${BASE_URL}/api/CustomUser/register`,
    getUserData: ()=> `${BASE_URL}/api/CustomUser/user-details`,
}

export default api_urls