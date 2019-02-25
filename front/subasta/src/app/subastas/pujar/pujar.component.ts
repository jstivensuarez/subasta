import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { PujaService } from 'src/app/services/puja.service';
import { Puja } from 'src/app/dtos/puja';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { constants } from 'src/app/util/constants';

@Component({
  selector: 'app-pujar',
  templateUrl: './pujar.component.html',
  styleUrls: ['./pujar.component.css']
})
export class PujarComponent implements OnInit {

  @Output() completo: EventEmitter<any> = new EventEmitter();
  control: FormControl;
  title: string;
  min: number;
  usuario: string;
  loteId: number;
  constructor(public activeModal: NgbActiveModal,
    private pujaService: PujaService,
    private alertService: MesaggesManagerService) {
    this.min = 0;
    this.title = "Realizar puja";
    this.control = new FormControl(0, [Validators.min(4000000)]);
  }

  ngOnInit() {
  }

  guardar() {
    if (this.control.valid) {
      const puja = new Puja();
      puja.loteId = this.loteId;
      puja.usuario = this.usuario;
      puja.valor = this.control.value;
      this.pujaService.post(puja).subscribe(res => {
        this.alertService.
          showSimpleMessage(constants.successTitle, constants.success, constants.successPuja);
        this.completo.emit(this.control.value);
        this.activeModal.close();
      }, err => {
        if (err == constants.subastaFinalizada) {
          this.alertService.
            showSimpleMessage(constants.errorPujaTarde, constants.alert, constants.errorPujatiempo);
        } else {
          this.alertService.
            showSimpleMessage(constants.errorTitle, constants.error, constants.errorPuja);
        }
        this.completo.emit(null);
        this.activeModal.close();
        console.error(err);
      });
    }
  }

  cancel() {
    this.activeModal.close();
  }


}
