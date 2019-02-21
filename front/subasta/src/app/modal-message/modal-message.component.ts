import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import { constants } from '../util/constants';


@Component({
  selector: 'app-modal-message',
  templateUrl: './modal-message.component.html',
  styleUrls: ['./modal-message.component.css']
})
export class ModalMessageComponent implements OnInit {

  @Input() currentMessageType: number;
  @Input() title: string;
  @Input() message: string;
  @Output() action = new EventEmitter();

  pujaType: number = constants.nuevaPuja;
  successType: number = constants.success;
  errorType: number = constants.error;
  alertType: number = constants.alert;
  confirmType: number = constants.confirm;
  result: boolean;

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {
  }

  close() {
    this.action.emit(this.result);
    this.activeModal.close();
  }

  cancel() {
    this.result = false;
    this.close();
  }

  confirm() {
    this.result = true;
    this.close();
  }
}
