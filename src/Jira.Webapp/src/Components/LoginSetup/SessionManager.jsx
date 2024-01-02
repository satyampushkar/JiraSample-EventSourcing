const SessionManager = {

    getToken() {
        const token = sessionStorage.getItem('token');
        if (token) return token;
        else return null;
    },

    setUserSession(userName, token) {
        sessionStorage.setItem('email', userName);
        sessionStorage.setItem('token', token);
    },

    removeUserSession(){
        sessionStorage.removeItem('email');
        sessionStorage.removeItem('token');
    }
}

export default SessionManager;