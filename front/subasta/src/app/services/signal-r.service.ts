import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { urlBase } from 'src/environments/environment';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  nuevoMensaje = new Subject<string>();
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

      this.hubConnection.on('enviarATodos', (data) => {
        console.log(data);
        this.nuevoMensaje.next(data);
      });
  }
}
