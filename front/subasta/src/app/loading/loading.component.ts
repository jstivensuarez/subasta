import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { LoadingService, LoaderState } from '../services/loading.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.css']
})
export class LoadingComponent implements OnInit {

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {
    
  }
  
  ngOnDestroy() {
    this.activeModal.close();
  }

  close(){
    if(this.activeModal){
      this.activeModal.close();
    }
  }
}
