export class Login {
  constructor() {
    this.userName = '';
    this.email = '';
    this.password = '';
    this.rememberMe = false;
  }
  userName: string;
  email: string;
  password: string;
  rememberMe: boolean;
}

export interface LoginResponse {
  id: number;
  identityId: number;
  name: string;
  phone: string;
  email: string;
  role: string;
  profilePhoto: string;
  token: string;
  isAdmin: boolean;
}
