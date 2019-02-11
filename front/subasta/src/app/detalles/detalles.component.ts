import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DomSanitizer } from '@angular/platform-browser';

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
  imageUrl: string = "http://localhost:3001/images/";
  safeURL: any;
  constructor(public activeModal: NgbActiveModal,
    private _sanitizer: DomSanitizer) {
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
    const url = this.imageUrl + this.mesagge['imagen'];
    return url;
  }
}
