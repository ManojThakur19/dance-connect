export interface Instructor {
  name: string;
  gender?: string;
  dob: Date;
  phone: string;
  hourlyRate: number;
  profilePic?: File;
  identityDocument?: File;
  shortIntroVideo?: File;
  street: string;
  city?: string;
  province?: string;
  postalCode?: string;
}

export interface InstructorResponse {
  id: number;
  name: string;
  gender?: string;
  dob: string;
  phone: string;
  email: string;
  averageRating: number;
  profileStatus: string;
  hourlyRate: string;
  profilePic?: string;
  identityDocument?: string;
  shortIntroVideo?: string;
  street: string;
  city?: string;
  province?: string;
  postalCode?: string;
}
