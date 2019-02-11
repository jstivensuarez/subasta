import { Component, OnInit } from '@angular/core';
import { Title, DomSanitizer } from '@angular/platform-browser';
import { EventoService } from 'src/app/services/evento.service';
import { Evento } from 'src/app/dtos/evento';

@Component({
  selector: 'app-subastas',
  templateUrl: './subastas.component.html',
  styleUrls: ['./subastas.component.css']
})
export class SubastasComponent implements OnInit {

  eventos: Evento[];
  title: string;
  imageUrl: string = "http://localhost:3001/images/LOTES/";
  constructor(private eventoService: EventoService,
    private _sanitizer: DomSanitizer) {
    this.title = "Subastas";
    this.eventos = [];
    this.getForClients();
  }

  ngOnInit() {
    
  }

  onFinished(){
    alert("Terminó");
  }

  getForClients(){
    this.eventoService.getForClients().subscribe(resp => {
      this.eventos = resp;
    }, err => {
      console.error(err);
    });
  }

  validarInicio(fechaInicio){
    debugger;
    const hoy = new Date();
    if(hoy >= new Date(fechaInicio)){
      return true;
    }
    return false;
  }

  getSegundos(total) {
    return total;
  }

  getVideo(video) {
    const url =
      this._sanitizer.bypassSecurityTrustResourceUrl(video.replace('watch?v=', 'embed/'));
    return url;
  }

  getImage(imagen) {
    const url = this.imageUrl + imagen;
    return url;
  }

  esVideo(imagen){
    if (/^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$/.test(imagen)) {
      return true;
    }
    return false;
  }
}
