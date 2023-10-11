import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"

@Injectable({
  providedIn: 'root'
})
export class RealtimeService {

  private connection!: signalR.HubConnection;

  constructor() { }

  public startConnection = () => {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5265/notify')
      .build();

    this.connection
      .start()
      .then(() => console.log('Connection started.'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public addNotificationListener = () => {
    this.connection.on("NotificationReceived", (message) => {
      console.log("Message got by client");
    })
  }

}
