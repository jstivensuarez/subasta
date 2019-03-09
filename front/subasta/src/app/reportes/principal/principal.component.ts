import { Component, OnInit } from '@angular/core';
import { ReportesService } from 'src/app/services/reportes.service';
import { EventoService } from 'src/app/services/evento.service';
import { Evento } from 'src/app/dtos/evento';
import { LotesService } from 'src/app/services/lotes.service';
import { Lote } from 'src/app/dtos/lote';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { constants } from 'src/app/util/constants';

@Component({
  selector: 'app-principal',
  templateUrl: './principal.component.html',
  styleUrls: ['./principal.component.css']
})
export class PrincipalComponent implements OnInit {

  title: string;
  eventos: Evento[];
  lotes: Lote[];
  selectedEvento: number;
  selectedLote: number;
  selectedReporte: number;
  type: string;
  total: any;
  constructor(private reportesService: ReportesService,
    private eventoService: EventoService,
    private lotesService: LotesService,
    private alertService: MesaggesManagerService) {
    this.title = "Reportes";
    this.type = "application/ms-excel";
    this.obtenerTotal();
    this.obtenerLotes();
    this.obtenerEventos();
  }

  ngOnInit() {
  }

  obtenerReporte() {
    if (this.selectedEvento && this.selectedReporte == 1) {
      this.obtenerLotesVendidos(this.selectedEvento);
    }

    if (this.selectedReporte == 2) {
      this.obtenerCompradoresPorLotes(this.selectedLote);
    }

    if (this.selectedReporte == 3) {
      this.obtenerTotal();
    }
  }

  obtenerLotesVendidos(eventoId) {
    this.reportesService.getLotesVendidos(eventoId).subscribe(
      resp => {
        this.downLoadFile(resp, this.type, "Reporte-Lotes-Vendidos.xlsx");
      }, err => {
        this.alertService.
          showSimpleMessage(constants.errorTitle, constants.error, constants.errorConsultaReporte);
        console.error(err);
      }
    );
  }

  obtenerCompradoresPorLotes(loteId) {
    this.reportesService.getCompradoresPorLote(loteId).subscribe(
      resp => {
        this.downLoadFile(resp, this.type, "Reporte-Compradores-Lotes.xlsx");
      }, err => {
        this.alertService.
          showSimpleMessage(constants.errorTitle, constants.error, constants.errorConsultaReporte);
        console.error(err);
      }
    );
  }

  obtenerTotal() {
    this.reportesService.getTotal().subscribe(res => {
      debugger;
      this.total = res;
    }, err => {
      this.alertService.
        showSimpleMessage(constants.errorTitle, constants.error, constants.errorConsultaReporte);
      console.error(err);
    });
  }

  downLoadFile(data: any, type: string, fileName: string) {
    var blob = new Blob([data], { type: type.toString() });
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = fileName;
    link.click();
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

  obtenerLotes() {
    this.lotesService.get().subscribe(
      resp => {
        this.lotes = resp;
      }, err => {
        console.error(err);
      }
    );
  }
}
