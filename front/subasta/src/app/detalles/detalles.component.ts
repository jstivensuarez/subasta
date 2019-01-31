import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { constants } from '../util/constants';
import { ThrowStmt } from '@angular/compiler';

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
  imageUrl: string = "http://localhost:50553/images/LOTES/";
  constructor(public activeModal: NgbActiveModal) {
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
    this.withImage =this.haveImage();
  }

  haveImage(){
    if( this.keys && this.mesagge["imagen"]){
      return true;
    }else{
      return false;
    }
  }

  getImage(name){
    debugger;
    const url= this.imageUrl+ this.mesagge['imagen'];
    return url;
  }
}
