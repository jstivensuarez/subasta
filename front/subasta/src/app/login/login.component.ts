import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Usuario } from '../dtos/usuario';
import { UsuarioService } from '../services/usuario.service';
import { MesaggesManagerService } from '../services/mesagges-manager.service';
import { constants } from '../util/constants';
import { Municipio } from '../dtos/municipio';
import { Departamento } from '../dtos/departamento';
import { TipoDocumento } from '../dtos/tipo-documento';
import { Cliente } from '../dtos/cliente';
import { TdServiceService } from '../services/td-service.service';
import { DepartamentoService } from '../services/departamento-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ClienteService } from '../services/cliente.service';
import { MunicipioService } from '../services/municipio.service';
import { Validation } from '../util/validations';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  tipoDocumentos: TipoDocumento[];
  departamentos: Departamento[];
  municipios: Municipio[];
  cliente: Cliente;
  selectedTd: number;
  selectedDepartamento: number;
  selectedMunicipio: number;
  form: FormGroup;
  formRegister: FormGroup;
  formPass: FormGroup;
  formChange: FormGroup;
  usuario: Usuario;
  usuarioPass: Usuario;
  claveRepeat: string;
  claveChangeRepeat: string;
  claveChange: string;
  selected: number;
  constructor(
    private usuarioService: UsuarioService,
    private tdService: TdServiceService,
    private departamentoService: DepartamentoService,
    private municipioService: MunicipioService,
    private clienteService: ClienteService,
    private route: ActivatedRoute,
    private router: Router,
    private alertService: MesaggesManagerService
  ) {
    this.selected = 0;
    this.usuario = new Usuario();
    this.cliente = new Cliente();
    this.tipoDocumentos = [];
    this.departamentos = [];
    this.municipios = [];
    this.obtenerTipoDocumento();
    this.obtenerDepartamentos();
    this.form = this.createForm();
    this.formRegister = this.createFormRegister();
    this.formPass = this.createFormPass();
    this.formChange = this.createFormChange();
  }

  cambiarTab(numero) {
      this.selected = numero;
      this.form = this.createForm();
      this.formRegister = this.createFormRegister();
      this.formPass = this.createFormPass();
      this.formChange = this.createFormChange();
  }

  ngOnInit() {
  }

  onSubmit() {
    if (this.form.valid) {
      const usuario = new Usuario();
      usuario.clave = this.clave.value;
      usuario.correo = this.ingreso.value;
      usuario.nombre = this.ingreso.value;
      this.login(usuario);
    }
  }

  onSubmitChange() {
    if (this.formChange.valid) {
      const usuario = new Usuario();
      usuario.clave = this.claveOld.value;
      usuario.claveChange = this.claveNew.value;
      usuario.correo = this.ingresoChange.value;
      usuario.nombre = this.ingresoChange.value;
      this.usuarioService.change(usuario).subscribe(res => {
        this.alertService.
          showSimpleMessage(constants.successTitle, constants.success, constants.successCambio);
        this.ingresoChange.setValue(null);
        this.claveOld.setValue(null);
        this.claveNew.setValue(null);
        this.selected = 0;
      }, err => {
        if (err.includes(401)) {
          this.alertService.
            showSimpleMessage(constants.errorTitle, constants.alert, constants.errorUnautorized);
        } else {
          this.alertService.
            showSimpleMessage(constants.errorTitle, constants.error, constants.errorCambio);
        }
      });
    }
  }

  onSubmitPass() {
    if (this.ingresoPass.value) {
      this.usuarioService.recover(this.ingresoPass.value).subscribe(res => {
        this.alertService.
          showSimpleMessage(constants.successTitle, constants.success, constants.successRecuperar);
        this.ingresoPass.setValue(null);
        this.selected = 0;
      }, err => {
        this.alertService.
          showSimpleMessage(constants.errorTitle, constants.error, constants.errorRecuperar);
      });
    }
  }

  onSubmitRegister() {
    if (this.formRegister.valid) {
      const cliente = new Cliente();
      cliente.nombre = this.nombre.value;
      cliente.correo = this.correo.value;
      cliente.telefono = this.telefono.value;
      cliente.direccion = this.direccion.value;
      cliente.tipoDocumentoId = this.td.value;
      cliente.clienteId = this.documento.value;
      cliente.municipioId = this.municipio.value;
      cliente.usuario = this.usuarioRegister.value;
      cliente.clave = this.claveRegister.value;
      this.crearCliente(cliente);
    }
  }

  login(usuario) {
    this.usuarioService.login(usuario).subscribe(
      res => {
        localStorage.setItem('token', res.token);
        this.usuarioService.redirectToMenu();
      }, err => {
        if (err.includes(401)) {
          this.alertService.
            showSimpleMessage(constants.errorTitle, constants.alert, constants.errorUnautorized);
        } else {
          this.alertService.
            showSimpleMessage(constants.errorTitle, constants.error, constants.errorLogin);
        }
        console.error(err);
      }
    );
  }

  crearCliente(cliente: Cliente) {
    this.usuarioService.register(cliente).subscribe(res => {
      this.alertService.
        showSimpleMessage(constants.successTitle, constants.successLogin, constants.successCreate);
      localStorage.setItem('token', res.token);
      this.usuarioService.redirectToMenu();
    }, err => {
      if (err === constants.alreadyExist) {
        this.alertService.
          showSimpleMessage(constants.errorTitle, constants.alert, constants.userAlreadyExists);
      } else {
        this.alertService.
          showSimpleMessage(constants.errorTitle, constants.error, constants.errorCreate);
      }
      console.error(err);
    });
  }

  validarCliente(nombre) {
    if (nombre) {
      this.usuarioService.validate(nombre).subscribe(res => {
        this.usuarioRegister.setErrors({});
        this.usuarioRegister.updateValueAndValidity({
          onlySelf: true
        });
      }, err => {
        this.usuarioRegister.setErrors({ 'invalid': true });
      });
    } else {
      this.usuarioRegister.setErrors({});
      this.usuarioRegister.updateValueAndValidity({
        onlySelf: true
      });
    }
  }

  createForm() {
    return new FormGroup({
      ingreso: new FormControl(this.usuario.correo, [Validators.required]),
      clave: new FormControl(this.usuario.clave, [Validators.required]),
    });
  }

  createFormPass() {
    return new FormGroup({
      ingreso: new FormControl('', [Validators.required]),
    });
  }

  createFormRegister() {
    this.obtenerMunicipios(this.selectedDepartamento);
    this.selectedTd = this.cliente.tipoDocumentoId;
    this.selectedMunicipio = this.cliente.municipioId;
    return new FormGroup({
      td: new FormControl(this.selectedTd),
      documento: new FormControl(this.cliente.clienteId, [Validators.required, Validators.pattern('^[1-9][0-9]*$')]),
      nombre: new FormControl(this.cliente.nombre, [Validators.required]),
      representante: new FormControl(this.cliente.representante),
      telefono: new FormControl(this.cliente.telefono, [Validators.required, Validators.pattern('^[0-9]*$')]),
      correo: new FormControl(this.cliente.correo, [Validators.required, Validators.email]),
      municipio: new FormControl(this.selectedMunicipio),
      departamento: new FormControl(this.selectedDepartamento),
      direccion: new FormControl(this.cliente.direccion, [Validators.required]),
      usuario: new FormControl(this.cliente.usuario, [Validators.required]),
      clave: new FormControl(this.cliente.clave, [Validators.required]),
      claveRepeat: new FormControl(this.claveRepeat, [Validators.required])
    }, [Validation.MatchValidator]);
  }

  createFormChange() {
    return new FormGroup({
      ingreso: new FormControl(this.cliente.usuario, [Validators.required]),
      clave: new FormControl(this.cliente.clave, [Validators.required]),
      claveRepeat: new FormControl(this.claveChangeRepeat, [Validators.required]),
      claveChange: new FormControl(this.claveChange, [Validators.required])
    }, [Validation.MatchValidatorChange]);
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

  obtenerCiudades(departamentoId) {
    this.municipio.setValue(null);
    this.obtenerMunicipios(departamentoId);
  }

  isNumberKey(evt) {
    const charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

  get ingresoChange() {
    return this.formChange.get('ingreso');
  }

  get claveNewRepeat() {
    return this.formChange.get('claveRepeat')
  }

  get claveOld() {
    return this.formChange.get('clave');
  }

  get claveNew() {
    return this.formChange.get('claveChange');
  }

  get ingreso() {
    return this.form.get('ingreso');
  }

  get clave() {
    return this.form.get('clave');
  }

  get nombre() {
    return this.formRegister.get('nombre');
  }

  get documento() {
    return this.formRegister.get('documento');
  }

  get claveRegister() {
    return this.formRegister.get('clave');
  }

  get claveRegisterRepeat() {
    return this.formRegister.get('claveRepeat');
  }

  get usuarioRegister() {
    return this.formRegister.get('usuario');
  }

  get telefono() {
    return this.formRegister.get('telefono');
  }

  get correo() {
    return this.formRegister.get('correo');
  }

  get direccion() {
    return this.formRegister.get('direccion');
  }

  get td() {
    return this.formRegister.get('td');
  }

  get departamento() {
    return this.formRegister.get('departamento');
  }

  get municipio() {
    return this.formRegister.get('municipio');
  }

  get ingresoPass() {
    return this.formPass.get('ingreso');
  }
}
