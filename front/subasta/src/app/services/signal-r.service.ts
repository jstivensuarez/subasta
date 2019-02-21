import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { urlBase } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private hubConnection: signalR.HubConnection
 
  public IniciarConeccion = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('http://localhost:3002/notificacion')
                            .configureLogging(signalR.LogLevel.Information)
                            .build();
 
    this.hubConnection
      .start()
      .then(() => console.log('Conección iniciada'))
      .catch(err => console.log('Error mientras se iniciaba la coneccion: ' + err))
  }
 
  public agregarListenerParaHub = () => {
    this.hubConnection.on('enviarATodos', (data) => {
      debugger;
      console.log(data);
    });
  }
}
