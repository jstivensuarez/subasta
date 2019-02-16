import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-editar-simple',
  templateUrl: './editar-simple.component.html',
  styleUrls: ['./editar-simple.component.css']
})
export class EditarSimpleComponent implements OnInit {

  @Output() completo: EventEmitter<any> = new EventEmitter();
  control: FormControl;
  title: string;
  constructor(public activeModal: NgbActiveModal) {
    this.title = "Editar";
    this.control = new FormControl('');
  }

  ngOnInit() {
  }

  guardar() {
    this.completo.emit(this.control.value);
    this.activeModal.close();
  }

  cancel(){
    this.activeModal.close();
  }
}
