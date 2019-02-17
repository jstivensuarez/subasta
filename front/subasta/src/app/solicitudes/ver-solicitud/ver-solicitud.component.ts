import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Solicitud } from 'src/app/dtos/solicitud-subasta';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { SolicitudService } from 'src/app/services/solicitud.service';
import { MesaggesManagerService } from 'src/app/services/mesagges-manager.service';
import { constants } from 'src/app/util/constants';

@Component({
  selector: 'app-ver-solicitud',
  templateUrl: './ver-solicitud.component.html',
  styleUrls: ['./ver-solicitud.component.css']
})
export class VerSolicitudComponent implements OnInit {

  @Input() solicitud: Solicitud;
  @Output() completo: EventEmitter<any> = new EventEmitter();
  constructor(public activeModal: NgbActiveModal,
    private solicitudService: SolicitudService,
    private alertService: MesaggesManagerService) {
    this.solicitud = new Solicitud();
  }

  ngOnInit() {
  }

  cancel() {
    this.activeModal.close();
  }

  aceptar() {
    this.solicitudService.aceptar(this.solicitud).subscribe(res => {
      this.completo.emit();
      this.cancel();
    }, err => {
      console.error(err);
    });
  }

  rechazar() {
    this.solicitudService.rechazar(this.solicitud).subscribe(res => {
      this.completo.emit();
      this.cancel();
    }, err => {
      console.error(err);
    });
  }
}
