import { Component, OnInit } from '@angular/core';
import { TipoDocumento } from 'src/app/dtos/tipo-documento';
import { Departamento } from 'src/app/dtos/departamento';
import { Municipio } from 'src/app/dtos/municipio';
import { TdServiceService } from 'src/app/services/td-service.service';
import { MunicipioService } from 'src/app/services/municipio.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Cliente } from 'src/app/dtos/cliente';
import { ClienteService } from 'src/app/services/cliente.service';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { constants } from 'src/app/util/constants';
import { DepartamentoService } from 'src/app/services/departamento-service.service';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';

@Component({
  selector: 'app-crear-propietario',
  templateUrl: './crear-propietario.html',
  styleUrls: ['./crear-propietario.css']
})

export class CrearPropietarioComponent implements OnInit {

  title: string = 'Crear propietario';
  tipoDocumentos: TipoDocumento[];
  departamentos: Departamento[];
  municipios: Municipio[];
  form: FormGroup;
  cliente: Cliente;
  selectedTd: number;
  selectedDepartamento: number;
  selectedMunicipio: number;
  selectedRepresentante: boolean;
  placeHolderNombre: string;
  isEditing: boolean;
  constructor(private tdService: TdServiceService,
    private departamentoService: DepartamentoService,
    private municipioService: MunicipioService,
    private clienteService: ClienteService,
    private route: ActivatedRoute,
    private router: Router,
    private alertService: MesaggesManagerService) {
    this.tipoDocumentos = [];
    this.departamentos = [];
    this.municipios = [];
    this.placeHolderNombre = "Nombre";
    this.selectedRepresentante = false;
    this.isEditing = false;
    this.cliente = new Cliente();
    this.obtenerTipoDocumento();
    this.obtenerDepartamentos();
    this.verificarUrl();
    this.form = this.createForm();
  }

  ngOnInit() {

  }

  obtenerTipoDocumento() {
    this.tdService.getTipoDocumentos().subscribe(
      resp => {
        this.tipoDocumentos = resp;
      }, err => {
        console.error(err);
      }
    )
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

  obtenerMunicipios(departamentoId) {
    if (departamentoId) {
      this.municipioService.getMunicipios(departamentoId).subscribe(
        resp => {
          this.municipios = resp;
        }, err => {
          console.error(err);
        }
      );
    }
  }

  obtenerCliente(id: string) {
    this.clienteService.getDto(id).subscribe(res => {
      this.cliente = res;
      this.selectedDepartamento = this.cliente.municipio.departamentoId;
      if (this.cliente.representante) {
        this.selectedRepresentante = true;
      }
      this.form = this.createForm();
    }, err => {
      console.error(err);
    });
  }

  crearCliente(cliente: Cliente) {
    this.clienteService.post(cliente).subscribe(res => {
      this.alertService.
        showSimpleMessage(constants.successTitle, constants.success, constants.successCreate);
      this.regresar();
    }, err => {
      if (err === constants.alreadyExist) {
        this.alertService.
          showSimpleMessage(constants.errorTitle, constants.error, constants.errorAlreadyExists);
      } else {
        this.alertService.
          showSimpleMessage(constants.errorTitle, constants.error, constants.errorCreate);
      }
      console.error(err);
    });
  }

  editarCliente(cliente: Cliente) {
    this.clienteService.put(cliente).subscribe(res => {
      this.alertService.
        showSimpleMessage(constants.successTitle, constants.success, constants.successUpdate);
      this.regresar();
    }, err => {
      this.alertService.
        showSimpleMessage(constants.errorTitle, constants.error, constants.errorUpdate);
      console.error(err);
    });
  }

  onSubmit(form: any) {
    const cliente = new Cliente();
    cliente.nombre = this.nombre.value;
    cliente.correo = this.correo.value;
    cliente.telefono = this.telefono.value;
    cliente.direccion = this.direccion.value;
    cliente.representante = this.representante.value;
    cliente.tipoDocumentoId = this.td.value;
    cliente.clienteId = this.documento.value;
    cliente.municipioId = this.municipio.value;
    if (!this.selectedRepresentante)
      cliente.representante = null;
    if (this.isEditing) {
      this.editarCliente(cliente)
    } else {
      this.crearCliente(cliente);
    }
  }

  verificarUrl() {
    this.route.queryParams.subscribe(params => {
      debugger;
      if (params['id']) {
        this.isEditing = true;
        this.title = 'Editar propietario';
        this.obtenerCliente(params['id']);
      } else {
        this.selectedRepresentante = false;
        this.isEditing = false;
        this.cliente = new Cliente();
        this.form = this.createForm();
        this.title = 'Crear propietario';
      }
    });
  }

  onChangeCheckRepresentante(evento) {
    this.selectedRepresentante = evento.checked;
    if (evento.checked) {
      this.representante.setValidators([Validators.required]);
      this.placeHolderNombre = "Razón social";
    } else {
      this.representante.clearValidators();
      this.representante.updateValueAndValidity();
      this.placeHolderNombre = "Nombre";
    }
  }

  isNumberKey(evt) {
    const charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

  obtenerCiudades(departamentoId) {
    this.municipio.setValue(null);
    this.obtenerMunicipios(departamentoId);
  }

  regresar() {
    this.router.navigate(['/listar-propietario']);
  }

  createForm() {
    this.obtenerMunicipios(this.selectedDepartamento);
    this.selectedTd = this.cliente.tipoDocumentoId;
    this.selectedMunicipio = this.cliente.municipioId;
    return new FormGroup({
      td: new FormControl(this.selectedTd),
      documento: new FormControl(this.cliente.clienteId, [Validators.required]),
      nombre: new FormControl(this.cliente.nombre, [Validators.required]),
      representante: new FormControl(this.cliente.representante),
      telefono: new FormControl(this.cliente.telefono, [Validators.required]),
      correo: new FormControl(this.cliente.correo, [Validators.required, Validators.email]),
      municipio: new FormControl(this.selectedMunicipio),
      departamento: new FormControl(this.selectedDepartamento),
      direccion: new FormControl(this.cliente.direccion, [Validators.required]),
      esRepresentante: new FormControl(this.selectedRepresentante)
    });
  }

  get nombre() {
    return this.form.get('nombre');
  }

  get documento() {
    return this.form.get('documento');
  }

  get representante() {
    return this.form.get('representante');
  }

  get telefono() {
    return this.form.get('telefono');
  }

  get correo() {
    return this.form.get('correo');
  }

  get direccion() {
    return this.form.get('direccion');
  }

  get td() {
    return this.form.get('td');
  }

  get departamento() {
    return this.form.get('departamento');
  }

  get municipio() {
    return this.form.get('municipio');
  }

  get esRepresentante() {
    return this.form.get('esRepresentante');
  }
}
