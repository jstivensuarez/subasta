import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MesaggesManagerService } from '../services/mesagges-manager.service';
import { LotesService } from '../services/lotes.service';
import { Lote } from '../dtos/lote';
import { ClienteService } from '../services/cliente.service';
import { Cliente } from '../dtos/cliente';
import { Puja } from '../dtos/puja';
import { PujaService } from '../services/puja.service';
import { constants } from '../util/constants';
import { UsuarioService } from '../services/usuario.service';

@Component({
  selector: 'app-confirmacion-compra',
  templateUrl: './confirmacion-compra.component.html',
  styleUrls: ['./confirmacion-compra.component.css']
})
export class ConfirmacionCompraComponent implements OnInit {

  puja: Puja;
  habilitarBoton: boolean;
  fecha: string;
  constructor(private route: ActivatedRoute,
    private router: Router,
    private alertService: MesaggesManagerService,
    private lotesService: LotesService,
    private usuarioService: UsuarioService,
    private pujaService: PujaService) {
    this.habilitarBoton = false;
    this.puja = new Puja();
    this.verificarUrl();

  }

  ngOnInit() {
  }

  verificarUrl() {
    this.route.params.subscribe(params => {
      if (params['info']) {
        if (this.usuarioService.isAuthenticated()) {
          const value = atob(params['info']).split("-");
          const loteId = parseInt(value[1].trim());
          this.calcularFechas(value[2]);
          this.obtenerPujaInfo(loteId);
        }else{
          localStorage.setItem("compra", params['info']);
          this.usuarioService.logoutSession();
        }
      }
    });
  }

  obtenerPujaInfo(id: number) {
    if (id) {
      this.pujaService.getGanador(id).subscribe(res => {
        this.puja = res;
      }, err => {
        console.error(err);
      });
    }
  }

  calcularFechas(fechaPago) {
    const hoy = new Date();
    const fecha = new Date(fechaPago)
    const dif = hoy.getTime() - fecha.getTime();
    const minutos = Math.round(dif / (1000 * 60));
    if (minutos < 15) {
      this.habilitarBoton = true;
    }
  }

  confirmar() {
    this.pujaService.confirmarGanador(this.puja.pujador.lote.loteId).subscribe(res => {
      this.alertService.
        showSimpleMessage(constants.successTitle, constants.success, constants.successConfirm);
      this.habilitarBoton = false;
    }, err => {
      this.alertService.
        showSimpleMessage(constants.errorTitle, constants.error, constants.errorConfirm);
      console.error(err);
    });
  }
}
