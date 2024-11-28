import { Component, OnDestroy, OnInit } from '@angular/core';
import { ChatService } from './chat.service';
import { Chat } from './chat';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent implements OnInit, OnDestroy {
  messages: Chat[] = [];
  message: string = '';
  receiverId?: number;

  constructor(private chatService: ChatService, private route: ActivatedRoute,) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.receiverId = parseInt(params['id']);
    });

    this.chatService.startConnection();
    this.chatService.receiveMessages();
    this.chatService.messages$.subscribe(messages => {
      this.messages = messages;

      //Instructor Reply to the Users
      if (this.messages.length > 0) {
        this.receiverId = this.messages[0]?.senderId as number;
      }
    });
  }

  ngOnDestroy(): void {
    this.chatService.stopConnection();
  }

  sendMessage(): void {
    const chat: Chat = {
      message: this.message,
      receiverId: this.receiverId
    }
    this.chatService.sendMessage(chat);
    this.message = '';
  }

}
