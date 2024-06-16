import axios from '../http-common';

class AuthService {
  login(user) {
    return axios
      .post('/Auth/login', {
        Username: user.username,
        Password: user.password
      })
      .then(response => {
        if (response.data.token) {
          localStorage.setItem('user', JSON.stringify(response.data));
        }

        return response.data;
      });
  }

  logout() {
    localStorage.removeItem('user');
  }

  getCurrentUser() {
    return JSON.parse(localStorage.getItem('user'));
  }

  isTokenValid() {
    const user = this.getCurrentUser();
    if (user && user.token) {
      const tokenPayload = JSON.parse(atob(user.token.split('.')[1]));
      return tokenPayload.exp > Date.now() / 1000;
    }
    return false;
  }
}

export default new AuthService();
