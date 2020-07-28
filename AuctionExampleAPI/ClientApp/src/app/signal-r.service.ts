import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr'

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private hubConnection: signalR.HubConnection;

  constructor() { 
    this.buildConnection();
    this.startConnection();
  }

  private buildConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('/refresh')
      .build();
  }

  private startConnection() {
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => {
        console.log('Error while starting connection: ' + err);
        setTimeout(function() { this.startConnection(); }, 3000);
      });
  }

  public refreshListener(func: Function): void {
    this.hubConnection.on('Refresh', function (id: number) { func(); });
  }

  public refreshInvoke(idItem: number) {
    this.hubConnection.invoke('Refresh', idItem);
  }
}
