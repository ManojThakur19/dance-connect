import { Injectable } from '@angular/core';
import { HttpTransportType, HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { Chat } from './chat';


@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private hubConnection: HubConnection;
  private messages = new BehaviorSubject<Chat[]>([]);
  public messages$ = this.messages.asObservable();

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Debug)
      .withUrl('https://localhost:7155/ChatHub', {
        accessTokenFactory: () => JSON.parse(localStorage.getItem('currentUser') ?? "").token.value || '',
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets 
      })
      .build();
  }

  startConnection(): void {
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.error('Error while starting connection:', err));
  }

  stopConnection(): void {
    this.hubConnection.stop();
  }

  sendMessage(messageChat: Chat): void {
    this.hubConnection.invoke('SendMessage', messageChat).catch(err => console.error(err));
  }

  receiveMessages(): void {
    this.hubConnection.on('ReceiveMessage', (messageChat: Chat) => {
      const currentMessages = this.messages.getValue();
      this.messages.next([...currentMessages, messageChat]);
    });
  }
}
