export interface User {
  name: string;
  gender?: string;
  dob: Date;
  phone: string;
  profilePic?: File;
  identityDocument?: File;
  street: string;
  city?: string;
  province?: string;
  postalCode?: string;
}

export interface UserResponse {
  id: number;
  name: string;
  gender?: string;
  dob: string;
  phone: string;
  email: string;
  profileStatus: string;
  profilePic?: string;
  identityDocument?: string;
  street: string;
  city?: string;
  province?: string;
  postalCode?: string;
}
