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
  }
}
