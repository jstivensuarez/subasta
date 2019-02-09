import { Component, OnInit } from '@angular/core';
import { SubastaService } from 'src/app/services/subasta.service';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { DatePipe } from '@angular/common';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Subasta } from 'src/app/dtos/subasta';
import { constants } from 'src/app/util/constants';
import { Validation } from 'src/app/util/validations';
import { Evento } from 'src/app/dtos/evento';
import { Router, ActivatedRoute } from '@angular/router';
import { EventoService } from 'src/app/services/evento.service';

@Component({
  selector: 'app-crear-subasta-nueva',
  templateUrl: './crear-subasta-nueva.component.html',
  styleUrls: ['./crear-subasta-nueva.component.css']
})
export class CrearSubastaNuevaComponent implements OnInit {

  eventos: Evento[];
  subasta: Subasta;
  form: FormGroup;
  minDate: Date;
  maxDate: Date;
  horaInicioActual: string;
  horaFinActual: string;
  isEditing: boolean;
  selectedEvento: number;
  title: string;
  constructor(
    private subastaService: SubastaService,
    private alertService: MesaggesManagerService,
    public datepipe: DatePipe,
    private router: Router,
    private eventoService: EventoService,
    private route: ActivatedRoute,
  ) {
    this.title = "Crear subasta";
    this.eventos = [];
    this.minDate = new Date();
    this.maxDate = new Date();
    this.subasta = new Subasta();
    this.obtenerEventos();
    this.verificarUrl();
    this.createForm();
  }

  ngOnInit() {
  }


  verificarUrl() {
    this.route.queryParams.subscribe(params => {
      if (params['id']) {
        this.isEditing = true;
        this.title = 'Editar subasta';
        this.obtenerSubasta(params['id']);
      } else {
        this.isEditing = false;
        this.subasta = new Subasta();
        this.createForm();
        this.title = 'Crear subasta';
      }
    });
  }

  obtenerSubasta(id: string) {
    this.subastaService.getDto(id).subscribe(res => {
      this.subasta = res;
      this.selectedEvento = this.subasta.eventoId;
      this.limitarHoras(this.selectedEvento);
      this.createForm();
    }, err => {
      console.error(err);
    });
  }

  obtenerEventos() {
    this.eventoService.get().subscribe(
      resp => {
        this.eventos = resp;
      }, err => {
        console.error(err);
      }
    )
  }

  onSubmit() {
    if (this.form.valid) {
      const subasta = new Subasta();
      subasta.horaFin = this.fechaFin.value;
      subasta.horaInicioAux = this.horaInicio.value;
      subasta.horaInicio = this.fechaInicio.value;
      subasta.horaFinAux = this.horaFin.value;
      subasta.descripcion = this.descripcion.value;
      subasta.eventoId = this.evento.value;
      if (this.isEditing) {
        subasta.subastaId = this.subasta.subastaId;
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
      this.regresar();
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
      this.regresar();
    }, err => {
      this.alertService.
        showSimpleMessage(constants.errorTitle, constants.error, constants.errorUpdate);
      console.error(err);
    });
  }

  limitarHoras(id){
    this.fechaFin.setValue(null);
    this.fechaInicio.setValue(null);
    const evento = this.eventos.find(e => e.eventoId === id);
    this.minDate = new Date(evento.fechaInicio);
    this.maxDate = new Date(evento.fechaFin);
  }

  regresar() {
    this.router.navigate(['/listar-subasta']);
  }

  createForm() {
    debugger;
    if (this.subasta.horaInicio && this.subasta.horaFin) {
      this.horaInicioActual = this.datepipe.transform(this.subasta.horaInicio, 'HH:mm');
      this.horaFinActual = this.datepipe.transform(this.subasta.horaFin, 'HH:mm');
      this.horaInicioActual = this.tConvert(this.horaInicioActual);
      this.horaFinActual = this.tConvert(this.horaFinActual);
    }
    this.form = new FormGroup({
      fechaInicio: new FormControl(this.subasta.horaInicio, [Validators.required]),
      fechaFin: new FormControl(this.subasta.horaFin, [Validators.required]),
      descripcion: new FormControl(this.subasta.descripcion, [Validators.required]),
      horaInicio: new FormControl(this.horaInicioActual, [Validators.required]),
      horaFin: new FormControl(this.horaFinActual, [Validators.required]),
      evento: new FormControl(this.selectedEvento),
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
  
  date(date: any, arg1: string): any {
    throw new Error("Method not implemented.");
  }

  get evento() {
    return this.form.get('evento');
  }

  get descripcion() {
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
