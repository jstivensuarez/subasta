import { Component, OnInit } from '@angular/core';
import { DepartamentoService } from 'src/app/services/departamento-service.service';
import { MunicipioService } from 'src/app/services/municipio.service';
import { ClienteService } from 'src/app/services/cliente.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { LotesService } from 'src/app/services/lotes.service';
import { SubastaService } from 'src/app/services/subasta.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Departamento } from 'src/app/dtos/departamento';
import { Municipio } from 'src/app/dtos/municipio';
import { Lote } from 'src/app/dtos/lote';
import { Animal } from 'src/app/dtos/animal';
import { constants } from 'src/app/util/constants';
import { AnimalService } from 'src/app/services/animal.service';
import { CategoriaService } from 'src/app/services/categoria.service';
import { Categoria } from 'src/app/dtos/categoria';
import { Sexo } from 'src/app/dtos/sexo';
import { Raza } from 'src/app/dtos/raza';
import { RazaService } from 'src/app/services/raza.service';

@Component({
  selector: 'app-crear',
  templateUrl: './crear.component.html',
  styleUrls: ['./crear.component.css']
})
export class CrearComponent implements OnInit {

  departamentos: Departamento[];
  municipios: Municipio[];
  categorias: Categoria[];
  sexos: Sexo[];
  razas: Raza[];
  lotes: Lote[];
  selectedMunicipio: number;
  selectedDepartamento: number;
  selectedCategoria: number;
  selectedSexo: string;
  selectedRaza: number;
  selectedLote: number;
  isEditing: boolean;
  recursoCargado: string;
  title: string;
  form: FormGroup;
  animal: Animal;
  constructor(private departamentoService: DepartamentoService,
    private municipioService: MunicipioService,
    private categoriaService: CategoriaService,
    private route: ActivatedRoute,
    private router: Router,
    private alertService: MesaggesManagerService,
    private lotesService: LotesService,
    private animalService: AnimalService,
    private razaService: RazaService) {
    this.animal = new Animal();
    this.title = 'Crear animal';
    this.departamentos = [];
    this.municipios = [];
    this.categorias = [];
    this.sexos = [];
    this.razas = [];
    this.lotes = [];
    this.obtenerDepartamentos();
    this.obtenerCategorias();
    this.obtenerRazas();
    this.obtenerLotes();
    this.verificarUrl();
    this.form = this.createForm();
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
    );
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

  obtenerRazas() {
    this.razaService.get().subscribe(
      resp => {
        this.razas = resp;
      }, err => {
        console.error(err);
      }
    );
  }

  obtenerLotes() {
    this.lotesService.get().subscribe(
      resp => {
        this.lotes = resp;
      }, err => {
        console.error(err);
      }
    );
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

  verificarUrl() {
    this.route.queryParams.subscribe(params => {
      if (params['id']) {
        this.isEditing = true;
        this.title = 'Editar Animal';
        this.obtenerAnimal(params['id']);
      } else {
        this.isEditing = false;
        this.animal = new Animal();
        this.form = this.createForm();
        this.title = 'Crear Animal';
      }
    });
  }

  obtenerAnimal(id: string) {
    this.animalService.getDto(id).subscribe(res => {
      this.animal = res;
      this.selectedDepartamento = this.animal.municipio.departamentoId;
      this.form = this.createForm();
    }, err => {
      console.error(err);
    });
  }

  obtenerCiudades(departamentoId) {
    this.municipio.setValue(null);
    this.obtenerMunicipios(departamentoId);
  }

  onSubmit() {
    this.validateFile();
    if (this.form.valid) {
      const payload = new FormData();
      payload.append('descripcion', this.descripcion.value);
      payload.append('peso', this.peso.value);
      payload.append('loteId', this.lote.value);
      payload.append('categoriaId', this.categoria.value);
      payload.append('razaId', this.raza.value);
      payload.append('sexo', this.sexo.value);
      payload.append('municipioId', this.municipio.value);
      if (this.foto.value) {
        payload.append('foto', this.animal.imagen[0]);
      }
      if (this.video.value) {
        payload.append('video', this.video.value);
      }
      debugger;
      if (this.isEditing) {
        payload.append('animalId', this.animal.animalId);
        payload.append('foto', this.animal.foto);
        this.editarAnimal(payload);
      } else {
        this.crearAnimal(payload);
      }
    }
  }

  crearAnimal(animal: FormData) {
    this.animalService.post(animal).subscribe(res => {
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

  editarAnimal(animal: FormData) {
    this.animalService.put(animal).subscribe(res => {
      this.alertService.
        showSimpleMessage(constants.successTitle, constants.success, constants.successUpdate);
      this.regresar();
    }, err => {
      this.alertService.
        showSimpleMessage(constants.errorTitle, constants.error, constants.errorUpdate);
      console.error(err);
    });
  }

  createForm() {
    this.obtenerMunicipios(this.selectedDepartamento);
    this.selectedMunicipio = this.animal.municipioId;
    //this.selectedCategoria = this.animal.categoriaId;
    this.selectedSexo = this.animal.sexo;
    this.selectedRaza = this.animal.razaId;
    this.selectedLote = this.animal.loteId;
    return new FormGroup({
      descripcion: new FormControl(this.animal.descripcion, [Validators.required]),
      municipio: new FormControl(this.selectedMunicipio),
      departamento: new FormControl(this.selectedDepartamento),
      categoria: new FormControl(this.selectedCategoria),
      sexo: new FormControl(this.selectedSexo),
      raza: new FormControl(this.selectedRaza),
      lote: new FormControl(this.selectedLote),
      foto: new FormControl(this.animal.imagen),
      peso: new FormControl(this.animal.peso),
      video: new FormControl(this.animal.video, [Validators.pattern('^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$')])
    });
  }

  uploadVideo(value) {
    this.recursoCargado = value;
    this.video.setValidators([Validators.required, Validators.pattern('^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$')])
    this.foto.setValue(null);
    this.animal.video = value;
    this.validateFile();
  }

  uploadImage(files) {
    if (files.length === 0 && !this.isEditing) {
      this.foto.setErrors({ 'invalid': true });
      return;
    }
    this.recursoCargado = files[0].name;
    this.video.setValue(null);
    this.animal.imagen = files;
    this.validateFile();
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

  regresar() {
    this.router.navigate(['/listar-animal']);
  }

  get raza() {
    return this.form.get('raza');
  }

  get lote() {
    return this.form.get('lote');
  }

  get peso() {
    return this.form.get('peso');
  }

  get sexo() {
    return this.form.get('sexo');
  }

  get categoria() {
    return this.form.get('categoria');
  }

  get descripcion() {
    return this.form.get('descripcion');
  }

  get departamento() {
    return this.form.get('departamento');
  }

  get municipio() {
    return this.form.get('municipio');
  }

  get foto() {
    return this.form.get('foto');
  }

  get video() {
    return this.form.get('video');
  }
}
