import { Component, OnInit } from '@angular/core';
import { Cliente } from 'src/app/dtos/cliente';
import { ClienteService } from 'src/app/services/cliente.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { PujadoresService } from 'src/app/services/pujadores.service';
import { LotesService } from 'src/app/services/lotes.service';
import { Pujador } from 'src/app/dtos/pujador';
import { Lote } from 'src/app/dtos/lote';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { constants } from 'src/app/util/constants';

@Component({
  selector: 'app-crear',
  templateUrl: './crear.component.html',
  styleUrls: ['./crear.component.css']
})
export class CrearPujadorComponent implements OnInit {

  clientes: Cliente[];
  lotes: Lote[];
  selectedCliente: number;
  selectedLote: number;
  pujador: Pujador;
  form: FormGroup;
  isEditing: boolean;
  title: string;
  constructor(private pujadoresService: PujadoresService,
    private clienteService: ClienteService,
    private lotesService: LotesService,
    private route: ActivatedRoute,
    private router: Router,
    private alertService: MesaggesManagerService) {
    this.clientes = [];
    this.lotes = [];
    this.pujador = new Pujador();
    this.obtenerPujadores();
    this.verificarUrl();
    this.form = this.createForm();
  }

  ngOnInit() {
  }

  obtenerPujadores() {
    this.clienteService.getPujadores().subscribe(
      resp => {
        this.clientes = resp;
      }, err => {
        console.error(err);
      }
    )
  }


  obtenerLotesEvent(clienteId) {
    this.lote.setValue(null);
    this.obtenerLotes(clienteId);
  }

  obtenerLotes(clienteId) {
    if (clienteId) {
      this.lotesService.getNoAsociados(clienteId).subscribe(
        resp => {
          this.lotes = resp;
        }, err => {
          console.error(err);
        }
      );
    }
  }

  obtenerTodosLotes() {
    this.lotesService.get().subscribe(
      resp => {
        this.lotes = resp;
      }, err => {
        console.error(err);
      }
    );
  }

  onSubmit() {
    if (this.form.valid) {
      const pujador = new Pujador();
      pujador.banco = this.banco.value;
      pujador.clienteId = this.cliente.value;
      pujador.loteId = this.lote.value;
      pujador.numeroConsignacion = this.numeroConsignacion.value;
      pujador.valorConsignacion = this.valorConsignacion.value;

      if (this.isEditing) {
        pujador.pujadorId = this.pujador.pujadorId;
        this.editarPujador(pujador)
      } else {
        this.crearPujador(pujador);
      }
    }
  }

  editarPujador(pujador: Pujador) {
    this.pujadoresService.put(pujador).subscribe(res => {
      this.alertService.
        showSimpleMessage(constants.successTitle, constants.success, constants.successUpdate);
      this.regresar();
    }, err => {
      this.alertService.
        showSimpleMessage(constants.errorTitle, constants.error, constants.errorUpdate);
      console.error(err);
    });
  }

  crearPujador(pujador: Pujador) {
    this.pujadoresService.post(pujador).subscribe(res => {
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

  verificarUrl() {
    this.route.queryParams.subscribe(params => {
      debugger;
      if (params['id']) {
        this.isEditing = true;
        this.title = 'Editar pujador';
        this.obtenerPujador(params['id']);
      } else {
        this.isEditing = false;
        this.pujador = new Pujador();
        this.form = this.createForm();
        this.title = 'Crear Pujador';
      }
    });
  }

  obtenerPujador(id) {
    this.pujadoresService.getDto(id).subscribe(res => {
      debugger;
      this.pujador = res;
      this.form = this.createForm();
    }, err => {
      console.error(err);
    });
  }

  regresar() {
    this.router.navigate(['/listar-pujador']);
  }

  createForm() {
    if (!this.isEditing)
      this.obtenerLotes(this.selectedCliente);
    else
      this.obtenerTodosLotes();
    this.selectedCliente = this.pujador.clienteId;
    this.selectedLote = this.pujador.loteId;
    return new FormGroup({
      cliente: new FormControl(this.selectedCliente),
      lote: new FormControl(this.selectedLote),
      banco: new FormControl(this.pujador.banco, [Validators.required]),
      numeroConsignacion: new FormControl(this.pujador.numeroConsignacion, [Validators.required]),
      valorConsignacion: new FormControl(this.pujador.valorConsignacion, [Validators.required])
    });
  }

  isNumberKey(evt) {
    const charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

  get cliente() {
    return this.form.get('cliente');
  }

  get lote() {
    return this.form.get('lote');
  }

  get banco() {
    return this.form.get('banco');
  }

  get numeroConsignacion() {
    return this.form.get('numeroConsignacion');
  }

  get valorConsignacion() {
    return this.form.get('valorConsignacion');
  }
}
