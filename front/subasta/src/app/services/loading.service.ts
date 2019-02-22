import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LoadingComponent } from '../loading/loading.component';

export interface LoaderState {
  show: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  modalRef: any;
  constructor(private modalService: NgbModal) { }

  show() {
    return this.modalService.open(LoadingComponent, { centered: true });
  }

  hide(modalRef) {
    try {
      if (modalRef) {
        setTimeout(function () {
          modalRef.componentInstance.close();
        }, 200);
      }
    } catch (err) { }
  }
}
