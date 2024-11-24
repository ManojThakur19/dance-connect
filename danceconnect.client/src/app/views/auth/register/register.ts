export class Register {
  constructor() {
    this.email = '';
    this.password = '';
    this.confirmPassword = '';
    this.userType = UserType.user;
  }
  email: string;
  password: string;
  confirmPassword: string;
  userType: UserType;
}

export enum UserType {
  instructor,
  user,
  admin
}
