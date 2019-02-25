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
import { CategoriaService } from 'src/app/services/categoria.service';
import { Categoria } from 'src/app/dtos/categoria';
import { RazaService } from 'src/app/services/raza.service';
import { ClasificacionService } from 'src/app/services/clasificacion.service';
import { Raza } from 'src/app/dtos/raza';
import { Clasificacion } from 'src/app/dtos/clasificacion';

@Component({
  selector: 'app-crear-lotes',
  templateUrl: './crear-lotes.component.html',
  styleUrls: ['./crear-lotes.component.css']
})
export class CrearLotesComponent implements OnInit {

  categorias: Categoria[];
  razas: Raza[];
  clasificaciones: Clasificacion[];
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
  selectedCategoria: number;
  selectedClasificacion: number;
  selectedRaza: number;
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
    private subastaService: SubastaService,
    private categoriaService: CategoriaService,
    private razaService: RazaService,
    private clasificacionService: ClasificacionService) {
    this.title = 'Crear lote';
    this.lote = new Lote();
    this.categorias = [];
    this.razas = [];
    this.clasificaciones = [];
    this.departamentos = [];
    this.municipios = [];
    this.propietarios = [];
    this.obtenerDepartamentos();
    this.obtenerCategorias();
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
    this.clienteService.getPropietarios().subscribe(
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

  obtenerDatosCategoria(categoriaId) {
    this.raza.setValue(null);
    this.clasificacion.setValue(null);
    this.obtenerRazas(categoriaId);
    this.obtenerClasificaciones(categoriaId);
  }

  obtenerRazas(categoriaId) {
    if (categoriaId) {
      this.razaService.getRazas(categoriaId).subscribe(
        resp => {
          this.razas = resp;
        }, err => {
          console.error(err);
        }
      );
    }
  }

  obtenerClasificaciones(categoriaId) {
    if (categoriaId) {
      this.clasificacionService.getClasificaciones(categoriaId).subscribe(
        resp => {
          this.clasificaciones = resp;
        }, err => {
          console.error(err);
        }
      );
    }
  }

  obtenerCategorias() {
    this.categoriaService.get().subscribe(
      resp => {
        this.categorias = resp;
      }, err => {
        console.error(err);
      }
    );
  }

  obtenerLote(id: string) {
    this.lotesService.getDto(id).subscribe(res => {
      this.lote = res;
      this.selectedDepartamento = this.lote.municipio.departamentoId;
      this.selectedSubasta = this.lote.subastaId;
      this.selectedCategoria = this.lote.categoriaId;
      this.selectedRaza = this.lote.razaId;
      this.selectedClasificacion = this.lote.clasificacionId;
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
      payload.append('categoriaId', this.categoria.value);
      payload.append('razaId', this.raza.value);
      payload.append('clasificacionId', this.clasificacion.value);

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
    } else {
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


  createForm() {
    this.obtenerMunicipios(this.selectedDepartamento);
    this.obtenerRazas(this.selectedCategoria);
    this.obtenerClasificaciones(this.selectedCategoria);
    this.selectedMunicipio = this.lote.municipioId;
    this.selectedPropietario = this.lote.clienteId;
    this.selectedSubasta = this.lote.subastaId;
    this.selectedClasificacion = this.lote.clasificacionId;
    this.selectedRaza = this.lote.razaId;
    return new FormGroup({
      nombre: new FormControl(this.lote.nombre, [Validators.required]),
      descripcion: new FormControl(this.lote.descripcion),
      precioBase: new FormControl(this.lote.precioBase, [Validators.required]),
      propietario: new FormControl(this.selectedPropietario),
      municipio: new FormControl(this.selectedMunicipio),
      departamento: new FormControl(this.selectedDepartamento),
      categoria: new FormControl(this.selectedCategoria),
      raza: new FormControl(this.selectedRaza),
      clasificacion: new FormControl(this.selectedClasificacion),
      subasta: new FormControl(this.selectedSubasta),
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

  get categoria() {
    return this.form.get('categoria');
  }

  get raza() {
    return this.form.get('raza');
  }

  get clasificacion() {
    return this.form.get('clasificacion');
  }

  get video() {
    return this.form.get('video');
  }

  get foto() {
    return this.form.get('foto');
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
}
