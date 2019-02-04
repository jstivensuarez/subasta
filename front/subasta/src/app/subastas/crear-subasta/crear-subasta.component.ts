import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Time } from '@angular/common';
import { DatePipe } from '@angular/common'
import { SubastaService } from 'src/app/services/subasta.service';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { Subasta } from 'src/app/dtos/subasta';
import { constants } from 'src/app/util/constants';
import { Evento } from 'src/app/dtos/evento';
import { Validation } from 'src/app/util/validations';
@Component({
  selector: 'app-crear-subasta',
  templateUrl: './crear-subasta.component.html',
  styleUrls: ['./crear-subasta.component.css'],
})
export class CrearSubastaComponent implements OnInit {

  @Input() evento: Evento;
  @Input() subasta: Subasta;
  @Output() completo: EventEmitter<any> = new EventEmitter();
  form: FormGroup;
  minDate: Date;
  horaInicioActual: string;
  horaFinActual: string;
  constructor(
    private subastaService: SubastaService,
    private alertService: MesaggesManagerService,
    public activeModal: NgbActiveModal,
    public datepipe: DatePipe
  ) {
    this.minDate = new Date();
    this.subasta = new Subasta();
  }

  ngOnInit() {
  }

  onSubmit() {
    const subasta = new Subasta();
    subasta.fechaLimite = this.fechaLimite.value;
    subasta.horaFin = this.fechaFin.value;
    subasta.horaInicioAux = this.horaInicio.value;
    subasta.horaInicio = this.fechaInicio.value;
    subasta.horaFinAux = this.horaFin.value;
    subasta.descripcion = this.descripcion.value;
    subasta.eventoId = this.evento.eventoId;
    if (this.subasta.subastaId) {
      subasta.subastaId = this.subasta.subastaId;
      this.editarSubasta(subasta);
    } else {
      this.crearSubasta(subasta);
    }
  }

  crearSubasta(subas) {
    this.subastaService.post(subas).subscribe(res => {
      this.alertService.
        showSimpleMessage(constants.successTitle, constants.success, constants.successCreate);
      this.completo.emit();
      this.activeModal.close();
    }, err => {
      this.alertService.
        showSimpleMessage(constants.errorTitle, constants.error, constants.errorCreate);
      console.error(err);
    });
  }

  editarSubasta(subasta: Subasta) {
    this.subastaService.put(subasta).subscribe(res => {
      this.alertService.
        showSimpleMessage(constants.successTitle, constants.success, constants.successUpdate);
        this.completo.emit();
        this.activeModal.close();
        
    }, err => {
      this.alertService.
        showSimpleMessage(constants.errorTitle, constants.error, constants.errorUpdate);
      console.error(err);
    });
  }

  createForm() {
    if(this.subasta.horaInicio && this.subasta.horaFin){
      this.horaInicioActual = this.datepipe.transform(this.subasta.horaInicio, 'HH:mm');
      this.horaFinActual = this.datepipe.transform(this.subasta.horaFin, 'HH:mm');
    }
    this.form =  new FormGroup({
      fechaLimite: new FormControl(this.subasta.fechaLimite, [Validators.required]),
      fechaInicio: new FormControl(this.subasta.horaInicio, [Validators.required]),
      fechaFin: new FormControl(this.subasta.horaFin, [Validators.required]),
      descripcion: new FormControl(this.subasta.descripcion, [Validators.required]),
      horaInicio: new FormControl( this.horaInicioActual, [Validators.required]),
      horaFin: new FormControl(this.horaFinActual, [Validators.required]),
    }, [Validation.SubastaFechas, Validation.SubastaHoras]);
  }
  date(date: any, arg1: string): any {
    throw new Error("Method not implemented.");
  }

  get descripcion(){
    return this.form.get('descripcion');
  }
  get fechaLimite() {
    return this.form.get('fechaLimite');
  }

  get fechaInicio() {
    return this.form.get('fechaInicio');
  }
  get fechaFin() {
    return this.form.get('fechaFin');
  }

  get horaInicio() {
    return this.form.get('horaInicio');
  }

  get horaFin() {
    return this.form.get('horaFin');
  }
}
