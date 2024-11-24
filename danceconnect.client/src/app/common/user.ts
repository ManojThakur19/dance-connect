export class User {
  constructor() {
    this.id = '';
    this.EMAIL = '';
    this.FULL_NAME = '';
    this.IS_ACTIVE = false;
    this.PRIMARY_CONTACT = '';
    this.SECONDARY_CONTACT = '';
    this.token = '';
    this.ROLE_NAME = '';
    this.IMAGE = '';
    this.is_super_admin =false;
  }
  EMAIL: string;
  FULL_NAME: string;
  IS_ACTIVE: boolean;
  PRIMARY_CONTACT: string;
  SECONDARY_CONTACT: string;
  id: string;
  token: string;
  ROLE_NAME: string;
  IMAGE: string;
  is_super_admin: boolean;
}
