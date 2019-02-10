import { Component, OnInit } from '@angular/core';
import { Lote } from 'src/app/dtos/lote';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DepartamentoService } from 'src/app/services/departamento-service.service';
import { MunicipioService } from 'src/app/services/municipio.service';
import { ClienteService } from 'src/app/services/cliente.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { Municipio } from 'src/app/dtos/municipio';
import { Departamento } from 'src/app/dtos/departamento';
import { LotesService } from 'src/app/services/lotes.service';
import { Cliente } from 'src/app/dtos/cliente';
import { constants } from 'src/app/util/constants';
import { SubastaService } from 'src/app/services/subasta.service';
import { Subasta } from 'src/app/dtos/subasta';

@Component({
  selector: 'app-crear-lotes',
  templateUrl: './crear-lotes.component.html',
  styleUrls: ['./crear-lotes.component.css']
})
export class CrearLotesComponent implements OnInit {

  departamentos: Departamento[];
  municipios: Municipio[];
  propietarios: Cliente[];
  subastas: Subasta[];
  lote: Lote;
  title: string;
  form: FormGroup;
  selectedMunicipio: number;
  selectedPropietario: string;
  selectedSubasta: number;
  selectedDepartamento: number;
  selectedAnticipo: boolean;
  isEditing: boolean;
  fotoControl: any;
  recursoCargado: string;
  constructor(
    private departamentoService: DepartamentoService,
    private municipioService: MunicipioService,
    private clienteService: ClienteService,
    private route: ActivatedRoute,
    private router: Router,
    private alertService: MesaggesManagerService,
    private lotesService: LotesService,
    private subastaService: SubastaService) {
    this.selectedAnticipo = false;
    this.title = 'Crear lote';
    this.lote = new Lote();
    this.departamentos = [];
    this.municipios = [];
    this.propietarios = [];
    this.obtenerDepartamentos();
    this.obtenerPropietarios();
    this.obtenerSubastas();
    this.verificarUrl();
    this.form = this.createForm();
  }

  ngOnInit() {
  }

  obtenerCiudades(departamentoId) {
    this.municipio.setValue(null);
    this.obtenerMunicipios(departamentoId);
  }

  obtenerDepartamentos() {
    this.departamentoService.getDepartamentos().subscribe(
      resp => {
        this.departamentos = resp;
      }, err => {
        console.error(err);
      }
    );
  }

  obtenerPropietarios() {
    this.clienteService.get().subscribe(
      resp => {
        this.propietarios = resp;
      }, err => {
        console.error(err);
      }
    );
  }

  obtenerSubastas() {
    this.subastaService.get().subscribe(
      resp => {
        this.subastas = resp;
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

  obtenerLote(id: string) {
    this.lotesService.getDto(id).subscribe(res => {
      debugger;
      this.lote = res;
      this.selectedDepartamento = this.lote.municipio.departamentoId;
      this.selectedSubasta = this.lote.subastaId;
      if (this.lote.valorAnticipo)
        this.selectedAnticipo = true;
      this.form = this.createForm();
    }, err => {
      console.error(err);
    });
  }


  verificarUrl() {
    this.route.queryParams.subscribe(params => {
      if (params['id']) {
        this.isEditing = true;
        this.title = 'Editar Lote';
        this.obtenerLote(params['id']);
      } else {
        this.isEditing = false;
        this.lote = new Lote();
        this.form = this.createForm();
        this.title = 'Crear Lote';
      }
    });
  }

  onSubmit() {
    this.validateFile();
    if (this.form.valid) {
      const payload = new FormData();
      payload.append('nombre', this.nombre.value);
      payload.append('descripcion', this.descripcion.value);
      payload.append('clienteId', this.propietario.value);
      payload.append('municipioId', this.municipio.value);
      payload.append('precioBase', this.precioBase.value);
      payload.append('subastaId', this.subasta.value);
      if (this.valorAnticipo.value && this.selectedAnticipo) {
        payload.append('valorAnticipo', this.valorAnticipo.value);
      } else {
        payload.append('valorAnticipo', "0");
      }
      if (this.foto.value) {
        payload.append('foto', this.lote.imagen[0]);
      }
      if (this.video.value) {
        payload.append('videoLote', this.video.value);
      }
      if (this.isEditing) {
        payload.append('loteId', this.lote.loteId);
        payload.append('fotoLote', this.lote.fotoLote);
        this.editarLote(payload);
      } else {
        this.crearLote(payload);
      }
    }

  }

  validateFile() {
    if (!this.isEditing) {
      if (!this.video.value && !this.foto.value) {
        this.video.setValidators([Validators.required, Validators.pattern('^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$')])
        this.foto.setErrors({ 'invalid': true });
        this.video.updateValueAndValidity();
      } else {
        this.foto.setErrors({});
        this.foto.updateValueAndValidity({
          onlySelf: true
        });
        this.video.setValidators([Validators.pattern('^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$')]);
        this.video.updateValueAndValidity();
      }
    }else{
      this.foto.setErrors({});
        this.foto.updateValueAndValidity({
          onlySelf: true
        });
        this.video.setValidators([Validators.pattern('^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$')]);
        this.video.updateValueAndValidity();
    }
  }

  crearLote(lote) {
    this.lotesService.post(lote).subscribe(res => {
      this.alertService.
        showSimpleMessage(constants.successTitle, constants.success, constants.successCreate);
      this.regresar();
    }, err => {
      this.alertService.
        showSimpleMessage(constants.errorTitle, constants.error, constants.errorCreate);
      console.error(err);
    });
  }

  editarLote(lote) {
    this.lotesService.put(lote).subscribe(res => {
      this.alertService.
        showSimpleMessage(constants.successTitle, constants.success, constants.successUpdate);
      this.regresar();
    }, err => {
      this.alertService.
        showSimpleMessage(constants.errorTitle, constants.error, constants.errorUpdate);
      console.error(err);
    });
  }

  regresar() {
    this.router.navigate(['/listar-lote']);
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

  createForm() {
    debugger;
    this.obtenerMunicipios(this.selectedDepartamento);
    this.selectedMunicipio = this.lote.municipioId;
    this.selectedPropietario = this.lote.clienteId;
    this.selectedSubasta = this.lote.subastaId;
    return new FormGroup({
      nombre: new FormControl(this.lote.nombre, [Validators.required]),
      descripcion: new FormControl(this.lote.descripcion, [Validators.required]),
      precioBase: new FormControl(this.lote.precioBase, [Validators.required]),
      valorAnticipo: new FormControl(this.lote.valorAnticipo),
      propietario: new FormControl(this.selectedPropietario),
      municipio: new FormControl(this.selectedMunicipio),
      departamento: new FormControl(this.selectedDepartamento),
      subasta: new FormControl(this.selectedSubasta),
      esAnticipo: new FormControl(this.selectedAnticipo),
      foto: new FormControl(this.lote.imagen),
      video: new FormControl(this.lote.video, [Validators.pattern('^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$')])
    });
  }

  uploadVideo(value) {
    this.recursoCargado = value;
    this.video.setValidators([Validators.required, Validators.pattern('^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$')])
    this.foto.setValue(null);
    this.lote.video = value;
    this.validateFile();
  }

  uploadImage(files) {
    if (files.length === 0 && !this.isEditing) {
      this.foto.setErrors({ 'invalid': true });
      return;
    }
    this.recursoCargado = files[0].name;
    this.video.setValue(null);
    this.lote.imagen = files;
    this.validateFile();
  }

  get video() {
    return this.form.get('video');
  }

  get foto() {
    return this.form.get('foto');
  }

  get valorAnticipo() {
    return this.form.get('valorAnticipo');
  }

  get precioBase() {
    return this.form.get('precioBase');
  }

  get descripcion() {
    return this.form.get('descripcion');
  }

  get nombre() {
    return this.form.get('nombre');
  }

  get propietario() {
    return this.form.get('propietario');
  }

  get subasta() {
    return this.form.get('subasta');
  }

  get departamento() {
    return this.form.get('departamento');
  }

  get municipio() {
    return this.form.get('municipio');
  }

  get esAnticipo() {
    return this.form.get('esAnticipo');
  }
}
