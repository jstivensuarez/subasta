import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DomSanitizer } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-detalles',
  templateUrl: './detalles.component.html',
  styleUrls: ['./detalles.component.css']
})
export class DetallesComponent implements OnInit {

  @Input() title: string;
  @Input() mesagge: any;
  @Output() action = new EventEmitter();
  keys: string[];
  result: boolean;
  withImage: boolean;
  withVideo: boolean;
  safeURL: any;
  otherComponent: any;
  constructor(public activeModal: NgbActiveModal,
    private _sanitizer: DomSanitizer,
    private modalService: NgbModal) {
    this.keys = [];
  }

  ngOnInit() {
  }

  close() {
    this.action.emit(this.result);
    this.activeModal.close();
  }

  cancel() {
    this.result = false;
    this.close();
  }

  getFinalMesagge() {
    this.keys = Object.keys(this.mesagge);
    this.withImage = this.haveImage();
    this.withVideo = this.haveVideo();
  }

  haveImage() {
    if (this.keys && this.mesagge["imagen"]) {
      return true;
    } else {
      return false;
    }
  }

  haveVideo() {
    if (this.keys && this.mesagge["video"]) {
      return true;
    } else {
      return false;
    }
  }

  getVideo() {
    const url =
      this._sanitizer.bypassSecurityTrustResourceUrl(this.mesagge['video'].replace('watch?v=', 'embed/'));
    return url;
  }

  getImage() {
    const url = environment.imageUrl + this.mesagge['imagen'];
    return url;
  }

  openOtherComponent(){
    const component = this.modalService.open(this.otherComponent).componentInstance;
    component.loteId = this.mesagge['loteId'];
    component.lote = this.mesagge["Nombre"];
    component.promedio = this.mesagge["Promedio"];
    component.obtenerAnimales();
  }
}
