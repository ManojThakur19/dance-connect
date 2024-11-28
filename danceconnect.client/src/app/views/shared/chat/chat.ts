export interface Chat {
  receiverId?: number;
  senderId?: number;
  message?: string;
}

export interface ChatResponse {
  senderId?: number;
  senderName: string;
  senderImage?: string;
  receiverId?: number;
  receiverName?: string;
  receiverImage?: string;
  message?: number;
  sentOn?: number;
}
