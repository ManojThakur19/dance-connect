export interface Contact {
  name: string;
  email: string;
  message: string;
}

export interface MessageReply {
  replyTo: number;
  message: string;
}

export interface ContactResponse {
  id: number;
  name: string;
  email: string;
  message: string;
  displayMessage: string;
  messageResponse?: string;
  isMessageResponded: boolean;
  date?: string;
}

