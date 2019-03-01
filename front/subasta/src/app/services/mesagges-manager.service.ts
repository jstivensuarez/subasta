import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { constants } from '../util/constants';
import { ModalMessageComponent } from '../modal-message/modal-message.component';
import { DetallesComponent } from '../detalles/detalles.component';

@Injectable({
  providedIn: 'root'
})
export class MesaggesManagerService {

  mensajeActual: string;
  constructor(private modalService: NgbModal) { }

  showSimpleMessageWithRetuns(title, type, mesagge) {
    if (!this.mensajeActual || this.mensajeActual != mesagge) {
      //alert("mensaje actual: "+this.mensajeActual+ " mensaje: "+mesagge)
      this.mensajeActual = mesagge;
      const modalRef = this.modalService.open(ModalMessageComponent);
      modalRef.componentInstance.currentMessageType = type;
      modalRef.componentInstance.title = title;
      modalRef.componentInstance.message = mesagge;
      modalRef.componentInstance.action.subscribe(resp => {
        this.mensajeActual = null;
      });
    }
  }

  showSimpleMessage(title, type, mesagge) {
    const modalRef = this.modalService.open(ModalMessageComponent);
    modalRef.componentInstance.currentMessageType = type;
    modalRef.componentInstance.title = title;
    modalRef.componentInstance.message = mesagge;
  }

  showConfirmMessage(title, mesagge): Observable<boolean> {
    const modalRef = this.modalService.open(ModalMessageComponent);
    modalRef.componentInstance.currentMessageType = constants.confirm;
    modalRef.componentInstance.title = title;
    modalRef.componentInstance.message = mesagge;
    return modalRef.componentInstance.action;
  }

  showDetails(title, object, otherComponent) {
    const modalRef = this.modalService.open(DetallesComponent);
    modalRef.componentInstance.title = title;
    modalRef.componentInstance.mesagge = object;
    modalRef.componentInstance.otherComponent = otherComponent;
    modalRef.componentInstance.getFinalMesagge();
  }
}
