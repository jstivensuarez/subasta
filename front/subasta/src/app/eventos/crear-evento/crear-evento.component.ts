import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Evento } from 'src/app/dtos/evento';
import { TdServiceService } from 'src/app/services/td-service.service';
import { DepartamentoService } from 'src/app/services/departamento-service.service';
import { MunicipioService } from 'src/app/services/municipio.service';
import { ClienteService } from 'src/app/services/cliente.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { Departamento } from 'src/app/dtos/departamento';
import { Municipio } from 'src/app/dtos/municipio';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TemplateParseError } from '@angular/compiler';
import { Subasta } from 'src/app/dtos/subasta';
import { EventoService } from 'src/app/services/evento.service';
import { SubastaService } from 'src/app/services/subasta.service';
import { constants } from 'src/app/util/constants';
import { CrearSubastaComponent } from 'src/app/subastas/crear-subasta/crear-subasta.component';
import { Validation } from 'src/app/util/validations';

@Component({
  selector: 'app-crear-evento',
  templateUrl: './crear-evento.component.html',
  styleUrls: ['./crear-evento.component.css']
})
export class CrearEventoComponent implements OnInit {

  isLinear = true;
  firstFormGroup: FormGroup;
  titleEvento: string;
  titleSubastas: string;
  titleLotes: string;
  minDate: Date;
  departamentos: Departamento[];
  municipios: Municipio[];
  evento: Evento;
  modalRef: any;
  selectedDepartamento: number;
  selectedMunicipio: number;
  displayedColumns: string[] = ['descripcion', 'fechaLimite', 'horaInicio', 'horaFin', 'acciones'];
  subastas: Subasta[];
  isEditing: boolean;
  subasta: Subasta;
  constructor(private _formBuilder: FormBuilder,
    private tdService: TdServiceService,
    private departamentoService: DepartamentoService,
    private municipioService: MunicipioService,
    private clienteService: ClienteService,
    private route: ActivatedRoute,
    private router: Router,
    private alertService: MesaggesManagerService,
    private eventoService: EventoService,
    private subastaService: SubastaService,
    private modalService: NgbModal) {
    this.isEditing = false;
    this.titleEvento = "Evento";
    this.titleSubastas = "Subastas";
    this.titleLotes = "Asociar lotes";
    this.evento = new Evento();
    this.minDate = new Date();
    this.subastas = [];
    this.verificarUrl();
    this.obtenerDepartamentos();
    this.firstFormGroup = this.createFirstForm();
  }

  ngOnInit() {

  }

  obtenerDepartamentos() {
    this.departamentoService.getDepartamentos().subscribe(
      resp => {
        this.departamentos = resp;
      }, err => {
        console.error(err);
      }
    )
  }

  obtenerCiudades(departamentoId) {
    this.municipio.setValue(null);
    this.obtenerMunicipios(departamentoId);
  }

  verificarUrl() {
    this.route.queryParams.subscribe(params => {
      if (params['id']) {
        this.isEditing = true;
        this.obtenerEvento(params['id']);
      } else {
        this.isEditing = false;
        this.evento = new Evento();
        this.firstFormGroup = this.createFirstForm();
      }
    });
  }

  obtenerEvento(id: string) {
    this.eventoService.getDto(id).subscribe(res => {
      this.evento = res;
      this.selectedDepartamento = this.evento.municipio.departamentoId;
      this.selectedMunicipio = this.evento.municipioId;
      this.firstFormGroup = this.createFirstForm();
      this.obtenerMunicipios(this.selectedDepartamento);
      this.obtenerSubastas();
    }, err => {
      console.error(err);
    });
  }

  obtenerMunicipios(departamentoId) {
    this.municipioService.getMunicipios(departamentoId).subscribe(
      resp => {
        this.municipios = resp;
      }, err => {
        console.error(err);
      }
    )
  }

  createFirstForm() {
    return new FormGroup({
      descripcion: new FormControl(this.evento.descripcion, [Validators.required]),
      municipio: new FormControl(this.selectedMunicipio),
      departamento: new FormControl(this.selectedDepartamento),
      fechaInicio: new FormControl(this.evento.fechaInicio, [Validators.required]),
      fechaFin: new FormControl(this.evento.fechaFin, [Validators.required,]),
    },
      Validation.EventoFechas // your validation method
    );
  }


  completarSubata(evento) {
    this.modalRef.close();
    this.obtenerSubastas();
  }

  obtenerSubastas() {
    if (this.evento.eventoId) {
      this.subastaService.getSubastas(this.evento.eventoId).subscribe(resp => {
        this.subastas = resp;
      }, err => {
        console.error(err);
      });
    }
  }

  onSubmitEvento() {
    const evento = new Evento();
    evento.descripcion = this.descripcion.value;
    evento.fechaFin = this.fechaFin.value;
    evento.fechaInicio = this.fechaInicio.value;
    evento.municipioId = this.municipio.value;
    if (this.evento.eventoId) {
      evento.eventoId = this.evento.eventoId; 
      this.editarEvento(evento);
    } else {
      this.crearEvento(evento);
    }
  }

  crearEvento(evento) {
    this.eventoService.post(evento).subscribe(res => {
      this.evento = res;
      this.alertService.
        showSimpleMessage(constants.successTitle, constants.success, constants.successCreate);
    }, err => {
      this.alertService.
        showSimpleMessage(constants.errorTitle, constants.error, constants.errorCreate);
      console.error(err);
    });
  }

  editarEvento(evento: Evento) {
    this.eventoService.put(evento).subscribe(res => {
      this.alertService.
        showSimpleMessage(constants.successTitle, constants.success, constants.successUpdate);
    }, err => {
      this.alertService.
        showSimpleMessage(constants.errorTitle, constants.error, constants.errorUpdate);
      console.error(err);
    });
  }

  eliminarSubasta(element) {
    this.alertService.showConfirmMessage(constants.deleteTitle, constants.confirmDelete).subscribe(
      resp => {
        if (resp) {
          this.subastaService.delete(element.subastaId).subscribe(
            resp => {
              this.alertService.
                showSimpleMessage(constants.successTitle, constants.success, constants.successDelete);
              this.obtenerSubastas();
            }, err => {
              this.alertService.
                showSimpleMessage(constants.errorTitle, constants.error, constants.errorDelete);
            }
          );
        }
      }
    );
  }

  editarSubasta(subasta){
    const component = this.modalService.open(CrearSubastaComponent).componentInstance;
    component.evento = this.evento;
    component.subasta = subasta;
    component.createForm();
    component.completo.subscribe( res=> {
      this.obtenerSubastas();
    });
  }

  agregarSubasta() {
    const component = this.modalService.open(CrearSubastaComponent).componentInstance;
    component.evento = this.evento;
    component.createForm();
    component.completo.subscribe( res=> {
      this.obtenerSubastas();
    });
  }

  get descripcion() {
    return this.firstFormGroup.get('descripcion');
  }

  get departamento() {
    return this.firstFormGroup.get('departamento');
  }

  get municipio() {
    return this.firstFormGroup.get('municipio');
  }

  get fechaInicio() {
    return this.firstFormGroup.get('fechaInicio');
  }

  get fechaFin() {
    return this.firstFormGroup.get('fechaFin');
  }
}
