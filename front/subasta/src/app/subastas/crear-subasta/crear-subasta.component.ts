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

  @Input() eventoInput: Evento;
  @Input() subastaInput: Subasta;
  @Output() completo: EventEmitter<any> = new EventEmitter();
  form: FormGroup;
  minDate: Date;
  maxDate: Date;
  horaInicioActual: string;
  horaFinActual: string;
  selectedAnticipo: boolean;
  constructor(
    private subastaService: SubastaService,
    private alertService: MesaggesManagerService,
    public activeModal: NgbActiveModal,
    public datepipe: DatePipe
  ) {
    this.selectedAnticipo = false;
    this.minDate = new Date();
    this.maxDate = new Date();
    this.subastaInput = new Subasta();
  }

  ngOnInit() {
  }

  onSubmit() {
    if (this.form.valid) {
      const subasta = new Subasta();
      subasta.horaFin = this.fechaFin.value;
      subasta.horaInicioAux = this.horaInicio.value;
      subasta.horaInicio = this.fechaInicio.value;
      subasta.horaFinAux = this.horaFin.value;
      subasta.descripcion = this.descripcion.value;
      subasta.eventoId = this.eventoInput.eventoId;
      if(this.valorAnticipo.value){
        subasta.valorAnticipo = this.valorAnticipo.value;
     }
      if (this.subastaInput.subastaId) {
        subasta.subastaId = this.subastaInput.subastaId;
        this.editarSubasta(subasta);
      } else {
        this.crearSubasta(subasta);
      }
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
    if (this.subastaInput.horaInicio && this.subastaInput.horaFin) {
      this.horaInicioActual = this.datepipe.transform(this.subastaInput.horaInicio, 'HH:mm');
      this.horaFinActual = this.datepipe.transform(this.subastaInput.horaFin, 'HH:mm');
      this.horaInicioActual = this.tConvert(this.horaInicioActual);
      this.horaFinActual = this.tConvert(this.horaFinActual);
    }
    this.form = new FormGroup({
      fechaInicio: new FormControl(this.subastaInput.horaInicio, [Validators.required]),
      fechaFin: new FormControl(this.subastaInput.horaFin, [Validators.required]),
      descripcion: new FormControl(this.subastaInput.descripcion, [Validators.required]),
      horaInicio: new FormControl(this.horaInicioActual, [Validators.required]),
      horaFin: new FormControl(this.horaFinActual, [Validators.required]),
      esAnticipo: new FormControl(this.selectedAnticipo),
      valorAnticipo: new FormControl(this.subastaInput.valorAnticipo),
    }, [Validation.SubastaFechas, Validation.SubastaHoras]);
  }
  
  tConvert(time) {
    time = time.toString ().match (/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];
  
    if (time.length > 1) { 
      time = time.slice (1);  
      time[5] = +time[0] < 12 ? ' am' : ' pm'; 
      time[0] = +time[0] % 12 || 12; 
    }
    return time.join ('');
  }

  onChangeCheckAnticipo(evento) {
    this.selectedAnticipo = evento.checked;
    if (evento.checked) {
      this.valorAnticipo.setValidators([Validators.required]);
    } else {
      this.valorAnticipo.clearValidators();
      this.valorAnticipo.updateValueAndValidity();
    }
  }

  date(date: any, arg1: string): any {
    throw new Error("Method not implemented.");
  }

  get esAnticipo() {
    return this.form.get('esAnticipo');
  }

  get valorAnticipo() {
    return this.form.get('valorAnticipo');
  }

  get descripcion() {
    return this.form.get('descripcion');
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
