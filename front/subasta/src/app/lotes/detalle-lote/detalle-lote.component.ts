import { Component, OnInit } from '@angular/core';
import { AnimalService } from 'src/app/services/animal.service';
import { Animal } from 'src/app/dtos/animal';
import { environment } from 'src/environments/environment';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-detalle-lote',
  templateUrl: './detalle-lote.component.html',
  styleUrls: ['./detalle-lote.component.css']
})
export class DetalleLoteComponent implements OnInit {

  animales: Animal[]
  loteId: number;
  lote: string;
  promedio: number;
  minPeso: number=0;
  maxPeso: number=0;
  constructor(private animalService: AnimalService,
    private activeModal: NgbActiveModal) {
    this.animales = [];
  }

  ngOnInit() {
  }

  obtenerAnimales() {
    debugger;
    this.animalService.getByLote(this.loteId).subscribe(
      resp => {
        debugger;
        this.animales = resp;
        this.maxPeso = Math.max.apply(Math,this.animales.map(function(o){return o.peso;}));
        this.minPeso = Math.min.apply(Math,this.animales.map(function(o){return o.peso;}));
      }, err => {
        console.error(err);
      });
  }

  getImage(image) {
    const url = environment.imageUrl + "/ANIMALES/"+image;
    return url;
  }
  
  esVideo(value) {
    let video = null;
    let imagen = null;
    if (/^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$/.test(value)) {
      return true;
    }
    return false;
  }

  close(){
    this.activeModal.close();
  }
}
