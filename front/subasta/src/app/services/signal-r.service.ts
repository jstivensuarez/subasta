
import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { urlBaseApi, environment } from 'src/environments/environment';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  nuevoMensaje = new Subject<string>();
  actualizar = new Subject<string>();
  private hubConnection: signalR.HubConnection

  public IniciarConeccion = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.endpointSignal)
      .configureLogging(signalR.LogLevel.Information)
      .build();

    //this.hubConnection.serverTimeoutInMilliseconds = 900000;
    //this.hubConnection.keepAliveIntervalInMilliseconds = 900000;
    
    this.hubConnection
      .start()
      .then(() => this.actualizar.next("Actualizar datos"))
      .catch(err => console.log('Error mientras se iniciaba la coneccion: ' + err))

    this.hubConnection.on('enviarATodos', (data) => {
      console.log(data);
      this.nuevoMensaje.next(data);
    });

    this.hubConnection.onclose(() => {
      setTimeout(function () {
        this.hubConnection
          .start()
          .then(() => this.actualizar.next("Actualizar datos"))
          .catch(err => console.log('Error mientras se iniciaba la coneccion: ' + err))
      }, 3000);
    });
  }

  verificarConeccion() {
    if (!this.hubConnection || this.hubConnection.state == signalR.HubConnectionState.Disconnected) {
      this.IniciarConeccion();     
    }
  }
}
