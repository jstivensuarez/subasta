import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ModalMessageComponent } from './modal-message.component';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

describe('ModalMessageComponent', () => {
  let component: ModalMessageComponent;
  let ngbActiveModal: { dismiss: jasmine.Spy, close: jasmine.Spy };
  let fixture: ComponentFixture<ModalMessageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ModalMessageComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {

    TestBed.configureTestingModule({
      declarations: [ModalMessageComponent]
    })
      .compileComponents();
    ngbActiveModal = jasmine.createSpyObj('NgbActiveModal', ['dismiss', 'close']);
    component = new ModalMessageComponent(<any>ngbActiveModal);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
