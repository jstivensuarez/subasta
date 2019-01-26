import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { constants } from '../util/constants';
import { ModalMessageComponent } from '../modal-message/modal-message.component';

@Injectable({
  providedIn: 'root'
})
export class MesaggesManagerService {

  constructor(private modalService: NgbModal) { }

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
}
